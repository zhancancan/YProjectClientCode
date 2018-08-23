local class 		= require "Class"
local base			= require "UnsaveProxy"
local this 			= class("Proxy_Scene",base)
local namespace 	= require "CSNamespace"
local sceneManager 	= namespace["SceneEntityManager"]

function this:ctor(code)
	self._proxycenter=require"ProxyCenter"
	self._proxycenter.sceneProtoCode = code
end


function this:pushData(buffer)
	local data=self:_parseData(buffer)
	if not data then return end
	self._proxycenter.setStatus(1)
	self._proxycenter.setScene(data.id)

	print("scene init start")
	sceneManager.AddListener(self,self.onSceneInited)
	sceneManager.ChangeScene(data)
end



function this:onSceneInited()
	self._proxycenter.setStatus(0)
	self._proxycenter.flush();
end

return this