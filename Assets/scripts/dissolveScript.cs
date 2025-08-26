using UnityEngine;

public class dissolveScript : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Transform ingredient;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameEventSO objectPickup;
    private float timer = -1;
    private Collider[] overlaps;
    public void Dissolve()
    {
        overlaps = Physics.OverlapSphere(transform.position, checkRadius,playerLayer);
        if (overlaps.Length > 0 && timer == -1) 
        {
            timer = 0;
            objectPickup.RaiseEvent(this, null);
        }
    }
    void Update()
    {
        if (timer != -1)
        {
            Destroy(parent);
        }
    }
}
