local this ={}
local currentLange
this._pool={}

function this.init(lang)
	if currentLange~=lang then
		 currentLange = lang
		local initor = _G["Lang_"..lang]
		if initor then
			this._pool ={}
			initor.init()
		end
	end
end


function this.get(key)
	if this._pool then return this._pool[key] end
	return "";
end

function this.format(key,...)
	local arg ={...}
	local str =this.get(key)
	for i=1,#arg do
		local reg = i-1
		reg = "%{"..reg.."%}"
		str = string.gsub(str,reg,arg[i])
	end
	return str
end


function this.formatSize(size)
	if size <1024 then
		return string.format("%.0f B",size)
	elseif size<1048576 then
		return string.format("%.0f K",size/1024)
	end
	return string.format("%.0f M",size/1048576)
end


return this