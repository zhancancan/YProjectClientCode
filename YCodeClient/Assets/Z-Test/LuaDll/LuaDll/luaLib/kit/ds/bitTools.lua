-- using the native bit pack


-- bit.rol
-- bit.rshift
-- bit.ror
-- bit.bswap
-- bit.bxor
-- bit.bor
-- bit.arshift
-- bit.bnot
-- bit.tobit
-- bit.lshift
-- bit.tohex
-- bit.band

local this={}
local bit = require "bit"

function this.getBool(mask,prt)
	local m = bit.lshift(1,prt)
	return bit.band(mask,m)~=0
end

return this

















-- local this={}
-- _G["bit"]=this
-- local _meta={}

-- function _meta.__base(lhs,rhs,op)
-- 	if lhs<rhs then lhs,rhs = rhs ,lhs end
-- 	local r= 0
-- 	local shift=1
-- 	while lhs~=0 do
-- 		local x = lhs % 2 -- go the each right
-- 		local y = rhs % 2
-- 		r = shift * op(x,y) + r
-- 		shift = shift * 2
-- 		lhs = math.modf(lhs*0.5)-- rshift
-- 		rhs = math.modf(rhs*0.5)
-- 	end
-- 	return r
-- end
-- function _meta.__opand(l,r)
-- 	return (l==1 and r==1 ) and 1 or 0 	-- &
-- end
-- function _meta.__opor(l,r)
-- 	return (l==1 or r==1 ) and 1 or 0	-- |
-- end
-- function _meta.__opxor(l,r)
-- 	return (l+r)==1 and 1 or 0			-- ^
-- end

-- -- xor == ^ operator
-- function this.opXor(x,y)
-- 	return _meta.__base(x,y,_meta.__opxor)
-- end

-- -- and == & operator
-- function this.opAnd(x,y)
-- 	return _meta.__base(x,y,_meta.__opand)
-- end
-- -- or == | operator
-- function this.opOr(x,y)
-- 	return _meta.__base(x,y,_meta.__opor)
-- end

-- -- not = ~ operate
-- function this.opNot(x)
-- 	return x>0 and -(x+1)or -x-1
-- end

-- -- left = << operate
-- function this.lshift(a,n)
-- 	return math.floor(a * (2^n))
-- end
-- -- right = >> operate
-- function this.rshift(a,n)
-- 	return math.floor(a/(2^n))
-- end

-- function this.getBool(mask,prt)
-- 	local t = this.lshift(1,prt)
-- 	return this.opAnd(t,mask)~=0
-- end

-- return this