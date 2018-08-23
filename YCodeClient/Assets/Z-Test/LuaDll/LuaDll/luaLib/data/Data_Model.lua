local ns 		= require "CSNameSpace"
local LuaEntity	= ns["LuaEntity"]
local class 	= require "Class"
local base		= require "Data"
local this		= class ("Data_Model",base)
local ml 		= {"body"}

local function isModelKey(arr, key)
	for i=1,#arr do
		if arr[i] == key then return true end
	end
	return false
end

function this:createEntity(ai)
	local en = LuaEntity.Create(self)
	rawset(self,"entity",en)
	local pos = self.position

	en:MoveTo(pos.x,pos.y,pos.z)

	en:CreateMachine(ai)

	local models = self.getModelList()
	for i=1 ,#models do
		local m = models[i]
		en:ChangeModel(m,self[m])
	end
	en.speed = self.speed
	en.orientation =self.orient
end


function this.getModelList()
	return ml
end


function this:onSelfChange(p,_,n)
	local en = self.entity
	if not en then return end
	if isModelKey(self:getModelList(),p) then
		en:ChangeModel(p,self[p])

	elseif p == "speed" then en.speed = n
	end
end

function this:dispose()
	if self._disposed then return end
	local en = rawget(self,"entity")
	if en then
		en:Dispose()
		rawset(self,"entity",nil)
	end
	self.super:dispose()
end


return this