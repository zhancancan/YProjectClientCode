local class 		= require "Class"
local this 			=class("Data_Bullet",require "Data")
local Logger 		= require "Logger"
local db 			= require "TypeDB"
local TypeClasses	= require "TypeClasses"

local table



local function getType(self)
	if not table then table = db.getTable(TypeClasses.TYPE_BULLET)	end

	if self then
		local bullet = rawget(self,"bullet")
		if not bullet then
			bullet = table:selectOne({id=self.bulletId})
			rawset(self, "bullet",bullet)
		end
		return bullet
	end
	return nil
end



local meta = {
	__index = function(t,k)
		if k == "name"  then
			local  def = getType(t)
			if def then return def.name end

		elseif k == "bullet" then--todo
			local  def = getType(t)
			if def then return def.bullet end
		end
		return nil
	end

}

function this:ctor()
	setmetatable(self._data, meta)
end


return this