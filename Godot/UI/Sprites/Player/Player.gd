extends CharacterBody2D

# TODO -- not needed 
const JUMP_VELOCITY = -400.0

# Stops movement! 
var freezePlayer = false 

# Get the gravity from the project settings to be synced with RigidBody nodes.
# var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

func handleFreezePlayerEvent(freezePlayer: bool):
	freezePlayer = freezePlayer

func get_user_input_movement():
	var input_direction = Input.get_vector("left", "right", "up", "down")
	velocity = input_direction * PlayerAttributes.SPEED

func _physics_process(delta):
	if Core.gameReady and not freezePlayer:
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
	# TODO -- not needed 
	velocity.y = JUMP_VELOCITY
	EvenSystem.connect(
		Constants.FREEZE_PLAYER_EVENT,
		handleFreezePlayerEvent
	)
