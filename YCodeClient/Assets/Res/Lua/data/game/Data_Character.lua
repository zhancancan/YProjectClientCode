local class 			= require "Class"
local DataClasses		= require "DataClasses"
local PropertyGroup		= require "PropertyGroup"
local log  				= require"Logger"
local this 				= class("Data_Character",require "Data")
local dbase 			= require "DataManager".dbase

local instance



local function getter(t,k)
	if k == "hero" then
		local hid = rawget(t,"heroId")
		local h = dbase:selectOne(DataClasses.DATA_USERHERO,hid)
		if h  then
			rawset (t , "hero",h)
		else
			log.error(string.format("Data_Character no hero find at %s",hid))
		end
		return h

	end

end



local meta = {__index = getter}

function this:ctor()
	if instance then
		log.error("singleton error: Data_Character can create only once")
		return
	end
	local pg = PropertyGroup.new(self,"property")
	self._data.property = pg
	setmetatable(self._data , meta)
end


function this:onSelfChange(p)
	if p == "heroId" then
		self.hero = nil
		self:invoke("hero")
	end
end


instance = this.new()
return instance