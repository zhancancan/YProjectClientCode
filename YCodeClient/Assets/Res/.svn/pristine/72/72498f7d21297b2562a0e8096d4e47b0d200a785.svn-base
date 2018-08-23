local class 	= require "Class"
local base 		= require "HudCore"
local this = class("HostHud",base);
local log = require "Logger"

function this:onEnter(c,d)
	self.hud =c;
	self.data = d

	c:SetString("txt",d.name)


	self.data:addListener(self,self.onDataChanged)

	local f = d.hp / d.maxHp
	self.hud:SetFloat("bar",f)

end

function this:onExit( ... )
	self.data:removeListener(self,self.onDataChanged)
end


function this:onDataChanged(_,property,oldV,newV)
	if property == "name" then
		self.hud:SetString("txt",newV)
	elseif property == "hp" then
		local f = newV / self.data.maxHp
		self.hud:SetFloat("bar",f)
	end
end

return this