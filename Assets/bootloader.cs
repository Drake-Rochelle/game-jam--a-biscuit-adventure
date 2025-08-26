using UnityEngine;

public class bootloader : MonoBehaviour
{
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject parent;
    void Start()
    {
        Instantiate(game);
        Destroy(parent);
    }
}
