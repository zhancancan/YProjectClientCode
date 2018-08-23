local this = {}

local med
local pane
local heroList ={}

local log 			= require "Logger"
local typeDB 		= require "TypeDB"
local TypeClasses	= require "TypeClasses"
------item
local item = {}
function item:draw(cell,data)
	cell:SetString("txt",data.name)
	cell:SetString("icon",data.heroType.icon)
end

function item:onInit(cell)
	cell:SetClickTrigger("img_0")
end
-----meta
local _meta ={}

function _meta.onClick()
	med:Hide()
end

function _meta.onListClick(_,evt)
	-- print(evt)
	-- print(evt.data)
	-- print(evt.data.id)
	-- print(evt.data.portrait)
	
	local h = evt.data
	med:PlayPortrait(h.portrait,h.showFx)
end
-----------mediator
function this:show()
	med:SetList("pane",heroList)
	local evt = heroList[1]
	pane.selectedData = evt
	med:PlayPortrait(evt.portrait,evt.showFx)
end

function this:hide( )
	med:SetList("pane",nil)
end

function this:initView(mediator)
	med = mediator
	med:SetString("btn_1","Exit")
	med:AddListener("btn_1",_meta.onClick)
	heroList = typeDB.toArray(TypeClasses.TYPE_HERO)
	pane = med:AddListener("pane",_meta.onListClick)
	med:SetPaneFactory("pane",item)
end
return this