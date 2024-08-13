extends Area2D

@onready var interactableObject = $InteractableObject
@onready var wallPhoneSprite = $Sprite2D

# Current diailogue that plays when you click the phone 
var currentDialogueText: Array

func handleInteractableCallback(event: InputEvent):
	Logging.infoLog("Wallphone clicked!", "Wallphone.gd")
	Dialogue.updateDialogueText(currentDialogueText) 

# Called when the node enters the scene tree for the first time.
func _ready():
	interactableObject.registerInteractable(
		self, 
		handleInteractableCallback, 
		wallPhoneSprite
	)
	
	# Default dialogue text for the phone 
	currentDialogueText = ["***** PHONE STATIC ******"]

