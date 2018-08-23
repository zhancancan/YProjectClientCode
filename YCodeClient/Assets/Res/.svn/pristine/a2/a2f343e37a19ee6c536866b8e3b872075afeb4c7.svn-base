local this={}
local med
local pane
local channels
local manager;
local isDirty

local item={};
local _meta = {}

function item:draw( cell,data )
	cell:SetString("txt",data.name)
end

function item:onInit(cell )
	cell:SetClickTrigger("img_0")
end


function this:show()
	pane:SetList(channels)
end

function this:hide()
	pane:SetList(nil)
end

function this:initView(mediator)
	med=mediator
	pane=med:AddListener("pane",_meta.onPaneClick)
	manager = require "MessageManager"
	channels=manager.getChannels(true)
	med:SetPaneFactory("pane",item)
end
function _meta.onPaneClick(_,evt)
	
end

return this;