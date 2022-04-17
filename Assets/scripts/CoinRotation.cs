using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!
	\brief Класс, отвечающий за вращение предметов
*/

public class CoinRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate() {
        transform.Rotate(0, 30 * Time.deltaTime, 0);
    }
}
