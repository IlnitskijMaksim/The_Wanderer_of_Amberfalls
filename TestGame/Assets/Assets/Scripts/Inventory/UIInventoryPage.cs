using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem inventoryItemUIPrefab; // ������ UI �������� ���������

        [SerializeField]
        private RectTransform contentPanel; // ������ �������� ���������

        [SerializeField]
        private UIInventoryDescription itemDescription; // ���� ��������

        [SerializeField]
        private MouseFollower mouseFollower; // �������� �� ����� ��'���

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>(); // ������ UI �������� ���������

        private bool isInventoryOpen = false; // ��������� �������� ���������

        public event Action<int> OnDescriptionRequested, // ���� ������ ����� ��������
            OnItemActionRequested, // ���� ������ ��������� 䳿 ��� ���������
            OnStartDragging; // ���� ������� ������������� ��������

        public event Action<int, int> OnSwapItems; // ���� ����� ����������

        private int currentlyDraggedItemIndex = -1; // ������ �������������� ��������

        [SerializeField]
        private ItemActionPanel actionPanel; // ������ �� ��� ���������

        // �����, ���� ����������� ��� ��������� ��'����
        private void Awake()
        {
            Hide(); // ���������� ��������� ��� �������
            mouseFollower.Toggle(false); // ��������� ��������� �� �����
            itemDescription.ResetDescription(); // �������� ����� ��������
        }

        // ������������ UI ���������
        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(inventoryItemUIPrefab, Vector3.zero, Quaternion.identity, contentPanel);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRigthMouseBtnClick += HandleShowItemActions;
            }
        }

        // ��������� ����� �������� UI ���������
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        // �������� ����������� �������� �� ��� ���������
        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        // �������� ���������� ������������� ��������
        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        // �������� ����� ����������
        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        // �������� �������������� ��������
        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        // �������� ������� ������������� ��������
        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        // ��������� ��������, ���� ������������
        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        // �������� ������ ��������
        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        // ³���������� ���������
        public void Show()
        {
            gameObject.SetActive(true);
            itemDescription.ResetDescription();
            isInventoryOpen = true;
            ResetSelection();
        }

        // �������� ������
        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        // ��������� 䳿 ��� ���������
        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButon(actionName, performAction);
        }

        // ����� �������� �� ��� ���������
        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
        }

        // ������ ������ � ��� ��������
        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);
        }

        // ���������� ���������
        public void Hide()
        {
            actionPanel.Toggle(false);
            gameObject.SetActive(false);
            isInventoryOpen = false;
            ResetDraggedItem();
        }

        // ��������, �� �������� ��������
        public bool IsInventoryOpen()
        {
            return isInventoryOpen;
        }

        // ��������� ����� �������� �� ������ � ��������
        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }

        // �������� ����� ��� �������� ���������
        internal void ReselAllItems()
        {
            foreach (var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}
