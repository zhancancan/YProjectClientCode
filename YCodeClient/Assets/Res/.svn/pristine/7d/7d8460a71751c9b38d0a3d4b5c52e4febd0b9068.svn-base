local this = {}

local med
local pane

local sceneListData

local _curSelectData

local item = {}
function item:draw(cell,data)
	cell:SetString("txt",data.name)
end

function item:onSelectChange( cell,data,selected )
	local content = data.name
	local str = content
	if selected then
		str = string.format("<color=red>%s</color>",content)
	end
	cell:SetString("txt",str)
end

function item:onInit(cell)
	cell:SetClickTrigger("txt")
end

-------local meta
local _meta = {}

function _meta.ChangeScene()
	-- print("ChangeScene to ".._curSelectData.name)
	
	local debugMsgCenter = require "DebugMessageCenter"
	debugMsgCenter.debugChangeScene(_curSelectData)
	
	med:Hide()

end

-------mediator
function this.show()
	if not sceneListData then
		local db = require "TypeDB"
		local classes = require "TypeClasses"
		sceneListData=db.toArray(classes.TYPE_PLACE)
	end
	pane:SetList(sceneListData)

	local selected = sceneListData[1]
	pane.selectedData = selected
	_curSelectData = selected
end

function this.hide()
	pane:SetList(nil)
	_curSelectData = nil
end

function this:initView(mediator)
	med = mediator
	pane = med:AddListener("pane",this.onPaneItemClick)



	
	med:SetPaneFactory("pane",item)

	med:SetString("lab","SceneList")
	med:SetString("btn","ChangeScene")

	med:AddListener("btn",_meta.ChangeScene)

	

end

function this.onPaneItemClick(_,evt)
	local data = evt.data
	_curSelectData = data
end


return this