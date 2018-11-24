using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Customer
{
    private List<Item> items = new List<Item>();
    public int phoneNumber;
    public List<string> itemNames = new List<string>();
    public float finalPrice;

    public void AddItem(Item _item)
    {
        Debug.Log("Item Added to cart: " + _item.name);
        items.Add(_item);
    }

    public List<Item> GetCart()
    {
        return items;
    }

    public void SetPhoneNumber(int _number)
    {
        phoneNumber = _number;
    }
}