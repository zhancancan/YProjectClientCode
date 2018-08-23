local class 	= require "Class"
local base 		= require "Data_Action"
local this 		= class("Data_CmdCast",base)
local tools 	= require "CommandUtils"
local log		= require "Logger"

function this:ctor()
	self.time = 0
end
-- - is self
function this:execute(_)
	if self.char.id == self.id then return end

	local d =self.table:selectOne(self.id)
	if d and d.entity then
		self.stateName= "FireSkill"
		if not self.fx then
			log.error("Data_CmdCast no fx found!")
		end
		self.position = tools.toVector3(self.position)
		self.forcast = tools.toVector3(self.forcast) -- change to Unity Vector3
		d.entity:ChangeState(self,0)
	end
end

return this