using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage; // ���������� ��������
        [SerializeField]
        private TMP_Text quantityTxt; // �������� ���� ��� ����������� ������� ��������
        [SerializeField]
        private Image borderImage; // ���������� ��� �������� ��������

        public event Action<UIInventoryItem> OnItemClicked, // ���� ���� �� �������
            OnItemDroppedOn, // ����, ���� ������� ������� �� ����� �������
            OnItemBeginDrag, // ���� ������� ������������� ��������
            OnItemEndDrag, // ���� ���������� ������������� ��������
            OnRigthMouseBtnClick; // ���� ����� ������ ���� �� �������

        private bool empty = true; // ��������� ���������� �����

        // �����, ���� ����������� ��� ��������� ��'����
        public void Awake()
        {
            ResetData(); // �������� ����� ��������
            Deselect(); // ������ ��������
        }

        // �������� ����� ��������
        public void ResetData()
        {
            itemImage.gameObject.SetActive(false); // ��������� ���������� ��������
            empty = true; // ������������ �������� ���������� �����
        }

        // ������ ��������
        public void Deselect()
        {
            borderImage.enabled = false; // ��������� ���������� ��� ��������
        }

        // ������������ ����� ��������
        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true); // �������� ���������� ��������
            itemImage.sprite = sprite; // ������������ ���������� ��������
            quantityTxt.text = quantity + ""; // ������������ ������� ��������
            empty = false; // ������ �������� ���������� �����
        }

        // �������� ��������
        public void Select()
        {
            borderImage.enabled = true; // �������� ���������� ��� ��������
        }

        // �������� ���� �� �������
        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Left)
            {
                OnItemClicked?.Invoke(this);
                OnRigthMouseBtnClick?.Invoke(this); // ������ ��䳿 ����� ������ ����
            }
        }

        // �������� ������� ������������� ��������
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this); // ������ ��䳿 ������� ������������� ��������
        }

        // �������� ���������� ������������� ��������
        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this); // ������ ��䳿 ���������� ������������� ��������
        }

        // �������� ��������� �������� �� ����� �������
        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this); // ������ ��䳿 ��������� �������� �� ����� �������
        }

        // �������� ������������� ��������
        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}
