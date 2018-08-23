local this={}
local med

local app 			= require "AppCenter"
local ns 			= require "CSNamespace"
local native 		= ns["NativeManager"]
local http 			= ns["LuaHttp"]

local item = {}


local function getTimeToNumber(time)
	return tonumber(time)
end

local function utcToTime(utc)
	local t = getTimeToNumber(utc)
	return os.date("%Y-%m-%d-%H-%M-%S",t)
end



function item:draw(cell,data)
	local str
	-- if data.state == 2 then

	-- end
	str = utcToTime(data.openTime)
	if getTimeToNumber(data.openTime) == -1 then
		str = "Open"--to do
	end
	cell:SetString("txt_0",str)
	cell:SetString("txt_1",data.name)
end

function item:onInit(cell)
    cell:SetClickTrigger("img_0")
	-- cell:SetClickTrigger("img")
end

function this.show( )
	local serverList = app.servers

	med:SetList("pane",serverList)
end
local function onItemClick(_,evt)
	local data = evt.data
	if getTimeToNumber(data.openTime) == -1 then
		--server is open ,hide and back to prev
		med:Hide()
	else
		--server is not open,show stop begin and end time
		if not data.stopBeginTime and not data.stopEndTime then
			med:Hide()
		else
			--todo str and title
			local str = string.format("from {0} to {1}",data.stopBeginTime,data.stopEndTime)
			local title = "server is not open"
			dialog.ask(str, function(r)
				
			end,title)
		end
	end
end
function this.hide( )
	med:SetList("pane",nil)
end

function this.initView(_,m)
	med=m
	med:SetPaneFactory("pane",item)
	med:AddListener("pane",onItemClick)
end


return this