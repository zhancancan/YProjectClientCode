local this={}
function this.show()
	local ds ={}
	for i=1,16 do
		ds[i] = {id=i,name="hellowItem_"..i}
	end
	this.pane:SetList(ds)
end

function this.hide()
	if(this.pane~=nil) then
		this.pane:SetList(nil)
	end
end

function this.initView(med)
	this.pane = med:GetUI("pane")
end
function this.OnDestory()

end

return this
