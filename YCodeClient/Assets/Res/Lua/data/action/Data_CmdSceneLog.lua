local class 	= require "Class"
local base 		= require "Data_Action"
local this 		= class("Data_CmdSceneLog",base)
local log 		= require "Logger"

function this:ctor()
	
end

function this:execute(_)
	log.log("Data_CmdSceneLog to do:"..self.message)


end

return this