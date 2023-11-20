using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Text quantityText;

    [SerializeField]
    private Image borderImage;

    private bool empty = true;

    public void Awake()
    {
        ResetData();        
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityText.text = quantity.ToString();
        empty = false;
    }
}
