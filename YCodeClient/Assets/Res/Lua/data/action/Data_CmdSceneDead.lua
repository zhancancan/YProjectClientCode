local class 		= require "Class"
local base 			= require "Data_Action"
local this 			= class("Data_CmdCast",base)
local TypeClasses 	= require "TypeClasses"
local db 			= require "TypeDB"
local log			= require "Logger"
local gameTicker	= require "GameTicker"

local data

-- - is self
function this:execute(_)

	if self.id == self.char.id then return end

	local d = self.table:selectOne(self.id)
	if d and d.entity then
		data = d
		self.stateName = "Dead"
		log.log(string.format("cmd player %s to dead",self.id))
		d.entity:ChangeState(self,0)


		self.table:remove(d,false)--dispose = false

		gameTicker.normalTicker:add(self)
		self._holdTime = gameTicker.time
	end


end

function this:updateNow(now)--wait 2 seconds--todo

	if now > self._holdTime + 2 *1000 then
		gameTicker.normalTicker:remove(self)
		-- log.log("remove after 2 seconds")
		if data then
			-- log.log("disp after 2 seconds")
			local disposer = data.dispose
			if disposer then
				disposer(data)
			end
		end
		
	end
end

return this