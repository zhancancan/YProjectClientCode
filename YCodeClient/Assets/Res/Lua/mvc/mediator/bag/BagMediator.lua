local this={}
function this.show(obj)
	local t = this.med:GetChildByName("sub_0")
	Logger.log("show bag",obj,t)
end

function this.hide( obj )
	Logger.log("hide" , obj)
end

function this.initView(med )
	this.med = med;
	this.closeBnt=med:GetUI("bagObj_return/btn");
	this.closeBnt:AddListener(this.OnClick);
end

function this.OnClick()
	this.med:Hide();
	manager.Open("bagMeditoar_main",p)
end

return this;
