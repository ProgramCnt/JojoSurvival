using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;
    public TextMeshProUGUI statInfo;
    public Vector2 tooltipOffset;

    RectTransform rt;

    private void Start()
    {
        itemName.raycastTarget = false;
        itemDesc.raycastTarget = false;
        statInfo.raycastTarget = false;

        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        
        rt.position = mousePos + tooltipOffset;
    }

    public void SetTooltipText(ItemData data)
    {
        itemName.text = data.itemName;
        itemDesc.text = data.description;
        statInfo.text = string.Empty;

        switch (data.type)
        {
            case ItemType.Consumable:
                for (int i = 0; i < data.consumableData.Length; i++)
                {
                    statInfo.text += $"{data.consumableData[i].type}: <color=orange>{data.consumableData[i].value}</color>\n";
                }
                break;
            case ItemType.Equipment:
                break;
        }
    }
}
