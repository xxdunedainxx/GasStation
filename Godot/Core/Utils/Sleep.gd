extends Node

# TODO - fix this shit lmao 

func sleep(tree: SceneTree,sleepTime: float):
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout

func slowSleep(tree: SceneTree,sleepTime: float):
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
	await tree.create_timer(sleepTime).timeout
