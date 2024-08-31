extends Area2D



func _ready():
	WaypointLocationRegistry.registryWaypoint(
		self,
		Constants.GAS_STATION_INSIDE_LEFT
	)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
