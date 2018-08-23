local class 	= require "Class"
local this 		= class("DataBase")
local log 		= require "Logger"

function this:ctor(name)
	self._table ={}
	self._name = name
end

function this:insertTable(tableName,t)
	if self._table[tableName]then log.warn(string.format("%s occupied",tableName)) return end
	self._table[tableName] =t
	return t
end

function this:getTable(tableName)
	if type(tableName)~="string" or #tableName == 0 then
		log.error(string.format("DataBase:getTable()-> %s is not string",tableName))
		return nil
	end
	return self._table[tableName]
end

function this:insert(tableName,data)
	if type(tableName)~="string" or #tableName == 0 then
		log.error(string.format("DataBase:insert()-> %s is not string",tableName))
		return
	end
	if (data~=nil) then
		local t=self._table[tableName]
		if(t==nil) then log.error(string.format("table [%s] not founnd",tableName)) return end
		t:insert(data)
	end
end
function this:remove(tableName,data)
	if type(tableName)~="string" or #tableName == 0 then
		log.error(string.format("DataBase:insert()-> %s is not string",tableName))
		return
	end
	if (data~=nil) then
		local t=self._table[tableName]
		if(t==nil) then log.error(string.format("table [%s] not founnd",tableName)) return end
		t:remove(data)
	end
end

-- arg can be the id (typeof string) or predict(function)
function this:selectOne(tableName,arg)
	if type(tableName)~="string" or #tableName == 0 then
		log.error(string.format("DataBase:insert()-> %s is not string",tableName))
		return
	end

	local t=self._table[tableName]
	if(t==nil) then	log.error(string.format("table [%s] not founnd",tableName)) return nil end
	return t:selectOne(arg)
end

-- arg can be predict(function)
function this:selectList(tableName,arg)
	if type(tableName)~="string" or #tableName == 0 then
		log.error(string.format("DataBase:insert()-> %s is not string",tableName))
		return
	end
	local t = self._table[tableName]
	if(t==nil)then	log.error(string.format("table [%s] not founnd",tableName)) return {} end
	return t:selectList(arg)
end

function this:toArray(tableName)
	if type(tableName)~="string" or #tableName == 0 then
		log.error(string.format("DataBase:insert()-> %s is not string",tableName))
		return
	end
	local t = self._table[tableName]
	if(t==nil)then 	log.error(string.format("table [%s] not founnd",tableName)) return {} end
	return t:toArray()
end

function this:clearTable(tableName, autodispose)
	if type(tableName)~="string" or #tableName == 0 then
		log.error(string.format("DataBase:insert()-> %s is not string",tableName))
		return
	end

	local t = self._table[tableName]
	if(t==nil)then	log.error(string.format("table [%s] not founnd",tableName)) return {} end
	t:clear(autodispose)
end
function this:clear(dispose)
	for _, v in pairs(self._table) do
		v:clear(dispose)
	end
end
return this