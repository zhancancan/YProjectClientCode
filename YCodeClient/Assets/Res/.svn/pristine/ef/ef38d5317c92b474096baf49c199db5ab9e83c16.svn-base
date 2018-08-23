local class 		= require "Class"
local this 			= class("Data_SkillObj",require "Data")
local db 			= require "TypeDB"
local TypeClasses	= require "TypeClasses"
local table


local function getter(t,k)
	if k == "typeData" then
		if not table then table = db.getTable(TypeClasses.TYPE_SKILL) end
		local sid = rawget(t,"skillId")
		if sid then
			local sk = table:selectOne(sid)
			if sk then rawset(t,"typeData",sk) end
			return sk
		end
	else
		local sk = t.typeData
		return  sk and sk[k] or nil
	end
	return nil
end



local meta = { __index =  getter}

function this:ctor()
	setmetatable(self._data, meta)
	self.fireTime = 0
	self.cooldown = 0
end

function this:onSelfChange(p)
	if p == "skillId" then
		self.typeData = nil
	end
end



return this