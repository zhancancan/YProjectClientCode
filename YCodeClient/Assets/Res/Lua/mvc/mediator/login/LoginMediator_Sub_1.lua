local app 			= require "AppCenter"
local ns 			= require "CSNamespace"
local native 		= ns["NativeManager"]
local NativeMethod 	= ns["NativeMethod"]

local this={}
local med
local sub_2

function this.show()

end
function this.hide()

end


local function onLoginInfoReply(c)
	app.loginInfo = c
	sub_2:Show()
end

local function onButtonClick(_)
	local input = med:GetString("txt_1")
	if #input <=2 then return end
	native.Send({method = NativeMethod.REQUEST_LOGIN,account=input,type = "Test"}, onLoginInfoReply)
end

function this.initView(_,m)
	med=m
	med:AddListener("btn",onButtonClick)
	sub_2 = med:GetSibling("sub_2")
end

return this