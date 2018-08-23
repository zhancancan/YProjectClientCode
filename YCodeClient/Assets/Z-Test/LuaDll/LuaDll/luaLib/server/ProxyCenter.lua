local Array = require "Array"
local log  	= require "Logger"


local CACHE_MODE_THROUGH	= 0
local CACHE_MODE_CACHE		= 1
local CACHE_MODE_IGNORE		= 2


local this = {}
this.connected 		= false
this.sceneProtoCode = -1


local proxyList={}
local postActions 	= Array()
local cache 		= Array()
local status 		= 0

local sceneId
local collectionObj;
local msgData
local msgCollection



local function distribute(code,buffer)
	local proxy = proxyList[code]
	proxy:pushData(buffer)
	return proxy
end

local function parseDatas(datas)
	local pushProxy={}
	for i=1,#datas do

		local data = datas[i]
		local code = data.code

		local p = proxyList[code]
		local sid = rawget(data,"sceneId")

		local canparse = not sceneId or  #sid==0 or sid == sceneId or code == this.sceneProtoCode

		if p then
			if canparse then
				if status==0  or code == this.sceneProtoCode then
					distribute(code,data.bytes)
					pushProxy[p]=true
				else

					local mode = p.cacheMode

					if mode == CACHE_MODE_THROUGH then
						distribute(code,data.bytes)
						pushProxy[p]=true

					elseif mode == CACHE_MODE_CACHE then
						cache:insert(datas[i])

					elseif mode == CACHE_MODE_IGNORE then
						log.log("proxy igonred",code)

					else
						log.error(string.format("proxy[%d] use not supported cache mode",code))
					end
				end-- end of status switch
			else
				log.warn(string.format("proxy[%d] sid =%s igonred by sceneId %s",code,sid, data.sceneId))
			end -- end of scene check

		else
			log.error(string.format("no proxy found at %d ",code))
		end -- end of proxy check

	end-- end of for loops

	for k,_ in pairs(pushProxy) do
		k:afterPush()
	end
	for i=1,#postActions do
		postActions[i]()
	end
 end


function this.add(_,code,proxy)
	proxyList[code] = proxy
	return proxy
end

function this.registerPostAction(action)
	if  type(action) ~= "function" then
		log.error("ProxyCenter:registerPostAction()-> action must be typeof function")
		return
	end
	if postActions:indexOf(action)==-1 then
		postActions:insert(action)
	end
end

function this.setScene(sid)
	sceneId = sid
end

function this.setStatus(st)
	status = st
end

function this.parse(buffer)
	-- local buffer = message.buffer
	collectionObj:ParseFromString(buffer)
	local sid = collectionObj.sceneId
	if not sid then sid = "" end

	local datas = collectionObj.datas
	for i=1 ,#datas do
		rawset(datas[i],"sceneId",sid)
	end

	parseDatas(collectionObj.datas);
end


function this.setParser(msgC, msgD)
	msgCollection = msgC
	msgData = msgD
	collectionObj = msgCollection()
end

function this.flush()
	status=0
	if cache then
		parseDatas(cache)
	end
	cache:clear()
end

function this.setConnected( ctd )
	this.connected =ctd
	print("connted",ctd)
end




local function setTo(proto,d)
	for k,v in pairs(d) do
		if type(v)=="table" then setTo(proto[k],v)
		else proto[k]=v
		end
	end
end




--serialize the list of data to byte[],the list struct should be
-- [{code=1,data={}},...,{code=n data={}}] -- for test only
function this.serialize(datas)
	local c = msgCollection()
	-- c.method=-1
	-- c.form=-1
	for i=1,#datas do
		local code = datas[i].code
		local data= datas[i].data
		local proxy = this._proxyList[code]
		local parser=proxy._parser();
		setTo(parser,data)
		local stream= parser:SerializeToString()
		local msg= msgData();
		msg.code=code;
		msg.bytes=stream
		table.insert(c.datas,msg)
	end
	local msgbytes=c:SerializeToString()
	return msgbytes
end





return this