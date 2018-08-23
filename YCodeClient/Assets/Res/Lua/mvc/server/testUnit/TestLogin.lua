local this ={}
local center 			= require "ProxyCenter"
local SocketForm 		= require "SocketForm"
local CommandClasses 	= require "CommandClasses"
local namespace 		= require "CSNamespace"
local server 			= namespace["LuaServer"]
local Logger 			= require "Logger"
local app 				= require "AppCenter"
local GameTicker 		= require "GameTicker"

function this.run()

	Logger.log("do test login")
	server.Connect("192.168.0.218",17001)

	local c = app.loginInfo
	local form = SocketForm()
	form.method =CommandClasses.UserLogin
	form.type = 0
	form.code= c.code
	form.zoneId=1
	form:send()





	do return end

	local place = {id="1",sceneType=0,placeFile="xinshou"}
	local player ={
		id="12",
		name="haha",
		body="RP_lvbu_0",
		-- position={x=25,y=7,z=52},
		position={x=38.8,y=27.5,z=54.6},
		speed=6
	}
	local npc ={--test npc
		id="22",
		position={x=43.8,y=27.5,z=54.6},
		level = 2,
		hp = 100,
		maxHp = 125,
		forcast = {x=43.8,y=27.5,z=54.6},
		npcId = "ghosts",
		isRemove = false,
	}
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
			{code=90002,data=character},
			{code=60012,data=npc}
		}
	)
	center.parse({buffer=t},-1)




	local manager = require "MessageManager"
	local Channels= require "MessageChannel".Channels
	local str="hello[e:1]world[e:3]test"
	-- str = "hello"
	local obj={channel=Channels.WORLD,senderId="12",name="hehehe",msgString=str,msgType=0,time=GameTicker.time}
	for  i=1,40 do
		local m =setmetatable({},{__index=obj})
		m.senderName="haha_"..i
		if math.random()>0.5 then m.senderId ="12" else m.senderId="124" end
		-- if math.random()>0.5 then m.msgString="abcdefg" end
		manager.add(m)
	end
end

return this