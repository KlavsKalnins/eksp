# eksp
Unity: 2021.1.0f1

6-7 hours of progress

[Video Demo](~/readme/demo.mp4)

<video width="720" controls>
  <source src="~/readme/demo.mp4" type="video/mp4">
</video>

## How to test
- By holding left click on the green panel you can rotate the player - it will rotate even verticaly etc.
- Right clicking an item will equip/unequip or replace another item depending on quality
## Implementation

- Extensible inventory system
- Managable Items
- If there is already everything equipped then it will swap out the worst item depending on the average
- Inventory saves and loads from Scriptable Objects
- if the developer chooses he can have from 0 to +99 accessory items, as it will stack
- Shows the players stats depending on the equipment worn


## Flaws

- Arhitecture might not be the best as it can be repetitive implementing UnityActions for the display of player equipment
- didn't add armor and accessory items
- Flaw or pro but you cannot have the same item in the inventory, just like a block in a blockchain it should be unique - items has Guid
