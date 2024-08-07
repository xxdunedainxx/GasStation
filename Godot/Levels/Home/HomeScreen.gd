extends Node2D

@onready var Core = preload("res://Core/Gameplay/Core/Core.tscn").instantiate()

# Called when the node enters the scene tree for the first time.
func _ready():
	Core.core_loader()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
