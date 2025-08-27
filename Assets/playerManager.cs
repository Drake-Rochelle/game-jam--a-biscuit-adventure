using UnityEngine;

public class playerManager : MonoBehaviour
{
    [SerializeField] private int numberOfIngredients;
    [SerializeField] private bool clearSaveData;
    private int[] inventory;
    private string[] inventoryString;
    private string saveString;
    private void Awake()
    {
        inventoryString = PlayerPrefs.GetString("INVENTORY").Split("\n");
        inventory = new int[numberOfIngredients];   
        if (inventoryString[0]!=string.Empty)
        {
            for (int i = 0; i < Mathf.Min(numberOfIngredients, inventoryString.Length); i++)
            {
                if (inventoryString[i] != "")
                {
                    inventory[i] = int.Parse(inventoryString[i]);
                }
            }
        }
    }
    public void OnPickup(Object sender, object data)
    {
        inventory[int.Parse(sender.name)]++;
        saveString = string.Empty;
        for (int i = 0; i < inventory.Length; i++)
        {
            saveString += inventory[i].ToString() + "\n";
        }
        PlayerPrefs.SetString("INVENTORY", saveString);
        PlayerPrefs.Save();

    }
    private void Update()
    {
        if (clearSaveData) 
        {
            clearSaveData = false;
            PlayerPrefs.DeleteAll();
        }
    }
}
