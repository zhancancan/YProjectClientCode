local this = {}
local Vector3  = Vector3
local log = require "Logger"
function this.toVector3(v)
	if v then
		return Vector3.New(v.x,v.y,v.z)
	end
	log.error("toVector3 error")
	return  Vector3.New(0,0,0)
end



return this