local Array = require "Array"
local class = require "Class"
local base =require"EventDispatcher"

local BagEvent={}
BagEvent.INSERT="bagevent:insert"
BagEvent.REMOVE="bagevent:remove"
BagEvent.UPDATE="bagevent:update"

local this = class("BagContainer",base)


this.BagEvent = BagEvent


local _cache = {}
local _meta={}
function _meta.addEvent(self,type,props)
	self._events.insert({type=type,data=props})
end
function _meta.dispatch(self)
	local len = self._events
	if len >0 then
		for i=1,len do self.dispatchEvent(self._events[i])end
		self._events:clear();
		self:dispatchEvent({type = BagEvent.UPDATE})
	end
end

function _meta.releaseEvent()
	for _,slot in pairs(_cache) do
		for _,v in pairs(slot) do
			_meta.dispatch(v)
		end
	end
end

function this.getContainer(host,bagType)
	if not _cache[host] then _cache[host]={} end
	local hc= _cache[host]
	if not hc[bagType] then	hc[bagType]=this.new(bagType) end
	return hc[bagType]
end
function this.removeHost(host)
	_cache[host]=nil
end

function this:ctor(bagType)
	self._bagType=bagType
	self._children = Array()
	self._events=Array()
end

function this:add(prop)
	if prop._container then prop._container:remove(prop) end
	local children = self._children
	if children.indexOf(prop)==-1 then
		children.insert(prop)
		_meta.addEvent(self,BagEvent.INSERT,prop)
	end
end

function this:remove(prop)
	if(self._children.remove(prop)) then
		_meta.addEvent(self,BagEvent.REMOVE,prop)
	end
end

local pcenter=require"ProxyCenter"
pcenter.registerPostAction(_meta.releaseEvent)

return this


