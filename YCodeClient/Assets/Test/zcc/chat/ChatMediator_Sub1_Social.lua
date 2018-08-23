--mediator
local class = require "Class"
local baseType = require "Mediator"
local c = class("ChatMediator_sub_1_Social",baseType)
local this = c.new()
local TypeClasses = require "TypeClasses"
local med

local tabItem = require "Sub1_TabItem"
local pageItem = require "Sub1_PageItem"

function this.show()
	local a = this.typeDB.toArray(TypeClasses.TYPE_EMOTETAB)
	-- local db = require "TypeDB"，这里因为table继承了baseType（Mediator）
	--所以可以用this.typeDB
	med:SetList("pane_0",a)
	med:SetList("pane_1",a)
end

function this.hide()
	med:SetList("pane_0",nil)
	med:SetList("pane_1",nil)
end

function this.initView(_,mediator)
	med = mediator;
	med:SetPaneFactory("pane_0",tabItem)
	med:SetPaneFactory("pane_1",pageItem)
end

return this
