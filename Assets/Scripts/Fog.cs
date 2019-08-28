using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour {

    Quaternion originalPosition;

    void Start() {
        originalPosition = transform.rotation;
    }

    void Update() {
        transform.rotation = originalPosition;
    }
}

