using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] ItemTooltip ItemInfo;
    public TextMeshProUGUI quantityText;

    void Start()
    {
        quantityText.raycastTarget = false;
    }

    public void OnPointerEnter()
    {
        ItemInfo.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        ItemInfo.gameObject.SetActive(false);
    }
}
