local class 			= require "Class"
local t_unit 			= require "PropertyUnit"
local this 				= class("ThingGroup")


local function parse(self,field,message)
	local pc =self._data[field]
	local dst = {}
	if message.things then
		local list = message.properties
		for i=1,#list do
			dst[i] = t_unit.new(list[i],self)
		end
	end
	pc._units =dst
end


function this:ctor(host,field,preset)
	self._units={}
	field = field or "things"
	if preset then
		for i=1,#preset do
			self._units[i] = t_unit.new(preset[i],host)
		end
	else
		local sp = rawget(host,"_specialParser")
		if not sp then	sp = {}	rawset(host,"_specialParser",sp) end
		sp[field] = parse
	end
end

return this