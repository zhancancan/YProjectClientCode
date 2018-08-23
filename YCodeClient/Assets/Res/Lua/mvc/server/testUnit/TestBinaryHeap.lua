local this ={}
local bh = require"BinaryHeap"
function this.test()
	local h = bh(function (a,b)return a>b end)
	for i=1,10 do
		h:add(math.random())
	end
	local t={}
	while not h:isEmpty() do
		table.insert(t,string.format("%s, ",h:check()))
		h:shift()
	end
	local s = table.concat(t)
	print(s)
end

return this