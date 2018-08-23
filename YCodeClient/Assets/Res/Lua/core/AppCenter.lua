local this={}
local ns 		= require "CSNamespace"
this.localData	= ns["LocalData"].instance

this.playerId="-1"

--the info object from native sdk
this.sdkInfo=nil

--this info object from getconfig,@see LoginAct_GetConfig.lua
this.configInfo=nil

this.loginInfo=nil

return this