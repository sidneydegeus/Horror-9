using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    //public GameObject OpenPanel;
    public bool Open = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoothing = 2f;
    public bool ForceDirection;
    Rigidbody rb;


    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update() {
        if (Open) {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smoothing);
        } else {
            Quaternion targetRotation = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smoothing);
        }
    }

    public void ChangeDoorState() {
        Open = !Open;
    }

    public void GetDestroyed(Vector3 direction) {
        rb.isKinematic = false;
        transform.GetComponent<BoxCollider>().enabled = false;
        rb.AddForce(((ForceDirection) ? transform.forward : -transform.forward) * 500);
        rb.useGravity = true;
        Destroy(gameObject, .6f);
    }
}
