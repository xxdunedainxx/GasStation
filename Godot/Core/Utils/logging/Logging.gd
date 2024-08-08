extends Node

var INFO_LOG="INFO"
var DEBUG_LOG="DEBUG"
var WARN_LOG="WARN"
var ERROR_LOG="ERROR"

# The log level for the app 
var __SET_LEVEL: String = "INFO"

# Logging levels hierarchy 
var LOGGING_LEVELS = {
	DEBUG_LOG: 1,
	WARN_LOG: 2,
	INFO_LOG: 3,
	ERROR_LOG: 4
}

func setLogLevel(level: String):
	__SET_LEVEL = level

func Log(logContent: String, level: String, clazz: String):
	
	# If the current log setting allows the specified log level, we log it :) 
	if LOGGING_LEVELS[level] >= LOGGING_LEVELS[__SET_LEVEL]:
		#var currentDay = Time.get_date_string_from_system()
		var currentTime = Time.get_datetime_string_from_system()
		print("[%s:%s::%s]: %s" % [level, currentTime, clazz, logContent])

func infoLog(logContent: String, clazz: String):
	Log(logContent, INFO_LOG, clazz)

func debugLog(logContent: String, clazz: String):
	Log(logContent, DEBUG_LOG, clazz)
	
func warnLog(logContent: String, clazz: String):
	Log(logContent, WARN_LOG, clazz)

func errorLog(logContent: String, clazz: String):
	Log(logContent, ERROR_LOG, clazz)
