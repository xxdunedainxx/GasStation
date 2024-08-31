extends Node

# TODO - fix this shit lmao 

func sleep(tree: SceneTree,sleepTimeMilliseconds: float):
	assert(sleepTimeMilliseconds >= 0)

	var currentMilis = 	Time.get_ticks_msec()
	var targetMilis = 	Time.get_ticks_msec()+ sleepTimeMilliseconds
	var eachSleepMillis = 1000
	
	while currentMilis < targetMilis:
		await tree.create_timer(.00001).timeout
		currentMilis = Time.get_ticks_msec()

#@Deprecated! 
func slowSleep(tree: SceneTree,sleepTimeMilliseconds: float):
	await sleep(
		tree,
		sleepTimeMilliseconds
	)
