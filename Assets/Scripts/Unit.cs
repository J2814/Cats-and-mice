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
        else if (this.CompareTag("Mouse") && otherUnit.CompareTag("Mouse"))
        {
            Ignore(otherUnit);
        }
    }

    private void Ignore(Unit otherUnit)
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), otherUnit.GetComponent<Collider>());
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
