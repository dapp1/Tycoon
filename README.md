# **Tycoon**

**How to test:**
1. Use a screen resolution of 1080x2400 or 1080x2340 and the Game window in the editor.
![image](https://github.com/user-attachments/assets/14e6a8ad-e9de-4f1d-8334-5c08d130919f)
2. Open the GameScene under the Assets/Scenes path and launch play mode
![image](https://github.com/user-attachments/assets/df2ca668-42e3-4bab-acbb-0e3205295f47)

**What was done in this project (without using external APIs like Zenject, DOTween, etc.):**

- System of resource production through buildings.

- Building improvement system:
  - Production speed.
  - Amount of resource produced.
  - Number of workers (only affects the start of production, can be expanded).

- Building configuration via ScriptableObjects.

- Resource system.

- Save progress to a JSON file.

- DI container similar to Zenject.

- Optimization of the scene from the asset store and restructuring of the scene.

- Simple bots using NavMesh: move from point to point.

- The game is made in LowPoly style.

**Use Camera**

Camera movement:

- The camera moves around the scene using the W, A, S, D keys.
  - W - forward movement.
  - S - backward movement.
  - A - movement to the left.
  - D - movement to the right.
- Camera scaling (zoom)
  - Use the mouse wheel to zoom the camera
 
 
