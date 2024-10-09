using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public MovementPath myPath;
    public Transform CurrentPoint;
    private bool moveForward;
    private int nextPointIndex;
    public float Speed;
    public float MaxDistance = 0.1f;

    
    private void Update()
    {
        UpdateMovement();
    }
    public void UpdateMovement()
    {
        if(CurrentPoint == null)
        {
            Debug.Log("Current point is null");
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, CurrentPoint.position, Time.deltaTime * Speed);

        float distanceSquared = (transform.position - CurrentPoint.position).sqrMagnitude;

        if (distanceSquared < MaxDistance * MaxDistance)
        {
            NextMove();
        }
    }

    public void NextMove()
    {
        if (moveForward)
        {
            if (CurrentPoint == myPath.PathElements[myPath.PathElements.Length - 1])
            {
                if (myPath.ForwardConnectedPath.isActiveAndEnabled)
                {
                    Transition(myPath.ForwardConnectedPath, true);
                }
                
                return;
            }
        }
        else
        {
            if (CurrentPoint == myPath.PathElements[0])
            {
                if (myPath.BackwardConnectedPath.isActiveAndEnabled)
                {
                    Transition(myPath.BackwardConnectedPath, false);
                }
                
                return;
            }
        }

        CurrentPoint = GetNextPathPoint();
    }

    public void Transition(Transform newPathFirstPoint)
    {
        myPath = newPathFirstPoint.GetComponentInParent<MovementPath>();
        if (myPath == null)
        {
            Debug.Log("Recieved point has no MovementPath");
            return;
        }


        if (myPath.PathElements[0] == newPathFirstPoint)
        {
            moveForward = true;
            nextPointIndex = 0;
        }
        else
        {
            moveForward = false;
            nextPointIndex = myPath.PathElements.Length - 1;

        }

        CurrentPoint = newPathFirstPoint;
    }

    public void Transition(MovementPath mp, bool forward)
    {
        myPath = mp;
        if (forward)
        {
            moveForward = true;
            nextPointIndex = 0;
            CurrentPoint = mp.PathElements[0];
        }
        else
        {
            moveForward = false;
            nextPointIndex = mp.PathElements.Length - 1;
            CurrentPoint = mp.PathElements[mp.PathElements.Length - 1];
        }
        
    }

    private Transform GetNextPathPoint()
    {
        if (moveForward == true)
        {
            nextPointIndex++;
        }
        else
        {
            nextPointIndex--;
        }

        return myPath.PathElements[nextPointIndex];
    }


}
