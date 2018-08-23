local TreePane = require "TreePane"


local this ={}
local med
local tree

local datas = {}
for i=1, 4 do
	local t = {}
	t.isMain =true
	datas[i] = t


	local children = {}
	for j = 1,3 do
		children [j] ={isMain =false}
	end
	t.children = children
end


local item ={}

function item.pickPrefab(pane,data,index)
	return data.isMain and 0 or 1
end

function item:onInit(cell,data)
	if data.isMain then
    cell:SetClickTrigger("icon")
	end

end

function item:onClick(_,data)

end

function item:draw(cell,data)
	if(data.isMain) then
	cell:SetString("icon","d")
	end
end



function this:show()
	tree:setList(datas)
end

function this:hide( )
	tree:setList(nil)
end

function this.initView(_,m)
	med = m
	tree  = TreePane.new(m:GetUI("pane"),false,true)
	med:SetPaneFactory("pane",item)
end


return this