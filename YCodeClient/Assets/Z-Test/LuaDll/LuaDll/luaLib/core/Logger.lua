local print = print

local LOG 	= 1
local WARN  = 2
local ERROR = 3


local this={}

local mode = 3


local function getinfo(c)
	table.insert(c,"\r\n")
	local level=3
	while true do
		local  info =  debug.getinfo(level,"Sln")
		if not info or (info.name and info.name == "xpcall") then	break end
		table.insert(c,string.format("[%s.lua:%d]:%s\r\n",info.short_src,info.currentline,info.name))
		level=level+1
	end
end

local function packTable(tb,c)
	table.insert(c,string.format("[Table:%s]",tb))
end

local function printLog(logType,...)
	local c = {}
	if logType == WARN then
		table.insert(c,"[Warn]")
	elseif logType == ERROR then
		table.insert(c,"[Error]")
	end
	local arg = {...}
	for i=1,#arg do
		if type(arg[i])=="table" then packTable(arg[i],c)
		else table.insert(c,string.format("%s ",arg[i]))
		end
	end
	getinfo(c)
	print(table.concat(c))
end


local function packTablefullInfo(tb,c)
	table.insert(c,"\r\n")
	table.insert(c,string.format("%s\r\n",tb))
	for k,v in pairs(tb) do
		table.insert(c,string.format("%s = %s\r\n",k,v))
	end
end


local function printDatailLog(logType,...)
	local c = {}
	if logType == WARN then
		table.insert(c,"[Warn]")
	elseif logType == ERROR then
		table.insert(c,"[Error]")
	end
	local arg = {...}
	for i=1,#arg do
		if type(arg[i])=="table" then packTablefullInfo(arg[i],c)
		else table.insert(c,string.format("%s ",arg[i]))
		end
	end
	getinfo(c)
	print(table.concat(c))
end

function this.log(...)
	if mode< LOG then return end
	printLog(this.logLevel,...)
end

function this.detail(...)
	if mode<LOG then return end
	printDatailLog(this.logLevel,...)
end


function this.warn(...)
	if mode < WARN then return end
	printLog(this.warnLevel,...)
end

function this.error(...)
	if mode < ERROR then return end
	printLog(this.errorLevel,...)
end

function this.check(condition,fail)
	if mode < ERROR then return end
	if not fail then fail = "check error" end
	if not condition then
		printLog(this.errorLevel,fail)
	end
end

return this