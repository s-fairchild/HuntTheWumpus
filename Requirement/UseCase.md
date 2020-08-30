# Hunt the Wumpus Game Use Cases
**William Chan**

**August 29, 2020**

## Use Case Name: play the game
**Actors:**
* Player
* Website
* System

**Triggers:**
* The player wants to play the Hunt the Wumpus game.
* The player is on the website and has clicked "start the game" button.

**Precondition**
* The player has a supported client web browser.
* The player is on Hunt the Wumpus website.
* The system is running.

**Post-Condition**
* The player has completed the game and exited the application.
* The system stops running.

**Normal Flow**

1. The player will navigate to Hunt the Wumpus website.
1. The website will display brief intro of the game. 
1. The website will display rules of the game.
1. The website will display how to play the game.
1. The website will display an option to start the game.  
1. The player will click on "start the game" button.  
1. The system will render and a new game begins.
1. The system will render a space that represents the map of the game. 
1. The system will partition the map into grids that represent caverns of the map.
1. The system will render an object in a cavern that represents the player's character.
1. The player will move his character to adjective grid.
1. The system will render character's updated location.
1. The system will display clue when player move into specific caverns.
1. The player will gather clues to find the Wumpus.  
1. The player will equip his weapon.
1. The player will fire his weapon in a direction. 
1. The system will display a flying arrow moving to the direction of player's choice. 
1. The system will display "you win" if player has won, else display Wumpus eating player's character.
1. The system will reveal the map and all objects in the map. 
1. The system stops running. 
 









