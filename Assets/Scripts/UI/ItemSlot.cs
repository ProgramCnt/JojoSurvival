using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public ItemData item;
    public Image icon;
    [SerializeField] ItemTooltip tooltip;
    public TextMeshProUGUI quantityText;
    public Inventory inventory;

    [Header("Slot Background Color")]
    public Color baseColor;
    public Color onPointColor;
    public Color EquipColor;

    public int idx;
    public int quantity;
    public bool equipped;

    Image bg;
    Button button;

    void Start()
    {
        bg = GetComponent<Image>();
        button = GetComponent<Button>();

        quantityText.raycastTarget = false;
    }

    public void Set()
    {
        icon.sprite = item.icon;
        quantityText.text = quantity > 1 ? $"{quantity.ToString()}/{item.maxStackAmount}" : string.Empty;
        bg.color = equipped ? EquipColor : bg.color;

        icon.gameObject.SetActive(true);
    }

    public void Clear()
    {
        item = null;
        icon.sprite = null;
        quantityText.text = string.Empty;

        icon.gameObject.SetActive(false);
    }

    public void OnPointerEnter()
    {
        if (!item)
            return;

        tooltip.gameObject.SetActive(true);
        tooltip.SetTooltipText(item);

        bg.color = onPointColor;
    }

    public void OnPointerExit()
    {
        if (!item)
            return;

        tooltip.gameObject.SetActive(false);
        bg.color = baseColor;
    }
   
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // 마우스 좌클릭
        {
            // 아이템 선택
            inventory.SelectItem(idx);
        }
        else if (eventData.button == PointerEventData.InputButton.Right) // 마우스 우클릭
        {
            // 아이템 사용
            inventory.OnClickUseItem(idx);
        }
    }
}
