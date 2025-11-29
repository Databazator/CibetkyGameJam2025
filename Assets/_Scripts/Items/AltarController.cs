using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class AltarController : MonoBehaviour
{
    private bool _hasItem = false;
    private Item _itemOnStand = null;
    private bool _interactable = false;
    private PlayerInventory _playerInventory = null;
    private PlayerController _playerController = null;
    public GameObject physicalItem;

    public void PlaceItem(Item item)
    {
        _hasItem = true;
        _itemOnStand = item;
        physicalItem.SetActive(true);
    }

    public bool HasItem()
    {
        return _hasItem;
    }

    public void RemoveItem()
    {
        _hasItem = false;
        _itemOnStand = null;
        physicalItem.SetActive(false);
    }

    public Item GetItemOnStand()
    {
        return _itemOnStand;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interactable = true;
            _playerInventory = other.GetComponent<PlayerInventory>();
            _playerController = other.GetComponent<PlayerController>();
            // Show interact button to take object
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interactable = false;
            _playerInventory = null;
            _playerController = null;
            // Hide interact button
        }
    }

    private void OnItemAccepted(Item item)
    {
        if (_itemOnStand == item)
        {
            Debug.Log("Player accepted item " + item.name + " from altar.");

            // Add item to player inventory
            _playerInventory.AddItem(item);
            RemoveItem();
        }
    }

    private void OnItemSkipped(Item item)
    {
        if (_itemOnStand == item)
        {
            Debug.Log("Player skipped item " + item.name + " from altar.");

            // Just remove item from stand
            RemoveItem();
        }
    }
    
    void Start()
    {
        // Remove any item on stand if accidentally left there
        RemoveItem();
        GameEvents.ItemAccepted += OnItemAccepted;
        GameEvents.ItemSkipped += OnItemSkipped;
    }

    void Update()
    {
        
        if (_playerController != null)
        {
            bool wasInteractPressed = _playerController.interactAction.ReadValue<float>() > 0f;
            if (_interactable && wasInteractPressed && _itemOnStand != null)
            {
                // Trigger shopping UI
                GameEvents.ItemFound.Invoke(_itemOnStand);
                _interactable = false;
            }   
        }
    }
}
