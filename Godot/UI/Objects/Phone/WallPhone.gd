extends Node2D

@onready var interactableObject = $InteractableObject

# Called when the node enters the scene tree for the first time.
func _ready():
	interactableObject.registerInteractable(self)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
