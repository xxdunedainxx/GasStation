extends Node

# Scene paths
var GAS_STATION_INSIDE_SCENE = "res://Levels/GasStationInside/GasStationInside.tscn"

# Signal constants

# User/player events 
var FREEZE_PLAYER_EVENT = "emitFreezePlayerEvent"

# Dialogue signals 
var DIALOGUE_WRITE_TEXT_SIGNAL = "emiteTextUpdate"
var DIALOGUE_SHOW_EVENT_SIGNAL = "emitShowDialogue"
var DIALOGUE_HIDE_EVENT_SIGNAL = "emitHideDialogue"
var DIALOGUE_EVENT_SIGNAL = "emitDialogueEvent"

# Controller signals  
var USER_HUB_CONTROLLER_PRESS  = "emitUserHub"
var MOUSE_LEFT_CLICK = "emitLeftClick"

# Event Types
var GENERIC_EVENT_SYSTEM = "emitEvent"
var DIALOGUE_EVENT = "DIALOGUE_EVENT"
var DIALOGUE_WRITING_EVENT = "DIALOGUE_WRITE_EVENT"

# Event supported additional fields 
var ADDITIONAL_EVENT_FIELDS = "additionalFields"
var FREEZE_PLAYER = "freezePlayer"

# NPC related keys
var NPC_SPEED = "npcSpeed"
