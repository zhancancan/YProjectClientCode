local colorRepl ="<color=%s>%s</color>"
local herfRepl = "<a herf=%s color=%s>%s</a>"
local string = require "string"
local this ={}
function this.html(str,color, herf)
	local len = string.len(color)
	if string.sub(color,1,2) == "0x" then color =string.sub(color,3,len) end
	if string.sub(color,1,1) ~= "#" then color = "#"..color end

	if herf then
		return string.format(herfRepl,herf,color,str)
	else
		return string.format(colorRepl,color,str)
	end
end

function this.split(str,sep)
	sep = sep or ","
	local fields = {}
	local patten = string.format("[^%s]+",sep)
	string.gsub(str,patten , function (c) fields[#fields+1] = c end)
	return fields
end

return this