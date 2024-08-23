# -------------------------NPC Base Script----------------------------------- #
# Base script / class for NPCs 
# --------------------------------------------------------------------------- #

extends Node

# Original character 2D ref 
var npcReference: CharacterBody2D
var npcNavAgentRef: NavigationAgent2D

# Attributes provided to the generic NPC script 
var npcAttributes: Dictionary 

# Current NPC movement Specification
# This dictionary expects:
# 	- destination coordinates: x/y coordinates as a vector 
#	- movement speed 
var npcMovementSpecification: Dictionary 

# Attaches parent NPC ref 
func attachNpcReference(
npcRef: CharacterBody2D, 
npcAttributesRef: Dictionary, 
navAgent: NavigationAgent2D):
	npcReference = npcRef 
	npcAttributes = npcAttributesRef
	npcNavAgentRef = navAgent
	
	npcMovementSpecification = {
		"npcDestination" : npcReference.global_position + Vector2(-30,5) 
	}


func handleEventSignal(eventData: Dictionary):
	Logging.debugLog("NPC handle event", "NPC.gd") 	

# Called when the node enters the scene tree for the first time.
func _ready():
	EvenSystem.connect(
		Constants.GENERIC_EVENT_SYSTEM,
		handleEventSignal
	) 
	
		


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _physics_process(delta):
	if Core.gameReady:
		__evalMoveNPC()
	else:
		pass 

# speed 
func __getNPCSpeed() -> int:
	return npcAttributes[Constants.NPC_SPEED]

# Evaluate npc movement 
# Here we use manhattan distance, so to do very linear x/y movements 
# TODO - need to determine if objects are in the way, and adjust the vector 
func __evalMoveNPC():


	# If we have a current, attached movement spec 
	if not npcMovementSpecification.is_empty():

		var npcDestination: Vector2 = npcMovementSpecification["npcDestination"]
		npcNavAgentRef.target_position = npcDestination
		
		# TODO -- this could be moved into 'moveLeft', 'moveRight', etc.. methods 
		#if npcDestination.x <= npcReference.position.x:
			#npcReference.velocity = Vector2.LEFT  * __getNPCSpeed()
		#elif npcDestination.x >= npcReference.position.x:
			#npcReference.velocity = Vector2.RIGHT  * __getNPCSpeed()
		#elif npcDestination.y <= npcReference.position.y:
			#npcReference.velocity = Vector2.DOWN  * __getNPCSpeed()
		#elif npcDestination.y >= npcReference.position.y:
			#npcReference.velocity = Vector2.UP  * __getNPCSpeed()
		
		var directionToMove: Vector2 = npcNavAgentRef.get_next_path_position() - npcReference.global_position
		directionToMove = directionToMove.normalized()
		npcReference.velocity = directionToMove * __getNPCSpeed()
		npcReference.move_and_slide()
		Sleep.sleep(get_tree(), .00000000001)
