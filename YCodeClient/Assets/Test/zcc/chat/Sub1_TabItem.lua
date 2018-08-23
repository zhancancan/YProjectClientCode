local tabItem = {}
function tabItem.draw(_,cell,data)
	cell:SetString("icon",data.icon)
end

function tabItem.onInit(_,cell)
	cell:SetClickTrigger("icon")
end

return tabItem