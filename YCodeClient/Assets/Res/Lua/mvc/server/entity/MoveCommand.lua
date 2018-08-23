local CommandClasses 	 = require "CommandClasses"
local SocketForm = require "SocketForm"

local this={}


function this.execute(x,y,z,fx,fy,fz,isLast)
	local  form = SocketForm(CommandClasses.CmdMove)
	form.type = 0
	form.position={x=x,y=y,z=z}
	form.forcast={x=fx,y=fy,z=fz}
	form.isLast =isLast
	form:send(true)
end

return this