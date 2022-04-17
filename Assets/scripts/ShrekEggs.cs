using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!
	\brief Класс отвечающий за паузу игры
*/

public class ShrekEggs : MonoBehaviour
{
    [SerializeField] AudioSource shrekVoice;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip allStars;
    [SerializeField] GameObject[] pump;
    [SerializeField] GameObject shrek;

    private async void OnTriggerEnter(Collider other) {
          shrekVoice.Play();
          audioSource.clip = allStars;
          audioSource.Play();
          for (int i =0; i < pump.Length; i++){
              pump[i].SetActive(false);
          }
          shrek.SetActive(true);
    }
}
