using UnityEngine;
using UnityEngine.UI;


public class fadeManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameEventSO sceneSwitch;
    [SerializeField] private float fadeInTimer = -1;
    [SerializeField] private float fadeOutTimer = -1;
    [SerializeField] private float fadeTime = 1;
    private bool swap;
    [SerializeField] private int frame = -1;
    private object sceneObject;
    [SerializeField] private int frameCount;
    private void Awake()
    {
        frameCount = Time.frameCount;
        frame = 0;
    }
    public void Fade(Object sender, object scene)
    {
        fadeInTimer = 0;
        sceneObject = scene;
    }
    private void Update()
    {
        if (frame != -1)
        {
            frame++;
        }
        if (frame == frameCount + 20)
        {
            fadeOutTimer = 0;
            frame = -1;
        }
        if (swap)
        {
            sceneSwitch.RaiseEvent(this, sceneObject);
            frame = 0;
            swap = false;
        }
        if (Time.frameCount == 20)
        {
            fadeOutTimer = 0;
        }
        if (fadeInTimer != -1)
        {
            fadeInTimer += Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, fadeInTimer / Time.timeScale);
            if (fadeInTimer > fadeTime * Time.timeScale)
            {
                fadeInTimer = -1;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                swap = true;
            }
        }
        else if (fadeOutTimer != -1)
        {
            fadeOutTimer += Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - (fadeOutTimer / Time.timeScale));
            if (fadeOutTimer > fadeTime * Time.timeScale)
            {
                fadeOutTimer = -1;
            }
        }
    }
}
