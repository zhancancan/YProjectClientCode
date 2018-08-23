local class 	= require "Class"
local base 		= require "Data_Action"
local this 		= class("Data_CmdCast",base)
local tools 	= require "CommandUtils"
local tdb		= require "TypeDB"
local TypeClasses 	= require "TypeClasses"
local gameticker	= require "GameTicker"
local log 			= require "Logger"

local function replaceValue(name,changeValue)
	-- changeValue[2] = "+100"--test
	-- name = "[double]{0}{1}"--test
	local text = name

	local reStr = "%{(%d+)%}"
	local matches = string.gmatch(text,reStr)
	for md in matches do
		local rep = table.concat( {"%{",md,"%}"})
		text = string.gsub(text,rep,tostring(changeValue[md + 1]))
	end

	return text
end


function this:ctor()
	self.time = 0
end
-- - is self
function this:execute(_)

	local sType = self.type
	-- sType = tostring(math.random(7,11))--test

	local typeData = tdb.selectOne(TypeClasses.TYPE_BATTLEINFO,sType)
	if not typeData then
		log.error(string.format("no typeData! type=%s",self.type))
		return
	end

	local d = self.table:selectOne(self.id)
	if d and d.entity then
		self.position = tools.toVector3(self.position)

		local max = typeData.maxCount
		local tName = typeData.name
		local cv = self.changeValue
		
		local text = replaceValue(tName,cv)

		self.text = text


		local prefab = typeData.body
		local index = math.random(1,max)
		prefab = string.format(prefab,index)
		-- prefab = "EP_criticaldamage_01"
		self.prefab = prefab

		self.stateName = "FloatText"
		d.entity:ChangeState(self,1)

	end

end



return this