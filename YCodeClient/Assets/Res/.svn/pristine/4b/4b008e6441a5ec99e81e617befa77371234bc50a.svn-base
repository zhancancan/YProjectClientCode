local class 		= require "Class"
local this 			= class("Data_UserQuestState",require "Data")
local db 			= require "DataManager".dbase
local DataClasses	= require "DataClasses"

local typeDB		= require "TypeDB"
local TypeClasses	= require "TypeClasses"

local log			= require "Logger"

local instance



local function onPropertyChange(self, p, o,n)
	if p == "questId" then
		self.quest 		= db:selectOne(DataClasses.DATA_USERQUEST, n)
		self.typeQuest 	= typeDB:selectOne(TypeClasses.TypeClasses.TYPE_QUEST,n)
		self:invoke("quest",o,n)

	elseif p == "subQuestId" then
		self.subQuest = typeDB.selectOne(TypeClasses.TYPE_SUBQUEST, n)
		self:invoke("subQuest",o,n)
	end
end


function this.ctor()
	if instance then
		log.error("singleton error: Data_UserQuestState can create only once")
		return
	end
end


function this:onCreate()
	self:addListener(self,onPropertyChange)
end


instance = this.new()
return instance