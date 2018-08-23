local this={};
function this.createItem()
	return this;
end
function this.pickPrefab( data,index)
	return 0
end

function this:draw(cell,data)
	cell:SetString("txt_0","2b")
	cell:SetString("txt_1","2b+")
	cell:SetString("txt_2","2b++")
	local i = cell.index%2;
	cell:SetActive("img_1",cell.selected);
end


function this:onInit(cell,data)
	-- print("onInit",cell.index)
	cell:SetClickTrigger("img_0");
	local v= Vector3.New(100);
	local t=cell:FindChild("img_0"):TweenMoveTo(v);
end

function this:onRemove(cell)
	-- print("onRemove",cell.index)
	cell:SetClickTrigger(nil);
end

function this:onDrag(cell,data,status)
	if(status == DragStatus_End) then
		Logger.log("onDrag",cell,status==DragStatus.End);
	end
end

function this:onDrop(cell,data,dropper)
	Logger.log("drop",dropper.draggee.data==cell);
end
function this:onClick(cell,data)
	Logger.log("click",cell,data);
end
function this:onSelectChange(cell,data,selected)
	Logger.log("selectChange",selected);
	local i=cell.index;
	if(i%2==0)then
		cell:SetActive("img_1",selected)
	else
		cell:SetActive("img_2",selected)
	end
end




return this;
