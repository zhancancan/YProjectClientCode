local this ={}
local namespace 	= require "CSNamespace"
local VersionGroup 	= namespace["VersionGroup"]
local lang  		= require "LangManager"
local ns 			= require "CSNamespace"
local native 		= ns.NativeManager

function this:onInit(med)
	local v = VersionGroup.GetSteamVersion()
	local str =v:ToString()
	str = lang.format("Preload.updateVer",str)
	med:SetString("txt_2",str)
end

function this:onUpdate(med,current,total)
	local r= current /total
	med:SetFloat("bar",r)
	local str = string.format("%.0f%%",r* 100)
	med:SetString("txt_1",str)

	str = lang.format("Preload.copy",current,total)
	med:SetString("txt_0",str)
end

function this:onErrorCode(med, code)
	local t = lang.get("Preload.ucWarning")
	local c =lang.get("Preload.ucError_"..code)
	print(t,c)
	native.Alert(t,c)
end

return this