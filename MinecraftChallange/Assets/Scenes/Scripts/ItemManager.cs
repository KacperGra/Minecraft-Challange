using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public List<RectTransform> slots;
    public int slotIndex = 0;
    private int baseSlotSize;

    private void Start()
    {
        baseSlotSize = (int)slots[0].sizeDelta.x;
        ResizeSlots();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            slotIndex = 0;
            ResizeSlots(); 
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            slotIndex = 1;
            ResizeSlots();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slotIndex = 2;
            ResizeSlots();
        }

    }

    private void ResizeSlots()
    {
        for(int i = 0; i < slots.Count; ++i)
        {
            if (slotIndex == i)
            {
                slots[i].sizeDelta = new Vector2(baseSlotSize + 25, baseSlotSize + 25);
            }
            else
            {
                slots[i].sizeDelta = new Vector2(baseSlotSize, baseSlotSize);
            }
        }
    }
}
