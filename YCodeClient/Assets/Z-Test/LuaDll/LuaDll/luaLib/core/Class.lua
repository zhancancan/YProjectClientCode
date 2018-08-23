local Logger 	= require "Logger"
local _class	= {}
local _counter	= {}


local function class_index(t,k)
	local meta = getmetatable(t)
	local arr = meta.inherit
	for i = 1, #arr do
		local src = arr[i]
		local v
		local src_type = type (src)
		if src_type == "function" then
			v = src(t,k)
		else
			v = src[k]
		end

		if v then
			return v
		end
	end
end

local function class_tostring(t)
	local meta = getmetatable(t)
	local class_type =  meta.class
	return string.format("%s[0x%X]",class_type._cname , meta.guid)
end



local function add_metatable(meta,src)
	if not src then return end
	local arr = meta.inherit

	local type_src = type(src)
	if type_src == "table" then
		if src.__index then
			if type(src.__index) == "table" then
				arr[2] = src.__index
			elseif type(src.__index) == "function" then
				arr[2] = src.__index
			end
		else
			arr[2] = src
		end

		if src.__newindex then
			if meta.__newindex then
				Logger.warn(string.format("[%s] __newindex replaced",meta.class._cname))
			end
			meta.__newindex = src.__newindex
		end


	elseif type_src == "function" then
			arr[2] = src
	else
		Logger.error("not support metatable type")
	end

end

local function create_meta(obj,class_type)
	local arr ={}
	arr[1] = _class[class_type]
	local guid = _counter[class_type]
	_counter[class_type] = guid + 1

	local meta ={
			__tostring 	  = class_tostring,
			__index 	  = class_index,
			class 		  = class_type,
			inherit 	  = arr,
			guid 		  = guid,
			add_metatable = add_metatable
		}

	local src = getmetatable(obj)
	add_metatable(meta, src)

	return meta
end


local function class(className,super)
	local class_type={_cname=className}
	class_type.ctor=false;
	class_type.super= super;
	class_type.new = function ( ... )
		local  obj = {}
		do
			local create ;
			create = function (c, ... )
				if c.super then
					create(c.super,...)
				end
				if(c.ctor )then
					c.ctor(obj,...)
				end
			end
			create(class_type,...)
		end ---end of do while

		obj.super = super

		setmetatable(obj, create_meta(obj, class_type))
		return obj;
	end

	local vtbl ={}
	_class[class_type] = vtbl;
	setmetatable(
		class_type,
		{
			__newindex=function(_,k,v)
				vtbl[k]=v
			end,
			__index = function (_,k)
				return vtbl[k] -- so the static function works now
			end
		}
	)

	if super then
		setmetatable(vtbl,{__index=
			function (_,k)
				local ret = _class[super][k]
				vtbl[k] = ret
				return ret
			end
		})
	end
	_counter[class_type] = 0
	return class_type
end
return class