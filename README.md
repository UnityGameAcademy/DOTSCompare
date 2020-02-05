# DOTSCompare

This project compares "classic" Unity workflows against their DOTS/ECS counterparts.

The demo uses two Monobehaviours, EnemySpawner and GameManager, to spawn the GameObjects or Entities.

To switch modes, set the GameManager's Mode to Classic/ECS Pure/ECS Conversion.

Check "Use Jobs" and "Use Burst" when testing with ECS to compare (note: Burst only makes sense when Jobs is enabled).

At runtime, use the spacebar to generate more cubes. Adjust the "SpawnIncrement" to control how many cubes are created for each keypress.

Though results will vary depending on hardware, here is what happened on my laptop with ECS Pure:

*"Classic" mode could generate just under 15,000 cubes to remain about 30fps.
*"ECS" with Jobs and Burst disabled could generate ~35,000 cubes at 30fps
*"ECS" with Jobs only enabled: 110,000 cubes at 30fps
*"ECS" with Jobs + Burst enabled: 170,000 cubes at 30fps

There is a small performance hit between ECS Pure and ECS Conversion.  The Conversion Workflow does require a small amount of additional overhead.

Open up the scripts to compare the different syntax used to generate the examples.  

Be sure to follow along with the YouTube channel for more explanations:
https://www.youtube.com/channel/UCWERX3S8tEGqNeLuQGCcJmw

Don't forget to check out these "classic" courses as well:
https://gameacademy.school/portfolio/

Stay tuned for more DOTS videos!

