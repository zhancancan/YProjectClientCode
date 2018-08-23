local class = require "Class"
local this = class("SingleProxy")
function this:ctor(name,data,parser)
	self._name =name
	self._data=data
	self._parser=parser
	self._parserObj=parser()
end


function this:pushData(buffer)
	self:__push(buffer,true)
end

function this:__push(buffer, triggerEvent)
	if buffer==nil then return end
	local message= self._parserObj
	message:ParseFromString(buffer)
	self._data:parseFromMessage(message,triggerEvent)
end

function this.afterPush() end

return this