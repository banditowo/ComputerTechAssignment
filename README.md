# ComputerTechAssignment
Simple space shooter game assignment using DOTS (Data-Oriented Technology Stack)ECS (Entity Component System) for better optimization; makes it easier and lighter to spawn bigger amounts of objects by programming and handling objects in a data oriented way.

Controls;

A/D - Rotate player

LMB - Shoot

R - Restart

Q - Quit


I did this assignment a bit backwards as I tried to get it done as fast as I could first since it was way over the due date and went straight into making it using DOTS; I then made it how I would’ve normally made it, as a different project, to then start comparing the two.

Here are some of the comparisons;

the lowest frames it could drop to:

![image](https://github.com/banditowo/ComputerTechAssignment/assets/105817022/ce3f7b16-c444-4b7b-9cb4-42a814a583b4)
![image](https://github.com/banditowo/ComputerTechAssignment/assets/105817022/b7b6249d-2965-4f1e-ad2e-be80473d7e70)

I tested this by just letting the screen get filled with the enemies and as seen, the one with the blue background is the one made by instantiating gameobjects, and the one with the black background is the one using the DOTS system by using busrtas and jobs etc. (I honestly forgot the logic behind it)

and the frames it would stay at by ‘playing’ the game and continuously shooting the enemies:

![image](https://github.com/banditowo/ComputerTechAssignment/assets/105817022/6148c7de-6d01-4071-a21c-c6c609c00f3d)
![image](https://github.com/banditowo/ComputerTechAssignment/assets/105817022/3fca8804-f41d-4ef8-af1c-706fa9ce5d9c)


 
