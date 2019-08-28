using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    private Item item;
    private string data;
    public GameObject tooltip;

    /*
     * This is the tooltip class. It will give you a display of the item where you are hoovering on.
     */

    private void Start()
    {
        tooltip.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.activeSelf){
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        data = item.Title + "\n" + item.Description;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
