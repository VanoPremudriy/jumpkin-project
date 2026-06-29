using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!
	\brief Класс, отвечающий за вращение предметов
*/
public class CoinRotation : MonoBehaviour
{
    [SerializeField] float rotateValue = 30f; 

    // Start is called before the first frame update
    private void FixedUpdate() {
        transform.Rotate(0, rotateValue * Time.deltaTime, 0);
    }
}
