local this={}
local med

local class 			= require "Class"
local TypeClasses		= require "TypeClasses"
local typeDB			= require "TypeDB"
local ns 				= require "CSNamespace"
local panelManager  	= ns.PanelManager

local error				= require "ErrorCenter"
local log 				= require "Logger"

local data_questState   = require "Data_UserQuestState"

local SocketForm 		= require "SocketForm"

local nextSubQuests
local count 

--Item
local item = {}

function item.draw(_,cell,data,index)
	-- if count ==1 then 
		-- if index ==1 then 
			cell:SetString("txt_0",data.name)
			cell:SetString("area",data.description)
			cell:SetString("icon",data.icon)
		-- end
	-- end
end

function item.init(_,cell,data)

end

--Mediator
local function  onErrorReceived(err)
	log.log("create med", err.code, err.msg)
end

local function onItemClick(_,evt)
	log.log(evt.data.index)
end

local function onSelectBtnClick()
	local form = SocketForm()
	form.method = CommandClasses.CmdAcceptSubQuest
	form.id = nextSubQuests[1].id
	form:send()
	panelManager.Close("SubTaskPanel_Achievement")
end

function this.show(_,subQuestId)
	error.addListener(onErrorReceived,this)
	local subQuest = typeDB.selectOne(TypeClasses.TYPE_SUBQUEST,subQuestId)
	nextSubQuests = subQuest.nextSubQuest
	count = #nextSubQuests
	-- if count == 1 then
		-- local temp = nextSubQuests[1]
		-- nextSubQuests[1] = nil
		-- nextSubQuests[2] = temp
	-- end
	med:SetList("pane",nextSubQuests)
end

function this.hide()
	error.removeListener(onErrorReceived)
	med:SetList("pane",nil)
end

function this.initView(_,meditor)
	med = meditor
	med:AddListener("btn",onSelectBtnClick)
	med:SetPaneFactory("pane",item)
	med:AddListener("pane",onItemClick)
end

return this