using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarySounds : MonoBehaviour {
    public AudioClip scareSound;

    private bool hasPlayesAudio;

    float timeLeft = 60.0f;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            hasPlayesAudio = false; 
            timeLeft = 60.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasPlayesAudio == false)
        {
            AudioSource.PlayClipAtPoint(scareSound, transform.position);
            hasPlayesAudio = true;
        }
    }
}
