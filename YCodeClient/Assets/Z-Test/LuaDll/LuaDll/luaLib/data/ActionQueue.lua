local heap 		 = require "BinaryHeap"
local GameTicker = require "GameTicker"

local this	= {}
local index	= 0

local function compare(a,b)
	local at = a.time and a.time or 0
	local bt = b.time and b.time or 0

	if at == bt then
		local ai = rawget(a,"uuid")
		local bi = rawget(b,"uuid")
		return ai<bi
	end
	return at < bt
end

local queue = heap(compare)



function this.add(action)
	local now = GameTicker.time
	if action.time <= now then
		action:execute(now)
	else
		rawset(action,"uuid",index)
		index = index + 1
		queue:add(action)
	end
end

function this.updateNow(_,now)
	while not queue:isEmpty() do
		local a = queue:check()
		if a.time > now then break end
		a = queue:shift()
		a:execute(now)
	end
end

local t = require "GameTicker"
t.normalTicker:add(this)
return this