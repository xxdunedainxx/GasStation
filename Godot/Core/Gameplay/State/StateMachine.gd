# State machine for managing game state :) 

extends Node

# Stashes all potential levels and scenes for transitions 
var LEVELS = {
	# Inside of the gas station 
	"GasStationInside": "res://Levels/GasStationInside/GasStationInside.tscn"
}

# TODO -- actually have state determination based on login 
# Requires the scene tree as an argument
# Bug with godot where the scene tree may not be available here. 
# In that case, pass it along from the caller trying to change scenes 
func loginToLevel(sceneTree:  SceneTree):
	sceneTree.change_scene_to_file(LEVELS["GasStationInside"])
	
func loadLevel():
	pass

# Determines state on startup 
func __loadState():
	pass

# Updates state every frame
func __updateState():
	pass

# Called when the node enters the scene tree for the first time.
func _ready():
	__loadState()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	__updateState()

