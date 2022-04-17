using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!
	\brief Класс, отвечающий логику игры, после завершения уровня
*/

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject endGameMenu;
    [SerializeField] GameObject _interface;
    
    private void OnTriggerEnter(Collider other) {
        other.gameObject.SetActive(false);
        endGameMenu.SetActive(true);
        _interface.SetActive(false);
        Cursor.visible = true;
         Time.timeScale = 0f;
         Cursor.lockState = CursorLockMode.None;
        
    }
}
