local class 		= require "Class"
local base			= require "TableProxy"
local this 			= class("Proxy_Npc",base)
local log 			= require "Logger"

function this:pushData(buffer)
	log.log("Proxy_Npc pushData")
	local isNew,data=self:_push(buffer)
	if not data then return end
	if isNew then
		log.log(string.format("NPC id:%s,isRemove %s,isDead %s",data.id,data.isRemove,data.isDead))
		if data.isRemove or data.isDead then return end
		data:createEntity("NpcMachine_0")
	end
end
return this