# -------------------------Interactable Base Script-------------------------- #
# Base class for things like interactables in the world of the game. 
# --------------------------------------------------------------------------- #

extends Node

var interactableNode: Node2D

# handle click 
func handleLeftClickEvent(event: InputEvent):
	Logging.debugLog("Left click event from GSObject", "GSObject.gd") 

# Register the actual object thats interactable 
func registerInteractable(node: Node2D):
	interactableNode = node 

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
