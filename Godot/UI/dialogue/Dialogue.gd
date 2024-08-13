# -------------------------Dialogue System------------------------------------- #
# The dialogue system is responsible for rendering the dialogue text in the UI.
#
# This system will show the dialogue canvas/hide the canvas, 
# 	invoke the DialogueCanvas, which handles text writing. 
# 
# It is effectively middleware for handling dialogue related events .
# --------------------------------------------------------------------------- #

extends Node2D

const DialogueCanvas = preload("res://UI/dialogue/DialogueCanvas.gd")
var dialogueCanvas: DialogueCanvas 
var __DialogueReady: bool = false

# Signals 
signal emiteTextUpdate(text)
signal emitShowDialogue()
signal emitHideDialogue()
signal emitDialogueEvent(event: Dictionary)

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
	handleEmitDialogueEvent(Constants.DIALOGUE_HIDE_EVENT_SIGNAL, false)


# TODO -- update speed 
func updateDialogueText(text: Array):
	# if interuptExistingDialogueSignal or 
	emit_signal(Constants.DIALOGUE_WRITE_TEXT_SIGNAL, text)
	handleEmitDialogueEvent(Constants.DIALOGUE_WRITING_EVENT, true)

# Writes to core event loop that a dialogue event has happened 
func handleEmitDialogueEvent(eventType: String, freezePlayer: bool):
	emit_signal(
		Constants.DIALOGUE_EVENT_SIGNAL,
		{
			"type": Constants.DIALOGUE_EVENT,
			"dialogueEventType": eventType,
			Constants.ADDITIONAL_EVENT_FIELDS : {
				Constants.FREEZE_PLAYER : freezePlayer 
			}
		}
	)

func dialogueReady():
	__DialogueReady = true

func isDialogueReady():
	return __DialogueReady
