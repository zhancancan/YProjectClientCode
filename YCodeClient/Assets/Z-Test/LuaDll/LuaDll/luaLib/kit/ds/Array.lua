local log = require "Logger"
local array_meta={}
local method ={}

local function Array(...)
	local a= {...}
	if #a == 1 and type(a[1])=="table" then a=a[1] end
	local new_array = setmetatable(a,array_meta)
	return new_array
end

array_meta.__index=function(t,k)
	local v =rawget(t,k)
	if v then
		if v=="null" then return nil end
		return v
	end
	if method[k] then return method[k] end

end

array_meta.__newindex =function (t,k,v)
	if rawget(t,k) == nil then log.error(string.format("error: [%s] index out of range.",tostring(k))) return end
	if v == nil then log.error(string.format("warning : can not remove element by using  `nil`.")) return end
	t[k]=v
end


function method:insert(v,at)
	local len = #self +1
	at = type(at) == "number"and at or len
	at = math.max(math.min(at ,len),1)
	table.insert(self,at,v)
end

function method:pop()
	local len = #self
	if len >0 then local t = self[len] self[len]=nil return t end
end
function method:shift()
	local len =#self
	if len > 0 then
		local t = self[1]
		for i=1,len do self[i]=self[i+1] end
		return t
	end
end

function method:removeAt(at)
	local arr = self
	at = type(at)== "number" and at or #arr
	local d = arr[at]
	table.remove(arr,at)
	return d
end

function method:remove(e)
	local i= self:indexOf(e)
	if i~=-1 then self:removeAt(i) return true end
	return false
end

function method:concat(t)
	local ml=#self
	for i=1,#t do self[i+ml]=t[i]end
end

function method:clone()
	local a =Array()
	a:concat(self)
	return a
end

function method:foreach(action)
	for i=1,#self do action(self[i])end
end

function method:indexOf(element)
	for i=1 , #self do
		if self[i] == element then return i end
	end
	return -1
end

function method:select(predict)
	local a= Array()
	for i=1,#self do if predict(self[i]) then a:insert(self[i])end end
	return a
end

function method:copy(arr, offset,len)
	if not offset then offset =1 end
	if not len then len =#self end
	arr:setLength(len)
	for i=1,len do
		arr[i] = self[i+offset-1]
	end
end
function method:reverse()
	local len = #self
	if len <=1 then return end
	for i=1,len/2 do
		local t = rawget(self, i)
		local index = len -(i-1)
		rawset(self, i, rawget(self,index))
		rawset(self, index, t)
	end
end


function method:first(predict)
	for i=1,#self do if predict(self[i]) then return self[i] end end
end
function method:clear()
	for i =#self,1,-1 do rawset(self,i,nil)end
end

function method:setLength(len)
	local ml=#self
	if len>ml then
		for i=ml+1,len do  rawset(self,i,"null") end
	elseif ml then
		for i=ml,len+1,-1 do rawset(self,i,nil) end
	end
end


function method:sort(f)
	table.sort(self,f)
end

return Array