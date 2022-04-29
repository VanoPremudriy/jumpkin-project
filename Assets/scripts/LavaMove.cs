using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*!
	\brief Класс отвечающий за движение объектов по осям
*/

public class LavaMove : MonoBehaviour
{

    ///Минимальная координата
    public float minY;

    ///Максимальная координата
    public float maxY;

    ///Скорость
    public float ySpeed;

    ///Логические поля
    bool inMax, inMin;

    ///Векто передвижения
    Vector3 vector3;

    void Start() ///Стартовый метод
    {
        inMax = true;
        minY = transform.position.y;
    }

     void FixedUpdate() {
        inY();
    }
    

    void inY() ///Движение по оси Y
    {
        if (transform.position.y <= maxY)
        transform.Translate(new Vector3(0, ySpeed, 0) * Time.deltaTime);
    }
}
