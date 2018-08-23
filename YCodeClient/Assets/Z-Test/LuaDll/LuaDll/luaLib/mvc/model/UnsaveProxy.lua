local class = require "Class"
local this = class("UnsaveProxy")

function this:ctor(code,dataProducer,parser)
	self._code=code
	self._dataProducer=dataProducer
	self._parser=parser
	self._parserObj=parser()
end

function this:_parseData(buffer)
	if buffer==nil then return nil end
	local message= self._parserObj
	message:ParseFromString(buffer)
	local data=self._dataProducer()
	data:parseFromMessage(message, false)
	return data
end

function this.afterPush()end


return this