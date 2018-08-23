local class = require "Class"
local log 	= require "Logger"
local try 	= require "try"
local this  = class("PropertyDispacher")

local function get( arr,watcher,method)
	for i=1,#arr do
		local w = arr[i]
		if w.watcher == watcher and w.method == method then	return w end
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

function this:ctor(host)
	self._listeners={}
	self._invoking=false
	self._host = host
end

function this:addListener(watcher,method)
	if type(watcher)=="userdata" then method = watcher.InvokeChange end
	if not watcher then
		log.error("Data:addListener()-> watcher can not be nil")
		return
	end
	if not method then
		log.error("Data:addListener()-> method can not be nil")
		return
	end
	local con = self._listeners
	if watcher and method then
		local a = get(con,watcher,method)
		if a then
			a._cancel=false
		else
			a = {watcher = watcher,method = method, _cancel = false, isnative=type(method) == "userdata"}
			table.insert(con , a)
		end
	end
end


function this:removeListener(watcher,method)
	if not watcher then
		log.error("Data:addListener()-> watcher can not be nil")
		return
	end

	local con = self._listeners
	if not method then
		for i=1,#con do
			local w = con[i]
			if w.watcher == watcher then w._cancel = true end
		end
	else
		for i=1,#con do
			local w = con[i]
			if w.watcher == watcher and w.method == method then w._cancel = true end
		end
	end
	if not self._invoking then
		clearCancel(con)
	end
end

function this:removeAllListener()
	local con = self._listeners
	if self._invoking then
		for i=1 ,#con do
			con[i]._cancel = true
		end
	else
		self._listeners={}
	end
end

function this:invoke(property,oldValue,newValue)
	local con =self._listeners
	self._invoking=true
	for i=1,#con do
		local w = con[i]
		if not w._cancel and w.method then
			try{
				function()
					if w.isnative then
						w.method(w.watcher,property,oldValue,newValue)
					else
						w.method(w.watcher,self._host,property,oldValue,newValue)
					end
				end
			}
		else
			w._cancel = true
		end
	end
	self._invoking=false
	clearCancel(con)
end

return this