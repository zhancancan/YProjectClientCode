local this={}
local namespace 	= require "CSNamespace"
local PanelManger 	= namespace["PanelManager"]

local med
local subMed
local dialog
local sourceTitle

function this.open(obj)
	PanelManger.Open("CustomDialogPanel_sys",obj);
end

function this.show(_,obj)
	dialog=obj
	if dialog.type == nil then dialog.type = 0 end
	local subName = string.format("sub_%s",dialog.type)
	subMed = med:GetChildByName(subName)
	subMed:Show()
	local txtName = string.format("sub_%s/txt",dialog.type)
	med:SetString(txtName,dialog.content)
	if dialog.title then
		-- med:SetString("bg",obj.title)
	else
		-- med:SetString("bg",sourceTitle)
	end
end
function this.hide()
	dialog=nil
end

local function onButtonClick(n)
	if not dialog then return end
	if dialog.callBack ~= nil then
		if dialog.type == 0 then
			if n== "btn_0"then
				dialog.callBack(true)
			elseif n == "btn_1" then
				dialog.callBack(false)
			end
		end
	end
	subMed:Hide()
	med:Hide()
end

function this.initView(_,m)
	med = m
	med:AddListener("sub_0/btn_0", onButtonClick)
	med:AddListener("sub_0/btn_1",onButtonClick)
	med:AddListener("sub_1/btn",onButtonClick)
	-- sourceTitle = med:GetString("bg")
end

return this