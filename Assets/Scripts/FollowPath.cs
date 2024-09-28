using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum MovementType
    {
        moving, // посто€нное движение 
        jerk    // рывок
    }

    public bool moveForward = true;
    public MovementType type = MovementType.moving;
    public MovementPath myPath;
    public float speed = 1f;
    public float maxDistance = .1f;
    public int nextPointIndex = 0;

    private Transform _pointInPath;  // проверка точек

    // Start is called before the first frame update
    void Start()
    {
        if (myPath == null)  // проверка на наличие пути
        {
            Debug.Log("ѕримени путь");
            return;  
        }

        nextPointIndex = -1;
        NextMove();

        if(_pointInPath == null)
        {
            Debug.Log("Ќужны точки");
            return;
        }

        transform.position = _pointInPath.position; 
    }

    void Update()
    {
        Movement();

    }

    private void Movement()
    {
        if (_pointInPath == null)
        {
            return;
        }

        if (type == MovementType.moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointInPath.position, Time.deltaTime * speed);
        }
        else if (type == MovementType.jerk)
        {
            transform.position = Vector3.Lerp(transform.position, _pointInPath.position, Time.deltaTime * speed);
        }

        float distannceSqure = (transform.position - _pointInPath.position).sqrMagnitude;

        if (distannceSqure < maxDistance * maxDistance)
        {
            NextMove();
        }
    }

    private void NextMove()
    {
        if (moveForward) 
        {
            if (_pointInPath == myPath.PathElements[myPath.PathElements.Length - 1])
            {
                Transition(myPath.ForwardPathStartPoint);
                return;
            }
        }
        else
        {
            if (_pointInPath == myPath.PathElements[0])
            {
                Transition(myPath.BackwardPathStartPoint);
                return;
            }
        }

        _pointInPath = GetNextPathPoint();
    }

    private void Transition(Transform newPathFirstPoint)
    {
        myPath = newPathFirstPoint.GetComponentInParent<MovementPath>();


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


        _pointInPath = newPathFirstPoint;
    }

    public Transform GetNextPathPoint()
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
