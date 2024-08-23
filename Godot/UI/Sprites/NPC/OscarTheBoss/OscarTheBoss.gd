extends CharacterBody2D

@onready var npc = $Npc
@onready var navAgent = $NavigationAgent2D

# Called when the node enters the scene tree for the first time.
func _ready():
	npc.attachNpcReference(
		self, 
		{
			Constants.NPC_SPEED : 10
		},
		navAgent
	)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass 
