local tq = require "TickerQueue"


local ns = require "CSNamespace"
local sys =  ns.SystemTime



local this={
	normalTicker=tq.new(0),
	secondTicker=tq.new(1000),
	time = 0,
	bias = 0
}


local pingTime		= 0
local clientTime	= 0

function this.updateNow(now)
	clientTime = now
	this.time = now + this.bias
	this.normalTicker:updateNow(this.time)
	this.secondTicker:updateNow(this.time)
end

function this.ping()
	pingTime = clientTime
end

function this.pingResponse(now)
	now = tonumber(now)
	local trans = (clientTime - pingTime)*0.5
	this.time = now + trans
	this.bias = this.time - clientTime
	sys.SetBias(this.bias)
end


return this