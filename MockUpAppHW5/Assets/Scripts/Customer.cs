﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Customer
{
    private List<Item> items = new List<Item>();
    private float finalPrice;

    public int phoneNumber;
    public List<string> itemNames = new List<string>();
    public string jsonPrice;

    public void AddItem(Item _item)
    {
        Debug.Log("Item Added to cart: " + _item.name);
        items.Add(_item);
    }

    public List<Item> GetCart()
    {
        return items;
    }

    public void SetPrice(float _price)
    {
        finalPrice = _price;
    }

    public float GetPrice()
    {
        return finalPrice;
    }

    public void SetPhoneNumber(int _number)
    {
        phoneNumber = _number;
    }
}