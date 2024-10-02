using UnityEngine;

public class Intersection : MonoBehaviour
{
    public MovementPath[] availablePaths; // Доступные пути из перекрестка
    public KeyCode switchKey; // Клавиша для переключения на этот перекресток

    // Метод для получения доступных путей
    public MovementPath[] GetAvailablePaths()
    {
        return availablePaths;
    }
}
