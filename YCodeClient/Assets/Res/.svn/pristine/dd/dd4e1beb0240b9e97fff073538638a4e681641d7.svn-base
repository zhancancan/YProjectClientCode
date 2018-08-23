local dataManager 	= require "DataManager"
local DataClasses 	= require "DataClasses"
local ns 			= require "CSNamespace"
local CompStatus 	= ns.CompStatus


local this={};

function this.new()
	return this
end


function this:enter(c)
	local db = dataManager.dbase
	db:clearTable(DataClasses.DATA_PLAYER,true)
	c:SetStatus(CompStatus.COMPLETE)
end


return this