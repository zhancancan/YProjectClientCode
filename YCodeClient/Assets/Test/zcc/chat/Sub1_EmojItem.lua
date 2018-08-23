local emoItem = {}
function emoItem.draw(_,cell,data)
	cell:SetString("icon",data.icon)
end

function emoItem.onInit(_,cell)
	cell:SetClickTrigger("icon")
end

return emoItem