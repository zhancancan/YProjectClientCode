local class 			= require "Class"
local p_unit 			= require "PropertyUnit"
local PropertyEvent 	= require "PropertyEvent"
local PropertyDispacher = require "PropertyDispatcher"
local Array 			= require "Array"
local this 				= class ("PropertyGroup")
local compareList		= Array()

local function get(arr,a)
	for i=1,#arr do
		local b = arr[i]
		if a.property == b.property then return b end
	end
	return nil
end

local function parse(self,field,message,shouldInvoke)
	local pc =self._data[field]
	local src = pc._units
	local dst = {}
	local host =self._host
	if message.properties then
		local list = message.properties
		for i=1,#list do
			dst[i] = p_unit.new(list[i],host)
		end
	end

	pc._units =dst

	if shouldInvoke then
		compareList:clear()

		for i=#src,1,-1 do
			local a= src [i]
			if not get(dst,a) then
				src[i] = nil
				compareList:insert({a=a})
			end
		end


		for i=1,#dst do
			local b = dst[i]
			local a = get(src,b)
			if a then
				if a.value ~= b.value then
					compareList:insert({a=a,b=b})
				end
			else
				compareList:insert({b=b})
			end
		end

		for i=1,#compareList do
			local r = compareList[i]
			local a = r.a
			local b = r.b
			local oldV ,newV
			if a then oldV = a.value  else oldV = 0 end
			if b then newV = b.value  else newV = 0 end

			local pk =  a and a.key or b.key
			local pn =  a and a.name or b.name


			local e = {data=self,property=pk,oldValue=oldV,newValue=newV}
			PropertyEvent.push(e)

			e = {data=pc,property=pn,oldValue=oldV,newValue=newV}
			PropertyEvent.push(e)
		end
		compareList:clear()
	end
end


function this:ctor(host,field,preset,is_type_data)
	self._host = host
	self._units = {}
	if preset then
		for i=1,#preset do
			self._units[i] =p_unit.new(preset[i],host)
		end
	end
	field = field and field or "property"
	if not is_type_data then
		local sp = rawget(host,"_specialParser")
		if not sp then	sp ={}	rawset(host,"_specialParser",sp) end
		sp[field] = parse
		self._dispatcher = PropertyDispacher.new(this)
	end
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


return this