using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int _inventoryCapacity = 20;
    [SerializeField] private Gun _equippedGun;

    private Dictionary<ItemScriptableObject, int> _playerInventory;

    private void Awake()
    {
        _playerInventory = new Dictionary<ItemScriptableObject, int>();
        
    }

    public void AddItem(Item item, int count)
    {
        if (_playerInventory.ContainsKey(item.GetItemSO()))
        {
            _playerInventory[item.GetItemSO()] += count;
        }
        else
        {
            _playerInventory.Add(item.GetItemSO(), 1);
        }

        GameController.instance.SavePlayerData();
        Destroy(item.gameObject);
    }
    public void AddItem(ItemScriptableObject item, int count)
    {
        if (_playerInventory.ContainsKey(item))
        {
            _playerInventory[item] += count;
        }
        else
        {
            _playerInventory.Add(item, 1);
        }

        GameController.instance.SavePlayerData();
    }

    public void DeleteItem(ItemScriptableObject item)
    {
        if (_playerInventory.ContainsKey(item))
        {
            _playerInventory.Remove(item);
        }

        GameController.instance.SavePlayerData();
    }

    public Dictionary<ItemScriptableObject, int> GetItems()
    {
        return _playerInventory;
    }

    public int GetInventoryCapacity()
    {
        return _inventoryCapacity;
    }

    public Gun GetEquippedGun()
    {
        return _equippedGun;
    }
}
