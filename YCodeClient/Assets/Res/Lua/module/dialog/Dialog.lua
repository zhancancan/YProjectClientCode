local namespace		= require "CSNamespace"
local panel  		= namespace["PanelManager"]
local NativeMethod 	= namespace["NativeMethod"]
local native 		= namespace["NativeManager"]
local this 			= {}

this.TYPE_TWOBTN	= 0
this.TYPE_ONEBTN	= 1

function this.ask(content, callBack, title)
	local o = {}
	o.content = content
	o.callBack = callBack
	o.title = title
	panel.Open("DialogPanel_sys",o)
end

function this.askByType(content,type,callBack,title)
	local o = {}
	o.content = content
	o.type = type
	o.callBack = callBack
	o.title = title
	panel.Open("CustomDialogPanel_sys",o)
end

function this.askNative(title,content,call)
	local o = {}
	o.method = NativeMethod.REQUEST_DIALOG
	o.title = title
	o.content =content
	native.Send(o, function (jo)call(jo.result) end)
end

return this