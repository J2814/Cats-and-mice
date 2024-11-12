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
        if (collider.CompareTag("DeathTrap"))
        {
            Die();
        }

        if (collider.gameObject.GetComponent<Unit>() == null) return;

        Unit otherUnit = collider.gameObject.GetComponent<Unit>();

        if(this.CompareTag("Cat") && otherUnit.CompareTag("Cat"))
        {
            //if (this.GetInstanceID() < otherUnit.GetInstanceID())
            Die(); 
           
        }
        else if (this.CompareTag("Mouse") && otherUnit.CompareTag("Cat"))
        {
            Die();
        }
    }
    private void Die()
    {
        if (this.CompareTag("Cat"))
        {
            LevelManager.CatDied?.Invoke();
        }

        if (this.CompareTag("Mouse"))
        {
            LevelManager.MouseDied?.Invoke();
        }

        Destroy(gameObject);
    }

}
