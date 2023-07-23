using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerVirtualCurrency;

    private Dictionary<string, ItemInstance> _characterInventoryItems = new();
    private string _characterID;

    private void Start()
    {
        
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
            resultCallback: result =>
            {
                HandleCharacterInventory(result.Inventory);
                _playerVirtualCurrency.text = result.VirtualCurrency["GF"].ToString();
            },
            errorCallback: error => { Debug.LogError($"Character Info Failed {error}"); });
    }

    private void HandleCharacterInventory(List<ItemInstance> inventoryItems)
    {
        for (var i = 0; i < inventoryItems.Count; i++)
        {
            _characterInventoryItems.Add(inventoryItems[i].ItemId, inventoryItems[i]);
            Debug.Log($"Catalog item {inventoryItems[i].DisplayName} added to catalog");
            Debug.Log($"Description: {inventoryItems[i].Annotation}");
        }
    }
}