local class 		= require "Class"
local base  		= require "Data"
local this  		= class ("Data_UserHero",base)
local DataClasses  	= require "DataClasses"

function this:getSkills()
	return self.dbase:selectList(DataClasses.DATA_SKILLOBJ,function (sk) return sk.heroId == self.id end)
end


return this