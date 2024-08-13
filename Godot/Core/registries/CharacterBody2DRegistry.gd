# -------------------------Character Body Registry -------------------------- #
# Registry for all 2D character bodies. 
# This is a useful way of globally referencing where stuff is in the world :) 
# --------------------------------------------------------------------------- #

extends Node

var CHARACTER_BODY_REGISTRY: Dictionary = {}

func registerCharacterBody(characterBody: CharacterBody2D, name: String):
	assert(hasCharacterBody(name) == false, "ERROR, duplicate character bodies!")
	CHARACTER_BODY_REGISTRY[name] = characterBody

func getCharacterBody(name: String) -> CharacterBody2D:
	return CHARACTER_BODY_REGISTRY[name]

func hasCharacterBody(name: String) -> bool:
	return name in CHARACTER_BODY_REGISTRY.keys()
