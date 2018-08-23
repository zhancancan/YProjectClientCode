local class 		= require "Class"
local this 			= class("Data_BuffObj",require "Data")
local Logger 		= require "Logger"
local db 			= require "TypeDB"
local TypeClasses	= require "TypeClasses"

local table



local function getType(self)
	if not table then table = db.getTable(TypeClasses.TYPE_BUFF)	end

	if self then
		local typeData = rawget(self,"typeData")
		if not typeData then
			local bid = rawget(self,"buffId")
			typeData = table:selectOne({id=bid})
			rawset(self, "typeData",typeData)
		end
		return typeData
	end
	return nil
end



local meta = {
	__index = function(t,k)
		if k == "name"  then
			local  def = getType(t)
			if def then return def.name end

		elseif k == "buff" then--todo
			local  def = getType(t)
			if def then return def.typeData end
		end
		return nil
	end

}

function this:ctor()
	setmetatable(self._data, meta)
end

return this