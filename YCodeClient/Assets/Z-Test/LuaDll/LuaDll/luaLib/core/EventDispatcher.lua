local class = require"Class"
local this =class("EventDispatcher")
local log = require "Logger"
local try = require "try"
local GameEvent=require "GameEvent"


local function get( arr,eventType,watcher,method)
	for i=1,#arr do
		local w = arr[i]
		if w.eventType == eventType and w.watcher == watcher and w.method == method then	return w end
	end
	return nil
end

local function clearCancel(arr)
	for i=#arr, 1 , -1 do
		if arr[i]._cancel then
			table.remove(arr, i)
		end
	end
end

function this:ctor(owner)
	self._listeners = {}
	self._invoking=false
	self._owner = owner and owner or self
end

function this:addListener(eventType,watcher, method)
	if type(eventType) ~= "string" or #eventType == 0 then
		log.error(string.format("EventDispatcher:addEventListener()- invalid eventType = %s",eventType))
		return self
	end
	if type(watcher) ~= "table" then
		log.error(string.format("EventDispatcher:addEventListener()- invalid watcher = %s" ,watcher))
		return self
	end

	if type(method) ~= "function" then
		log.error(string.format("EventDispatcher:addEventListener()- invalid method = %s",type(method)))
		return self
	end


	eventType =string.upper(eventType)

	local con = self._listeners

	local a = get(con,eventType,watcher,method)
	if a then
		a._cancel =false
	else
	   a = {eventType = eventType,watcher = watcher, method = method, _cancel=false}
	   table.insert(con , a)
	end

	return self
end

-- remove all the listener use eventType;
function this:removeListenerByEvent(eventType)
	if type(eventType) ~= "string" or #eventType == 0 then
		log.error("EventDispatcher:removeListenerByEvent()- invalid eventType")
		return self
	end

	eventType =string.upper(eventType)
	local con = self._listeners
	for i=1,#con do
		local u = con[i]
		if u.eventType == eventType then u._cancel=true end
	end

	if not self._invoking then
		clearCancel(con)
	end
	return self
end

function this:removeListener(eventType,watcher,method)
	if type(eventType) ~= "string" or #eventType == 0 then
		log.error("EventDispatcher:removeListener()- invalid eventType")
		return self
	end
	if type(watcher) ~= "table" then
		log.error("EventDispatcher:removeListener()- invalid watcher")
		return self
	end

	local con = self._listeners
	eventType=string.upper(eventType)
	if method then
		for i=1,#con do
			local u = con[i]
			if u.eventType == eventType and u.watcher ==watcher and u.method == method then u._cancel=true end
		end
	else
		for i=1,#con do
			local u = con[i]
			if u.eventType == eventType and u.watcher ==watcher then u._cancel=true end
		end
	end

	if not self._invoking then
		clearCancel(con)
	end

	return self
end

function this:removeListenerByWatcher(watcher)
	if not watcher then return end
	local con = self._listeners
	for i=1,# con  do
		local u = con [i]
		if u.watcher == watcher then u._cancel =true end
	end
	if not self._invoking then
		clearCancel(con)
	end

	return self
end

function this:hasEventListener(eventType)
	if not eventType then return false end
	eventType = string.upper(eventType)
	local con = self._listeners
	for i=1,# con  do
		local u = con [i]
		if u.eventType == eventType then return true end
	end
	return false
end

function this:removeAllListeners()
	if self._invoking then
		local con = self._listeners
		for i=1,# con  do
			local u = con [i]
			u._cancel = true
		end
	else
		self._listeners={}
	end
end


function this:dispatchEvent(event)
	if type (event) == "string" then event = GameEvent.new(event)end
	if not event or type(event.type) ~= "string" or #event.type ==1 then
		log.error("EventDispatcher:dispatchEvent()- invalid event")
		return self
	end

	local et = string.upper(event.type)
	local con = self._listeners

	self._invoking=true
	event._stopped=false
	event.target =self._owner

	for i=1,#con do
		local w = con[i]
		if w.eventType ==et then
			if not w._cancel and w.watcher and w.method then
				try{ function() w.method(w.watcher,event)end}
			else
				w._cancel = true
			end
		end
		if event._stopped then break end
	end
	self._invoking=false

	clearCancel(con)

	return self
end

return this