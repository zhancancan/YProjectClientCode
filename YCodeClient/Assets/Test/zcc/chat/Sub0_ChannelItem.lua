local channelItem = {}

function channelItem.draw(_,cell,data)
	cell:SetString("txt",data.name)
	cell:SetActive("redPoint",data.unread>0)
end 

function channelItem.onInit(_,cell)
	cell:SetClickTrigger("img")
end

return channelItem;