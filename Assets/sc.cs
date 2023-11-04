using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSimpleLandscape : MonoBehaviour
{
    public int terrainWidth = 100;  // Width of the terrain
    public int terrainLength = 100; // Length of the terrain
    public int terrainHeight = 20;  // Maximum height of the terrain

    private void Start()
    {
        // Create a new terrain
        Terrain terrain = Terrain.CreateTerrainGameObject(new TerrainData()).GetComponent<Terrain>();

        // Set terrain size
        terrain.terrainData = new TerrainData
        {
            size = new Vector3(terrainWidth, terrainHeight, terrainLength),
            heightmapResolution = terrainWidth + 1,
            alphamapResolution = terrainWidth + 1,
        };

        // Generate a simple heightmap
        float[,] heights = new float[terrainWidth, terrainLength];
        for (int x = 0; x < terrainWidth; x++)
        {
            for (int y = 0; y < terrainLength; y++)
            {
                heights[x, y] = Mathf.PerlinNoise(x * 0.1f, y * 0.1f) * terrainHeight;
            }
        }
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
