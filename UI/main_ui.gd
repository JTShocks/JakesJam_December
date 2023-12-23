extends Control

var player_stats := preload("res://Scripts/WaveSystem/PlayerStats.tres")
@onready var health := $PanelContainer/VBoxContainer/Health
@onready var wave_number := $"PanelContainer/VBoxContainer/Wave Number"
@onready var wave_interval_timer := $"PanelContainer/VBoxContainer/Wave Interval Timer"
@onready var cash := $PanelContainer/VBoxContainer/Cash
@export var game_controller : Node
@onready var wave_system  = game_controller.get_node("Wave System")
var wave_timer : Timer

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	wave_timer = wave_system.waveIntervalTimer
	
	health.text = "Player Health: " + str(player_stats.MaxHealth)
	wave_number.text = "Wave: " #+ str(insert_wave_number_here)
	wave_interval_timer.text = "Time Until Next Wave: " + str(snappedf(wave_timer.time_left, 1))
	cash.text = "Total Cash:" #+ str(cash)
