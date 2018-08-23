local this = {msg={},funs={}}

local _call = {}
local manager 		= require "MessageManager"
local namaspace 	= require "CSNamespace"
local PanelManager  = namaspace ["PanelManager"]
local Logger 		= require "Logger"
local MessageChannel 	= require "MessageChannel"
local Channels		= MessageChannel.Channels

local GameTicker    = require "GameTicker"

function this.initMsg()

	local t = {}
	table.insert(t,{key="help",id=1,name="Show Cmd Orders",notMenu=1})
	table.insert(t,{key="scene",name="Open Scene List",func=_call.openSceneList})
	table.insert(t,{key="role",name="Show Role List",func=_call.showRoleList})
	table.insert(t,{key="lua",name="Open lua script input",func=_call.openLuaScriptTest})
	table.insert(t,{key="gold",name="Change Gold",notMenu=1,func=this.funs.gold})
	table.insert(t,{key="logout",name="Logout account",func=_call.logoutAccount})
	table.insert(t,{key="mission",name="Show mission panel",func=_call.showMissionOfZcc})
	----------
	this.msg = t
end

function this.getFormatMsgList()
	local t = {}
	for i,v in ipairs(this.msg) do
		if v.notMenu and v.notMenu > 0 then
		else
			table.insert(t,v)
		end
	end
	return t
end

function this.holdFunc(msg)
	if msg.func and type(msg.func) == "function" then
		msg.func()
	end
end

--------------------------FUNCS
----input cmd +funcName
--open Menu
function this.funs.open()
	PanelManager.Close("ChatPanel_social_subb")
	PanelManager.Open("PromptPanel_cmdList")
end
function this.funs.gold( pk,pv )
	pv = tonumber(pv) or 500
	local str = "Gold"
	if pk == "gold" then
		str = "Money"
	elseif pk == "crystal" then
		str = "Crystal"
	end
	local s = (pv>0) and "+" or ""
	str = str..s..pv
	Logger.log(str)
end

------call func
--show message in chat panel
--base
function this.showSystemMessage(debugMode,pk,pv)
	local k,v = pk,pv
	local content

	if pk == "help" then
		content = "CMD list:\nopen:Open CMD Menu\n"
		for i,item in pairs(this.msg) do
			-- print(item.key)
			content = content..item.key.." : "..item.name.."\n"
		end
	else
		if not k then
			content = "Do cmd: %s"
		else
			content = "Do CMD: %s  And Get Value: %s"
		end

		content = string.format(content,k,v)
	end



	local msg = {channel = Channels.SYSTEM,senderId = "-999",name = "DebugControl",
				msgString = content,msgType = 0,time = GameTicker.time}
	local m = setmetatable({},{__index=msg})
	m.senderName = "DebugLv."..debugMode
	manager.add(m)
end
-------
--match with cmd +key
---show scene
function _call.openSceneList(pk,pv)
	PanelManager.Close("PromptPanel_cmdList")
	PanelManager.Close("ChatPanel_social_subb")
	PanelManager.Open("PromptPanel_sceneList")

end

---show lua test script
function _call.openLuaScriptTest(pk,pv)
	PanelManager.Close("PromptPanel_cmdList")
	PanelManager.Close("ChatPanel_social_subb")
	PanelManager.Open("PromptPanel_luaScript")

end

--- show role
function _call.showRoleList(pk,pv)
	-- print("Open SwitchRolePanel_preloading")
	PanelManager.Close("ChatPanel_social_subb")
	PanelManager.Close("PromptPanel_cmdList")
	PanelManager.Open("SwitchRolePanel_preloading")
	-- PanelManager.Open("CreatRole_0_preloading")

end

function _call.logoutAccount()
	PanelManager.Close("ChatPanel_social_subb")
	local loginTools = require "LoginTools"
	loginTools.logOut()
end


function _call.showMissionOfZcc()
	PanelManager.Close("ChatPanel_social_subb")
	PanelManager.Open("TaskChapterPanel_Achievement")
end

---------------------------init
this.initMsg()

return this