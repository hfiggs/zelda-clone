Current Game Controls: 	

	-Keyboard:
		W ------- Move Link Up
		A ------- Move Link Left
		S ------- Move Link Down
		D ------- Move Link Right
		Z/N ----- Attack
		X/M ----- Use Equipped Item
		1 ------- Equip Bow
		2 ------- Equip Boomerang
		3 ------- Equip Bomb
		U ------- Cycle current room forward
		I ------- Cycle current room backwared
		Q ------- Quit Game

	-GamePad:
		DPadUp/LeftStickUp -------------- Move Link Up
		DPadLeft/LeftStickLeft ---------- Move Link Left
		DPadDown/LeftStickDown ---------- Move Link Down
		DPadRight/LeftStickRight -------- Move Link Right
		A ------------------------------- Attack
		B ------------------------------- Use Equipped Item
		RightStickLeft ------------------ Equip Bow
		RightStickUp -------------------- Equip Boomerang
		RightStickRight ----------------- Equip Bomb
		RightShoulder ------------------- Cycle current room forward
		LeftShoulder -------------------- Cycle current room backwared
		Back ---------------------------- Quit Game


Current Bugs:
	- Particles are not present for certain events and projectiles (no enemy death, no sword beam explosion)

	- Bats collision in "Bow Room" can be buggy (vibrating effect)

	- Speeds and Cooldowns of enemies and players could be more accurate to the game

	- Link does not flash when timer is picked up and enemies stop animating (pushed to next sprint)

	- Puzzle Events don't trigger doors opening (pushed to next sprint)

	- Sometimes player and/or enemies can get pushed outside the walls


Code Metrics/Analysis:

	- A visual studio "Code Metrics" was run on the project, which takes into account things like cyclomatics complexity, inheritance, coupling, and lines of code. Our project received a maintainability index (MI) score of 82 overall, which is relatively high. The Microsoft Docs website lists projects with MI between 20 and 100 as having high maintainability.

	- The same VS tool revealed that nearly all of the files in the project contain less than 100 lines of code. The few that do are mostly factories, which are permissible.

	- Our project contains 0 errors and 0 warnings.


Other important information:

	- Switching to certain rooms can push the player outside of the screen, either restart the program or find a room with an open door near where you were pushed off and walk through it back into the room

	- The NuGet package "ResolutionBuddy" is currently being used to help scaling up the game window. The package can be found here: https://www.nuget.org/packages/ResolutionBuddy/2.0.4

	- The above package required us to update the target .NETFramework version to v4.8. Prof. Boggus said that this was fine as long as we didn't run into compatibility issues with MonoGame. The version download can be found here: https://dotnet.microsoft.com/download/visual-studio-sdks?utm_source=getdotnetsdk&utm_medium=referral

	- The resolution scaling currently isn't dynamic, but the target resolution can be set on line 43 of Game1.cs. On the same line a boolean flag can be set so that the resolution is automatically selected based on the monitor.

	- This package allows us to develop to what's called a "virtual" resolution (which in this case is 256x176), and then ResolutionBuddy handles everything from there.

	- The Sprint 3 Reflection and Code Reviews can both be found in the top level folder called "Documentation."