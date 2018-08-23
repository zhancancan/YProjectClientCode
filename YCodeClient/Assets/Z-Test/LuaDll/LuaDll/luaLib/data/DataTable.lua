local class 		= require "Class"
local log 			= require "Logger"
local try 			= require "try"
local GameEvent 	= require "GameEvent"
local TableEvent 	= require "EventType".TableEvent


local this = class("DataTable",require "EventDispatcher")


local function invoke(self,evtType,data)
	try{
		function ()
			local e= GameEvent.new(evtType)
			e.data =data
			self:dispatchEvent(e)
		end
	}
end

local function findById(self,id)
	return self._table[id]
end

local function findByFunc(self,predict)
	for _,v in pairs(self._table) do
		if predict(v)then return v end
	end
	return nil
end


function this:ctor(name)
	self._name = name;
	self._table = {}
	self._size =0
	self._dirty=false
	self.__tostring =function ()
		return "[DataTable]"..name
	end
end
-- arg can be id(string) or predict function
function this:selectOne(arg)
	local t = type(arg)
	if t =="string" or t == "number" then return findById(self,arg)
	elseif t=="function" then return findByFunc(self,arg)
	else
		log.error("DataTable.selecton()=> arg must be id (string,number) or predict(function)")
		return nil
	end
end

function this:selectList(predict)
	if type(predict)~="function" then
		log.error("DataTable:findByFunc() -> predict must be function")
		return
	end

	local list = {}
	for _,v in pairs(self._table) do
		if predict(v) then	list[#list+1]=v	end
	end
	return list
end

function this:toArray()
	local  list = {}
	for _,v in pairs(self._table) do
		list[#list+1] =v
	end
	return list
end

function this:foreach(action)
	if type(action) ~= "function" then
		log.error("DataTable:foreach() -> action must be function")
		return
	end
	for _,v in pairs(self._table) do
		action(v)
	end
end

function this:insert(data)
	if type(data) ~= "table" then
		log.error("DataTable:insert() -> ["..self._name.."]data must be table")
		return
	end
	local id = data.id
	local id_type = type (id)

	if id_type ~= "string" and id_type ~= "number" then
		log.error("DataTable:insert() -> ["..self._name.."]data.id must be string or number")
		return
	end

	local  prev = self._table[id]
	if prev~=nil
		then self:remove(prev)
	else
		self._size=self._size+1
	end
	self._table[id]=data
	invoke(self,TableEvent.INSERT,data)
end

function this:remove(data)
	local t = type(data)
	if t~="string" and t ~="number" and t~="function" and t~="table" then
		log.error("DataTable:remove() -> data must be table, string, function, number")
		return
	end

	if t =="string" or t == "number" then
		data = self._table[data]
	elseif t == "function" then
		data = self.findByFunc(data)
	end

	if(data~=nil) then
		local disposer = data.dispose
		if disposer~=nil then disposer(data) end
		self._table[data.id]=nil
		self._size =self._size-1
		invoke(self,TableEvent.REMOVE,data)
	end
end

function this:clear(autoDispose)
	for k,v in pairs(self._table) do
		if autoDispose and type(v.dispose)== "function" then v:dispose() end
		self._table[k] = nil;
	end
end

function this:count()
	return self._size
end


function this:invokeUpdate()
	if self._dirty then
		invoke(self,TableEvent.UPDATE)
		self._dirty=false
	end
end

return this