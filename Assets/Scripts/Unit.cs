using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private MovementController movementController;

    private Collider hitbox;

    public float DeathTime = 0.7f;
    void Start()
    {
        hitbox = GetComponent<Collider>();
        movementController = GetComponent<MovementController>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("DeathTrap"))
        {
            Die();
        }

        if (this.CompareTag("Mouse") && collider.CompareTag("Salvation"))
        {
            Saved();
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
        hitbox.enabled = false;

        GetComponentInChildren<FaceAnimator>().PlayDeathAnim();

        movementController.AllowMovement = false;

        if (this.CompareTag("Cat"))
        {
            LevelManager.CatDied?.Invoke();
        }

        if (this.CompareTag("Mouse"))
        {
            LevelManager.MouseDied?.Invoke();
        }
        StartCoroutine(WaitForDeath());
        
    }

    private void Saved()
    {
        
    }


    public IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(DeathTime);
        Destroy(this.gameObject);
    }

}
