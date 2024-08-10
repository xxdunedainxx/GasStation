# -------------------------Player Attributes--------------------------------- #
# Stores all player related attributes
# Helper methods are provided for manipulating attributes (trust, speed, etc).
# 
# Additional helper methods are also provided 
#  for persisting these attributes to the saved gamestate file 
# --------------------------------------------------------------------------- #

extends Node

var TRUST = 10

# Speed will likely be constant, with debuf variations 
var SPEED = 10 

# Temporary debufs 
var SPEED_DEBUF = 0

# Alter trust, can be negative or positive :) 
func changeTrust(change: int):
	# Dont allow it to drop below 0 
	TRUST = (max(0, TRUST + change))
# Calculate speed 
func getSpeed(): 
	return SPEED - SPEED_DEBUF

# Load data from persistence 
func fromJson(json: Dictionary):
	pass
	
# Export data to persistence 
func toJson() -> Dictionary:
	return {
		"trust": TRUST,
		"speed": SPEED
	}

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
