# unity-architectures
The repo demonstrates a variety of different approaches to the same problem, creating a simple Asteroids game in Unity.

## Why
Unity projects involve a confluence of design philosophies from the engine team, plugin apis, and single or team of game developers.  It's fascinating to walk in another developer's shoes and understand how they solve problem in a completely different way, sometimes equally valid, other times with notable drawbacks.

This repository demonstrates some simple theses extended out to a variation of the classic Asteroids game.  By making the same game with different implementations, it becomes easy to compare and contrast the approaches.

Implementing such a simple project is absolutely NOT sufficent to draw conclusions on the validity of an architecture when extended to a large project.  To be fair, it is very hard to take ANY repository and draw conclusions on how its architecture would apply to a different game.

My hope is that looking at the source code of various implementations invokes some curiosity and expands the reader's knowledge-base.  I also plan to use it as reference material for instruction related to design variations.

I am happy to accept pull requests, please follow the general code standards for consistency.

## Architectures
The implementations can be found in the Assets/Implementations folder.

### EntityComponent
Entity component uses the Entitas Entity-Component framework and breaks all features into Systems.  It uses a pure ECS meaning components have no functionality, only data.  Unity's GameObjects are simply used as views, core logic is almost completely decoupled from the "Unity way".

The philosophy of entity component is to make systems highly decoupled and easily testable.  Currently there are no unit tests to validate that philosophy, but they may be added in the future.

### Kiss (Keep it simple, stupid)
This is an example of a prototype architecture that uses the core systems of Unity, including a MonoBehaviour component approach, UnityEvents, prefab instantiation, and configurable properties in the inspector.  

Components were created as needed, with the core philosophy being don't write code until it's needed, but if it's not extra work keep behaviors flexible and tweakable.

### NoOO (No object-oriented style)
This architecture, or you might call it an anti-architecture, avoids object-oriented principles and uses Unity sparingly.  

Almost all code is in one class, specifically one update loop.  It is not very customizable since tweakable parameters are all magic numbers.  Gameplay objects are generated in code rather than prefabs.

## Future Additions
I'd like to add a couple additional architectures that are popular.
- Inheritance-based
- Data oriented, decoupled from Unity like the Entitas example but not using ECS
- Dependecy Injection/Everything is a binding (Zenject?)
- MVVVM or MVC