extends Button


func _ready():
	# var button = Button.new()
	# button.text = "Click me"
	pressed.connect(self._login)
	# --> good example for later add_child(button)

func _login():
	print("Logging in!")
	StateMachine.loginToLevel(get_tree())

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
