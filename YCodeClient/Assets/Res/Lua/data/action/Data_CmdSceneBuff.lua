local class 		= require "Class"
local base 			= require "Data_Action"
local this 			= class("Data_CmdCast",base)
local TypeClasses 	= require "TypeClasses"
local db 			= require "TypeDB"

function this:ctor()
	self.time = 0
end
-- - is self
function this:execute(_)

	local typeBuff = db.selectOne(TypeClasses.TYPE_BUFF,self.buffId)
	print(string.format("Data_CmdSceneBuff %s", typeBuff),self.id)

	local d = self.table:selectOne(self.id)
	if d and d.entity then
		self.prefab = "ceshifeixing"

		self.stateName = "AddBuff"
		d.entity:ChangeState(self,1)
	end


end

return this