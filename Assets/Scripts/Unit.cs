using UnityEngine;

public class Unit : MonoBehaviour
{
    private MovementController movementController;
    public bool IsCat;

    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Intersection intersection = other.GetComponent<Intersection>();
        if (intersection != null)
        {
            intersection.OnUnitEnter(this, movementController.myPath, movementController.CurrentPoint);
        }
    }


}
