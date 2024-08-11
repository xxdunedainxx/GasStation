# -------------------------NPC Base Script----------------------------------- #
# Base script / class for NPCs 
# --------------------------------------------------------------------------- #

extends Node

# Original character 2D ref 
var npcReference: CharacterBody2D

# Attributes provided to the generic NPC script 
var npcAttributes: Dictionary 

# Attaches parent NPC ref 
func attachNpcReference(npcRef: CharacterBody2D, npcAttributesRef: Dictionary):
	npcReference = npcRef 
	npcAttributes = npcAttributesRef

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
func __evalMoveNPC():
	npcReference.velocity = Vector2.LEFT  * __getNPCSpeed()
	npcReference.move_and_slide()
	Sleep.sleep(get_tree(), .00000000001)
	
