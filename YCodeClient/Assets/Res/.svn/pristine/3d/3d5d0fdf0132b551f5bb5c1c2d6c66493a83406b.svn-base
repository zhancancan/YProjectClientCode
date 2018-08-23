local class 		= require "Class"
local base			= require "TableProxy"
local this 			= class("Proxy_Player",base)
local user 			= require "Data_Character"

function this:pushData(buffer)
	local isNew,data=self:_push(buffer)
	if not data then return end
	if isNew then
		local id = data.id
		if id == user.id then
			data:createEntity("hostMachine_0")
		else
			data:createEntity("subPlayerMachine_0")
		end
	end
end

return this