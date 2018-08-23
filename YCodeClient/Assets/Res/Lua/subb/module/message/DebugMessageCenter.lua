local this = {debugMode = 1}

-- local debugMode = 1

---------
local _debugWords = require "DebugWords"

function this.debugHoldMessage(inputContent)
	local debugMode = this.debugMode
	if debugMode <= 0 then return 1 end

	if #inputContent <= 0 then
		return -1
	end
	-- print("debug holdMessage")
	local upper = inputContent
	-- local upper = string.upper(inputContent)

	local m = "cmd%s(%w+)%((%d*)%)"
	local pk,pv = string.match(upper,m)

	if not pk then
		m = "cmd%s+(%w+)%s*(%d*)"
		pk = string.match(upper,m)
	end
	-- print(pk)
	-- print(pv)
	if pk then
		local matched = false
		local fn = _debugWords.funs[pk]
		if fn ~= nil then
			if type(fn) == "function" then
				fn(pv)
				_debugWords.showSystemMessage(debugMode,pk,pv)
				matched = true
			end
		end
		if not matched then
			for _,item in pairs(_debugWords.msg) do
				if item.key == pk then
					if item.func and type(item.func) == "function" then
						item.func(pk,pv)
					end
					_debugWords.showSystemMessage(debugMode,pk,pv)
					matched = true
					break
				end
			end
		end
		if matched then return 0 end
	end



	return 1
end

----socket message
local center = require "ProxyCenter"
-- local SocketForm = require "SocketForm"
-- local CommandClasses = require "CommandClasses"
-- function this.socketSend()
-- 	local form = SocketForm()
-- 	form.method = CommandClasses.ChatRequest
-- 	form.channel = _currentChannel.channel
-- 	form.msgType=0
-- 	form.description = txt
-- 	form:send()
-- end

---socket debug recv
--debug change scene
function this.debugChangeScene(data)
	if this.debugMode <= 0 then return 1 end
	if not data then return 1 end

	local place = {}

	-- local filter = {
	-- 	"name",
	-- 	"_lpk",
	-- 	"maxInstance",
	-- 	"onPick",
	-- 	"userThreshold",
	-- }

	-- for k,v in pairs(data) do
	-- 	local matched = false
	-- 	for _,sv in pairs(filter) do
	-- 		if k == sv then
	-- 			matched = true
	-- 		end
	-- 	end
	-- 	if not matched then
	-- 		place[k] = v
	-- 		print(k,v)
	-- 	end
	-- end
	place.id = data.id
	local mapName = string.gsub(place.id,"place_","") 

	if not place.placeFile then
		-- place.placeFile = "Assets/Res/Data/Output/Place/"..place.id..".bytes"
		place.placeFile = place.id
	end

	place.sceneType = 0
	-- place.id = 0

	local pos = {x=40,y=27,z=52}--xinshou

	if mapName == "senlin" then
		pos = {x=58,y=20,z=49}
	elseif mapName == "shamo" then
		pos = {x=210,y=18.0,z=11.5}
	end


	local player ={
		id="4",
		name="haha",
		-- body="Assets/Res/Arts/Role/Hero/lvbu/lvbu_0_sp/RP_lvbu_0.prefab",
		body="RP_libai_0",
		position=pos,
		speed=6
	}

	-- for k,v in pairs(player.position) do
	-- 	print(k,v)
	-- end

	local character ={
		id="12",
		heroId = "2",
		isRemove=false,
		level = 1,
		exp = 2,
		loginTime = os.time(),
		lastLoginTime = os.time(),
		lastLogoutTime = os.time(),
	}
	local t=center.serialize(
		{
			{code=60006,data=place},
			{code=60004,data=player},
			--{code=90002,data=character}
		}
	)
	center.parse({buffer=t},-1)

end

-----------------
function this.init()
	this.initTestLoginData()
end

function this.initTestLoginData()

end


this.init()
return this