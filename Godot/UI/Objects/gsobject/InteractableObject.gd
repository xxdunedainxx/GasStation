# -------------------------Interactable Base Script-------------------------- #
# Base class for things like interactables in the world of the game. 
# --------------------------------------------------------------------------- #

extends Node

# parent sprite, used for parent rect to determine click position
var interactableSprite2D: Sprite2D

# Parent node original 
var interactableNode: Area2D

# Callable from the parent node 
var interactableNodeCallback: Callable

# handle click 
# generic clicik handler for interactables! 
func handleLeftClickEvent(event: InputEvent):
	Logging.debugLog("Left click event from GSObject", "InteractableObject.gd") 
	
	if __isInteractableClicked() \
		and __canPlayerInteract():
		Logging.infoLog("Clicking interactable!", "InteractableObject.gd")
		interactableNodeCallback.call(event)

# Register the actual object thats interactable 
func registerInteractable(node: Area2D, callback: Callable, sprite2D: Sprite2D):
	interactableNode = node 
	interactableNodeCallback = callback 
	interactableSprite2D = sprite2D

# Called when the node enters the scene tree for the first time.
func _ready():
	# For Text writing 
	Controller.connect (
		Constants.MOUSE_LEFT_CLICK,
		handleLeftClickEvent
	)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

# Checks if the interactable is clicked 
func __isInteractableClicked() -> bool:
	return interactableSprite2D.get_rect().has_point (
		interactableSprite2D.get_local_mouse_position()
	)

# Checks if the player is close enough to interact with the object 
func __canPlayerInteract() -> bool:
	return interactableNode.overlaps_body(
		CharacterBody2dRegistry.getCharacterBody(Constants.PLAYER_BODY_REGISTRY_VALUE)
	)
