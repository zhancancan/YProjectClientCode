local class 		= require "Class"
local this 			= class("PropertyUnit")
local db 			= require "TypeDB"


local function ensureSet(t)
	local set = rawget(t,"set")
	if not set then
		set = db.selectOne(t.sourceType,{id=t.sourceId})
		rawset(t,"set",set)
	end
end


local function getter(t,k)
	ensureSet(t)
	if k == "icon" then
		return t.set.icon
	elseif k == "name" then
		return t.set.name
	end
	return nil
end

function this:ctor(msg,host)
	self.value 	  	= msg.value
	self.sourceType	= msg.sourceType
	self.sourceId 	= msg.sourceId
	self.host 	  	= host
	setmetatable(self,{__index=getter})
end


return this