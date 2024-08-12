# -------------------------Controller-------------------------------------- #
# The main controller handler for the game
# 
# This class is primarily an event 'brocker', where it will emit signals
# 	to subscribers when particular events happen.   
# 
# This class is effectively a wrapper on the godot input API, but allows for 
# 	controller modifications, and additional controller logic if NEEDED. 
# --------------------------------------------------------------------------- #

extends Node

signal emitUserHub()
signal emitLeftClick(event: InputEvent)

var CONTROLLER_INPUT_MAPPINGS = {
	"I": userHub
}

# Called when the node enters the scene tree for the first time.
func _ready():
	Logging.infoLog("Controller init", "Controller.gd")
	
	# Tells godot that this class wants input signals 
	set_process_input(true)

func _input(event: InputEvent):
	Logging.debugLog("Controller processing", "Controller.gd")
	
	for inputMapping in CONTROLLER_INPUT_MAPPINGS:
		if Input.is_key_pressed(OS.find_keycode_from_string(inputMapping)):
			CONTROLLER_INPUT_MAPPINGS[inputMapping].call(event)
		elif Input.is_mouse_button_pressed(1):
			leftMouseClick(event)
		else:
			pass

# User hub event 
func userHub(event: InputEvent):
	Logging.infoLog("User hub!", "Controller.gd")
	emit_signal(Constants.USER_HUB_CONTROLLER_PRESS)

# Left mouse click event emission 
func leftMouseClick(event: InputEvent):
	Logging.infoLog("Left mouse click!", "Controller.gd")
	emit_signal(Constants.MOUSE_LEFT_CLICK, event)

