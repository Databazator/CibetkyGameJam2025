using UnityEngine;

public class ShopScreen : UIScreen
{
    Button _acceptButton;
    Button _skipButton;
    Item _currentItem;
    Label _itemNameLabel;
    Label _itemDescLabel;
    Image _itemImage;

    public override void Initialize()
    {
        base.Initialize();

        _acceptButton = _root.Q("AcceptButton") as Button;
        _acceptButton.clicked += OnAcceptClicked;

        _skipButton = _root.Q("SkipButton") as Button;
        _skipButton.clicked += OnSkipClicked;

        _itemNameLabel = _root.Q("ItemName") as Label;
        _itemDescLabel = _root.Q("ItemDescription") as Label;
        _itemImage = _root.Q("ItemSprite") as Image;
    }

    public void SetItem(Item item)
    {
        _currentItem = item;
        _itemNameLabel.text = item.ItemName;
        _itemDescLabel.text = item.ItemDescription;
        _itemImage.sprite = item.ItemIcon;
    }

    private void OnAcceptClicked()
    {

        UIEvents.ShopClosed?.Invoke();
    }

    private void OnSkipClicked()
    {
        UIEvents.ShopClosed?.Invoke();
    }
}
