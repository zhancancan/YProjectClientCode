local baseType = require "Mediator"
local class = require "Class"
local c = class("",baseType)
local this=c.new()

local TypeClasses = require "TypeClasses"

require "DefNamespace"
local ChatStatus = _G["ChatStatus"]
local PanelManager = _G["PanelManager"]

require "MessageChannel"
local Channels = _G["Channels"]

local SocketForm = require "SocketForm"
local CommandClasses = require "CommandClasses"

local med
local emoPanel
local channelPane
local input
--local settingPanel
local _currentChannel
local emoList
local _meta ={}

function this.show()
	_currentChannel = channelPane.selectedData
	_meta.updateChannel()
end

function this.hide()
	_currentChannel=nil
end

function this:initView(mediator)
	med = mediator;

	med:AddListener("btn_0",_meta.onBnttonClick);
	med:AddListener("btn_1",_meta.onBnttonClick);
	med:AddListener("btn_2",_meta.submit);
    med:AddListener("btn_3",_meta.voiceRecord);
	med:AddListener("cb",_meta.onCbChanged)
	input=med:GetUI("txt")
	emoPanel = med:GetSibling("sub_1");
	emoPanel:AddListener("pane_1",_meta.onEmoClick)
	channelPane=med:GetSibling("sub_0"):AddListener("pane_0",_meta.onChannelChanged)
end

function this.destroyView()
	emoPanel=nil
	channelPane =nil
end

function _meta.onBnttonClick(n)
	if n=="btn_1"then
		if emoPanel.active then	emoPanel:Hide()	else emoPanel:Show() end
	elseif n =="btn_0" then
		manager.Close("chatPanel_social");
	end
end

function _meta.submit()
	local txt = input.text
	if #txt<=0 then return end
	if not emoList then
		emoList = this.typeDB.toArray(TypeClasses.TYPE_EMOTE)
	end
	for i=1,#emoList do
		local e=emoList[i]
		local k = "%("..e.key.."%)"
		local rep = "{e:"..e.id.."}"
		txt=string.gsub(txt,k,rep)
	end
	local form = SocketForm()
	form.method = CommandClasses.ChatRequest
	form.channel = _currentChannel.channel
	form.msgType=0
	form.description = txt
	form:send()
	print(txt)
end

function _meta.voiceRecord(status,packet)
	if status == ChatStatus.Start then
		PanelManager.Open("VoicePanel_social")
	else
		PanelManager.Close("VoicePanel_social")
	end
	print(status == ChatStatus.Start,packet)
end

function _meta.onCbChanged(_,selected)
	med:SetActive("txt",selected)
end

function _meta.onEmoClick(_,evt)
	local d =evt.data
	input:Insert("("..d.key..")")
	if d.immediateSend == 1 then 
		_meta.submit() 
	end
end

function _meta.onChannelChanged(_,evt)
	_currentChannel=evt.data
	_meta.updateChannel()
end

function this.onCheckBoxChange()
	print("onCheckBoxChange")
end

function _meta.updateChannel()
	med:SetActive("btn_0",_currentChannel.channel==Channels.HELP)
	med:SetActive("cb",_currentChannel.keepVoice>0)
end

return this
