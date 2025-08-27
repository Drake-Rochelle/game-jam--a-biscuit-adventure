using UnityEngine;

public class objectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private int[] total;
    [SerializeField] private Terrain terrain;
    private Vector3 pos;
    private GameObject obj;
    private int seed;
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
        for (int a = 0; a < objects.Length; a++) 
        {
            for (int i = 0; i < total[a]; i++)
            {
                pos.x = Random.Range(0, terrain.terrainData.size.x);
                pos.z = Random.Range(0, terrain.terrainData.size.z);
                pos.y = terrain.SampleHeight(pos)+1;
                obj = Instantiate(objects[a], pos, Quaternion.identity);
                obj.transform.SetParent(transform);
            }
        }
    }
}
