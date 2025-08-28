using UnityEngine;

public class buttonManager : MonoBehaviour
{
    [SerializeField] private GameEventSO recipe;
    private Vector2Int i;
    private void Start()
    {
        i.y = int.Parse(transform.parent.name);
        i.x = int.Parse(name);
    }
    public void OnClick()
    {
        recipe.RaiseEvent(this, (object)i);
    }
}
