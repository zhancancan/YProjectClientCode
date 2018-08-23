local function sandbox_finally(block)
    return {finally = block[1]}
end
return sandbox_finally