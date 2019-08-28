using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ItemDatabase))]
public class Inventory : MonoBehaviour {
    public FirstPersonCamera FPScamera;
    public Player player;

    [Header("Inventory Panel")]
    public GameObject inventoryPanel;
    public bool booleanInventory = true;

    [Space(10)]

    [Header("Inventory Items")]
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    [Space(10)]

    [Header("Sound")]
    public AudioClip openSound;
    public AudioClip closeSound;

    int slotAmount;

    ItemDatabase database;
    GameObject slotPanel;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();


    void Start()
    {
        database = GetComponent<ItemDatabase>();
        slotAmount = 20;
        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;

        for(int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory(booleanInventory);
        }
    }

    public void Save()
    {
        SaveLoadManager.SaveInventory(items);
    }

    public void Load()
    {
        InventoryData loadedStats = SaveLoadManager.LoadInventory();
        for (int i = 0; i < items.Count; i++)
        {
            AddItem(loadedStats.id[i]);
        }
    }

    public void OpenInventory(bool openClose)
    {
        if (openClose)
        {
            inventoryPanel.SetActive(true);
            booleanInventory = false;
            AudioSource.PlayClipAtPoint(openSound, transform.position, 1.0f);
            FPScamera.inventoryOpen = true;
            player.inventoryOpen = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            inventoryPanel.SetActive(false);
            booleanInventory = true;
            AudioSource.PlayClipAtPoint(closeSound, transform.position, 1.0f);
            FPScamera.inventoryOpen = false;
            player.inventoryOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemById(id);
        if (itemToAdd.Stackable && checkInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    Debug.Log(itemToAdd.Slug);
                    itemObj.transform.position = itemObj.transform.parent.position;
                    itemObj.name = itemToAdd.Title;
                    break;
                }
            }
        }
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemById(id);

        if (itemToRemove.Stackable && checkInInventory(itemToRemove))
        {
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].ID == id)
                {
                    ItemData data = slots[j].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

                    if (data.amount == 0)
                    {
                        Destroy(slots[j].transform.GetChild(0).gameObject);
                        items[j] = new Item();
                        break;
                    }
                    if (data.amount == 1)
                    {
                        slots[j].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";
                        break;
                    }
                    break;
                }
            }
        }
        else
            for (int i = 0; i < items.Count; i++)
                if (items[i].ID != -1 && items[i].ID == id)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    items[i] = new Item();
                    break;
                }
    }

    public bool checkInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
            if (items[i].ID == item.ID)
                return true;
        return false;
    }
}
