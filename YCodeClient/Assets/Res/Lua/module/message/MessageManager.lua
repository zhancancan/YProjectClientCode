local class 			= require "Class"
local base 				= require "EventDispatcher"
local MessageChannel 	= require "MessageChannel"
local Channels 			= MessageChannel.Channels
local Array 			= require "Array"

local bit 				= require "bit"
local bitTools 			= require "bitTools"
local gameTicker 		= require "GameTicker"
local char 				= require "Data_Character"

local TableEvent 		= require "EventType".TableEvent


local this = class("MessageManager",base).new()

this.unread = 0
this.char = char

local _channelPool ={}
local _meta ={}
_meta._localSettingDirty=true
_meta._localSettingRead=false
_meta._settingChannels=Array()
_meta._generalMessage=Array()
_meta._maxGeneralMessage=0
_meta._simpleMessages=Array()
_meta._maxSimpleMessages=1
_meta.unread=0

local log = require "Logger"


function _meta.ensureLocalData()
	if _meta._localData == nil then
		local app=require "AppCenter"
		_meta._localData=app.localData
		_meta._localKey= (require"LocalDataKey").ChatSetting
	end
end


function _meta.getGeneralMessages()
	this.readLocalSetting()
	local arr = Array()
	local source = _meta._generalMessage
	for i=#source,1,-1 do
		local msg = source[i]
		local chn = _channelPool[msg.channel]
		if chn.inGeneral then arr :insert(msg)end
		if #arr > _meta._maxGeneralMessage then break end
	end
	arr:reverse()
	return arr
end

--获取当前channel的信息，是否包括需要全部显示(msgType=2)的信息
function _meta.getCurrectChannelMessage(channel,includeAllShow)
	if channel.channel == Channels.SYSTEM then
		return channel.messages
	end

	if includeAllShow then

		local tb = Array()
		local list = channel.messages
		for i=1,#list do
			local msg = list[i]
			tb:insert(msg)
		end
		local sysList = _channelPool[Channels.SYSTEM].messages
		for i=1,#sysList do
			local m =sysList[i]
			if m.msgType == 2 then
				tb:insert(m)
			end
		end

		table.sort( tb, function(a,b)
			if a.time < b.time then
				return true
			end
			if a.time >= b.time then
				return false
			end
		end )

		return tb
	else
		return channel.messages
	end
end

---------------------------

function this.add(msg)
	local c_id
	if msg.channel == Channels.PRIVATE then
		c_id = msg.senderId
		if not _channelPool[c_id] then _channelPool[c_id] = MessageChannel.new({name=msg.senderName,id=msg.senderId},true) end
	else
		c_id = msg.channel
	end
	local c = _channelPool[c_id]
	local deleted = c:add(msg)
	if deleted then _meta._generalMessage:remove(deleted) end
	_meta._generalMessage:insert(msg)

	_meta._simpleMessages:insert(msg)
	if #_meta._simpleMessages > _meta._maxSimpleMessages then _meta._simpleMessages:removeAt(1)end
	this.unread = _meta.unread +1
	this:dispatchEvent({type=TableEvent.UPDATE})
end

function this.get(channel)
	if type(channel)~="table" then
		log.error("chanel must be typeof table")
		return
	end

	local arr = Array()
	local list
	if channel.channel == Channels.GENERAL then list=_meta.getGeneralMessages()
	-- else list = channel.messages
	else list = _meta.getCurrectChannelMessage(channel,true)--todo ,params true->test
	end

	local prevTime=0
	if list then
		for i=1,#list do
			local m =list[i]
			if m.time-prevTime>60000 then
				arr:insert({time=m.time,isTime=true})
				prevTime=m.time
			end
			arr:insert(m)
		end
	end
	if channel.unread ~=0 then
		this.unread = this.unread - channel.unread
		channel.unread=0;
		this:dispatchEvent({type=TableEvent.UPDATE})
	end
	return arr
end
function this.createChannel(channel)
	_channelPool[channel.id] = MessageChannel.new(channel,false)
	if channel.canBeSelected == 1 then _meta._settingChannels:insert(_channelPool[channel.id])end
	if channel.id == Channels.GENERAL then _meta._maxGeneralMessage = channel.keepWord end
end
function this.getChannelByType(channelType)
	 return _channelPool[channelType]
end

function this.getChannels(canSendMessage)
	local a=Array()
	for _,v in pairs(_channelPool) do
		if canSendMessage then
			if v.canSendMessage == 1 then
				a:insert(v)
			end
		else
			a:insert(v) 
		end
	end
	return a
end

function this.getDefaultSelectedChannel(canSendMessage)
	for _,v in pairs(_channelPool) do
		if canSendMessage then
			if v.channel == Channels.CURRENT then
				return v
			end
		else
			if v.channel == Channels.GENERAL then
				return v
			end
		end
	end

	return nil
	
end

function this.getSettingChannels()
	return _meta._settingChannels
end

function this.getSimpleMessages()
	return _meta._simpleMessages
end

function this.getIsGeneral(pChannel)
	if type(pChannel)~= "table" then
		log.error("channel must be typeof table")
		return false
	end
	return pChannel.channel == Channels.GENERAL
end

function this.changeChannelAfterSend()
	this:dispatchEvent({type=TableEvent.UPDATE})
end

function this.readLocalSetting()
	if _meta._localSettingDirty then
		_meta.ensureLocalData()
		local mask=_meta._localData:ReadInt(_meta._localKey,-2)
		for i=1,#_meta._settingChannels do
			local v = _meta._settingChannels[i]
			if mask ==-2 then v.inGeneral=true else v.inGeneral=bitTools.getBool(mask,v.channel) end
		end
		_meta._localSettingDirty=false
	end
end
function this.writeLocalSetting()
	_meta.ensureLocalData()
	local mask=0
	for i=1,#_meta._settingChannels do
		local v = _meta._settingChannels[i]
		if v.inGeneral then
			local m= bit.lshift(1,v.channel)
			mask=bit.bor(mask,m)
		end
	end
	_meta._localData:WriteInt(_meta._localKey,mask)
	_meta._localSettingDirty=false
	this:dispatchEvent({type=TableEvent.UPDATE})
end

-- local iconReg 		= "%[e:(%w+)%]"
local iconReg 		= "%{e:(%w+)%}"
local iconContent 	= "<quad name=\"%s\" size=%d width=1 %s/>"

local typeDB 		= require "TypeDB"
local TypeClasses 	= require "TypeClasses"
local namesapce 	= require "CSNamespace"
local iconUtils 	= namesapce["IconUtils"]

local function replaceIcon(str,useSmall)
	local matches= string.gmatch(str,iconReg)
	local table = typeDB.getTable(TypeClasses.TYPE_EMOTE)
	local temp={}
	for m in matches do
		if not temp[m] then
			local emo = table:selectOne({id=tonumber(m)})
			if emo then
				-- local repl="%[e:"..m.."%]"
				local repl="%{e:"..m.."%}"
				if not useSmall or emo.isSmall==1 then
					local icon = emo.icon
					local w=0
					local _=0
					if useSmall then
						w=24
					else
						w,_=iconUtils.TryGetIconSize(icon,w,_)
					end
					local iconType = emo.isAnim==1 and "m" or "i"
					local ic = string.format(iconContent,icon,w,iconType)
					str= string.gsub(str,repl,ic)
				else
					local ic = string.format("[%s]",emo.key)
					str= string.gsub(str,repl,ic)
				end
			end
			temp[m]=true
		end
	end
	return str
end


function this.getDescription(msg, useSmall)
	return replaceIcon(msg.msgString,useSmall)
end

return this