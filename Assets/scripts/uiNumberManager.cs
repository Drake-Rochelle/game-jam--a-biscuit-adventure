using TMPro;
using UnityEngine;

public class uiNumberManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private string[] str;
    
    void Update()
    {
        str = PlayerPrefs.GetString("INVENTORY").Split("\n");
        tmp.text = "0";
        if (str.Length > int.Parse(name))
        {
            if (str[int.Parse(name)] != "")
            {
                tmp.text = str[int.Parse(name)];
            }
        }
    }
}
