local this ={}
local med
local TypeClasses = require "TypeClasses"
local app 			= require "AppCenter"
local ns 			= require "CSNamespace"
local typeDB 		= require "TypeDB"

local item={
	draw = 	function (_,cell,data)
				cell:SetString("icon",data.icon)
			end,
	onInit = function (_,cell)
				cell:SetClickTrigger("icon")
			end
}

local page={
	draw = 	function (_,cell,data)
				cell:SetPaneFactory("pane",item)
				cell:SetList("pane",data.emotes)
			end,
	onRemove=function (_,cell)
				cell:SetList("pane",nil)
				cell:SetPaneFactory("pane",nil)
			end
}

local tab={
	draw =function (_,cell,data)
		cell:SetString("icon",data.icon)
		cell:SetClickTrigger("icon")
	end
}


function this.show()
	local a = typeDB.toArray(TypeClasses.TYPE_EMOTETAB)
	med:SetList("pane_0",a)
	med:SetList("pane_1",a)
end

function this.hide()
	med:SetList("pane_0",nil)
	med:SetList("pane_1",nil)
end

function this.initView(_,mediator)
	med=mediator
	med:SetPaneFactory("pane_0",tab)
	med:SetPaneFactory("pane_1",page)
end

return this;