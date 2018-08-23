local this={}
local lang 			= require "LangManager"
local TimeFormat  	= require "TimeFormat"
local namesapce 	= require "CSNamespace"
local VersionGroup 	= namesapce["VersionGroup"]

function this.onInit(_,med)
	local v = VersionGroup.GetServerVerion()
	med:SetString("txt_2",lang.format("Preload.updateVer",v:ToString()))
end
function this.onUpdate(_,med,current,total,speed)
	local p = current/total
	med:SetFloat("bar",p)
	p =string.format("%.0f%%",p*100)
	med:SetString("txt_1",p)
	local time = (total - current)/speed
	time = TimeFormat.format(time,"%H:%M:%S")
	p = lang.format("Preload.download",lang.formatSize(current),lang.formatSize(total),time)
	med:SetString("txt_0",p)
end
return this