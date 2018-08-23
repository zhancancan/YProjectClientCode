local class 			= require "Class"
local base 				= require "EventDispatcher"
local SocketForm 		= require "SocketForm"
local CommandClasses 	= require "CommandClasses"

local user				= require "Data_Character"
local DataClasses		= require "DataClasses"

local dbase 	  		= require "DataManager".dbase

local GameTicker 		= require "GameTicker"
local log				= require "Logger"
local Vector3 			= Vector3

local table

local this = class("CharacterUseSkill",base).new()




local function getEntity()
	if not table then table = dbase:getTable(DataClasses.DATA_SCENEOBJ) end
	local p = table:selectOne(user.id)
	return p and p.entity or nil
end




local function callServer(skill, position, orient, forcast)
	local form = SocketForm()
	form.method = CommandClasses.CmdCast
	form.id = skill
	form.position = position
	form.forcast = forcast
	form.orient = orient
	form:send()
end

function this.tryCast(skill)
	local cd = tonumber(skill.cooldown)
	local now = GameTicker.time
	if now < cd then
		-- to do notify player cd
		return
	end

	local e = getEntity()
	if not e then
		log.warn("no player found")
		return
	end

	local pos = e.position
	local orient = e.orientation
	local forcast = Vector3.New(pos.x,pos.y,pos.z)
	skill.fireTime = now
	skill.cooldown = now  + skill.setToCooldown * 1000

	local data={
		stateName="FireSkill",
		fx = skill.fx,
		position = pos,
		orient  = orient,
		forcast = forcast
	}

	e:ChangeState(data,0)
	callServer(skill.id, pos, orient, forcast)
end

return this