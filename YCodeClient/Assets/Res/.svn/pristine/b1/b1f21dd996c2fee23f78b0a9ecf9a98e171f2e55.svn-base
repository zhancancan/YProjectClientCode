local class 	= require "Class"
local user		= require "Data_Character"

local base 		= require "SingleProxy"
local this 		= class("Proxy_UserState",base)
-- local log 		= require "Logger"
local loginTools= require "LoginTools"

function this:ctor()
	self.__tostring=function()return "[Proxy_UserState]" end
	self._inited=false
end


function this:pushData(buffer,serverId)
	self:__push(buffer,serverId,self._inited)
	if not self._inited then
		local c = require "AppCenter";
		local data = self._data._data
		c.playerId=self._data._data.id
		c.zoneId = data.zoneId
		c.name = data.name


		user.id = c.playerId
		if tonumber(user.id) > 0 then
			--charactor existed
			--go scene with other data
			-- user.name = data.name
			self._inited=true
		else
			--no exist,show create role

			loginTools.goCreateRole()
		end


		-- self._inited=true
	end
end

return this