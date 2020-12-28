      ______    _     _                   _____ _                  
     |___  /   | |   | |                 / ____| |                 
        / / ___| | __| | __ _   ______  | |    | | ___  _ __   ___ 
       / / / _ \ |/ _` |/ _` | |______| | |    | |/ _ \| '_ \ / _ \
      / /_|  __/ | (_| | (_| |          | |____| | (_) | | | |  __/
     /_____\___|_|\__,_|\__,_|           \_____|_|\___/|_| |_|\___|
     
     by Hunter Figgs, Jared Perkins, Patrick Haughn, Sergei Fedulov, and Jeffrey Gaydos
     
About:

	A clone of the original Zelda, built from the ground up during a semester-long project with MonoGame/C#.

Current Game Controls: 	

	-Keyboard:
		W/UpArrow --------- Move Up
		A/LeftArrow ------- Move Left
		S/DownArrow ------- Move Down
		D/RightArrow ------ Move Right
		Z/N --------------- Attack
		X/M --------------- Use Equipped Item

		Q ------- Quit Game
		Esc ----- Enter/Exit HUD
		Enter --- Start Game
		Shift --- Navigate Selection Menus

		F1 ------ Mute Game
		F2 ------ Volume Down
		F3 ------ Volume Up

	-GamePad:
		Left/RightStickUp --------------- Move Up
		Left/RightStickLeft ------------- Move Left
		Left/RightStickDown ------------- Move Down
		Left/RightStickRight ------------ Move Right
		Left/RightTrigger --------------- Attack
		Left/RightBumper ---------------- Use Equipped Item

		Back ---------------------------- Quit Game
		Start --------------------------- Enter/Exit HUD
		BigButton ----------------------- Start Game

	-Other notes:
		When using Multiplayer HUD, use up/down keys to switch inventories


Difficulty Settings:

	-Easy:
		This diffiuclty most closely resembles the original Legeon of Zelda game on the NES.

	-Medium:
		This difficulty adds a greater varity of monsters to the rooms as well as adds more monsters to each room.

	-Hard:
		This diffuclty adds more mechanics to the monsters and incresase certain monsters health and damage.


All Extra Features

	- Multiplayer - split keyboard controls for 2 humans

	- AI - pathfinds to the player until finding an enemy, at which point it attacks, for 1 human

	- Difficulty - 3 difficulty settings to choose from

	- Added extra rooms - Exterior of dungeon and portal rooms to the right

	- Portals - Portals can only be created on white tiles in the rooms to the right of the starting room (overworld)

	- Full Screen vs Windowed option - Maximize with the typical windows button or by pressing F4

	- Persistant Volume Controls - increase, decrease, and mute all sound using F1, F2, and F3 using built in project properties

	- Special Death sequence - only plays once to avoid player annoyance


Current Bugs:

	- Bats collision in "Bow Room" can be buggy (vibrating effect)

	- Link does not flash when timer is picked up and enemies stop animating

	- Sometimes player and/or enemies can get pushed outside the walls (This has been greatly reduced since the last sprint, but it is still occuring sometimes. Apologies if this inconveniences testing. Sometimes enemies get pushed outside walls, when they are needed to complete room puzzle.)


Code Metrics/Analysis (as of Sprint #5):

	- A visual studio "Code Metrics" was run on the project, which takes into account things like cyclomatics complexity, inheritance, coupling, and lines of code. Our project received a maintainability index (MI) score of 79 overall, which is relatively high. The Microsoft Docs website lists projects with MI between 20 and 100 as having high maintainability.

	- The same VS tool revealed that most of the files in the project contain less than 100 lines of code. Most that do contain more than 100 lines are factories, which are permissable. Some however, such as Player1 class need to be reduced.

	- Our project contains 0 errors and 0 warnings.


Other important information:

	- Since the feedback from Sprint #4 was never received, only the feedback up through Sprint #3 has been addressed.

	- Muting the game (via F1) sometimes persists between running the game multiple times. If you can't get any audio, just press F3 until audio is suitable level.

	- Documentation (including Sprint Reviews, Code Reviews, and Sprint Board Screenshots) can be found in the top-level folder '_Documentation'.
