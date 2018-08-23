local this={}
local med

local TreePane 			= require "TreePane"

local class 			= require "Class"
local TypeClasses		= require "TypeClasses"
local typeDB			= require "TypeDB"
local ns 				= require "CSNamespace"

local error				= require "ErrorCenter"
local log 				= require "Logger"

local db 				= require "DataManager".dbase
local DataClasses		= require "DataClasses"
local data_quest 		= require "Data_UserQuest"
local data_questState   = require "Data_UserQuestState"

local SocketForm 		= require "SocketForm"

local lang 				= require "LangManager"
local dialog 			= require "Dialog"

local tree

local selection
local goalDesCell

local questData
local questsDone
local questId

--Item
local function onTaskBtnClick()
	log.log("accept task")
	if data_questState.questId == nil then
		local form = SocketForm()
		form.method = CommandClasses.CmdAcceptQuest
		form.id = questData.id
		form:send()
	else
		dialog.askByType(lang.get("Mission.quest_isgoing"),dialog.TYPE_ONEBTN)
	end
end

local function onTaskDesBtnClick()
	goalDesCell:SetActive("sub_0",false)
	goalDesCell:SetActive("sub_1",true)
end

local function onTaskGoalBtnClick()
	goalDesCell:SetActive("sub_0",true)
	goalDesCell:SetActive("sub_1",false)
end

local item = class("TaskListItem")

function item.createItem()
	return item.new()
end

function item.pickPrefab(pane,data,index)
	return data.isMain and 0 or 1
end

function item.onInit(_,cell,data)
	if data.isMain then
    	cell:SetClickTrigger("img_0")
	else
		goalDesCell = cell
		cell:AddListener("btn_0",onTaskBtnClick)
		cell:AddListener("sub_0/btn_1",onTaskDesBtnClick)
		cell:AddListener("sub_1/btn_2",onTaskGoalBtnClick)
	end
end

function item.draw(_,cell,data,index)
	if data.isMain then
		cell:SetString("txt_0",data.name)
		cell:SetActive("img_1",data.isNew)
		cell:SetActive("img_3",data.id==questId)
		if data.isNew then 
			cell:SetInt("star",0)
			cell:SetActive("img_2",false)
			cell:SetActive("img_4",false)
		else
			local times = data.questDone.times
			cell:SetInt("star",data.questDone.star)
			cell:SetString("img_4/txt",lang.format("Mission.quest_doneTimes",times))
			cell:SetActive("img_2",times==0 and data.id~=questId)
			cell:SetActive("img_4",times>0 and data.id~=questId)
		end
	else
		if questData then
			cell:SetString("sub_1/area",questData.description)
			cell:SetActive("btn_0",questData.id ~= questId and questData:condition())
		end
	end
end

local function onItemClick(_,evt)
	log.log(evt.data.id)
	questData = evt.data
end

local function  onErrorReceived(err)
	log.log("create med", err.code, err.msg)
	if err.msg == "QUEST_CONDITION_NOT_REACHED" then 
		dialog.askByType(lang.get("Mission.quest_isgoing"),dialog.TYPE_ONEBTN)
	end
end

--Mediator
local datas = {}

local function judgeEqual(quests,id)
	for i=1,#quests do
		if quests[i].id==id then return true end
	end
	return false
end

local function getIndex(quests,id)
	for index=1,#quests do
		if quests[index].id==id then return index end
	end
end

local function getData(quests)
	for i=#quests,1,-1 do
		local isEqual = judgeEqual(questsDone,quests[i].id)
		local isAccept = quests[i]:condition()

		if isEqual then 
			local index = getIndex(questsDone,quests[i].id)
			local time = tonumber(questsDone[index].lastStartTime)
			if time > 0 then 
				quests[i].isNew = false 
				quests[i].questDone = questsDone[index]
			else 
				quests[i].isNew = true 
			end
		else
			quests[i].isNew = true
			if not isAccept and quests[i].id ~= questId then quests[i]=nil end
		end
	end
	return quests
end

local function sortQuests(quests)
	local new={}
	local old={}
	for k,v in pairs(quests) do
		if v.isNew then table.insert(new,v) else table.insert(old,v) end
	end
	table.sort(old,function(first,next) return first.questDone.times>next.questDone.times end)
	for k,v in pairs(old) do
		table.insert(new,v)
	end
	return new
end

local function doTree(quests)
	for i=1,#quests do
		local t = quests[i]
		t.isMain =true
		datas[i] = t

		local children = {}
		children [1] = {isMain =false}
		t.children = children
	end
	return quests
end

function this.show(_,chapter)
	error.addListener(onErrorReceived,this)
	questId = data_questState.questId
	questsDone = db:getTable(DataClasses.DATA_USERQUEST):toArray()
	
	local quests = chapter.quests
	--judge is this chapter quest and quest state
	quests = getData(quests)
	quests = sortQuests(quests)
	quests = doTree(quests)
	-- datas:reverse()
	tree:setList(datas)
end

function this.hide()
	error.removeListener(onErrorReceived)
	tree:setList(nil)
end

function this.initView(_,meditor)
	med = meditor
	tree = TreePane.new(med:GetUI("pane"),false,true)
	med:SetPaneFactory("pane",item)

	med:AddListener("pane",onItemClick)
end

return this