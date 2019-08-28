using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothV;

    public float Sensitivity = 5.0f;
    public float Smoothing = 2.0f;

    public bool inventoryOpen = false;

    GameObject player;

	// Use this for initialization
	void Start () {
        player = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (!inventoryOpen)
        {
            Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            md = Vector2.Scale(md, new Vector2(Sensitivity + Smoothing, Sensitivity + Smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / Smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / Smoothing);
            mouseLook += smoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
        }
    }
}
