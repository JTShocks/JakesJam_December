extends Control

var player_stats := preload("res://Scripts/WaveSystem/PlayerStats.tres")
@onready var health := $PanelContainer/VBoxContainer/Health
@onready var wave_display := $"PanelContainer/VBoxContainer/Wave Number"
@onready var wave_interval_timer := $"PanelContainer/VBoxContainer/Wave Interval Timer"
@onready var cash := $PanelContainer/VBoxContainer/Cash

@export var wave_system : Node
@export var player : CharacterBody2D

@onready var wave_info = wave_system
#@onready var spawner_system  = game_controller.get_node("Spawner System")
@onready var player_info_node = player
var wave_timer : Timer
var wave_number : int

# TODO: Ask where the Cash info is kept.
# TODO Ask where wave number is kept.
func _process(_delta):
 #spawner_system.enemiesToSpawn
	wave_timer = wave_info.waveIntervalTimer
	wave_number = wave_info.currentWaveCount
	
	health.text = "Player Health: " + str(player_info_node.currentHealth)
	wave_display.text = "Wave: " + str(wave_number)
	wave_interval_timer.text = "Time Until Next Wave: " + str(snappedf(wave_timer.time_left, 1))
	cash.text = "Total Cash:" + str(player_info_node.playerMoney)
