local class = require "Class"
local Array = require "Array"
local log 	= require "Logger"

local this 	= class("TypeTable")

local function find(self,condition,args)
	local len = #condition
	local con = self._con
	local numArgs = #args
	if len~=numArgs then log.error("indexField.lenght != args.length") return nil end
	for x=1,len do
		local property = condition[x]
		local sub = con._query[property]
		local val  = args[x]
		if x == len then
			return sub._datas[val]
		end
		local child
		if sub then child = sub._children[val] else child=nil end
		if not sub or not child then
			for t=1,# con._list do
				local d = con._list[t]
				local r = true
				for k=x,len do if d[condition[k]]~=args[k] then r=false break end end
				if r then return d end
			end
			return nil
		end
		con=child
	end
end


local function matchConditionToIndexField(self, condition)
	if not condition then return nil,nil,false end
	local indexField = self._indexField;
	local fc,fa
	for i=1,#indexField do

		local index =indexField[i]
		local c={}
		local a={}

		for k, v in pairs (condition)  do
			local q=index:indexOf(k) c[q]=k	a[q]=v
		end

		if #c ==#index then return c,a,true end-- find the perfect match indexField now

		if not fc or #fc <#c then fc =c  fa=a end
	end
	if fc and #fc>0 then return fc,fa,false end
	return nil,nil,false
end


local function filterList(predict,list)
	local __newlist={}
	for i=1,#list do if predict(list[i]) then __newlist[#__newlist+1]=list[i]end end
	return __newlist
end

local function pickFromList(predict,list)
	for i=1,#list do if predict(list[i])then return list[i] end end
	return nil
end


local function cloneArray(list)
	local _clone={}
	for i=1, #list do
		_clone[i]=list[i]
	end
	return _clone
end

local function locateList(self,condition,args)
	if condition ==nil then return self._everything end
	local len = #condition
	local con = self._con
	for x=1,len do

		local property 	= condition[x]
		local sub 		= con._query[property]
		local val  		= args[x]

		if not sub._children then-- reach the deepest
			local o =sub._datas[val]
			if o then return {o} else return {} end
		end

		local child = sub._children[val]

		if child == nil then
			return con._list
		else
			con=child
			if x == len then
				if sub._datas then return {sub._datas[val]}
				elseif con._list then return cloneArray(con._list)
				else
					log.error("TypeTable.localList() => find nothing")
				end
			end
		end
	end
end




local function getListByCondition(condition, list)
	if not condition then return list end
	local arr=Array()
	for i=1,#list do
		local allfit=true
		local data=list[i]
		for k,v in pairs(condition) do
			if data[k]~=v then allfit=false break end
		end
		if allfit then arr:insert(data)end
	end
	return arr
end

function this:ctor(name,indexField)
	self._con={_query={}}
	self._name=name
	self._indexField=Array()
	self._everything={}
	for i=1,#indexField do
		self._indexField:insert(Array(indexField[i]))
	end
	self._size=0
end


function this:insert(data)
	local len = #self._indexField
	local con = self._con
	for x=1, len do
		local index=self._indexField[x]
		local ylen = #index
		for y=1 ,ylen do
			local property = index[y]
			local val = data[property]
			local sub = con._query[property]
			if not sub then
				sub = {_name=property}
				con._query[property] =sub
			end

			if y == ylen then
				if sub._datas ==nil then sub._datas ={} end
				sub._datas[val]=data
			 else
				 if not sub._children then sub._children={} end
				 if not sub._children[val] then sub._children[val]={_query={}}end
				 con=sub._children[val]
				 if not con._list then con._list={}end
				 con._list[#con._list+1]=data
			 end
		end
	end
	self._everything[#self._everything+1]=data
	self._size=self._size+1
	return data
end


function this:selectOne(condition,predict)
	if not condition and not predict then
		log:error(string.format("TypeTable[%s] selectOne: both condition and predict is nil",self._name))
		return nil
	end

	local tc = type(condition)
	if tc == "string" or tc == "number" then
		local mainIndex = self._indexField[1]
		if not mainIndex then
			log:error(string.format("TypeTable[%s] has no indexField",self._name))
			return nil
		end
		if #mainIndex~=1 then
			log:error(string.format("TypeTable[%s] main index is multi index,and have on input parameter ",self._name))
			return
		end
		local cobj={}
		cobj[mainIndex[1]] = condition
		condition = cobj
	end

	local c,a,match = matchConditionToIndexField(self,condition)
	if c and a then
		if type(predict)=="function" then
			local tempList = locateList(self,c,a)
			return pickFromList(predict,tempList)
		else
			if match then return find(self,c,a) end
			local tempList =locateList(self,c,a)
			local arr = getListByCondition(condition,tempList)
			return arr[1]
		end
	end

	if predict~=nil and type(predict)=="function" then
		return pickFromList(predict,self._everything)
	else
		log.error("TypeTable:selectOne()=> some how error")
	end
end


function this:selectList(condition,predict)
	if not condition and not predict then
		log:error(string.format("TypeTable[%s] selectList: both condition and predict is nil",self._name))
		return nil
	end

	local tc = type(condition)
	if tc == "number" or tc == "string" then
		log:error(string.format("TypeTable[%s] selectList: condition must be table or function ",self._name))
		return nil
	end

	if tc == "function" then
		predict = condition
		condition = nil
	end

	local c,a,match=matchConditionToIndexField(self,condition)
	local l;
	if c and a then
		l = locateList(self,c,a)
	else
		l = cloneArray(self._everything)
	end
	if not match and condition~=nil then l = getListByCondition(condition,l) end
	if type(predict)=="function" then l=  filterList(predict,l)end
	l=Array(l)
	return l
end

function this:toArray()
	local l = cloneArray(self._everything)
	return Array(l)
end

function this:foreach(action)
	for i=1,# self._everything do action(self._everything[i]) end
end

function this:count()
	return #self._size
end


return this