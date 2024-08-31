extends Node

var WAY_POINT_REGISTRY: Dictionary = {}

func registryWaypoint(waypoint: Area2D, name: String):
	assert(hasWaypoint(name) == false, "ERROR, duplicate waypoint bodies!")
	WAY_POINT_REGISTRY[name] = waypoint

func getWaypoint(name: String) -> Area2D:
	return WAY_POINT_REGISTRY[name]

func hasWaypoint(name: String) -> bool:
	return name in WAY_POINT_REGISTRY.keys()
