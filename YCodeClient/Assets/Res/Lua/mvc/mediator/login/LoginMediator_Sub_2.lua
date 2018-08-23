local app 			= require "AppCenter"
local ns 			= require "CSNamespace"
local http 			= ns["LuaHttp"]
local log           = require "Logger"

local GameTicker	= require "GameTicker"



local this={}
local med

local sub_3

local selectedServer

local isEnterLock = true

local function updateServerInfo()
	local id
	local name
	if selectedServer then
		id = selectedServer.id
		name = selectedServer.name
	else
		-- if not
		if app.myServer and #app.myServer == 1 then
			for k,v in pairs(app.servers) do
				if v.id == app.myServer[1] then
					id = v.id
					name = v.name
					break
				end
			end
		else
			id = app.servers[1].id
			name = app.servers[1].name
		end
	end

	med:SetString("txt_0",id)
	med:SetString("txt_1",name)
	app.loginInfo.serverId = id
	log.log("app.loginInfo.serverId:"..app.loginInfo.serverId)
end


local function onServerListLoaded(status,c)
	if status ==1 then
		GameTicker.pingResponse(c.time)
		app.servers = c.servers
		app.myServer = c.myServers
		app.gateway = c.gateway
		-- find current;
		updateServerInfo()
		isEnterLock = false
	else

	end

end

local function onLoginReply(status ,c)
	if status==1 then
		GameTicker.ping()
		-- app.loginInfo = c--todo!!!!!!
		local url = app.configInfo.ServerListApi
		url = string.gsub(url,"{a}",app.loginInfo.account)
		http.Send(url, onServerListLoaded)
		log.log("server url:",url)
	else
		-- handle load err
	end
end


function this.show()
	local url = app.configInfo.LoginApi
	local info = app.loginInfo
	isEnterLock = true
	url = string.gsub(url,"{p}",info.type)
	url = string.gsub(url,"{c}",info.code)
	url = string.gsub(url,"{t}",GameTicker.time)
	log.log(url)
	http.Send(url,onLoginReply)
end

local function onButtonClick(n)
	if isEnterLock then return end
	if n == "btn_0" then
		 -- do enter game
		-- local loginTools = require "LoginTools"
		-- loginTools.login()
		local loginNet = require "LoginNet"
		loginNet.startUserLogin()
	end
	if n == "btn_1" then sub_3:Show() end

end
local function onServerSelected(_, evt)
	selectedServer = evt.data
	app.myServer = {selectedServer.id}
	updateServerInfo()
end

function this.resume()
	updateServerInfo()
end


function this.initView(_,m)
	med=m
	med:AddListener("btn_0",onButtonClick)
	med:AddListener("btn_1",onButtonClick)
	sub_3 = med:GetSibling("sub_3")
	sub_3:AddListener("pane",onServerSelected)
end



return this