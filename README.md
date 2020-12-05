Authors: 

	-Jared Perkins
	-Hunter Figgs
	-Sergei Fedulov
	-Patrick Haughn
	-Jeff Gaydos


Current Game Controls: 	

	-Keyboard:
		W ------- Move Link Up
		A ------- Move Link Left
		S ------- Move Link Down
		D ------- Move Link Right
		Z/N ----- Attack
		X/M ----- Use Equipped Item

		Q ------- Quit Game
		Esc ----- Enter/Exit HUD
		Enter --- Start Game

		F1 ------ Mute Game
		F2 ------ Volume Down
		F3 ------ Volume Up

	-GamePad:
		DPad/LeftStickUp ---------------- Move Link Up
		DPad/LeftStickLeft -------------- Move Link Left
		DPad/LeftStickDown -------------- Move Link Down
		DPad/LeftStickRight ------------- Move Link Right
		A ------------------------------- Attack
		B ------------------------------- Use Equipped Item

		Back ---------------------------- Quit Game
		Start --------------------------- Enter/Exit HUD
		BigButton ---------------------- Start Game

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

- Special Death sequence - plays once to avoid player annoyance

Current Bugs:

	- Bats collision in "Bow Room" can be buggy (vibrating effect)

	- Link does not flash when timer is picked up and enemies stop animating

	- Sometimes player and/or enemies can get pushed outside the walls (This has been greatly reduced since the last sprint, but it is still occuring sometimes. We will take another pass at improving it before next sprint. Apologies if this inconveniences testing. Sometimes enemies get pushed outside walls, but they are needed to complete room puzzle.)

	- Stunning an enemy with boomerang that was already stunned with the clock will lead to the enemy being unstunned after the timer runs out. They should stay permanently stunned.


Code Metrics/Analysis (as of Sprint #4):

	- A visual studio "Code Metrics" was run on the project, which takes into account things like cyclomatics complexity, inheritance, coupling, and lines of code. Our project received a maintainability index (MI) score of 80 overall, which is relatively high. The Microsoft Docs website lists projects with MI between 20 and 100 as having high maintainability.

	- The same VS tool revealed that most of the files in the project contain less than 100 lines of code. Most that do contain more than 100 lines are factories, which are permissable. Some however, such as Player1 class need to be reduced.

	- Our project contains 0 errors and 0 warnings.


Other important information:

	- Since the feedback from Sprint #3 was received on the last day of Sprint #4, not every piece of feedback was able to be fixed (although most were). We will work hard to fix the rest during Sprint #5.

	- Muting the game (via F1) sometimes persists between running the game multiple times. If you can't get any audio, just press F3 until audio is suitable level.

	- Documentation (including Sprint Reviews and Code Reviews) can be found in the top-level folder '_Documentation'.