extends Node

func sleep(tree: SceneTree,sleepTime: float):
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
