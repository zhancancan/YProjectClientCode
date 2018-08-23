local this = {}

local med
local pane

local debugCMDs = require "DebugWords"
local listData

local _curSelectData

local item = {}
function item:draw(cell,data)
	cell:SetString("txt",data.name)
end

function item:onSelectChange( cell,data,selected )
	local content = data.name
	local str = content
	if selected then
		str = string.format("<color=red><b>%s</b></color>",content)
	end
	cell:SetString("txt",str)
end

function item:onInit(cell)
	cell:SetClickTrigger("txt")
end

-------local meta
local _meta = {}

function _meta.doCmd()
	-- local debugMsgCenter = require "DebugMessageCenter"

	debugCMDs.holdFunc(_curSelectData)	
	med:Hide()

end

-------mediator
function this.show()
	if not listData then
		listData = debugCMDs.getFormatMsgList()
		-- local db = require "TypeDB"
		-- local classes = require "TypeClasses"
		-- listData=db.toArray(classes.TYPE_PLACE)
	end
	pane:SetList(listData)

	local selected = listData[1]
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

	med:SetString("lab","CMD List")
	med:SetString("btn","Do CMD")

	med:AddListener("btn",_meta.doCmd)

	

end

function this.onPaneItemClick(_,evt)
	local data = evt.data
	_curSelectData = data
end


return this