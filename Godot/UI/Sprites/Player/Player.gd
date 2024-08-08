extends CharacterBody2D

const SPEED = 100
const JUMP_VELOCITY = -400.0

# Get the gravity from the project settings to be synced with RigidBody nodes.
# var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

func get_user_input_movement():
	var input_direction = Input.get_vector("left", "right", "up", "down")
	velocity = input_direction * SPEED

func _physics_process(delta):
	if Core.gameReady:
		Logging.debugLog("Player physics processing..", "Player.gd")
		get_user_input_movement()
		move_and_slide()
	else:
		Logging.debugLog("BAD NO PHYSICS PROCESSOR", "Player.gd")

func _process(delta):
	if Core.gameReady:
		Logging.debugLog("Player process frame..", "Player.gd")
	else:
		Logging.debugLog("Player waiting for game ready state...", "Player.gd")		 

# Called when the node enters the scene tree for the first time.
func _ready():
	# Just testing stuff.. 
	velocity.y = JUMP_VELOCITY
