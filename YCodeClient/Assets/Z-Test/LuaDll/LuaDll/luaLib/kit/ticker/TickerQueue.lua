local class = require"Class"
local log 	= require "Logger"
local try 	= require "try"

local this 	= class("TickerQueue")



local function get( arr,watcher,method)
	for i=1,#arr do
		local w = arr[i]
		if  w.watcher == watcher and w.method == method then return w end
	end
	return nil
end

local function clearCancel(arr)
	for i=#arr, 1 , -1 do
		if arr[i]._cancel then
			table.remove(arr,i)
		end
	end
end

function this:ctor(interval)
	self._queue={}
	self._updating=false;
	self._needPurge=false;
	self._interval=interval;
	self._lastUpdate=0;
end

function this:add(watcher,method)
	method =  method or watcher.updateNow
	if type(method)~="function"then
		log.error("ticker,method must be function ")
		return
	end
	local con = self._queue
	local a = get(watcher,method)
	if a then a._cancel =false
	else
		a = {watcher = watcher,method = method, _cancel =false}
		table.insert(con , a)
	end
end

function this:remove(watcher, method)
	method = method and method or watcher.updateNow
	if type(method)~="function"then
		log.error("ticker,method must be function ")
		return
	end

	local con = self._queue
	for i =1 ,#con do
		local u = con[i]
		if u.watcher == watcher and u.method == method then
			u._cancel = true
		end
	end

	if not self._updating then
		clearCancel(con)
	else
		self._needPurge =true
	end
end

function this:updateNow(now)
	if now-self._lastUpdate<self._interval then return end
	self._lastUpdate = now
	local con = self._queue
	for i=1 ,# con do
		local u = con [i]
		local w = u.watcher
		local m = u.method
		if not u._cancel and w and m then
			try{function () m(w,now)end}
		else
			u._cancel=true
			self._needPurge=true
		end
	end
	self._updating=false
	if self._needPurge then
		clearCancel(con)
	end
	self._needPurge=false
end

function this:clear()
	self._queue:clear()
end
return this