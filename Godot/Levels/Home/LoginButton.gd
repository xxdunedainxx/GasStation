extends Button

@onready var stateMachine = load("res://Core/Gameplay/State/StateMachine.gd").new()

func _ready():
	# var button = Button.new()
	# button.text = "Click me"
	pressed.connect(self._login)
	# --> good example for later add_child(button)

func _login():
	print("Logging in!")
	# get_tree().change_scene_to_file("res://Levels/GasStationInside/GasStationInside.tscn")
	stateMachine.loginToLevel(get_tree())

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
