# NotFarm Project

This project is a Unity-based simulation game where players can interact with a garden environment. Players can collect seeds, water garden beds, and plant flowers that grow through different stages.

## Project Structure

- **Assets**
  - **_Project**
    - **Prefabs**
      - `Flower.prefab`: Prefab for the flower model with three growth stages.
      - `GardenBed.prefab`: Prefab representing the garden bed for planting flowers.
      - `Seed.prefab`: Prefab for the seed item that players can collect and use.
    - **Scripts**
      - **Environment**
        - `Bucket.cs`: Manages the bucket's water state and interactions.
        - `GardenBed.cs`: Manages the garden bed's wet and dry states, including watering.
        - `Seed.cs`: Represents the seed item with methods for interaction.
        - `Flower.cs`: Manages the flower's growth stages and interactions with the garden bed.
      - **Interface**
        - `ITool.cs`: Defines the contract for tools.
        - `IInventoryItem.cs`: Defines the contract for inventory items.
      - **Player**
        - `PlayerController.cs`: Manages player input and interactions, including planting seeds.
    - **Materials**
      - `DryMaterial.mat`: Material for the dry state of the garden bed.
      - `WetMaterial.mat`: Material for the wet state of the garden bed.
      - `FlowerStage1.mat`: Material for the first growth stage of the flower.
      - `FlowerStage2.mat`: Material for the second growth stage of the flower.
      - `FlowerStage3.mat`: Material for the third growth stage of the flower.
    - **Sprites**
      - `SeedIcon.png`: Icon for the seed item used in the inventory.
- **ProjectSettings**: Contains project settings for Unity.
- **Packages**: Contains package information for Unity.

## Gameplay Features

- Players can collect seeds and plant them in garden beds.
- Flowers grow through three stages, which are visually represented by different materials.
- The growth of flowers is dependent on the garden bed being watered.
- Players can water garden beds using buckets filled with water.

## How to Play

1. Collect seeds by interacting with seed items in the environment.
2. Approach a garden bed and press "E" to plant a seed if you have one in your inventory.
3. Use a bucket to water the garden bed to facilitate flower growth.
4. Watch the flowers grow through their stages as time progresses and the garden bed remains wet.

## Future Enhancements

- Implement additional plant types and growth mechanics.
- Add animations for planting and growing flowers.
- Create a user interface for inventory management and seed collection.