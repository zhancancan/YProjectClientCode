local class 		= require "Class"
local this 			= class("Data_UserCurrency",require "Data")
local db			= require "TypeDB"
local TypeClasses 	= require "TypeClasses"

local log 			= require "Logger"

local function getter(t,k)
	local rt = rawget(t,"typeData")
	local rid = rawget(t,"id")
	if not rt and rid then
		rt =db.selectOne(TypeClasses.TYPE_CURRENCY,rid)
		if rt then 	rawset(t,"typeData",rt) end
	end
	if rt then
		if k == "name" then
			return rt.name
		elseif k == "icon" then
			return rt.icon
		elseif k == "key" then
			return rt.key
		end
	end
end

local meta ={__index = getter}

function this:ctor()
	setmetatable(self._data,meta)
end


function this:onSelfChange(p,o,n)
	if p == "value" then
		log.warn("TO DO: fly the icon as value changed",self.name)
	end

end


return this