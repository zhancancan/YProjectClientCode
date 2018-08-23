local this={}
local lang = require "LangManager"
function this.onInit(_,med)
	med:SetFloat("bar",0)
	med:SetString("txt","")
end

function this.onUpdate(_,med,current,total)
	local p = current/total
	med:SetFloat("bar",p)
	p =string.format("%.0f%%",p*100)
	p = lang.format("Preload.sceneLoad",p)
	med:SetString("txt",p)
end

return this