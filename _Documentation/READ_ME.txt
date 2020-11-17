Authors: Jared Perkins, Hunter Figgs, Sergei Fedulov, Patrick Haughn, Jeff Gaydos

Controls (Keyboard):
			start menu:
						Q: Quit the Game
						Enter: Start the game
						F1: Mute/Unmute the Audio
						F2: Volume Down
						F3: Volume Up

			paused:
						Q: Quit the Game
						left/right shift: UnPause the game
						F1: Mute/Unmute the Audio
						F2: Volume Down
						F3: Volume Up

			In Game:
						Q: Quit the Game
						left/right shift: Pause the game
						F1: Mute/Unmute the Audio
						F2: Volume Down
						F3: Volume Up

						WASD/Arrow Keys: moves the player
						Z/N: Attack
						X/M: Use Item
						Escape: Open HUD
			
			HUD:
						A/D/Arrows: Select Item
						Escape: Close HUD
						Q: Quit Game

Controls (Gamepad):

			Start Menu:
						Back: Quit the game
						Start: Start the game

			Paused:
						Back: Quit the game
						BigButton: Start the Game

			In Game:
			
						Back: Quit the game
						Start: Enter HUD
						BigButton: Puase the Game
						DPAD/Left Stick: Move the character

						A: Attack
						B: Use item


Current Bugs:

	- Bats collision in "Bow Room" can be buggy (vibrating effect)

	- Link does not flash when timer is picked up and enemies stop animating

	- Sometimes player and/or enemies can get pushed outside the walls (This has been greatly reduced since the last sprint, but it is still occuring sometimes. We will take another pass at improving it before next sprint. Apologies if this inconveniences testing. Sometimes enemies get pushed outside walls, but they are needed to complete room puzzle.)


Code Metrics/Analysis (As of Sprint #4):

	- A visual studio "Code Metrics" was run on the project, which takes into account things like cyclomatics complexity, inheritance, coupling, and lines of code. Our project received a maintainability index (MI) score of 80 overall, which is relatively high. The Microsoft Docs website lists projects with MI between 20 and 100 as having high maintainability.

	- The same VS tool revealed that most of the files in the project contain less than 100 lines of code. Most that do contain more than 100 lines are factories, which are permissable. Some however, such as Player1 class need to be reduced.

	- Our project contains 0 errors and 0 warnings.


Other notes:

	- Since the feedback from Sprint 3 was received on the last day of Sprint #4, not every piece of feedback was able to be fixed (although most were). We will work hard to fix the rest during Sprint #5.

	- Muting the game (via F1) sometimes persists between running the game multiple times. If you can't get any audio, just press F3 until audio is suitable level.