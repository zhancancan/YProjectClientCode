local class 		= require "Class"
local co 			= require "coroutine"
local log 			= require "Logger"


local namespace 	= require "CSNamespace"
local CompStatus 	= namespace["CompStatus"]

local GameTicker = require "GameTicker"
local this = class("Test Lua Action")

function this:enter(c)
	self.action =c
	self._coupdate=co.start(self.execute,self)
	co.start(self._wait,self,2)
	self._prev=GameTicker.time
	self._startTime=GameTicker.time
end
function this:execute()
	local c=0
	while true do
		c=c+1
		co.wait(0.1)

		self._prev =GameTicker.time
	end
end
function this:_wait(d)
	co.wait(d)
	self.action:SetStatus(CompStatus.COMPLETE)
	if self._coupdate~=nil then
		co.stop(self._coupdate)
		log.log("stop coroutine :" , self._coupdate)
		self._coupdate=nil
	end
end
function this:exit()
	self.action=nil

end

return this;