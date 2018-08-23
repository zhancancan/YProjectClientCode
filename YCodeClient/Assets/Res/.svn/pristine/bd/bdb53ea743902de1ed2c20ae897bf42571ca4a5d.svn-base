local class = require "Class"
local base 	= require "Data_Model"
local this 	= class("Data_Player",base)
local log	= require "Logger"

local supportModel  = {"body","horse"}

function this.getModelList()
	return supportModel
end

function this:onSelfChange(p,_,n)
	local en = rawget (self, "entity")
	if not en then return end
	if p == "body" then
		en:ChangeModel("body",self.body)
	elseif p == "speed" then
		en.speed = n
	elseif p == "hp" then
		--todo
		log.log(string.format("hp change to %s",self.hp))
	end
end

return this