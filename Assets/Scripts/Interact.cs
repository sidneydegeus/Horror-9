using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour {

    public float interactDistance = 2f;
    public Inventory inventory;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance)) {
                if (hit.collider.CompareTag("Door"))
                {
                    hit.collider.transform.GetComponent<Door>().ChangeDoorState();
                }
                if (hit.collider.CompareTag("DoorWood"))
                {
                    hit.collider.transform.parent.GetComponent<WoodenDoor>().ChangeDoorState();
                }
                else if (hit.collider.CompareTag("DoorToInside")) {
                    inventory.Save();
                    SceneManager.LoadScene("AsylumLevel1");
                }
                else if (hit.collider.CompareTag("DoorToOutside")) {
                    inventory.Save();
                    SceneManager.LoadScene("HorrorLevel");
                }
            }

        }
    }
}
