local class 		 = require "Class"
local base			 = require "UnsaveProxy"
local this 			 = class("Proxy_Ping",base)
local SocketForm 	 = require "SocketForm"
local CommandClasses = require "CommandClasses"
local GameTicker 	 = require "GameTicker"

function this.pushData(_)
	GameTicker.ping()
	local form = SocketForm(CommandClasses.Ping)
	form.msg = ""
	form:send()
end


return this