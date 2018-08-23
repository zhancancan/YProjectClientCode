local app = _G["AppCenter"]
require "TimeFormat"
local TimeFormat =_G["TimeFormat"]

local messageItem = {}

function messageItem.pickPrefab(_,data,_)
	if data.isTime then return 0
	elseif data.senderId==app.playerId and data.msgType==0 then return 1
	elseif data.senderId~=app.playerId and data.msgType==0 then return 2
	elseif data.senderId~=app.playerId and data.msgType==1 then return 3
	else return 4
	end
end

function messageItem.draw(_,cell,data)
	if not manager then manager = require "MessageManager" end
	if data.isTime then
		cell:SetString("txt",TimeFormat.format(data.time*0.001,TimeFormat.hh_mm_ss))
	else
		cell:SetString("txt",data.senderName)
		cell:SetString("richTxt",manager.getDescription(data,false))
	end
end 

return messageItem;