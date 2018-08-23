local class = require "Class"
local this = class("TableProxy")

function this:ctor(table,code,dataProducer,parser)
	self._name =table._name
	self._table=table
	self._code=code
	self._dataProducer=dataProducer
	self._parser=parser
	self._parserObj=parser()
	self.__tostring=function() return "[TableProxy]"..self._name end
end

function this:pushData(buffer)
	return self:_push(buffer)
end

function this:_push(buffer)
	if buffer==nil then return end
	local message= self._parserObj
	message:ParseFromString(buffer)
	local data = self._table:selectOne(message.id)
	local isNew
	if message.isRemove==1 then
		if data then
			self._table:remove(data)
		end
	else
		if data==nil then
			isNew=true
			data=self._dataProducer()
		end
		data:parseFromMessage(message,not isNew)
		if isNew then self._table:insert(data) end
		self._table._dirty=true
		data:onUpdate()
	end
	return isNew,data
end


function this:afterPush()
	self._table:invokeUpdate()
end

return this