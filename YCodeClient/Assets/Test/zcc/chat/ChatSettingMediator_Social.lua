local this = {}
local med
local pane
local manager = require "MessageManager"
local channels
local isDirty

local channelItem = require "ChatSetting_ChannelItem"

function this.show()
	pane:SetList(channels)
end

function this.hide()
	pane:SetList(nil)
	if isDirty then manager.writeLocalSetting() end
end

function this.initView(_,mediator)
	med=mediator
	pane=med:AddListener("pane",this.onChannelClick)
	manager.readLocalSetting()
	channels=manager.getSettingChannels()

	med:SetPaneFactory("pane",channelItem)
end

function this.onChannelClick(_,evt)
	evt.data.inGeneral=evt.item:GetBool("cb")
	isDirty=true
end

return this;