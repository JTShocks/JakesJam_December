extends Control

var player_stats := preload("res://Scripts/WaveSystem/PlayerStats.tres")
@onready var health := $PanelContainer/VBoxContainer/Health
@onready var wave_display := $"PanelContainer/VBoxContainer/Wave Number"
@onready var wave_interval_timer := $"PanelContainer/VBoxContainer/Wave Interval Timer"
@onready var cash := $PanelContainer/VBoxContainer/Cash
@export var game_controller : Node
@onready var wave_system  = game_controller.get_node("Wave System")
@onready var spawner_system  = game_controller.get_node("Spawner System")
var wave_timer : Timer
var wave_number := 1

# TODO: Ask where the Cash info is kept.
# TODO Ask where wave number is kept.

func _process(delta):
	wave_timer = wave_system.waveIntervalTimer
	#wave_number = current wave???? #spawner_system.enemiesToSpawn
	
	health.text = "Player Health: " + str(player_stats.MaxHealth)
	wave_display.text = "Wave: " + str(wave_number)
	wave_interval_timer.text = "Time Until Next Wave: " + str(snappedf(wave_timer.time_left, 1))
	cash.text = "Total Cash:" #+ str(cash)
