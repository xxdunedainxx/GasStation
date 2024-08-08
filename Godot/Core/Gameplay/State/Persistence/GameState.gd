extends Node

const DialogueTree = preload("res://Core/Gameplay/State/Persistence/States/DialogueTree.gd")

var GAME_STATE 



var CHAPTER_1 = {
	"DialogueTree" : {
		"rootNode" : {
			"children" : [
				{
					"dialogueText" : ["Rod your fucking late", "Get the fuck over here"],
					"currentNode" : true,
					"children" : [
						{
							"dialogueText": "good",
							"currentNode" : false,
							"children" : []
						}
					]
				}
			]
		}
	}
}

var CHAPTERS = {
	"Chapter1" : CHAPTER_1
}

func init():
	GAME_STATE = {
		"level" : Constants.GAS_STATION_INSIDE_SCENE,
		"chapter" : CHAPTERS["Chapter1"],
		"dialogueTree" : DialogueTree.new().init(CHAPTERS["Chapter1"]["DialogueTree"])
	}	

func fetchCurrentDialogue():
	return GAME_STATE["dialogueTree"].currentNode.dialogueText
