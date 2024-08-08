extends Node2D

const DialogueCanvas = preload("res://UI/dialogue/DialogueCanvas.gd")
var dialogueCanvas: DialogueCanvas 
var __DialogueReady: bool = false

# Signals 
signal emiteTextUpdate(text)
signal emitShowDialogue()
signal emitHideDialogue()

# Called when the node enters the scene tree for the first time.
func _ready():
	dialogueCanvas = DialogueCanvas.new()
	dialogueCanvas.init()
	Logging.infoLog("Dialogue system read", "DialogueSystem.gd")
	hideDialogue()
	dialogueReady()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func showDialogue():
	emit_signal(Constants.DIALOGUE_HIDE_EVENT_SIGNAL)

func hideDialogue():
	emit_signal(Constants.DIALOGUE_HIDE_EVENT_SIGNAL)

# TODO -- update speed 
func updateDialogueText(text: Array):
	emit_signal(Constants.DIALOGUE_WRITE_TEXT_SIGNAL, text)

func dialogueReady():
	__DialogueReady = true

func isDialogueReady():
	return __DialogueReady
