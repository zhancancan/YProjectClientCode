local manager 		= require "MessageManager"
local app 			= require "AppCenter"
local TimeFormat 	= require "TimeFormat"
local TextUtils 	= require "TextUtils"
local TableEvent 	= require "EventType".TableEvent

local user = require "Data_Character"

local this={}
local med

local inputPane

---------------------------------item-------------------------------------------------

local channelItem={}
function channelItem.draw(_,cell,data)
	-- for k,v in pairs(data.preset) do
	-- 	print(k,v)
	-- end
	-- print(data.name)
	-- print(data.preset.name)
	cell:SetString("txt",data.name)
	cell:SetActive("redPoint",data.unread>0)
end


function channelItem.onInit(_,cell)
	cell:SetClickTrigger("img_0")
end


local messageItem={}
function messageItem.pickPrefab(_,data,_)
	-- if data.isTime then return 0
	-- elseif data.senderId==app.playerId and data.msgType==0 then return 1
	-- elseif data.senderId~=app.playerId and data.msgType==0 then return 2
	-- elseif data.senderId~=app.playerId and data.msgType==1 then return 3
	-- else return 4
	-- end
	if data.isTime then return 0
	elseif data.senderId==user.id and data.msgType==0 then return 1
	elseif data.senderId~=user.id and data.msgType==0 then return 2
	elseif data.senderId~=user.id and data.msgType==1 then return 3
	elseif data.senderId~=user.id and data.msgType==2 then return 2--msgType=2:全部显示-暂用--todo
	else return 4
	end
end
local currentChannel
local channelPane
function messageItem.draw(_,cell,data)
	if not manager then manager = require "MessageManager" end
	if data.isTime then
		cell:SetString("txt",TimeFormat.format(data.time*0.001,TimeFormat.hh_mm))
	else
		local senderName = data.senderName
		if manager.getIsGeneral(currentChannel) then
			local chl = manager.getChannelByType(data.channel)
			-- print("fontColor:",chl.fontColor)
			-- local color = string.sub(chl.fontColor,2,#chl.fontColor-2)
			local color = string.sub(chl.fontColor,2,#chl.fontColor)
			local t0 = TextUtils.html(string.format("[%s]",chl.name),color)
			-- local t0 = TextUtils.html(string.format("[%s]",chl.name),chl.fontColor)
			local t1 = data.senderName
			senderName = t0..t1
		end
		cell:SetString("txt",senderName)
		cell:SetString("richTxt",manager.getDescription(data,false))
	end
end

------------------------------------------mediator---------------------------------------------



local function onMessageUpdate()
	if channelPane.selectedData and currentChannel ~= channelPane.selectedData then
		currentChannel = channelPane.selectedData
	end
	med:SetList("pane_1",manager.get(currentChannel))
	med:SetFloat("pane_1",1)
	med:SetList("pane_0", manager.getChannels())

end
local function onChannelChange(_,evt)
	currentChannel=evt.data
	onMessageUpdate()
end

local function onCallbackChannelChange(_,evt)
	channelPane.selectedData = evt.data
	currentChannel=evt.data
	onMessageUpdate()
end

function this.show()
	if not currentChannel then
		local a = manager.getChannels()
		currentChannel = a[1]
	end
	manager:addListener(TableEvent.UPDATE,this,onMessageUpdate)
	onMessageUpdate()
	channelPane.selectedData=currentChannel
end

function this.hide()
	med:SetList("pane_0",nil)
	med:SetList("pane_1",nil)
	manager:removeListener(TableEvent.UPDATE,this,onMessageUpdate)
end

function this.initView(_,mediator)
	med=mediator
	channelPane = med:AddListener("pane_0",onChannelChange)
	med:SetPaneFactory("pane_0",channelItem)
	med:SetPaneFactory("pane_1",messageItem)
	inputPane = med:GetSibling("sub_3"):AddListener("pane",onCallbackChannelChange)
end



return this;