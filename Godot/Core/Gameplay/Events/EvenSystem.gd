# -------------------------Event System-------------------------------------- #
# Event Sub System enables us to manage all 'events' within the game, under a 
# single queue and handler system. Here events are handled as 'dictionaries' 
# and emmitted as signals which other systems can subscribe to. 
# Events can be things such as:
# 	* Dialogue events
#	* Random events
#	* Character events 
#	* 'Battles'
#	* 'Cut scenes' 
# 
# This is useful for a generic way to handle 'one to many' signals, where one
# publishing is emiting signals to multiple subscribers. 
#
# Some events are 1:1 and do not need to go through the event handler loop. 
# --------------------------------------------------------------------------- #

extends Node

# Generic event emmitter 
signal emitEvent()
signal emitFreezePlayerEvent(freezePlayer: bool)

# Emit the event 
# Events coming in should look something like this:
#		{
#			"type": Constants.DIALOGUE_EVENT,
# 			"dialogueEventType": eventType,
# 			"additionalFields" : {
# 				"freezePlayer": true 
# 			}
# 		}

var SUB_EVENT_SYSTEM_MAPPING = {
	Constants.FREEZE_PLAYER: freezePlayerEvent
}

func freezePlayerEvent(freezePlayer: bool):
	Logging.infoLog("Freeze player event!", "EventSystem.gd")
	emit_signal(Constants.FREEZE_PLAYER_EVENT, freezePlayer) 

func emitEventHandler(data: Dictionary):
	Logging.infoLog("Emit handling.. %s" % data["type"], "EventSystem.gd")
	
	# Emit signal to subscribers 
	emit_signal("emitEvent", data) 

	# There could be other possible sub signals 
	__subSignalEvents(data)

# Called when the node enters the scene tree for the first time.
func _ready():
	Logging.infoLog("Start EvenSystem", "EventSystem.gd")
	__registerSignals() 

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

# Signals registered here will be emitted to the generic emit handler loop! 
func __registerSignals():
	
	# Listen to dialogue related events 
	Dialogue.connect(
		Constants.DIALOGUE_EVENT_SIGNAL, 
		emitEventHandler
	) 

func __subSignalEvents(eventData: Dictionary):
	if Constants.ADDITIONAL_EVENT_FIELDS in eventData.keys():
		var additionalFields: Dictionary = eventData[Constants.ADDITIONAL_EVENT_FIELDS]
		for field in additionalFields.keys():
			if field in SUB_EVENT_SYSTEM_MAPPING.keys():
				SUB_EVENT_SYSTEM_MAPPING[field].call(additionalFields[field])

