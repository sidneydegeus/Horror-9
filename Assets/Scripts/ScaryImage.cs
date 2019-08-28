using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryImage : MonoBehaviour
{
    public GameObject imagePanel;

    private bool hasPlayedImage;

    float timeLeft = 60.0f;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            hasPlayedImage = false;
            timeLeft = 60.0f;
        }
    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasPlayedImage == false)
        {
            imagePanel.SetActive(true);
            yield return new WaitForSeconds(2);
            imagePanel.SetActive(false);
            hasPlayedImage = true;
        }
    }
}
