local class = require "Class"
local this = class("CpxCondition_Test");

local log= require "Logger"

function this:enter()
	log.log("enter");
end
function this:update(time)
	log.log("update",time);
end
function this:exit()
	log.log("exit");
end

function this:isValidate(time,trigger)

end

return this;