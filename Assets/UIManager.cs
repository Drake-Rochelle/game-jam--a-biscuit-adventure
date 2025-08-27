using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform UI;
    [SerializeField] private float speed;
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private bool up;

    public void OnUI()
    {
        up = !up;
        Time.timeScale = Time.timeScale != 0 ? 0 : 1;
    }
    void Update()
    {
        if (up)
        {
            UI.anchoredPosition -= new Vector2(speed, 0);
        }
        else
        {
            UI.anchoredPosition += new Vector2(speed, 0);
        }
        UI.anchoredPosition = new Vector2(Mathf.Clamp(UI.anchoredPosition.x, min, max), UI.anchoredPosition.y);
    }
}
