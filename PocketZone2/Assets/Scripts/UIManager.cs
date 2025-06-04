using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject InventoryInterface;
    [SerializeField] private InventoryUIItem InventoryItemPrefab;
    [SerializeField] private Transform InventoryItemsHandler;
    [SerializeField] private GameObject TempCanvas;
    [SerializeField] private GameObject DeathPanel;
    [SerializeField] private GameObject WinPanel;

    private List<InventoryUIItem> _inventoryItems;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _inventoryItems = new List<InventoryUIItem>();
        TempCanvas.SetActive(false);
        DeathPanel.SetActive(false);
        WinPanel.SetActive(false);

        CloseInventory();
    }

    public void OpenDeathInterface()
    {
        TempCanvas.SetActive(true);
        DeathPanel.SetActive(true);
    }

    public void OpenWinInterface()
    {
        TempCanvas.SetActive(true);
        WinPanel.SetActive(true);
    }

    public void ShowItemDeleteButton(InventoryUIItem item)
    {
        foreach (InventoryUIItem _item in _inventoryItems)
        {
            _item.HideDeleteButton();
        }

        if (item != null)
            item.ShowDeleteButton();
    }

    public void OpenInventory()
    {
        InventoryInterface.SetActive(true);
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        foreach (InventoryUIItem item in _inventoryItems)
        {
            Destroy(item.gameObject);
        }

        _inventoryItems.Clear();

        Dictionary<ItemScriptableObject, int> playerItems = GameController.instance.Player.GetInventory().GetItems();

        foreach (ItemScriptableObject item in playerItems.Keys.ToArray())
        {
            InventoryUIItem newitem = Instantiate(InventoryItemPrefab, InventoryItemsHandler);
            newitem.UpdateItem(item, playerItems[item]);
            _inventoryItems.Add(newitem);
        }

        for (int i = _inventoryItems.Count; i < GameController.instance.Player.GetInventory().GetInventoryCapacity(); i++)
        {
            InventoryUIItem newitem = Instantiate(InventoryItemPrefab, InventoryItemsHandler);
            _inventoryItems.Add(newitem);
            newitem.UpdateItem(null, 0);
        }
    }

    public void CloseInventory()
    {
        InventoryInterface.SetActive(false);
    }
}
