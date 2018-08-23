local emojItem = require "Sub1_EmojItem"

local pageItem = {}
function pageItem.draw(_,cell,data)
	cell:SetPaneFactory("pane",emojItem)
	cell:SetList("pane",data.emotes)
end

function pageItem.onRemove(_,cell)
	cell:SetList("pane",nil)
	cell:SetPaneFactory("pane",nil)
end

return pageItem