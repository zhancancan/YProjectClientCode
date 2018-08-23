local namespace 	= require "CSNamespace"
local server 		= namespace["LuaServer"]


local factory = {}
function factory.setParser(a,b)
	factory.collectionProto=a
	factory.dataProto=b
end

_G["socketFactory"]  = factory;

local function setTo(proto,d)
	for k,v in pairs(d) do
		if type(v)=="table" then setTo(proto[k],v)
		else proto[k]=v
		end
	end
end


local function serialize(code,data)
	local proto=factory[code]()
	setTo(proto,data)
	local stream= proto:SerializeToString()
	local msg= factory.dataProto()
	msg.code=code
	msg.bytes=stream
	local c = factory.collectionProto()
	table.insert(c.datas,msg)
	local msgbytes=c:SerializeToString()
	return msgbytes
end


local function send(self,replace)
	-- local b = byteData()
	-- b.code = self.method
	-- b.buffer=_helper.serialize(self._content._method,self._data)
	-- server.Send(b,replace)
	local code = self._content._method
	local buffer = serialize(code,self._data)
	server.Send(code,buffer, replace)
end



local meta={}
meta.__newindex=function (t,k,v)
	if k == "method" then
		t._content._method=v
	else
		t._data[k]=v
	end
end

meta.__index=function (t,k)
	if k == "method" then return t._content._method end
	if k == "send" then return send end
	return t._data[k]
end

local function new(method)
	local o ={}
	o._data={}
	o._content={}
	o._content._method = method
	o=setmetatable(o,meta)
	return o
end



return new