local class=require "Class"
local this =class("Data_CmdDestroyObject",require "Data_Action")

-- - is self
function this:execute()
	local ids = self.ids;
	if not ids then return end
	for i=1 ,# ids do
		local id = ids[i]
		if id ~= self.char.id then self.table:remove(id,true) end
	end
end

return this