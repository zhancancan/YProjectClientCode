local class 	= require "Class"
local base 		= require "Data_Action"
local this 		= class("Data_CmdMove",base)
local tools 	= require "CommandUtils"

function this:ctor()
	self.time = 0
end
-- - is self
function this:execute()
	if self.char.id == self.id then return end

	self.stateName= "Move"
	local d =self.table:selectOne(self.id)
	if not self.isLast then self.isLast = false end

	if d then
		local e = rawget(d, "entity")
		if e then
			self.position = tools.toVector3(self.position)
			self.forcast  = tools.toVector3(self.forcast)
			e:ChangeState(self,0)
		end
	end
end

return this