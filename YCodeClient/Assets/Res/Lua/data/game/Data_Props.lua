local class 		= require "Class"
local bagManager 	= require "BagContainer"
local character 	= require "Data_Character"

local this =class("Data_Props",require "Data")


local _meta ={}
function _meta.onBagChange(self,bagType)
	local con = bagManager.getContainer(character.getInstance(),bagType)
	con.add(self)
end

function this:onSelfChange(property,_,newVal)
	if property == "bagType"then
		_meta.onBagChange(self,newVal)
	end
end

return this