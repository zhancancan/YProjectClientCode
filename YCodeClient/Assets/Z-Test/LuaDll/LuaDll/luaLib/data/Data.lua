local class 		= require "Class"
local propertyEvent = require "PropertyEvent"
local Dispatcher 	= require "PropertyDispatcher"
local Array 		= require "Array"
local log 			= require "Logger"

local this 			= class("Data")

local function pick(message)
	local d={}
	if not message._fields then
		-- the message could be array
		for i=1, #message do
			local v = message[i]
			if type(v) == "table" then d[i] = pick(v)
			else d[i]= v end
		end
		return d
	end

	for k,v in pairs(message._fields) do
		if type(v)=="table" then d[k.name] = pick(v)
		else d[k.name] =v
		end
	end
	return d
end


function this:ctor()
	self._data={}
	self.dbase = ( require "DataManager" ) .dbase
	self._dispatcher = Dispatcher.new(self)
	setmetatable(self ,{
			__index = self._data,
			__newindex = self._data
		}
	)
end


function this:addListener(watcher,method)
	self._dispatcher:addListener(watcher,method)
end

function this:removeListener(watcher,method)
	self._dispatcher:removeListener(watcher,method)
end

function this:removeAllListener()
	self._dispatcher:removeAllListener()
end
function this:invoke(property,oldValue,newValue)
	self._dispatcher:invoke(property,oldValue,newValue)
end


function this:parseFromMessage(message,shouldInvode)
	local parser = self._specialParser
	for k,v in pairs(message._fields) do
		if v then
			local field = k.name
			if parser and parser[field] then
				parser[field](self,field,v,shouldInvode)
			else
				local o = self._data[k.name]
				local n = v

				if type(n)=="table" then
					self._data[field] = pick(n)
				else
					self._data[field] = n
				end

				if(o~=n)then
					local e = {data=self,property=field,oldValue=o,newValue=n}
					if shouldInvode then propertyEvent.push(e)end
					if self.onSelfChange then
						if not self._events then self._events=Array() end
						self._events:insert(e)
					end
				end
			end
		else
			log.log(k , "is nil")
		end

	end
end



local function updateSelfProperty(self)
	local es = self._events
	if es then
		for i=1,#es do
			local e= es[i]
			self.onSelfChange(self,e.property,e.oldValue,e.newValue)
		end
		es:clear()
	end
end


function this:onUpdate()
	updateSelfProperty(self)
end



function this:dispose()
	if rawget(self,"_disposed") then return end
	self:removeAllListener()
	rawset(self,"_disposed",true)
end

return this