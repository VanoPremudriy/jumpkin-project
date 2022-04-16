using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekEggs : MonoBehaviour
{
    [SerializeField] AudioSource shrekVoice;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip allStars;
    [SerializeField] GameObject[] pump;
    [SerializeField] GameObject shrek;

    bool isTrue =false;
    // Start is called before the first frame update
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {
    }

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
