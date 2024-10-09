using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Unit _unit;
    private MovementPath _myPath;
    public Transform CurrentPoint;
    private bool _moveForward;
    private int _nextPointIndex;
    private float _speed;
    private float _maxDistance;

    public MovementController(Unit unit, MovementPath path, bool moveForward, float speed,  float maxDistance)
    {
        _unit = unit;
        _myPath = path;
        _moveForward = moveForward;
        _speed = speed;
        _maxDistance = maxDistance;

        CurrentPoint = path.PathElements[0];
        _nextPointIndex = 0;
    }

    public void UpdateMovement()
    {
        if(CurrentPoint == null)
        {
            Debug.Log("Current point is null");
            return;
        }

        _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, CurrentPoint.position, Time.deltaTime * _speed);

        float distanceSquared = (_unit.transform.position - CurrentPoint.position).sqrMagnitude;

        if (distanceSquared < _maxDistance * _maxDistance)
        {
            NextMove();
        }
    }

    public void NextMove()
    {
        if (_moveForward)
        {
            if (CurrentPoint == _myPath.PathElements[_myPath.PathElements.Length - 1])
            {
                Transition(_myPath.ForwardPathStartPoint);
                return;
            }
        }
        else
        {
            if (CurrentPoint == _myPath.PathElements[0])
            {
                Transition(_myPath.BackwardPathStartPoint);
                return;
            }
        }

        CurrentPoint = GetNextPathPoint();
    }

    public void Transition(Transform newPathFirstPoint)
    {
        _myPath = newPathFirstPoint.GetComponentInParent<MovementPath>();
        if (_myPath == null)
        {
            Debug.Log("Recieved point has no MovementPath");
            return;
        }


        if (_myPath.PathElements[0] == newPathFirstPoint)
        {
            _moveForward = true;
            _nextPointIndex = 0;
        }
        else
        {
            _moveForward = false;
            _nextPointIndex = _myPath.PathElements.Length - 1;

        }

        CurrentPoint = newPathFirstPoint;
    }

    private Transform GetNextPathPoint()
    {
        if (_moveForward == true)
        {
            _nextPointIndex++;
        }
        else
        {
            _nextPointIndex--;
        }

        return _myPath.PathElements[_nextPointIndex];
    }


}
