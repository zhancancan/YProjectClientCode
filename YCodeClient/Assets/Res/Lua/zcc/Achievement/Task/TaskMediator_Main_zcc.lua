local this={}
local med

local class 			= require "Class"
local TypeClasses		= require "TypeClasses"
local typeDB			= require "TypeDB"
local ns 				= require "CSNamespace"
local panelManager  	= ns.PanelManager

local error				= require "ErrorCenter"
local log 				= require "Logger"
local dialog 			= require "Dialog"

local lang 				= require "LangManager"

local data_questState   = require "Data_UserQuestState"

local sub_0
local sub_1
local sub_2

local quest,subQuest
local subQuestState
local subQuestProgress,subQuestMaxProgress
local questId,subQuestId--,typeSubeQuestId
local cbTown,cbTask,cbNormal,cbBattle,cbNPC,cbQuestDelete,cbSubQuestDone,cbQuestDone

--Mediator
local function  onErrorReceived(err)
	log.log("create med", err.code, err.msg)
end

local function onQuestBtnClick(n)
	panelManager.Open("TaskChapterPanel_Achievement",subQuestId)
end

local function onSelectBtnClick()
	if subQuestState==1 then
		panelManager.Open("SubTaskPanel_Achievement",subQuestId)
	else
		dialog.askByType(lang.get("Mission.subQuset_isgoing"),dialog.TYPE_ONEBTN)
	end
end

--Test code
local function onSelectChange(n,isOn)
	if n=="cb_0" then 
		cbTown.selected = isOn
		cbTask.selected = not isOn 
		cbNormal.selected = true
		cbBattle.selected = false
	end
	if n=="cb_1" then 
		cbTask.selected = isOn 
		cbTown.selected = not isOn
		cbQuestDelete.selected = false
	end
	if n=="cb_2" then 
		cbNormal.selected = isOn 
		cbBattle.selected = not isOn
	end
	if n=="cb_3" then 
		cbBattle.selected = isOn 
		if cbTown.selected then cbBattle.selected = false else cbNormal.selected = not isOn end
	end
	if n=="cb_4" then 
		cbNPC.selected = isOn
	end
	if n=="cb_5" then
		cbQuestDelete.selected = isOn
	end
	if n=="cb_6" then
		cbSubQuestDone.selected = isOn
	end
	if n=="cb_7" then
		cbQuestDone.selected = isOn
		cbQuestDelete.selected = isOn
		cbSubQuestDone.selected = isOn
	end

	--QuestDone
	if cbQuestDone.selected then
		subQuestProgress = 100
		if subQuestProgress >= subQuestMaxProgress then
			panelManager.Open("TaskDeliveryPanel_Achievement")
		end
	else
		subQuestProgress=data_questState.subQuestProgress
	end

	--QuestDelete
	if cbQuestDelete.selected then
		questId=nil
	else
		questId=data_questState.questId
	end

	--SubQuestDone
	if cbSubQuestDone.selected then
		subQuestState=1
	else
		subQuestState=data_questState.subQuestState
	end

	--palce change and state change show quest
	if cbTown.selected then 
		if questId then
			sub_0:Hide() sub_1:Show()
			med:SetString("sub_1/txt_0",quest.name)
			if cbNPC.selected then
				sub_2:Show()
			else
				sub_2:Hide()
			end
		else
			sub_0:Show() sub_1:Hide() sub_2:Hide()
		end
	else
		if cbTask.selected then
			if cbNormal.selected then 
				med:SetString("sub_1/txt_1",subQuest.name)
				med:SetString("sub_1/txt_0",subQuest.description)
				if cbNPC.selected then 
					sub_0:Hide() sub_1:Show() sub_2:Show() 
				else
					sub_0:Hide() sub_1:Show() sub_2:Hide() 
				end
			else 
				sub_0:Hide() sub_1:Hide() sub_2:Hide() 
			end
		end
	end
end

function this.show()
	error.addListener(onErrorReceived,this)
    questId = data_questState.questId
    subQuestId = data_questState.subQuestId
    subQuestState = data_questState.subQuestState
    subQuestProgress = data_questState.subQuestProgress
    subQuestMaxProgress = data_questState.subQuestMaxProgress

	if questId then 
		quest = typeDB.selectOne(TypeClasses.TYPE_QUEST,questId)
		-- typeSubeQuestId = quest.subQuest
	end
	subQuest = typeDB.selectOne(TypeClasses.TYPE_SUBQUEST,subQuestId)

	--Test code
	cbTown.selected = true
	cbNormal.selected = true
	cbTask.selected = false 
	cbBattle.selected = false
	cbNPC.selected=false
	cbQuestDelete.selected = false
	cbSubQuestDone.selected = false
	cbQuestDone.selected = false

	if questId then
		sub_0:Hide() sub_1:Show()
		med:SetString("sub_1/txt_0",quest.name)
		if cbNPC.selected then sub_2:Show() else sub_2:Hide() end
	else
		sub_0:Show() sub_1:Hide() sub_2:Hide()
	end
end

function this.hide()
	error.removeListener(onErrorReceived)
end

function this.initView(_,meditor)
	med = meditor
	med:AddListener("sub_0/btn",onQuestBtnClick)
	med:AddListener("sub_2/btn",onSelectBtnClick)

	--Test code
	cbTown=med:AddListener("test/cb_0",onSelectChange)
	cbTask=med:AddListener("test/cb_1",onSelectChange)
	cbNormal=med:AddListener("test/cb_2",onSelectChange)
	cbBattle=med:AddListener("test/cb_3",onSelectChange)
	cbNPC=med:AddListener("test/cb_4",onSelectChange)
	cbQuestDelete=med:AddListener("test/cb_5",onSelectChange)
	cbSubQuestDone=med:AddListener("test/cb_6",onSelectChange)
	cbQuestDone=med:AddListener("test/cb_7",onSelectChange)

	sub_0=med:GetChildByName("sub_0")
	sub_1=med:GetChildByName("sub_1")
	sub_2=med:GetChildByName("sub_2")
end

return this