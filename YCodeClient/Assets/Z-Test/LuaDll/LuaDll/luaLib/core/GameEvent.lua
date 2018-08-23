local class = require "Class"
local this	= class("GameEvent")



function this:ctor(t)
	self.type = t
end
function this:stopProgation()
	self._stopped=true
end

return this