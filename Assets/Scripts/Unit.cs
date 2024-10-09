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
        
    }


}
