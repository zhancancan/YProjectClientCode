local class=require "Class"
local this =class("Data_MoveAction",require"Data")
local log  =require "Logger"
-- - is self
function this.execute(_)
	log.log("Data_MoveAction execute")
end

return this