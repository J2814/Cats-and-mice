using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
   public enum PathTypes
    {
        opened, //незамкнутая 
        closed  // зацикленная
    }

    public PathTypes pathType;        // настоящий тип пути
    public int movementDirection = 1; // направление движения 1/-1
    public int moveingTo = 0;          // к какой точке двигаемся
    public Transform[] PathElements;  // массив из точек движения

    public void OnDrawGizmos()        // отображает линии между точками пути
    {
        if (PathElements == null || PathElements.Length < 2) return;

        for (int i = 1; i < PathElements.Length; i++)  // все точки массива
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position); // рисует линию между двумя точками
        }

        if(pathType == PathTypes.closed) // если зацикленный тип
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position); //рисуем от 1 к последней
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()  //получает положение следующей точки
    {
        if(PathElements == null || PathElements.Length < 1) // есть ли точки которым нужно проверять положение
        {
            yield break;
        }

        while(true)
        {
            yield return PathElements[moveingTo];

            if(PathElements.Length == 1) continue; // если точка одна, то выйти

            if(pathType == PathTypes.opened)     // если линия не зациклена
            {
                if(moveingTo <= 0)  // если двигаемся по нарастающей
                {
                    movementDirection = 1; // +1 к движению
                }
                else if(moveingTo >= PathElements.Length - 1)  // если двигаемся по убывающей
                {
                    movementDirection = -1;   
                }
            }

            moveingTo = moveingTo + movementDirection; // диапазон движения от -1 до 1

            if(pathType == PathTypes.closed)
            {
                if(moveingTo >= PathElements.Length)
                {
                    moveingTo = 0;
                }

                if(moveingTo < 0)
                {
                    moveingTo = PathElements.Length - 1;
                }
            }
        }
    }
}
