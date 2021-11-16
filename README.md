# betrayal-recreation
A passion project set on recreating Avalon Hill's Betrayal at House on the Hill for digitial use

[[_TOC_]]
## **Classes**
### **Player**
The Player class is the class the user uses to interact with the game. This class has properties to track room buffs, current stat indices, current room tile information, and hold cards picked up in game.

**Constructor**
`public Player(Character details, string name, bool ishuman)`
Player takes a character object to use in game, a screenname, and a boolean value that determines if the player is a CPU or Human operated player.
### **Character**
The Character class is the class used to define all playable characters the players can use in game. These characters are assigned a set of stats, token color, name and other identifying pieces of information.
**Constructor**
`public Character(int id, string name, string birth, string color, int knowI, int sanI, int mitI, int spdI, int[] knowA, int[] sanA, int[] mitA, int[] spdA)`
This constructor is possibly the longest constructor function in the whole api. The parameters indicated populate the stat blocks, starting indices in those stat blocks, and personal identity information. 

### **Room**
The Room class contains all information about any one room tile. Rooms in Betrayal can contain a Card, doors, stairs or special movement rules that this class stores and checks for every room.

**Constructor**
`public Tile(int id, string name, string[] level, string card)`
This constructor takes a tile id, room name, an array of levels this room can be found on, and the card type that can be found in this room.

### **Card**
The Card class is used to store information about cards in the game. 
Cards can vary in type from Items, Events and Omens, each with their own special characteristics affecting anywhere from the entire game or one specific player.

No other information available as this feature is still in progress.

## **Mechanics**
Found in the Mechanics.cs file, the Game Class contains all logic for controlling aspects of the game.
```
public Game(Assembly assembly, string ResourcePath) {
     packName = ResourcePath.Split('\\')[ResourcePath.Split('\\').Length - 1];
     Setup(ResourcePath);           
}
```
This constructor sets a variable named `packName` equal to the name of the last folder in the `ResourcePath` then runs a function called `Setup(ResourcePath)`.
