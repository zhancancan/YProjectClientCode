local this={}
local med
local pane
local channels
local manager;
local isDirty


local item={};

function item.draw(_,cell,data)
	cell:SetString("cb",data.name)
	cell:SetClickTrigger("cb")
	cell:SetBool("cb",data.inGeneral)
end

function this:show()
	pane:SetList(channels)
end

function this:hide()
	pane:SetList(nil)
	if isDirty then manager.writeLocalSetting()end
end

function this:initView(mediator)
	med=mediator
	pane=med:AddListener("pane",this.onPaneClick)
	manager = require "MessageManager"
	manager.readLocalSetting()
	channels=manager.getSettingChannels()
	med:SetPaneFactory("pane",item)
end
function this.onPaneClick(_,evt)
	evt.data.inGeneral=evt.item:GetBool("cb")
	isDirty=true
end

return this;