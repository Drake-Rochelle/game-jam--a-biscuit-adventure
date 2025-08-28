using UnityEngine;
[System.Serializable] class arr
{
    public GameObject[] objs;
}
[System.Serializable] class arrInt
{
    public int[] values;
}

public class objectManager : MonoBehaviour
{
    [SerializeField] private arr[] objects;
    [SerializeField] private arrInt[] total;
    [SerializeField] private Terrain terrain;
    private Vector3 pos;
    private GameObject obj;
    private int seed;
    [SerializeField] private Texture2D texture;
    [SerializeField] private Vector2 bounds;
    private int biome;
    private int GetDominantTerrainLayerIndex(GameObject terrainObject, Transform targetTransform)
    {
        Terrain terrain = terrainObject.GetComponent<Terrain>();
        if (terrain == null) return -1;

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;

        int mapX = Mathf.FloorToInt((targetTransform.position.x - terrainPos.x) / terrainData.size.x * terrainData.alphamapWidth);
        int mapZ = Mathf.FloorToInt((targetTransform.position.z - terrainPos.z) / terrainData.size.z * terrainData.alphamapHeight);

        mapX = Mathf.Clamp(mapX, 0, terrainData.alphamapWidth - 1);
        mapZ = Mathf.Clamp(mapZ, 0, terrainData.alphamapHeight - 1);

        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        int dominantIndex = 0;
        float maxWeight = 0f;

        for (int i = 0; i < terrainData.alphamapLayers; i++)
        {
            float weight = splatmapData[0, 0, i];
            if (weight > maxWeight)
            {
                maxWeight = weight;
                dominantIndex = i;
            }
        }

        return dominantIndex;
    }
    Color Sample(Vector3 pos)
    {
        if (texture == null || bounds.x <= 0f || bounds.y <= 0f) return Color.clear;
        return texture.GetPixel(Mathf.FloorToInt(Mathf.Clamp01(pos.x / bounds.x) * texture.width), Mathf.FloorToInt(Mathf.Clamp01(pos.z / bounds.y) * texture.height));
    }
    int Biome(Color color)
    {
        color.a = 1-color.a;
        if (color.r>color.g && color.r > color.b && color.r > color.a)
        {
            return 0;
        }
        else if (color.g > color.r && color.g > color.b && color.g > color.a)
        {
            return 1;
        }
        else if (color.b > color.r && color.b > color.g && color.b > color.a)
        {
            return 2;
        }
        else if (color.a > color.r && color.a > color.g && color.a > color.b)
        {
            return 3;
        }
        return 0;
    }


    void Awake()
    {
        seed = PlayerPrefs.GetInt("SEED");
        if (seed == 0) 
        {
            seed = Random.Range(1, 999999999);
            PlayerPrefs.SetInt("SEED", seed);
            PlayerPrefs.Save();
        }
        Random.InitState(seed);
        for(int b = 0; b < 4; b++)
        {
            for (int a = 0; a < objects[b].objs.Length; a++)
            {
                for (int i = 0; i < total[b].values[a]; i++)
                {
                    pos.x = Random.Range(0, terrain.terrainData.size.x);
                    pos.z = Random.Range(0, terrain.terrainData.size.z);
                    pos.y = terrain.SampleHeight(pos) + 1;
                    biome = Biome(Sample(pos));
                    if (biome == b) 
                    {
                        obj = Instantiate(objects[b].objs[a], pos, Quaternion.identity);
                        obj.transform.SetParent(transform);
                    }
                }
            }
        }
    }
}
