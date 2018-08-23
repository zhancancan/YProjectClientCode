local channelItem = {}
-- function item.createItem() return item end
function channelItem.draw(_,cell,data)
	cell:SetString("cb",data.name)
	cell:SetClickTrigger("cb")
	cell:SetBool("cb",data.inGeneral)
end

return channelItem	