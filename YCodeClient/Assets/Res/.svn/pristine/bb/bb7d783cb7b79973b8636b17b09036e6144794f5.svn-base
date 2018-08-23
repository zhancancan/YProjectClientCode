local class  = require "Class"
local base   = require "HudCore"
local this = class("NpcHud",base)

function this:onEnter(c,d)
	self.hud =c;
	self.data = d

	c:SetString("txt",d.name..d.id)
	-- self.txt.text=d.name;

	self.data:addListener(self,self.onDataChanged)

	local f = d.hp / d.maxHp
	self.hud:SetFloat("bar",f)

end

function this:onExit( ... )
	self.data:removeListener(self,self.onDataChanged)
end

function this:onDataChanged(data,property,oldV,newV)
	-- print(string.format("property:%s",property))

	if property == "name" then
		self.hud:SetString("txt",newV)
	elseif property == "hp" then
		-- print(string.format("o:%s,n:%s|maxHp %s",oldV,newV,data.maxHp))
		local f = newV / self.data.maxHp
		self.hud:SetFloat("bar",f)
	end
end

return this;