
local class = require "Class"
local base	= require "UnsaveProxy"
local this 	= class("Proxy_Error",base)
local log 	= require "Logger"
local events  = require "ErrorCenter"


function this:pushData(buffer)
	local data=self:_parseData(buffer)
	if not data then return end
	log.error("Data_Error",data.code,data.msg)
	events.push(data)
end


return this