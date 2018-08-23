local class 		= require "Class"
local base			= require "UnsaveProxy"
local this 			= class("Proxy_Command",base)
local log			= require "Logger"


function this:pushData(buffer)
	local data=self:_parseData(buffer)
	if not data then return end
	log.log("command result:",data.result)
end

return this