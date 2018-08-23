local this={}
local _meta={}
local med
local heroPane

local TypeClasses	= require "TypeClasses"
local TypeDB		= require "TypeDB"
local ns 			= require "CSNamespace"
local panelManager  		= ns["PanelManager"]
local native 		= ns.NativeManager

local LoginTools 	= require "LoginTools"
local loginNet 		= require "loginNet"

local Logger 		= require "Logger"
local dialog 		= require "Dialog"

local error			= require "ErrorCenter"

local GameTicker    = require "GameTicker"

local lang 			= require "LangManager"

local Array 		= require "Array"
local trait 		= Array()
local lastName 		= Array()
local firstName 	= Array()
local heroId
local currentName	= nil
local nameRegxhalf  = "<*>*%s*[.]*"

--Grid
local heroItem={}
function heroItem.draw(_,cell,data)
	cell:SetString("icon",data.icon)
end

function heroItem.onInit(_,cell)
	cell:SetClickTrigger("icon")
end

local function updateHeroModel(hero)
	med:PlayPortrait(hero.portrait,hero.showFx)
end

local function updateHeroInfo(startHero)
	med:SetString("obj_0/icon",startHero.icon)
	med:SetString("obj_0/txt",startHero.name)
	med:SetString("obj_0/area",startHero.desc)
	heroId = startHero.heroId
end

local function saveRandomNameMsg()
	local nameArr=TypeDB.toArray(TypeClasses.TYPE_RANDOMNAME)
	for k,v in pairs(nameArr) do
		if v.position == 0 then
			trait:insert(v.word)
		elseif v.position ==1 then
			lastName:insert(v.word)
		else
			firstName:insert(v.word)
		end
	end
end

local function randomName()
	local randomName
	while true do
		local traitTemp = trait[math.random(#trait)]
		local lastNameTemp = lastName[math.random(#lastName)]
		local firstNameTemp = firstName[math.random(#firstName)]
		randomName = table.concat({traitTemp,lastNameTemp,firstNameTemp})
		if randomName ~= currentName then break end
	end
	currentName = randomName
	med:SetString("obj_2/txt",currentName)
end

local function checkName(str)
	for k in string.gmatch(str, nameRegxhalf) do
		if #k ~= 0 then
			return false;
		end
	end
	local strTemp = str:gsub('　',"")
	return #strTemp==#str
end

local function onBtnClick(btn)
	if btn=="btn_0" then
		randomName()
	elseif btn=="btn_1" then
		local name=med:GetString("obj_2/txt")
		if #name~=0 then
			local isRight = checkName(name)
			if isRight then
				Logger.log(string.format("%s","name format right!"))
				loginNet.startUserRegister(name,tostring(heroId))
			else
				Logger.log(string.format("%s","name format error!"))
				dialog.askByType(lang.get("Login.illegal_character"),dialog.TYPE_ONEBTN)
			end
		else
			Logger.log(string.format("%s","please enter name"))
			dialog.askByType(lang.get("Login.please_inputName"),dialog.TYPE_ONEBTN)
		end
	end
end

local function onHeroItemClick(_,evt)
	updateHeroModel(evt.data.hero)
	updateHeroInfo(evt.data)
end

local function onBackBtnClick()
	LoginTools.askLogout(
		function (r)
			if r then
				LoginTools.doLogout();
				med.Hide(med);
			end
		end
	)
end

local function  onErrorReceived(err)
	Logger.log("create med", err.code, err.msg)
	if err.code == 30003 then
		dialog.askByType(lang.get("Login.name_isUsed"),dialog.TYPE_ONEBTN)
	end
end

--Mediator
function this.show()
	panelManager.Close("LoginPanel_login")

	local startHeroArr=TypeDB.toArray(TypeClasses.TYPE_STARTHERO)
	med:SetList("obj_1/pane",startHeroArr)
	med:SetInt("obj_1/pane",0)

	currentName = med:GetString("obj_2/txt")
	local time = tonumber(tostring(GameTicker.time):reverse():sub(4, 12))
	math.randomseed(time)
	randomName()

	heroPane.selectedData=startHeroArr[1]
	updateHeroModel(startHeroArr[1].hero)
	updateHeroInfo(startHeroArr[1])
	error.addListener(onErrorReceived,this)
end

function this.hide()
	med:SetList("obj_1/pane",nil)
	error.removeListener(onErrorReceived)
end

function this.initView(_,meditor)
	med=meditor
	med:AddListener("obj_2/btn_0",onBtnClick)
	med:AddListener("obj_2/btn_1",onBtnClick)
	med:AddListener("obj_3/btn",onBackBtnClick)

	med:SetPaneFactory("obj_1/pane",heroItem)
	heroPane=med:AddListener("obj_1/pane",onHeroItemClick)
	saveRandomNameMsg()
end

return this