using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class CatalogManager : MonoBehaviour
{
    private Dictionary<string, CatalogItem> _catalogItems = new();
    
    private void Start()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(),
            resultCallback: result =>
            {   
                Debug.Log("CatalogItems successfully");
                HandleCatalog(result.Catalog);
            },
            errorCallback: errorCallback =>
            {
                Debug.LogError($"Catalog Failed {errorCallback}");
            }); 
    }

    private void HandleCatalog(List<CatalogItem> catalogItems)
    {
        for (var i = 0; i < catalogItems.Count; i++)
        {
            
            _catalogItems.Add(catalogItems[i].ItemId, catalogItems[i]);
            Debug.Log($"Catalog item {catalogItems[i].DisplayName} added to catalog");
            Debug.Log($"Description: {catalogItems[i].Description}");
        }
    }

    
}