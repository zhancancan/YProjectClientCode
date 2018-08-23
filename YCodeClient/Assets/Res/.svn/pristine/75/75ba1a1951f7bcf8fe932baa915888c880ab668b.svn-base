local class = require "Class"
local this  = class("MessageChannel")
local Array = require "Array"

local Channels=	{
					GENERAL = 1,
					WORLD 	= 2,
					CURRENT = 3,
					UNION	= 4,
					PRIVATE = 5,
					HELP	= 6,
					SYSTEM	= 7
				}


this.Channels = Channels

local manager

local privateMaxWord=0
function this:ctor(obj,isPlayer)
	if not manager then
		manager= require "MessageManager"
	end
	self.messages=Array()
	self.isPlayer=isPlayer
	if isPlayer then
		self._maxWord=privateMaxWord
		self.player=obj
		self.channel = Channels.PRIVATE
		self.preset=manager.getChannelByType(Channels.PRIVATE).preset
		self.name=obj.name
		self.id=obj.id
	else
		self._maxWord=obj.keepWord
		self.preset=obj
		if obj.id==Channels.PRIVATE then privateMaxWord=obj.keepWord end
		self.channel = obj.id
		self.id = obj.id
	end
	self.unread=0
	setmetatable(self,self.preset)
end


function this:add(msg)
	self.messages:insert(msg)
	local d
	if #self.messages>self._maxWord then
		d = self.messages:removeAt(1)
	end
	self.unread = self.unread +1
	return d
end

return this