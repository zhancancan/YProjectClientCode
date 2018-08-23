local this={}
function this.show()

end

function this.hide()
end

function this.initView(med)
	this.med = med;
	this.close = med:GetUI("closeBtn");
	this.close:AddListener(this.onClick);
end
function this.OnDestory()
	if this.close then
		this.close:removeListener(this.onClick)
		this.close=nil
	end
end

function this.onClick()
	this.med:Hide();
end

return this;
