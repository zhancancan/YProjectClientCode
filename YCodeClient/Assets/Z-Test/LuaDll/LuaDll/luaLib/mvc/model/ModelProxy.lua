local class 	= require "Class"
local base 		= require "TableProxy"
local this		= class("ModelProxy",base)
local log 		= require "Logger"

function this.pushData()
	log.error("ModelProxy:pushData must be override")
end

return this