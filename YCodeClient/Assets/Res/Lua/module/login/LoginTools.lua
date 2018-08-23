local this ={}

local lang 			= require "LangManager"
local user			= require "Data_Character"
local dialog 		= require "Dialog"

local ns 			= require "CSNamespace"
local panel  		= ns["PanelManager"]
local native 		= ns["NativeManager"]
local server 		= ns["LuaServer"]
local NativeMethod 	= ns["NativeMethod"]

local loginNet  	= require "LoginNet"
local dbase 		= (require "DataManager").dbase

local log 			= require "Logger"

function this.startLogin()
	panel.Open("LoginPanel_login")
end
native.AddHandler(NativeMethod.START_LOGIN , this.startLogin)

local function nativeLogout()
	native.Send({method = NativeMethod.CALL_LOGOUT})
	log.log("native logout")
end

local function doLoginOut(r)
	if not r then return end
	this.doLogout()
end

function this.logOut()
	dialog.ask(lang.get("Login.askLogout"),doLoginOut)
end

function this.askLogout(func)
	dialog.ask(lang.get("Login.askLogout"), func)
end

function this.doLogout()
	server.Close()
	nativeLogout()
end

function this.login()
	dbase:clear(true)
	-- user.id=-2
	-- panel.Close("LoginPanel_login")



	-- panel.Open("CreatRolePanel_preloading")
end

function this.goCreateRole()

	panel.Open("CreatRolePanel_preloading")
end


return this