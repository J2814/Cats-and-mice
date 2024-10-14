using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private MovementController movementController;

    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Unit>() == null) return;

        Unit otherUnit = collider.gameObject.GetComponent<Unit>();

        if(this.CompareTag("Cat") && otherUnit.CompareTag("Cat"))
        {
            Die();
        }
        else if (this.CompareTag("Mouse") && otherUnit.CompareTag("Cat"))
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }

}
