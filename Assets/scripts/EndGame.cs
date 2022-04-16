using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject endGameMenu;
    [SerializeField] GameObject _interface;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject.FindGameObjectWithTag("EndGameMenu").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        other.gameObject.SetActive(false);
        endGameMenu.SetActive(true);
        _interface.SetActive(false);
        Cursor.visible = true;
         Time.timeScale = 0f;
         Cursor.lockState = CursorLockMode.None;
        
    }
}
