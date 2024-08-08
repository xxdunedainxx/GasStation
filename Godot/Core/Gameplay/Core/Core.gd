extends Node


var gameReady: bool = false

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func coreLoader():
	Logging.setLogLevel(Logging.INFO_LOG)
	Logging.infoLog("Start main", "Core.gd")
	__gameReady()

func __gameReady():
	Logging.infoLog("Game is ready!", "Core.gd") 
	gameReady = true
