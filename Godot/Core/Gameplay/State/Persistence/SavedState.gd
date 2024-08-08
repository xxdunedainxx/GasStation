extends Node

var saveFileName = "user://gasStation.json"

# Called when the node enters the scene tree for the first time.
func _ready():
	__loadGameSave()

func saveGame():
	var saveFile = FileAccess.open(saveFileName, FileAccess.WRITE)
	saveFile.close()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

# Load the save on game startup 
func __loadGameSave():
	var saveFile = FileAccess.open(saveFileName, FileAccess.WRITE)

	if saveFile.file_exists(saveFileName):
		Logging.infoLog("Loading game save", "SaveState.gd")
	else:
		Logging.infoLog("File doesnt exist!",  "SaveState.gd")
