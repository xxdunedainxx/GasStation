extends Node

class_name DialogueTreeNode

# Array of dialogue tree nodes 
var children = []
var nonStructuredChildren = []
var dialogueText = []

# Determines in state, if this is the current node of selection 
var currentNode = false

# Initlaizes root node, takes in children as array and initalizes them each 
func init(data):
	initChildren(data)
	return self

func searchForCurrentNode():
	for child in children:
		if child.currentNode:
			return child

func initChildren(data):
	for child in data["children"]:
		children.append(fromJson(child))
	
	for child in children: 
		child.initChildrenNonRoot()

func initChildrenNonRoot():
	for child in nonStructuredChildren:
		children.append(fromJson(child))
	
	for child in children: 
		child.initChildrenNonRoot()

func toJson() -> Dictionary:
	return {
		"dialogueText" : dialogueText,
		"currentNode" : currentNode
	}
	
func fromJson(data: Dictionary) -> DialogueTreeNode:
	var dialogueTreenode = DialogueTreeNode.new()
	dialogueTreenode.dialogueText = data["dialogueText"]
	dialogueTreenode.currentNode = data["currentNode"]
	dialogueTreenode.nonStructuredChildren = data["children"]
	return dialogueTreenode
