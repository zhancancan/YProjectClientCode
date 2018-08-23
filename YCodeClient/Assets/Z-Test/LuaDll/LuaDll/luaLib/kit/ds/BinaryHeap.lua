local log = require "Logger"

local function swap(list, a,b )
	local t = list[a]
	list[a] = list[b]
	list[b] = t
end


local function sort(self)
	local struct= self._struct
	local len=#struct

	if len <= 2 then return end

	if len == 3 then
		local a = struct[2]
		local b = struct[3]
		if not self._compare(a,b) then
			swap(struct, 2,3)
		end
	else
		local n =1
		local left= n*2
		local right = left+1
		local move = struct[n+1]
		while right < len do
			local x= struct[left+1]
			local y= struct[right+1]
			local min = self._compare(x,y) and left or right
			if self._compare(move,struct[min+1]) then break end
			swap(struct, min +1 , n+1)
			n=min
			left= n*2
			right=left+1
		end
	end
end



local function add(self,obj)
	if not obj then return end
	local struct =self._struct
	local len = #struct
	struct[len+1] =obj
	local parent = math.floor(len *0.5)
	while parent>=1 and self._compare(struct[len+1],struct[parent+1]) do
		swap(struct,len+1,parent + 1)
		len=parent
		parent=math.floor(len *0.5)
	end
end

local function shift(self)
	local struct= self._struct
	local len = #struct
	if len ==1 then return nil end
	local shiftObj= struct[2]
	struct[2] = struct[len]
	struct[len]=nil
	if #struct == 1 then return shiftObj end
	sort(self)
	return shiftObj
end

-- same to stack.peek, not remove from the list
local function check(self)
	local struct= self._struct
	local len = #struct
	if len ==1 then return nil end
	local shiftObj= struct[2]
	if len ==2 then return shiftObj end
	sort(self)
	return shiftObj
end


local function isEmpty(self)
	return #self._struct<=1
end

local function default_compare(a,b)
	return a < b
end

local meta={
	_compare = default_compare,
	add 	 = add,
	shift 	 = shift,
	check 	 = check,
	isEmpty  = isEmpty
}

meta.__index = function (_,k)
	if meta[k] then return meta[k] end
	log.error(string.format("error: don't get data through [%s],us shift() or check()",tostring(k)))
end

meta.__newindex = function (_,k)
	log.error(string.format("error: don't set data through [%s],us add() function",tostring(k)))
end





local function BinaryHeap(compare)
	local heap={}
	heap._struct={"null"}
	if compare then heap._compare=compare end
	setmetatable(heap,meta)
	return heap
end




return BinaryHeap;