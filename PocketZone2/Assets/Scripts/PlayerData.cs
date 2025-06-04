using System;
using System.Collections.Generic;
using System.Linq;
[Serializable]
public class PlayerData
{
    public List<ItemScriptableObject> PlayerInventory;
    public int Health;
    public float[] Position;

    public PlayerData(Player player)
    {
        Dictionary<ItemScriptableObject, int> inventory = player.GetInventory().GetItems();
        PlayerInventory = new List<ItemScriptableObject>();

        foreach (ItemScriptableObject item in inventory.Keys.ToArray())
        {
            for(int i = 0; i < inventory[item]; i++)
                PlayerInventory.Add(item);
        }

        Health = player.GetHealth();

        Position = new float[3];
        Position[0] = player.transform.position.x;
        Position[1] = player.transform.position.y;
        Position[2] = player.transform.position.z;
    }
}
