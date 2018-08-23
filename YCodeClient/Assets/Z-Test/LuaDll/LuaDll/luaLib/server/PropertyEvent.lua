local Array = require "Array"

local this={}
local events = Array()

function this.push(evt)
	events:insert(evt)
end

local function release()
	for i =1 ,#events do
		local p = events[i]
		p.data:invoke(p.property,p.oldValue,p.newValue)
	end
	events:clear();
end

local pc  = require "ProxyCenter"

pc.registerPostAction(release)

return this