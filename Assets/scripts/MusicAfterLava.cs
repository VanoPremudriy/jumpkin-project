using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAfterLava : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        music.clip = clip;
        music.Play();
    }
}
