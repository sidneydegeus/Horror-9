using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {

    Quaternion originalPosition;

	void Start () {
        originalPosition = transform.rotation;
	}
	
	void FixedUpdate () {
        transform.rotation = originalPosition;
	}
}
