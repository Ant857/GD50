My game is a player vs all battle royale where the player must eliminate all enemies in order to win. The storm slowly closes in once the timer reaches zero seconds. 
Each time the circle shrinks it becomes half the size and then stops again. The player can drop weapons and pick them back up. 
Current weapons in the game include a sniper, machine gun, and pistol. All have different firing rates, damage values, and ammo capacities.
"R" is used to reload or players can automatically reload by attempting to shoot when no bullets are in the magazine. The player can also regenerate health over time. 
If the player dies to either the storm or the enemies then they are brought to a game over screen.
WASD is used to move and left mouse click is to shoot. Mouse wheel scrolls through weapons. The player's character will look at the position of the mouse at all times.
The enemies can navigate the map through the obstacles using a nav mesh system. 
They will walk around until spotting the player and then proceed to chase and shoot the player. 
The map is procedurally generated using the Unity tilemap system and the navmesh is baked at the start of runtime.

I believe my game meets the complexity requirements of the final project. 
The AI uses Unity's built in navmesh agent and is able to stay away from the storm, patrol the area, and chase the player until they are close enough to shoot.
The rarity of different obstacles on the map is easily adjustable along with the size of the map.
The zone is a square with a circle as a mask that is moved and rescaled progressively through scripts to work properly.
The game has sound effects for shooting and reloading along with title screen music.
The white circle represents the size and position of the the upcoming circle.
Three gamestates include the title screen, game over screen, and the play state.
Ultimately, the zone mechanics, navmesh and agents, player system, and procedurally generated tilemap bring this game above the complexity requirement.
