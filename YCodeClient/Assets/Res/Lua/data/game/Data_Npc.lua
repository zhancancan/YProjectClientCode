local class 		= require "Class"
local base 			= require "Data_Model"
local this 			= class("Data_Npc",base)
local db 			= require "TypeDB"
local TypeClasses	= require "TypeClasses"

local table



local function getTypeNpc(self)
	if not table then table = db.getTable(TypeClasses.TYPE_NPC)	end

	if self then
		local npc = rawget(self,"npc")
		if not npc then
			npc = table:selectOne({id=self.npcId})
			rawset(self, "npc",npc)
		end
		return npc
	end
	return nil
end



local meta = {
	__index = function(t,k)
		if k == "name"  then
			local  def = getTypeNpc(t)
			if def then return def.name end

		elseif k == "body" then
			local  def = getTypeNpc(t)
			--print("npc get body", def.body)
			if def then return def.body end
		end
		return nil
	end

}

function this:ctor()
	setmetatable(self._data, meta)
end


function this:onSelfChange(p,_,n)
	local en = rawget (self, "entity")
	if not en then return end
	if p == "npcId" then
		rawset(self,"npc",nil) -- clear the npc cache
		en:ChangeModel("body",self.body)
	elseif p == "speed" then
		en.speed = n
	end
end


return this