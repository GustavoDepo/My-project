using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Lazer : MonoBehaviour
{
    private LineRenderer lazerBeam;

    void Start()
    {
        lazerBeam = GetComponent<LineRenderer>();
    }

    // Здесь параметр — одиночная точка Vector2
    public IEnumerator lazerMove(Vector2 coord)
    {
        // выводим координаты одной точки
        Debug.Log("Значение координат для лазера: " + coord.ToString());

        // пример установки позиций на LineRenderer (подстроить под вашу логику!)
        // coord.x и coord.y — скалярные значения
        lazerBeam.SetPosition(2, new Vector3(0 + -1*coord.y, -8.57f, 5f + 1*coord.x));

        // если хотите анимацию движения лазера — здесь цикл с шагом и yield return null

        yield return null;
    }
}
