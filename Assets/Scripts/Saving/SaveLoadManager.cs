using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager
{
    public static void SaveInventory(List<Item> items)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/inventory.sav", FileMode.Create);

        InventoryData data = new InventoryData(items);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static InventoryData LoadInventory()
    {
        if (File.Exists(Application.persistentDataPath + "/inventory.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/inventory.sav", FileMode.Open);

            InventoryData data = bf.Deserialize(stream) as InventoryData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File does not exist.");
            return null;
        }
    }
}

[Serializable]
public class InventoryData
{
        public int[] id;
        public string[] title;
        public int[] value;
        public string[] description;
        public bool[] stackable;
        public string[] slug;

    public InventoryData(List<Item> items)
    {
        id = new int[items.Count];
        title = new string[items.Count];
        value = new int[items.Count]; 
        description = new string[items.Count];
        stackable = new bool[items.Count];
        slug = new string[items.Count];

        for(int i = 0; i < items.Count; i++)
        {
            id[i] = items[i].ID;
            title[i] = items[i].Title;
            value[i] = items[i].Value;
            description[i] = items[i].Description;
            stackable[i] = items[i].Stackable;
            slug[i] = items[i].Slug;
        }
    }
}
