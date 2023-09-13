using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    public int terrainSize = 256;
    public float islandSize = 0.5f;
    public float centralIslandScale = 0.6f;
    public float smallerIslandsScale = 0.3f;
    public float heightMultiplier = 10f;
    public float perlinScale = 0.1f;

    private void Start()
    {
        GenerateIsland();
    }

    void GenerateIsland()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateHeightmap(terrain.terrainData);
    }

    TerrainData GenerateHeightmap(TerrainData terrainData)
    {
        terrainData.heightmapResolution = terrainSize + 1;
        terrainData.size = new Vector3(terrainSize, heightMultiplier, terrainSize);

        float[,] heights = new float[terrainSize, terrainSize];
        Vector2 islandCenter = new Vector2(terrainSize / 2, terrainSize / 2);

        for (int x = 0; x < terrainSize; x++)
        {
            for (int y = 0; y < terrainSize; y++)
            {
                // Calculate distances to island centers
                float distanceToCentralIsland = Vector2.Distance(new Vector2(x, y), islandCenter) / (terrainSize * centralIslandScale);
                // Calculate smaller island centers and distances
                // ...

                // Combine the heightmaps of central and smaller islands using falloff masks
                float centralIslandHeight = Mathf.Clamp01(Mathf.PerlinNoise(x * perlinScale, y * perlinScale) - distanceToCentralIsland);
                float smallerIslandsHeight = 0f; // Calculate height based on smaller island centers and falloff
                float totalHeight = Mathf.Max(centralIslandHeight, smallerIslandsHeight);

                // Apply height to terrain
                heights[x, y] = totalHeight;
            }
        }

        terrainData.SetHeights(0, 0, heights);

        return terrainData;
    }
}