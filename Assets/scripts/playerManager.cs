using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering;

[System.Serializable]
public class Arr
{
    public int[] items;
}
public class playerManager : MonoBehaviour
{

    [SerializeField] private int numberOfIngredients;
    [SerializeField] private float hungerInterval;
    [SerializeField] private float starveInterval;
    [SerializeField] private float healInterval;
    [Space]
    [Space]
    [SerializeField] private Arr[] recipes;
    [SerializeField] private Arr[] products;
    [SerializeField] private int menuWidth;
    [Space]
    [Space]
    [SerializeField] private bool clearSaveData;
    [Space]
    [SerializeField] private RectTransform healthButter;
    [SerializeField] private RectTransform hungerButter;
    [SerializeField] private GameEventSO fade;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private int[] hungerRestore;
    private int[] inventory;
    private string[] inventoryString;
    private string saveString;
    private int hunger = 255;
    private int health = 255;
    private float hungerTimer = 0;
    private float starveTimer = -1;
    private float healTimer = -1;
    private int index;
    private float minBar = 46.973f;
    private float maxBar = 347.7302f;
    private bool dead;
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
    public void Eat(Object sender, object data)
    {
        int i = (int)data;
        if (inventory.Length > i)
        {
            if (inventory[i] > 0 && hungerRestore[i]!=0)
            {
                hunger += hungerRestore[i];
                inventory[i]--;
                saveString = string.Empty;
                for (int a = 0; a < inventory.Length; a++)
                {
                    saveString += inventory[a].ToString() + "\n";
                }
                PlayerPrefs.SetString("INVENTORY", saveString);
                PlayerPrefs.Save();
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
    public void OnRecipe(Object sender, object data)
    {
        index = (((Vector2Int)data).y * menuWidth) + ((Vector2Int)data).x;
        index = Mathf.Clamp(index,0,recipes.Length-1);
        bool doCraft = true;
        for (int i = 0; i < recipes[index].items.Length; i++)
        {
            if (recipes[index].items[i] > inventory[i])
            {
                doCraft = false;
                continue;
            }
        }
        if (doCraft) 
        {
            for (int i = 0; i < recipes[index].items.Length; i++)
            {
                inventory[i] -= recipes[index].items[i];
                inventory[i] += products[index].items[i];
            }
            saveString = string.Empty;
            for (int i = 0; i < inventory.Length; i++)
            {
                saveString += inventory[i].ToString() + "\n";
            }
            PlayerPrefs.SetString("INVENTORY", saveString);
            PlayerPrefs.Save();
        }
    }
    private void Update()
    {
        hunger = Mathf.Clamp(hunger, 0, 255);
        health = Mathf.Clamp(health, 0, 255);
        if (hunger <= 0 && starveTimer == -1)
        {
            starveTimer = 9999;
        }
        if (hunger >= 100 && healTimer == -1) 
        {
            healTimer = 99999;
        }
        if (hunger > 0 && hunger < 100)
        {
            healTimer = -1;
            starveTimer = -1;
        }
        hungerTimer += Time.deltaTime;
        if (healTimer != -1)
        {
            healTimer += Time.deltaTime;
        }
        if (starveTimer != -1)
        {
            starveTimer += Time.deltaTime;
        }
        if (hungerTimer > hungerInterval)
        {
            hungerTimer = 0;
            hunger--;
        }
        if (healTimer > healInterval)
        {
            healTimer = 0;
            health++;
        }
        if (starveTimer > starveInterval)
        {
            starveTimer = 0;
            health--;
        }
        healthButter.sizeDelta = new Vector2(healthButter.sizeDelta.x, Mathf.Lerp(minBar, maxBar, (float)health / (float)256));
        hungerButter.sizeDelta = new Vector2(healthButter.sizeDelta.x, Mathf.Lerp(minBar, maxBar, (float)hunger / (float)256));
        if (clearSaveData) 
        {
            clearSaveData = false;
            PlayerPrefs.DeleteAll();
        }
        if (health == 0 && !dead)
        {
            dead = true;
            fade.RaiseEvent(this, (object)mainMenu);
            PlayerPrefs.DeleteAll();
        }
    }
}
