local this={}
local med

local heroList




local item={}
function item.draw(_,cell,data)
	cell:SetString("icon",data.icon)
	cell:SetString("txt",data.name)
end
function item.onInit(_,cell)
	cell:SetClickTrigger("img_0")
end


function this.show()
	med:SetList("pane",heroList)
	med:SetInt("pane",0)
	local h = heroList[1].hero
	med:PlayPortrait(h.portrait,h.showFx)
end

function this.hide()
	med:SetList("pane",nil)
end


local function onClick(n)
	if n == "btn_1" then

	elseif n== "btn_0" then
		med:Hide()
	end

end
local function onListClick(_,evt)
	local h = evt.data.hero
	med:PlayPortrait(h.portrait,h.showFx)
end

function this.initView(_,m)
	med = m;
	med:AddListener("btn_0" ,onClick)
	med:AddListener("btn_1" ,onClick)
	heroList = TypeDB.toArray(TypeClasses.TYPE_STARTHERO)
	med:AddListener("pane",onListClick)
	med:SetPaneFactory("pane",item)
end

return this;
