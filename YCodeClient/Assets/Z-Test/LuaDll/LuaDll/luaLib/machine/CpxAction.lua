local class= require "Class"
local CpxAction = class("CpxAction")
local this = CpxAction;

function this:enter(action)
	self._action=action
end

return this