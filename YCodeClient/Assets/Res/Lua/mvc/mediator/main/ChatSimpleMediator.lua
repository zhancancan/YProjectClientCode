local this = {}
local med
local manager
local settingPanel
local PanelManager
local TableEvent 		= require "EventType".TableEvent

--------------------------------item ------------------------------------

local format= "[%s] [%s] %s"
local TextUtils = require "TextUtils"

local messageItem={}
function messageItem.draw(_,cell,data)
	if not manager then manager = require "MessageManager" end
	if not TextUtils then TextUtils =require "TextUtils" end
	local chl = manager.getChannelByType(data.channel)
	-- local color = string.sub(chl.fontColor,2,#chl.fontColor)
	-- local t0 = TextUtils.html(chl.name,color)
	local t0 = TextUtils.html(chl.name,chl.fontColor)
	local t1 = data.senderName
	local t2 = manager.getDescription(data,true)
	local str = string.format(format,t0,t1,t2);
	cell:SetString("txt",str)
end


--------------------------------meditor ------------------------------------

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
		if not settingPanel or not settingPanel.active then settingPanel =  PanelManager.Open("ChatSettingPanel_social") end
	elseif n == "btn_1"then
		 PanelManager.Open("ChatPanel_social_subb")
		 -- PanelManager.Open("ChatPanel_social")
	end
end

function this.initView(_,mediator)
	med=mediator
	med:AddListener("btn_0",onClick)
	med:AddListener("btn_1",onClick)
	med:SetPaneFactory("pane",messageItem)

	manager = require "MessageManager"
	local ns =require "CSNamespace"
	PanelManager = ns["PanelManager"]
	require "EventType"
	-- TableEvent = _G["TableEvent"]
end



return this;