local class 		= require "Class"
local base			= require "TableProxy"
local this 			= class("Proxy_Aoe",base)
-- local log 			= require "Logger"


function this:pushData(buffer)
	local isNew,data=self:_push(buffer)
	if not data then return end
	if isNew then
		data:createEntity("AoeMachine_0")
	end
end

return this