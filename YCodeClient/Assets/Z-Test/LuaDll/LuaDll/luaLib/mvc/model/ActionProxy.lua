local class = require "Class"
local this = class("ActionProxy")
local queue = require "ActionQueue"

function this:ctor(code,producer,parser)
	self._code =code
	self._producer = producer
	self._parser=parser
	self._parserObj=parser()
	self.cachable=true;
end


local function parse(self ,buffer)
	if buffer==nil then return end
	local message= self._parserObj
	message:ParseFromString(buffer)
	local data = self._producer()
	data:parseFromMessage(message,false)
	return data
end

function this:pushData(buffer)
	local data = parse(self,buffer)
	queue.add(data)
end

function this.afterPush() end

return this