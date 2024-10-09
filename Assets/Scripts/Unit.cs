using UnityEngine;

public class Unit : MonoBehaviour
{
    public MovementPath myPath;
    public float speed = 1f;
    public float maxDistance = .1f;
    private MovementController movementController;

    void Start()
    {
        movementController = new MovementController(this, myPath, true, speed, maxDistance);
    }

    void Update()
    {
        movementController.UpdateMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        Intersection intersection = other.GetComponent<Intersection>();
        if (intersection != null)
        {
            intersection.OnUnitEnter(this, myPath, movementController.CurrentPoint);
        }
    }


}
