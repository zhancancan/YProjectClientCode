local this={}
local events ={}

function this.addListener(method, methodObj)
	if not methodObj then methodObj = "null" end
	events[method] = methodObj
end

function this.removeListener(method)
	events[method] = nil
end

function this.push(d)
	for k, v in pairs (events) do
		k( d, v)
	end
end

return this