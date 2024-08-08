extends CanvasLayer

@onready var dialogueText = $DialogueText

var WRITE_SPEED: float = 0.0000000000001

func _ready():
	__registerSignals()

func __registerSignals():
	Dialogue.connect(
		Constants.DIALOGUE_WRITE_TEXT_SIGNAL, 
		updateDialogueText
	)

	Dialogue.connect(
		Constants.DIALOGUE_HIDE_EVENT_SIGNAL, 
		hideDialogue
	)

	Dialogue.connect(
		Constants.DIALOGUE_SHOW_EVENT_SIGNAL, 
		showDialogue
	)

# Called when the node enters the scene tree for the first time.
func init():
	pass
	
func showDialogue():
	visible = true

func hideDialogue():
	visible = false

func updateDialogueText(text: Array):
	showDialogue()
	

	for t in text:
		# Write items 1 item at a time, based on a delay	
		var itemsWritten = 1
		for index in range(len(t)):
			dialogueText.text = t.substr(0, itemsWritten) 
			itemsWritten += 1
			await Sleep.sleep (
				get_tree(),
				WRITE_SPEED
			)
	
