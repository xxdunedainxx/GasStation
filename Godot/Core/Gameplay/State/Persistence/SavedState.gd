extends Node

# For local debug, can be found: C:\Users\zach\AppData\Roaming\Godot\app_userdata\GasStationGame
# Source: https://docs.godotengine.org/en/stable/tutorials/io/data_paths.html#:~:text=The%20location%20of%20the%20user,contained%20within%20Godot's%20data%20folder.
var saveFileName = "user://gasStation.json"

# Stash current game state in save file 
func saveGame():
	var saveFile = FileAccess.open(saveFileName, FileAccess.WRITE)
	saveFile.store_var(
		JSON.stringify(
			GameState.GAME_STATE
		)
	)
	saveFile.close()


# Load the save on game startup 
func loadGameSave():
	var saveFile = FileAccess.open(saveFileName, FileAccess.WRITE)

	if saveFile.file_exists(saveFileName):
		Logging.infoLog("Loading game save", "SaveState.gd")
		var rawJson = JSON.parse_string(saveFile.get_as_text())

		# Set the current game state
		GameState.GAME_STATE = rawJson
	else:
		Logging.infoLog("Save File doesnt exist! Default game state will be used",  "SaveState.gd")
