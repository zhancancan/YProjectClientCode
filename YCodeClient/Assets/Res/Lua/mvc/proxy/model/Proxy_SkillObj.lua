local class 		= require "Class"
local base			= require "TableProxy"
local this 			= class("Proxy_SkillObj",base)
local namespace 	= require "CSNamespace"
local db 			= require "TypeDB"
local classes 		= require "TypeClasses"
local log 			= require "Logger"


function this:pushData(buffer)
	log.log("Proxy_SkillObj pushData")
	local isNew,data=self:_push(buffer)
	if not data then return end
	if isNew then
		-- log:log(data.skillId)
		-- local model =namespace["Data_ModelLua"].new
		-- local o = model(data)
		-- data.model = o


	end

end

return this