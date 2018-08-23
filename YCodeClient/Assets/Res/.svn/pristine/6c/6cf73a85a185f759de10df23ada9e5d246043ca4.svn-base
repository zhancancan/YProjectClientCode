local class = require "Class"

local base = require "SingleProxy"
local this = class("Proxy_UserState",base)
local log = require "Logger"

function this:ctor()
	self._inited=false
end

local function onPropertyChanged(_,data,p,o,n)
	-- print(data,p,o,n)
end


function this:pushData(buffer)
	self:__push(buffer,self._inited)
	log.log("Proxy_UserState pushData")
	if not self._inited then
		local c = require "AppCenter";
		c.playerId=self._data.id
		self._data.property:addListener(self,onPropertyChanged)
		self._inited=true
	end
end



return this