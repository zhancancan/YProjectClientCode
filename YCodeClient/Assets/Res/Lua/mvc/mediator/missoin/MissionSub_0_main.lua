local typeDB 		= require "TypeDB"
local TypeClasses 	= require "TypeClasses"


local this={}

local sub_1
local med



local chaptList
local function getChapterList()
	if not chaptList then
		chaptList = typeDB.toArray(TypeClasses.TYPE_MAINQUEST)
	end
	return chaptList
end




local item ={}

function item:draw( cell,data )
	cell:SetString("icon",data.icon)
end

function item:onClick(cell,data)
	sub_1:Show(data)
end

function item:onInit(cell)
	cell:SetClickTrigger("icon")
end

function this.show()
	local arr = getChapterList()
	med:SetList("pane",arr)
end
function this.hide( )
	med:SetList("pane",nil)
end
function this.initView(_,m)
	med=m
	sub_1 = med:GetSibling("sub_1")
	med:SetPaneFactory("pane",item)
end


return this