local this={}
local med

local class 			= require "Class"
local TypeClasses		= require "TypeClasses"
local typeDB			= require "TypeDB"
local ns 				= require "CSNamespace"
local panelManager  	= ns.PanelManager

local error				= require "ErrorCenter"
local log 				= require "Logger"
local dialog 			= require "Dialog"

local lang 				= require "LangManager"

local data_questState   = require "Data_UserQuestState"

--Mediator
local function  onErrorReceived(err)
	log.log("create med", err.code, err.msg)
end

function this.show()
	error.addListener(onErrorReceived,this)
end

function this.hide()
	error.removeListener(onErrorReceived)
end

function this.initView(_,meditor)
	med = meditor
end

return this