extends Node

var scale := 4.0
# Called when the node enters the scene tree for the first time.
func _ready():
	get_window().size = Vector2(1920/scale,1080/scale)
