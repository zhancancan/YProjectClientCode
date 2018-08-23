local function sandbox_catch(block)
	return {catch=block[1]}
end
return sandbox_catch
