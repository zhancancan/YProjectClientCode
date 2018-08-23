
require "TypeDataInitor"
local class 		= require "Class"
local base			= require "UnsaveProxy"
local this 			= class("Proxy_Message",base)
local manager 		= require "MessageManager"
local log 			= require "Logger"

function this:ctor()
	local db = require "TypeDB"
	local TypeClasses = require "TypeClasses"
	local cs = db.toArray(TypeClasses.TYPE_CHATCHANNEL)
	for i=1,#cs do
		manager.createChannel(cs[i])
	end
end

function this:pushData(buffer)
	local data=self:_parseData(buffer)
	if not data then return end


	manager.add(data)
	log.log("senderId:",data.senderId)
	log.log("msgString:",data.msgString)
	log.log("msgType:",data.msgType)

end

return this