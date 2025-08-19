# No Base-Specific CC Breach Effects
Allows CC makers to easily remove base-specific breach character effects like convict's smoke, hunter's dog or gunslinger's 7 credit cost. To do this, just create a file named `{name}-ccremove.spapi` (where `{name}` is the name of your mod) in your mod's files and put the nameShorts of all custom characters you want to apply this to. There can be as many of `-ccremove.spapi` files as you want and one file can contain unlimited character nameShorts, one nameShort for each line.

# Example
Here's an example of a file that would remove the breach dog from Dallan's [Omicron](https://enter-the-gungeon.thunderstore.io/package/Dallan/Omicron_the_Loop_Hero_Lich_Custom_Character/) custom character:  
`Omicron-ccremove.spapi` (name of the file)
```
Omicron
```