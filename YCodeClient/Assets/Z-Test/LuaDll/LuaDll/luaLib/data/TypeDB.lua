local this={}
local log  = require "Logger"

function this.selectOne(tableName, condition, predict)
    local tb = this[tableName]
    if tb then return tb:selectOne(condition, predict) end
    log.error(string.format("table %s not found" ,tableName))
    return nil
end

function this.selectList(tableName, condition, predict)
   local tb = this[tableName]
    if tb then return tb:selectList(condition, predict) end
    log.error(string.format("table %s not found" ,tableName))
    return nil
end

function this.toArray(tableName)
	local tb = this[tableName]
    if tb then return tb:toArray() end
    log.error(string.format("table %s not found" ,tableName))
    return nil
end
function this.getTable(table)
    return this[table]
end
return this
