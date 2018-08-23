local class 		= require "Class"
local this 			= class("Data_UserQuest",require "Data")
local db 			= require "TypeDB"
local TypeClasses	= require "TypeClasses"
local log			= require "Logger"




local function getter(t,k)
	local td = rawget(t,"typeData")
	if k == "typeData" then return td end
	return td and td[k] or nil
end

local meta = {__index = getter}

function this:ctor()
	setmetatable(self._data , meta)
end


function this:onCreate()
	local id = rawget(self._data,"id")
	local td =  db:selectOne(TypeClasses.TYPE_QUEST, id)
	if td then
		self.typeData = td
	else
		log.error(string.format("Data_UserQuest:onCreate can not find define data by id %s",id))
	end
end


function this:onSelfChange(p)
	if p == "questId" then
		self.quest = nil
	end
end

return this