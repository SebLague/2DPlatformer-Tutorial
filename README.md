# 2DPlatformer

The source code for the various episodes of my Unity tutorial series on creating a 2D platformer controller.
Available to watch here: http://bit.ly/2DController

Notes on implementation:
The player object should have a player-specific layer assigned to it. This layer must NOT be included in the Controller2D's collision mask. Moving platforms should contain the player layer in their passenger mask.
