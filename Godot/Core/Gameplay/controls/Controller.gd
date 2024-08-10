extends Node

signal emitUserHub()

var CONTROLLER_INPUT_MAPPINGS = {
	"I": userHub
}

# Called when the node enters the scene tree for the first time.
func _ready():
	Logging.infoLog("Controller init", "Controller.gd")
	
	# Tells godot that this class wants input signals 
	set_process_input(true)

func _input(event):
	Logging.debugLog("Controller processing", "Controller.gd")
	
	for inputMapping in CONTROLLER_INPUT_MAPPINGS:
		if Input.is_key_pressed(OS.find_keycode_from_string(inputMapping)):
			CONTROLLER_INPUT_MAPPINGS[inputMapping].call(event)
			
	
func userHub(event):
	Logging.infoLog("User hub!", "Controller.gd")
	emit_signal(Constants.USER_HUB_CONTROLLER_PRESS)

