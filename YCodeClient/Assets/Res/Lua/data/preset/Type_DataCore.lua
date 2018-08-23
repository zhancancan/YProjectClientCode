local class 	= require "Class"
local this  	= class("Type_DataCore")

function this:ctor( obj )
	for k, v in pairs(obj) do
		rawset(self,k,v)
	end
end
return this