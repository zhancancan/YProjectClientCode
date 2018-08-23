local this={}
local med
local sub_1

local TypeClasses		 = require "TypeClasses"
local typeDB		 	 = require "TypeDB"
local ns 				 = require "CSNamespace"

local error				 = require "ErrorCenter"
local log 				 = require "Logger"

local db 				 = require "DataManager".dbase
local DataClasses		 = require "DataClasses"
local data_quest 	     = require "Data_UserQuest"
local data_questState    = require "Data_UserQuestState"

--Item
local item={}
function item.draw(_,cell,data,index)
	cell:SetString("txt_0",data.caption)
	cell:SetString("txt_1",data.title)
	cell:SetActive("img_1",data.isNew)
end

function item.onInit(_,cell)
	cell:SetClickTrigger("img_0")
end

local function onItemClick(_,evt)
	sub_1:Show(evt.data)
end

local function  onErrorReceived(err)
	log.log("create med", err.code, err.msg)
end

--Mediator
local function judgeEqual(quests,id)
	for i=1,#quests do
		if quests[i].id==id then 
			return true
		end
	end
	return false
end

local function getIndex(quests,id)
	for index=1,#quests do
		if quests[index].id==id then return index end
	end
end

function this.show()
	error.addListener(onErrorReceived,this)
	local questId = data_questState.questId

	local questsDone = db:getTable(DataClasses.DATA_USERQUEST):toArray()
    local chapters = typeDB.toArray(TypeClasses.TYPE_CHAPTER)

    for i=#chapters,1,-1 do
    	local quests = chapters[i].quests
    	local isNew=true
    	--judge chapter state
		for j=1,#quests do
			local isEqual = judgeEqual(questsDone,quests[j].id)
			if isEqual then 
				local index = getIndex(questsDone,quests[j].id)
				local time = tonumber(questsDone[index].lastStartTime)
				if time > 0 then isNew = false break end
			end
		end
		chapters[i].isNew = isNew

		--is chapter show or hide
		local isAacept,isEqual
		for j=1,#quests do
			isAacept = quests[j]:condition()
			isEqual = judgeEqual(questsDone,quests[j].id)
			if isEqual or isAccept then break end
		end
		if not isEqual and not isAccept then 
			for j=1,#quests do
				if quests[j].id == questId then else chapters:removeAt(i) end
			end
		end
    end

	chapters:reverse()
	med:SetList("pane",chapters)
end

function this.hide()
	error.removeListener(onErrorReceived)
end

function this.initView(_,meditor)
	med=meditor
	med:SetPaneFactory("pane",item)
	med:AddListener("pane",onItemClick)
	sub_1 = med:GetSibling("sub_1")
end

return this