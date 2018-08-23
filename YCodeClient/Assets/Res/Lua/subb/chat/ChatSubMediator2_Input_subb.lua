local this={}

local namspace 		= require "CSNamespace"
local ChatStatus 	= namspace["ChatStatus"]
local PanelManager 	= namspace["PanelManager"]


local Channels 		= require "MessageChannel".Channels
local med
local emoPanel
local channelPane
local input
local settingPanel


local SocketForm 		= require "SocketForm"
local CommandClasses 	= require "CommandClasses"
local manager 			= require "MessageManager"
local debugMsgCenter 	= require "DebugMessageCenter"
local lang 				= require "LangManager"
local log 				= require "Logger"
local gameTicker    	= require "GameTicker"


local _currentChannel;
local _sendChannel
local emoList
local _isVis
local pane

local sendChanPane

local lastSendWorldTime = 0

local item={};

function item:draw( cell,data )
	cell:SetString("txt",data.name)
end

function item:onInit(cell )
	cell:SetClickTrigger("img_0")
end
local _meta ={}
function _meta.onCbChanged(_,selected)
	med:SetActive("txt",selected)
	med:SetActive("btn_5",selected)
	med:SetActive("btn_1",selected)
end

function _meta.onChannelChanged(_,evt)
	_currentChannel=evt.data
	_meta.updateChannel()

end
function _meta.updateChannel()
	-- med:SetActive("btn_0",_currentChannel.channel==Channels.HELP)
	-- med:SetActive("cb",_currentChannel.keepVoice>0)
	med:SetActive("btn_0",_sendChannel.channel==Channels.PRIVATE or _sendChannel.channel==Channels.CURRENT)
	med:SetActive("cb",_sendChannel.keepVoice>0)
	med:SetActive("btn_4",_currentChannel.channel == Channels.GENERAL)
	-- med:SetActive("img_1",_currentChannel.channel == Channels.GENERAL)
	-- med:SetActive("img_3",_currentChannel.channel == Channels.GENERAL)

end

function _meta.voiceRecord(status,packet)
	if status == ChatStatus.Start then
		PanelManager.Open("VoicePanel_social")
	else
		PanelManager.Close("VoicePanel_social")
	end
end
function _meta.onBnttonClick(n)
	if n=="btn_1"then
		-- print("emoPanel.active ",emoPanel.active)
		if emoPanel.active then	emoPanel:Hide()	else emoPanel:Show() end
	elseif n =="btn_0" then
		--todo
		--send location
	elseif n == "btn_4" then
		if not settingPanel or not settingPanel.active then settingPanel =  PanelManager.Open("ChatSettingPanel_social") end

	elseif n == "btn_5" then
		if sendChanPane.active then 
			sendChanPane:Hide() 
		else 
			sendChanPane:Show() 
			pane.selectedData = _sendChannel
		end
	end
end
function _meta.onEmoClick(_,evt)
	local d =evt.data

	if d.immediateSend == 1 then
		med:SetString("txt","")
		input:Insert("("..d.key..")")
		_meta.submit(true)
		emoPanel:Hide()

	else
		input:Insert("("..d.key..")")
	end

end

function _meta.onPanelClick(_,evt)
	local d = evt.data


	_isVis = false
	-- med:SetActive("pane",false)
	sendChanPane:Hide()
	_currentChannel = evt.data
	_sendChannel = evt.data

	log.log("selected send:".._sendChannel.channel)
	_meta.updateChannel()



end

function _meta.submit()
	local txt = input.text
	if #txt<=0 then return end
	_meta.ensureEmoList();
	for i=1,#emoList do
		local e=emoList[i]
		local k = "%("..e.key.."%)"
		local rep = "{e:"..e.id.."}"
		txt=string.gsub(txt,k,rep)
	end

	if debugMsgCenter.debugHoldMessage(txt) >= 0 then-->0:no debug hold,=0:match debug,<0:txt has problem
		if _meta.holdSCMDMessage(txt) == 0 then
			_meta.sendMessage(txt)
		end
	end
	med:SetString("txt","")
end

function _meta.holdSCMDMessage(content)
	local m = "scmd%s(.+)"
	local pk = string.match(content,m)
	if not pk then return 0 end

	local form = SocketForm()
	form.method = CommandClasses.Command
	form.cmd = pk
	form:send()
	log.log("to send scmd  .cmd:",pk)

	return -1
end

function _meta.sendMessage(content)
	if not med.active then return end

	if _sendChannel.channel == Channels.WORLD then
		local d = gameTicker.time - lastSendWorldTime
		if d <= 1*1000 then
			log.error("wait a moment~then try send again:"..d)
			return
		end
		lastSendWorldTime = gameTicker.time

	end
	--todo ,to do
	--form info shall be right
	local form = SocketForm()
	form.method = CommandClasses.ChatRequest

	form.channel = _sendChannel.channel
	-- form.channel = _currentChannel.channel
	form.msgType = 0--todo
	form.description = content
	form.toUserName = nil--todo
	form.toUserId = nil--todo
	form.media = nil--todo

	form:send()

	log.log("do send channel:".._sendChannel.channel)

	if _currentChannel.channel ~= _sendChannel.channel and _currentChannel.channel ~= Channels.GENERAL then
		--todo
		channelPane.selectedData = _sendChannel
		_currentChannel=_sendChannel
		_meta.updateChannel()
		manager.changeChannelAfterSend()
	end


end


function _meta.ensureEmoList()
	if not emoList then
		local db = require "TypeDB"
		local classes = require "TypeClasses"
		emoList=db.toArray(classes.TYPE_EMOTE)
	end
end

--------------------------mediator
function this.show()
	_currentChannel = channelPane.selectedData
	_sendChannel=manager.getDefaultSelectedChannel(true)
	_meta.updateChannel()
end

function this.hide()
	_sendChannel=nil
	_currentChannel=nil
	_isVis = false
end

function this:initView(mediator)
	med=mediator
	med:AddListener("btn_0",_meta.onBnttonClick)
	med:AddListener("btn_1",_meta.onBnttonClick)
	med:AddListener("btn_2",_meta.submit)
	med:AddListener("btn_3",_meta.voiceRecord)
	med:AddListener("cb",	_meta.onCbChanged)
	med:AddListener("btn_4",_meta.onBnttonClick)
	med:AddListener("btn_5",_meta.onBnttonClick)
	input=med:GetUI("txt")
	emoPanel = med:GetSibling("sub_2")
	emoPanel:AddListener("pane_1",_meta.onEmoClick)
	channelPane=med:GetSibling("sub_0"):AddListener("pane_0",_meta.onChannelChanged)

	sendChanPane = med:GetSibling("sub_3")
	pane = sendChanPane:AddListener("pane",_meta.onPanelClick)
	-- pane=med:AddListener("pane",_meta.onPanelClick)
	-- med:SetPaneFactory("pane",item)
end


function this.destroyView()
	emoPanel=nil
	channelPane =nil
	sendChanPane = nil
end

------------for Debug

return this;