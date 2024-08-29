# TODO -- look into lockks for dialogues: 
# 		--> https://docs.godotengine.org/en/stable/classes/class_mutex.html

extends CanvasLayer

# Dialogue text component to update in the UI 
@onready var dialogueText = $DialogueText

# Default write speed 
var WRITE_SPEED: float = 50

# Current text we are writing to screen 
var CURRENT_TEXT: Array = []
# Index in the array of strings we are current writing out 
var CURRENT_TEXT_INDEX: int = 0
# Determines if the writer is writing 
var IS_WRITING = false 
# Boolean to interupt the writer 
var WRITING_INTERUPT = false 
# Boolean to state we are at the end of the sentence 
var END_OF_WRITER = false 

# Used for locking the writer 
var WRITER_LOCK = Mutex.new()

# Called when the node enters the scene tree for the first time.
func init():
	pass

func dialogueIsCurrentlyActive() -> bool:
	return visible

func showDialogue():
	visible = true

func hideDialogue():
	visible = false

func updateDialogueText(text: Array):
	# TODO -- might need an override to interupt current dialogue 
	if not dialogueIsCurrentlyActive():
		showDialogue()

		# Stash text in the class variables 
		CURRENT_TEXT = text
		CURRENT_TEXT_INDEX = 0
		
		# Write the first sentence 
		__writeSentenceToUI(0)
	else:
		Logging.warnLog(
			"Cant display new dialogue text because dialogue is already open!", 
			"DialogueCanvs.gd"
		)


# Handles a left click event for the text writer
# If it is currently writing, emit a write interrupt
# Otherwise go to the next text element 
func handleLeftClickEvent(event: InputEvent):
	# If we are visible and writing 
	if visible:
		# If its not already interupted, interupt it
		# TODO - could be an edge case here..  
		if IS_WRITING and not WRITING_INTERUPT:
			WRITING_INTERUPT = true 
		# End of the dialogue array 
		elif __isEndOfWriter():
			# TODO -- maybe something to indicate its the last sentence? 
			pass
		else:
			# Write next sentence 
			if WRITER_LOCK.try_lock():
				if __isEndOfWriter():
					Logging.errorLog("Writer already ended!", "DialogueCanvas.gd")
				else:
					__writeSentenceToUI(CURRENT_TEXT_INDEX)

func _ready():
	__registerSignals()

# Write text per frame if we are current in write mode... 
func _process(delta):
	pass 

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

	# For Text writing 
	Controller.connect(
		Constants.MOUSE_LEFT_CLICK,
		handleLeftClickEvent
	)

# Write items 1 item at a time, based on a delay	
func __writeSentenceToUI(currentIndex: int):
	
	var text: String = CURRENT_TEXT[currentIndex]
	
	# Take the lock..
	WRITER_LOCK.lock()
	
	# Take 'IS_WRITING' lock 
	IS_WRITING = true

	var itemsWritten = 1
	for index in range(len(text)):
		dialogueText.text = text.substr(0, itemsWritten) 
		itemsWritten += 1
		await Sleep.sleep (
			get_tree(),
			WRITE_SPEED
		)

		# Write the remaining text
		if WRITING_INTERUPT:
			dialogueText.text = text
			WRITING_INTERUPT = false 
			break
			
	CURRENT_TEXT_INDEX+=1
	
	if __isEndOfWriter() and (currentIndex + 1) >= len(CURRENT_TEXT):
		# Little animation at the end of the statement :) 
		var dotThereOrNot = false 
		while visible:
			dialogueText.text = text + "." if dotThereOrNot else text
			await Sleep.slowSleep (
				get_tree(),
				WRITE_SPEED * 10
			)			
			dotThereOrNot = !dotThereOrNot
	
	# Release the lock 
	IS_WRITING = false 
	
	# Release the lock 
	WRITER_LOCK.unlock()
		
func __isEndOfWriter() -> bool:
	return CURRENT_TEXT_INDEX >= len(CURRENT_TEXT)
