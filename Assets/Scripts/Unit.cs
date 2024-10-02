using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum MovementType
    {
        moving, 
        jerk    
    }

    public bool moveForward = true; 
    public MovementType type = MovementType.moving;
    public MovementPath myPath;
    public float speed = 1f;
    public float maxDistance = .1f;
    public int nextPointIndex = 0;

    private Transform _pointInPath;  

    void Start()
    {
        if (myPath == null) 
        {
            Debug.Log("Path is null");
            return;  
        }

        nextPointIndex = -1;
        NextMove();

        if(_pointInPath == null)
        {
            Debug.Log("Points is null");
            return;
        }

        transform.position = _pointInPath.position; 
    }

    void Update()
    {
        Movement();
        CheckForSwitch(); // check the keystroke to switch
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

    private int switchPressCount = 0; // Счетчик нажатий клавиш

    private void CheckForSwitch()
    {
        // Получаем все объекты типа Intersection
        Intersection[] intersections = FindObjectsOfType<Intersection>();

        foreach (var intersection in intersections)
        {
            if (Input.GetKeyDown(intersection.switchKey))
            {
                switchPressCount++; // Увеличиваем счетчик нажатий
                SwitchPath(intersection.GetAvailablePaths());
            }
        }
    }

    private void SwitchPath(MovementPath[] newPaths)
    {
        if (newPaths.Length > 0)
        {
            // Вычисляем индекс следующего пути на основе счетчика нажатий
            int nextPathIndex = switchPressCount % newPaths.Length; // Циклический выбор пути

            myPath = newPaths[nextPathIndex]; // Переключаемся на путь по индексу

            // Устанавливаем nextPointIndex в 0 и обновляем целевую точку
            nextPointIndex = 0; // Начинаем с первой точки нового пути
            _pointInPath = myPath.PathElements[nextPointIndex]; // Устанавливаем новую целевую точку

            // Дополнительно, если хотите сохранить текущее положение, можно найти ближайшую точку на новом пути:
            float closestDistance = float.MaxValue;
            int closestIndex = 0;

            // Ищем ближайшую точку на новом пути
            for (int i = 0; i < myPath.PathElements.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, myPath.PathElements[i].position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            // Устанавливаем позицию объекта на ближайшую точку на новом пути
            transform.position = myPath.PathElements[closestIndex].position;
            nextPointIndex = closestIndex; // Устанавливаем индекс на ближайшую точку
            _pointInPath = myPath.PathElements[nextPointIndex]; // Устанавливаем целевую точку
        }
    }


}
