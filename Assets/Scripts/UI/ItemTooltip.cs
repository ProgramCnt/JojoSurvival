using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;
    public TextMeshProUGUI statInfo;

    private void Start()
    {
        itemName.raycastTarget = false;
        itemDesc.raycastTarget = false;
        statInfo.raycastTarget = false;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        transform.position = mousePos;
    }
}
