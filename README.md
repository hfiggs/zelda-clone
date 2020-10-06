﻿Current Program Controls: 	W - Move Link Up
				A - Move Link Left
				S - Move Link Down
				D - Move Link Right
				1 - Use Arrow
				2 - Use Boomerang
				3 - Use Bomb
				E - Damage Link
				Z - Attack
				N - Attack
				R - Reset
				T - Cycle current block left
				Y - Cycle current block right
				U - Cycle current Item left
				I - Cycle current Item right
				O - Cycle current enemy left
				P - Cycle current enemy right


Current Bugs:

	- Positions being passed around isn't super precise (i.e. boomerang may come back to top left of player instead of dead-center). Not a ton of attention was paid to this since this will be explored more when dealing with collision handling.

	- Some "speeds" are not super accurate (some too fast, some too slow), such as the sword beam. Speeds of projectiles and enemy movement will be reconsidered when level design is coming together.


Other important information:

	- The NuGet package "ResolutionBuddy" is currently being used to help scaling up the game window. The package can be found here: https://www.nuget.org/packages/ResolutionBuddy/2.0.4

	- The above package required us to update the target .NETFramework version to v4.8. Prof. Boggus said that this was fine as long as we didn't run into compatibility issues with MonoGame. The version download can be found here: https://dotnet.microsoft.com/download/visual-studio-sdks?utm_source=getdotnetsdk&utm_medium=referral

	- The resolution scaling currently isn't dynamic, but the target resolution can be set on line 43 of Game1.cs. On the same line a boolean flag can be set so that the resolution is automatically selected based on the monitor.

	- This package allows us to develop to what's called a "virtual" resolution (which in this case is 256x176), and then ResolutionBuddy handles everything from there.