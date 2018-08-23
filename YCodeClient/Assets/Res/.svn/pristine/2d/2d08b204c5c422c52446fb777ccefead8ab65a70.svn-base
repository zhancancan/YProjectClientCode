local class 	= require "Class"
local base 		= require "Data_Action"
local this 		= class("Data_CmdMoveTo",base)
local tools 	= require "CommandUtils"
function this:ctor()
	self.time = 0
end

function this:execute(_)
	if self.char.id == self.id then return end
	self.stateName= "MoveTo"
	local d = self.table:selectOne(self.id)
	if d and d.entity then
		self.position = tools.toVector3(self.position)
		self.forcast  = tools.toVector3(self.forcast)
		d.entity:ChangeState(self,0)
	end
end

return this