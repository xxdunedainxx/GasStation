extends CharacterBody2D

@onready var npc = $Npc

# Called when the node enters the scene tree for the first time.
func _ready():
	npc.attachNpcReference(self, {
		Constants.NPC_SPEED : 10
	})

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass 
