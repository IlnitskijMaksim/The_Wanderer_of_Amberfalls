using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIInventoryDescription itemDescription;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    private bool isInventoryOpen = false;

    public Sprite image;
    public int quantity;
    public string title, description;

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }


    public void InitializeInventoryUI(int inventorysize)
    {

        for (int i = 0; i < inventorysize; i++) 
        { 
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentPanel);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRigthMouseBtnClick += HandleShowItemActions;
        }
        
    }

    private void HandleShowItemActions(UIInventoryItem item)
    {

    }

    private void HandleEndDrag(UIInventoryItem item)
    {

    }

    private void HandleSwap(UIInventoryItem item)
    {

    }

    private void HandleBeginDrag(UIInventoryItem item)
    {

    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        itemDescription.SetDescription(image, title, description);
        listOfUIItems[0].Select();
    }

    public void Show()
    { 
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
        isInventoryOpen = true;

        listOfUIItems[0].SetData(image, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        isInventoryOpen = false;
    }

    public bool IsInventoryOpen()
    {
        return isInventoryOpen;
    }
}
