extends Node

const DialogueTreeNode = preload("res://Core/Gameplay/State/Persistence/States/DialogueTreeNode.gd")

# Array of dialogue tree nodes 
var rootNode: DialogueTreeNode
var currentNode: DialogueTreeNode

func init(json):
	rootNode = DialogueTreeNode.new().init(json["rootNode"])
	Logging.infoLog("Dialogue tree constructed", "DialogueTree.gd")
	determineCurrentNode()
	return self

func determineCurrentNode():
	currentNode = rootNode.searchForCurrentNode()
	Logging.debugLog("Current node determined", "DialogueTree.gd")
