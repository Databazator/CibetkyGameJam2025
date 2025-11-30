using UnityEngine;
using UnityEngine.UIElements;

public class ShopScreen : UIScreen
{
    Button _acceptButton;
    Button _skipButton;
    Item _currentItem;
    Label _itemNameLabel;
    Label _itemDescLabel;
    Label _itemCostLabel;
    // VisualElement _rarityGlow;
    // Image _itemImage;

    public override void Initialize()
    {
        base.Initialize();

        _acceptButton = _root.Q("AcceptButton") as Button;
        _acceptButton.clicked += OnAcceptClicked;

        _skipButton = _root.Q("SkipButton") as Button;
        _skipButton.clicked += OnSkipClicked;

        _itemNameLabel = _root.Q("ItemName") as Label;
        _itemDescLabel = _root.Q("ItemDescription") as Label;
        _itemCostLabel = _root.Q("ItemCost") as Label;
        // _rarityGlow = _root.Q("RarityGlow") as VisualElement;
        // _itemImage = _root.Q("ItemSprite") as Image;
    }

    public void SetItem(Item item)
    {
        _currentItem = item;
        _itemNameLabel.text = item.itemName;
        _itemDescLabel.text = item.description;
        _itemCostLabel.text = ": " + item.GetCost(GameObject.FindWithTag("Player")).ToString();
        // _itemImage.sprite = item.itemIcon;
        // Change tint of background image based on rarity
        Color glowColor = Color.white;
        switch (item.rarity)
        {
            case Item.Rarity.Common:
                glowColor = new Color(192f / 255f, 192f / 255f, 192f / 255f); // Silver
                break;
            case Item.Rarity.Uncommon:
                glowColor = Color.green;
                break;
            case Item.Rarity.Rare:
                glowColor = Color.blue;
                break;
            // case Item.Rarity.Epic:
            //     glowColor = new Color(128f / 255f, 0f, 128f / 255f); // Purple
            //     break;
            // case Item.Rarity.Legendary:
            //     glowColor = new Color(1f, 223f / 255f, 0f); // Gold
            //     break;
        }
        // Set glow to the item name label as well using outline
        _itemNameLabel.style.unityTextOutlineColor = glowColor;
    }

    private void OnAcceptClicked()
    {
        Debug.Log("Item accepted: " + _currentItem.itemName);
        GameEvents.ItemAccepted?.Invoke(_currentItem);
        UIEvents.ShopClose?.Invoke();
    }

    private void OnSkipClicked()
    {
        Debug.Log("Item skipped: " + _currentItem.itemName);
        UIEvents.ShopClose?.Invoke();
    }
}
