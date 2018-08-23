local class 		= require "Class"
local base			= require "UnsaveProxy"
local this 			= class("Proxy_PingResp",base)
local GameTicker 	= require "GameTicker"

function this:pushData(buffer)
	local data=self:_parseData(buffer)
	if not data then return end
	GameTicker.pingResponse(data.time)
end


return this