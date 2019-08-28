using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public int itemID;
    public Inventory inventory;
    public GameObject textPanel;
    public AudioClip pickupSound;
    public bool active;

    private void Update()
    {
        if (active && Input.GetKeyDown(KeyCode.E))
        {
            addToInventory(itemID);
            this.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            textPanel.SetActive(false);
            active = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textPanel.SetActive(true);
            active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        textPanel.SetActive(false);
        active = false;
    }

    public void addToInventory(int itemID)
    {
        inventory.AddItem(itemID);
    }
}
