local this={}
local med
local sub_0
local sub_1
local sub_2
local app 			= require "AppCenter"
local TextUtils 	= require "TextUtils"
local ns 			= require "CSNamespace"
local native 		= ns["NativeManager"]
local NativeMethod 	= ns["NativeMethod"]



local function onLoginInfoReply(c)
	app.loginInfo = c
	if c.type == "Test" then
		sub_1:Show(c)
	else
		sub_2:Show(c)
	end
end

local function onLoginTypesReply(c)
	local types = TextUtils.split(c.LoginTypes)
	app.supportLoginTypes  = types
	if #types == 1 then
		native.Send({method = NativeMethod.REQUEST_LOGIN, type =types[1]} ,onLoginInfoReply)
	else
		sub_0:Show(types)
	end
end

function this.show()
	local req = {}
	req.method = NativeMethod.REQUEST_LOGIN_TYPES
	req.LoginTypes = app.configInfo.LoginTypes
	native.Send(req, onLoginTypesReply)
end

function this.hide( )
	-- body
end
function this.initView(_,m)
	med=m
	sub_0 = med:GetChildByName("sub_0")
	sub_1 = med:GetChildByName("sub_1")
	sub_2 = med:GetChildByName("sub_2")
	--	
	-- this:initLisenter()
end

-- function this:initLisenter()
-- 	sub_0:AddListener("pane",this.onSub_0_click)
-- end

----on event
-- function this:onSub_0_click(env)
-- 	local data = env.data.name
-- 	native.Send({method = NativeMethod.REQUEST_LOGIN, type =data} ,onLoginInfoReply)
	
-- end

return this