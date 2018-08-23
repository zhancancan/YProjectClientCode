local  this ={}
local log =require"Logger"

function this.notify(code,...)
	local c = this[code]
	if c then c.execute(...)
	else log.error(string.format("command %d not found",code))
	end
end

return this