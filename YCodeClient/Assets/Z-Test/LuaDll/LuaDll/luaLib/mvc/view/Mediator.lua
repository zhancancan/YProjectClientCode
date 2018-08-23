local c = require "Class"
local this = c("Mediator")

function this:ctor()
	self.dbase 	= (require "DataManager").dbase
	self.char 	= require "Data_Character"
	self.typeDB = require "TypeDB"
end

return this