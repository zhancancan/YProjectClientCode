local Logger = require "Logger"
function Main()
	Logger.log("logic start")
end

function OnLevelWasLoaded(level)
	collectgarbage("collect")
	Time.timeSinceLevelLoad = 0;
end

function OnApplicationQuit()

end
return nil;