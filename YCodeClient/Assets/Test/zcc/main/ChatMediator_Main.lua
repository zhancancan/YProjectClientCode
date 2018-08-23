local this = {}
local med
local settingPanel

require "DefNamespace"
local PanelManager = _G["PanelManager"]
local manager= require "MessageManager"
require "EventType"
local TableEvent = _G["TableEvent"]

local messageItem = require "Chat_MessageItem"

local function onReadChange()
	med:SetActive("redPoint",manager.unread>0)
	med:SetList("pane",manager.getSimpleMessages())
end

function this.show()
	onReadChange()
	manager:addListener(TableEvent.UPDATE,this,onReadChange)
end

function this.hide()
	med:SetList(nil)
	manager:removeListener(TableEvent.UPDATE,this,onReadChange)
end

local function onClick(n)
	if n == "btn_0" then
		if not settingPanel or not settingPanel.active 
			then settingPanel =  PanelManager.Open("ChatSettingPanel_social") 
		end
	elseif n == "btn_1"then
		 PanelManager.Open("chatPanel_social")
	end
end

function this.initView(_,mediator)
	med=mediator
	med:AddListener("btn_0",onClick)
	med:AddListener("btn_1",onClick)
	med:SetPaneFactory("pane",messageItem)
end

return this;