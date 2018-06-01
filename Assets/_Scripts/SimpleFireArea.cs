using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleFireArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    private bool touched;
    private int pointerID;
    private bool canFire;

    void Awake()
    {
        touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = true;
            pointerID = eventData.pointerId;
            canFire = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId.Equals(pointerID))
        {
            canFire = false;
            touched = false;
        }
    } 

    public bool CanFire()
    {
        return canFire;
    }
}
