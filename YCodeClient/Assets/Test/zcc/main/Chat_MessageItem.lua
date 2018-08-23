local messageItem = {}
local format= "[%s] [%s] %s"
local TextUtils

function messageItem.draw(_,cell,data)
	if not manager then manager = require "MessageManager" end
	if not TextUtils then TextUtils =require "TextUtils" end
	local channel = manager.getChannelByType(data.channel)
	local t0 = TextUtils.html(channel.name,channel.fontColor)
	local t1 = data.senderName
	local t2 = manager.getDescription(data,true)
	local str = string.format(format,t0,t1,t2)
	cell:SetString("txt",str)
end

return messageItem