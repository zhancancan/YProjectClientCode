local this={}
local namespace 	= require "CSNamespace"
local PanelManger 	= namespace["PanelManager"]


local med
local dialog
local sourceTitle

function this.open(obj)
	PanelManger.open("DialogPanel_sys",obj);
end

function this.show(_,obj)
	dialog=obj
	med:SetString("txt",obj.content)
	if dialog.title then
		med:SetString("bg",obj.title)
	else
		med:SetString("bg",sourceTitle)
	end
end
function this.hide()
	dialog=nil
end


local function onButtonClick(n)
	if not dialog or dialog.callBack == nil then return end
	if n== "btn_0"then
		dialog.callBack(true)
	elseif n == "btn_1" then
		dialog.callBack(false)
	end
	med:Hide()
end

function this.initView(_,m)
	med = m
	med:AddListener("btn_0", onButtonClick)
	med:AddListener("btn_1",onButtonClick)
	sourceTitle = med:GetString("bg")
end
return this