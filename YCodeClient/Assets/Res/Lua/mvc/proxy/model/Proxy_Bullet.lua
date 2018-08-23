local class 		= require "Class"
local base			= require "TableProxy"
local this 			= class("Proxy_Bullet",base)
local namespace 	= require "CSNamespace"
local db 			= require "TypeDB"
local classes 		= require "TypeClasses"
local log 			= require "Logger"


function this:pushData(buffer)
	log.log("Proxy_Bullet pushData")
	local isNew,data=self:_push(buffer)
	if not data then return end
	if isNew then
		log.error("**todo**")


	end

end

return this