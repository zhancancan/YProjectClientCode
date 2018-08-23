local class = require "Class"
local this = class("LoginAct_GetConfig")

local AppCenter 	= require "AppCenter"
local namespace 	= require "CSNamespace"
local CompStatus 	= namespace["CompStatus"]
local http 			= namespace["LuaHttp"]



local function onDataReceive(status, info)
	if status == 1 then 	AppCenter.configInfo=info
	else
		print("Config load Error")
	end
end

function this:enter(c)
	self.action =c
	local url = AppCenter.sdkInfo.staticSite;
	 print("get config")
	 -- http.Send(url,onDataReceive)
	 c:SetStatus(CompStatus.COMPLETE)
end


function this:exit()
	self.action=nil
end

return this;