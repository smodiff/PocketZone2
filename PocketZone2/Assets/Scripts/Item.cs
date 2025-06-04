using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public ItemScriptableObject _itemSO;

    public void SetupItem(ItemScriptableObject item)
    {
        _itemSO = item;
        _spriteRenderer.sprite = item.ItemImage;
    }

    public ItemScriptableObject GetItemSO()
    {
        return _itemSO;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;
        print("Player in Trigger!");
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<PlayerInventory>().AddItem(this, 1);
        }
    }
}
