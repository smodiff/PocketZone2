using UnityEngine;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMPro.TextMeshProUGUI _itemCountText;
    [SerializeField] private Button _deleteItemButton;

    private ItemScriptableObject _item;
    private int _itemCount = 0;

    private void Start()
    {
        HideDeleteButton();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            UIManager.instance.ShowItemDeleteButton(this);
        });
    }

    public void UpdateItem(ItemScriptableObject item, int count)
    {
        _item = item;
        _itemCount += count;
        if (item != null)
        {
            _itemImage.sprite = item.ItemImage;
            if (_itemCount > 1)
                _itemCountText.text = _itemCount.ToString();
            else
                _itemCountText.text = "";

            _deleteItemButton.onClick.AddListener(() =>
            {
                DeleteItem();
                UIManager.instance.UpdateInventory();
            });
        }
        else
        {
            _itemImage.enabled = false;
            _itemCountText.text = "";
        }
    }

    public void ShowDeleteButton()
    {
        _deleteItemButton.gameObject.SetActive(true);
    }

    public void HideDeleteButton() 
    {
        _deleteItemButton.gameObject.SetActive(false);
    }

    public void DeleteItem()
    {
        GameController.instance.Player.GetInventory().DeleteItem(_item);
    }
}
