local Logger = require "Logger"

local function traceback(errors)
	--init result
	local result =""
	if errors then
		result=errors.."\n"
	end
	result =result.."stack traceback:\n"

	-- make result
	local level=2
	while true do
		local info =debug.getinfo(level,"Sln")
		-- end?
		if not info or (info.name and info.name == "xpcall") then
			break
		end
		--functions
		if info.what == "C" then
			result = result..string.format("		[C]: in function '%s'\n",info.name)
		elseif info.name then
			result = result..string.format("		[%s.lua:%d]:'%s'\n",info.short_src,info.currentline,info.name)
		elseif info.what == "main" then
			result = result..string.format("		[%s.lua:%d]: in main chunk\n",info.short_src,info.currentline)
			break;
		else
			result = result..string.format("		[%s:%d]:\n",info.short_src,info.currentline)
		end
		--next
		level = level+1
	end
	return result
end




local function  joint(...)
	local result = {}
	for _,t in ipairs({...})do
		if(type(t)=="table")then
			for k,v in pairs(t) do
				if type(k) =="number" then table.insert(result,v)
				else result[k]=v end
			end
		else
			table.insert(result,t)
		end
	end
	return result
end

local function sandbox_try(block)
	local try =block[1]
	local funcs = joint(block[2] or{},block[3] or{})
	local ok,errors = xpcall(try,traceback)
	if not ok then
		if funcs and funcs.catch then
			funcs.catch(errors)
		else
			Logger.log(errors)
		end
	end
	if funcs and funcs.finally then
		funcs.finally(ok,errors)
	end
	local a
	if ok then a=errors else a=nil end
	return  a
end

return sandbox_try