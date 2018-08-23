local user 			= require "Data_Character"
local dbase 		= require "DataManager" .dbase

local manager 		= require "CharacterUseSkill"

local this={}
local med
local class 		= require "Class"
local item 			= class("ControlMediator_SkillItem")
local GameTicker 	= require "GameTicker"
local TableEvent 	= require "EventType".TableEvent
local DataClasses 	= require "DataClasses"

local log 			= require "Logger"

function item.createItem()
	return item.new()
end

function item.pickPrefab(pane,data,index)
	return index == 0 and 0 or 1
end

function item:draw(cell,data)
	-- cell:SetString("txt",data.name)
	cell:SetClickTrigger("icon")
	cell:SetString("icon",data.icon)
end

function item:onInit(cell,data,index)
	self.data = data
	self.cell = cell
	GameTicker.normalTicker:add(self)
	self.isShowCD = true
	self.index = index
end

function item:updateNow(now)
	local cell = self.cell
	local data = self.data
	local start = data.fireTime
	local due = data.cooldown
	local cd = due - start
	local left = due - now

	local p = left / cd -- percent
	p = p < 0 and 0 or p
	p = p > 1 and 1 or p

	cell:SetFloat("cover",p)

	if left < 1000 then
		cell:SetString("txt","")
	else
		local sec = tostring(math.floor(left * 0.001))
		cell:SetString("txt",sec)
	end

end


function item:onRemove()
	GameTicker.normalTicker:remove(self)
end


function item:onClick(_,data)
	-- print(string.format("ControlMediator:item_onclick: %s",data))
	manager.tryCast(data)
end

local function resetPane()
	local h = user.hero
	if h then
		med:SetList("pane",h:getSkills())
	else
		med:SetList("pane",nil)
	end
end

local function onTableUpdate()
	resetPane()
end
local function onHeroUpdate(p)
	if p == "heroId" then
		resetPane()
	end
end

function this:show()
	dbase:getTable(DataClasses.DATA_SKILLOBJ):addListener(TableEvent.UPDATE,self, onTableUpdate)
	dbase:getTable(DataClasses.DATA_USERHERO):addListener(TableEvent.UPDATE,self, onTableUpdate)
	user:addListener(self,onHeroUpdate)
	resetPane()
end

function this:hide()
	med:SetList("pane",nil)
	dbase:getTable(DataClasses.DATA_SKILLOBJ):removeListener(TableEvent.UPDATE,self, onTableUpdate)
	dbase:getTable(DataClasses.DATA_USERHERO):removeListener(TableEvent.UPDATE,self, onTableUpdate)
	user:removeListener(self,onHeroUpdate)
end

function this:initView(mediator)
	med = mediator
	med:SetPaneFactory("pane",item)
	-- med:AddListener("pane",onClick)
end

return this;
