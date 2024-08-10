extends Control

@onready var user_hub_canvas = $UserHubCanvas
@onready var save_button = $UserHubCanvas/SaveButton

func _ready():
	__registerSignals()

# Toggle user hub on / off 
func userHubInteraction():
	user_hub_canvas.visible = (!user_hub_canvas.visible)

# Bind to save button, simple save game function 
func saveGame():
	Logging.infoLog("Saving game!", "Userhub.gd") 
	SavedState.saveGame()

# Registers signals the user hub should care about 
func __registerSignals():
	
	# Toggling the userhub on/off 
	Controller.connect(
		Constants.USER_HUB_CONTROLLER_PRESS, 
		userHubInteraction
	)
	
	# Listen for save button pressed 
	save_button.pressed.connect(
		self.saveGame
	)

func _process(delta):
	pass
