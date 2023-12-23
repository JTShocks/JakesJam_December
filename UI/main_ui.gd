extends Control

var player_stats := preload("res://Scripts/WaveSystem/PlayerStats.tres")
@onready var health := $PanelContainer/VBoxContainer/Health
@onready var wave_number := $"PanelContainer/VBoxContainer/Wave Number"
@onready var wave_interval_timer := $"PanelContainer/VBoxContainer/Wave Interval Timer"
@onready var cash := $PanelContainer/VBoxContainer/Cash


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	health.text = "Player Health: " + str(player_stats.MaxHealth)
	wave_number.text = "Wave: " #+ str(insert_wave_number_here)
	wave_interval_timer.text = "Time Until Next Wave: " #+ str(time)
	cash.text = "Total Cash:" #+ str(cash)
