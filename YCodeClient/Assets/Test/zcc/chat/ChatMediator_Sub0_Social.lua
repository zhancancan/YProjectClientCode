local this={}
local med
local channelPane

local manager = require "MessageManager"
require "EventType"
local TableEvent  = _G["TableEvent"]

local currentChannel

local channelItem = require "Sub0_ChannelItem"
local messageItem = require "Sub0_MessageItem"

local function onMessageUpdate()
	med:SetList("pane_0",manager.getChannels())
	med:SetList("pane_1",manager.get(currentChannel))
	med:SetFloat("pane_1",1)
end

local function onChannelClick(_,evt)
	currentChannel=evt.data
	onMessageUpdate()
end		

function this.show()
	if not currentChannel then
		local a = manager.getChannels()
		currentChannel=a[1]
	end
	manager:addListener(TableEvent.UPDATE,this,onMessageUpdate)
	onMessageUpdate()
	channelPane.selectedData=currentChannel
end

function this.hide()
	med:SetList("pane_0",nil)
	manager:removeListener(TableEvent.UPDATE,this,onMessageUpdate)
end

function this.initView(_,mediator)
	med=mediator
	channelPane = med:AddListener("pane_0",onChannelClick)
	med:SetPaneFactory("pane_0",channelItem)
	med:SetPaneFactory("pane_1",messageItem)
end

return this
