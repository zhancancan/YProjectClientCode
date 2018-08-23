local this = {}

local med
local pane
local input

local log = require "Logger"
local _meta = {}

function _meta.DoLuaScript(luaString)
	if #luaString <= 0 then
		return
	end
	log.log("===DoLuaScript Start===")

	local namespace = require "CSNamespace"
	local PanelManager = namespace["PanelManager"]
	PanelManager.DoLuaScript(luaString)

	log.log("===DoLuaScript End===")
end

function _meta.onPaneClick()
	-- body
end

function _meta.onButtonClick()

	_meta.DoLuaScript(input.text)
end

function this:initView(mediator)
	med = mediator
	med:SetString("txt_0","Lua Script")
	med:AddListener("btn",_meta.onButtonClick)
	input = med:GetUI("txt_1")
	med:SetString("btn","Run Script")
end


return this