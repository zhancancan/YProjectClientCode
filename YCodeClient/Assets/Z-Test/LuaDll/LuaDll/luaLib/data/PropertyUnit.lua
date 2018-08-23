local TypeClasses 	= require "TypeClasses"
local class 		= require "Class"
local this 			= class("PropertyUnit")
local db 			= require "TypeDB"


local function ensureSet(t)
	local set = rawget(t,"set")
	if not set then
		set = db.selectOne(TypeClasses.TYPE_PROPERTY,{id=t.property})
		rawset(t,"set",set)
	end
end


local s__index = function (t,k )
	if k == "bodyType" then
		return t.type
	elseif k == "propertyType" then
		ensureSet(t)
		return t.set.id
	elseif k == "key" then
		ensureSet(t)
		return t.set.key
	elseif k == "name" then
		ensureSet(t)
		return t.set.name
	elseif k == "icon" then
		ensureSet(t)
		return t.set.icon
	end
	return nil
end

function this:ctor(msg,host)
	self.value 	  = msg.value
	self.type 	  = msg.type
	self.property = msg.property
	self.host 	  = host
	setmetatable(self,{__index=s__index})
end


return this