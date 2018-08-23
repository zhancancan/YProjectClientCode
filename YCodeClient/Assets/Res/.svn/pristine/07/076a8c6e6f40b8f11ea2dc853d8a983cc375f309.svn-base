local this={}
local med

local sub_1
local sub_2
local pane

local app 			= require "AppCenter"
local TextUtils 	= require "TextUtils"
local ns 			= require "CSNamespace"
local native 		= ns["NativeManager"]
local NativeMethod 	= ns["NativeMethod"]

local channelData
local item = {}

local function onLoginInfoReply(c)
	app.loginInfo = c
	if c.type == "Test" then
		sub_1:Show(c)
	else
		sub_2:Show(c)
	end
end
----------
function item:draw(cell,data)
	cell:SetString("txt",data)
end

function item:onInit(cell)
	cell:SetClickTrigger("icon")
end

--------mediator
---------

function this:show(pTypes)
	med:SetList("pane",pTypes)
end
function this:hide( )
	med:SetList("pane",nil)
end
function this.initView(_,m)
	med=m

	sub_1 = med:GetSibling("sub_1")
	sub_2 = med:GetSibling("sub_2")

	med:AddListener("pane",this.onPaneClick)
	med:SetPaneFactory("pane",item)
end

function this.onPaneClick(_,env)
	local data = env.data
	native.Send({method = NativeMethod.REQUEST_LOGIN, type =data} ,onLoginInfoReply)
end
return this