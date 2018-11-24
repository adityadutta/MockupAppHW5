using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    public static Customer m_customer;
    public GameObject m_menuPanel;
    public GameObject m_menuExpand;
    public GameObject m_cartPanel;
    public static GameObject m_lastItem;

    private void Start()
    {
        m_customer = new Customer();
        m_customer.SetPhoneNumber(1234567890);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (m_customer != null)
            {
                foreach (Item _item in m_customer.GetCart())
                {
                    Debug.Log("Item: " + _item.name + "Price: " + _item.price);
                }
            }
        }
    }
    //params: _item: Item to display on the placemat
    public void SelectMenuItem(GameObject _item)
    {
        if (m_lastItem != null)
        {
            m_lastItem.SetActive(false);
        }
        //m_panel.SetActive(!m_panel.activeSelf);
        _item.SetActive(true);
        m_lastItem = _item;
    }

    public void MenuToggle()
    {
        m_menuPanel.SetActive(!m_menuPanel.activeSelf);
        m_menuExpand.SetActive(!m_menuExpand.activeSelf);
    }

    public void CartToggle()
    {
        m_cartPanel.SetActive(!m_cartPanel.activeSelf);
    }

    public void AddItemToCart()
    {
        Item _item = m_lastItem.GetComponent<Item>();
        Debug.Log(_item.name);
        if (_item != null && m_customer != null)
            m_customer.AddItem(_item);
    }

    public void Checkout()
    {
        foreach (Item _item in m_customer.GetCart())
        {
            m_customer.itemNames.Add(_item.name);
            m_customer.SetPrice(m_customer.GetPrice() + _item.price);
        }
        m_customer.jsonPrice = m_customer.GetPrice().ToString();
        string jsonString = JsonUtility.ToJson(m_customer);
        Debug.Log(jsonString);
        string filePath = Application.dataPath + "/StreamingAssets/data.json";
        File.WriteAllText(filePath, jsonString);
        StartCoroutine(POSTRequest.PostRequest("http://localhost:3000/form", jsonString));
        Debug.Log("Checked out. Thank you!");
    }
}