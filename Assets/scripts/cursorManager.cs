using UnityEngine;
using UnityEngine.InputSystem;

public class cursorManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cursors;
    public void onPickup()
    {
        cursors[0].SetActive(!cursors[0].activeInHierarchy);
        cursors[1].SetActive(!cursors[1].activeInHierarchy);
    }
}
