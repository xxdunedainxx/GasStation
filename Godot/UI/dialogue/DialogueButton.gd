extends Button

func _ready():
	pressed.connect(self.__closeDialogue)

func _process(delta):
	pass

# Close out the dialogue canvas 
func __closeDialogue():
	Dialogue.hideDialogue()
