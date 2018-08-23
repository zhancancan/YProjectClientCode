local class =require("Class")
local this = class("Data_Action")
local table
local char = require "Data_Character"

local function ensureTable()
	if not table then
		local dbmgt 	  = require "DataManager"
		local db		  = dbmgt.dbase
		local DataClasses = require "DataClasses"
		table = db:getTable(DataClasses.DATA_SCENEOBJ)
	end
end


function this:ctor()
	self.time=0
end

local function pick(self, n,message)
	local d={}
	if not message._fields then
		-- should be array
		for i=1 , #message do
			local c = message[i]
			if type(c) == "table" then d[i] = pick(self,n,c)
			else
				d[i] = c
			end
		end
		return d
	end

	for k,v in pairs(message._fields) do
		if type(v)=="table" then d[k.name] = pick(self, n.."."..k.name,v)
		else d[k.name] =v
		end
	end
	return d
end


function this:parseFromMessage(message)
	ensureTable()
	self.table = table
	self.char  = char
	for k,v in pairs(message._fields) do
		if type(v)=="table" then
			self[k.name] = pick(self,k.name,v)
		else
			self[k.name]= v
		end
	end
	self. time = tonumber(self.time)
end




return this