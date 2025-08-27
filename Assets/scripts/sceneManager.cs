using UnityEngine;


public class sceneManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuScene;
    private GameObject currentScene;


    void Start()
    {
        currentScene = Instantiate(mainMenuScene);
    }
    public void loadScene(Object sender, object scene)
    {
        Destroy(currentScene);
        currentScene = Instantiate((GameObject)scene);
    }
}
