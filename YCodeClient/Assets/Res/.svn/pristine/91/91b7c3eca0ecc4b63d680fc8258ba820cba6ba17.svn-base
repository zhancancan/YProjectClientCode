local this ={}
local center 			= require "ProxyCenter"
local SocketForm 		= require "SocketForm"
local CommandClasses 	= require "CommandClasses"
local namespace 		= require "CSNamespace"
local server 			= namespace["LuaServer"]
local Logger 			= require "Logger"
local app 				= require "AppCenter"
local panel  			= namespace["PanelManager"]


local GameTicker 		= require "GameTicker"


function this.connectServer( )
	if not app.gateway then
		Logger.error("Error!! no server info!")
		return
	end

	local url = app.gateway.host
	local port = tonumber(app.gateway.port)
	Logger.log("Connect server:"..url.." : "..port)
	server.Connect(url,port)
end

function this.startUserLogin()
	this.connectServer()
	GameTicker.ping()
	local c = app.loginInfo
	local serverId = app.loginInfo.serverId
	Logger.log("startUserLogin code:" .. c.code.."|server:"..serverId)
	local form = SocketForm()
	form.method = CommandClasses.UserLogin
	form.type = 0--0=dev,1=online------todo
	form.code= c.code
	form.zoneId=tonumber(serverId)
	form:send()
end

function this.startUserRegister( name,heroId )
	Logger.log("startUserRegister name:"..name.."|"..heroId)
	GameTicker.ping()
	local form = SocketForm()
	form.method = CommandClasses.UserRegister
	form.name = name
	form.heroId = heroId
	form:send()
	-- panel.Close("LoginPanel_login")
end

function this.disconnectServer()
	server.Close()
end
return this