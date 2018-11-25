using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
    public static GameObject m_lastItem;
    public static Customer m_customer;

    public GameObject m_menuPanel;
    public GameObject m_menuExpand;
    public GameObject m_cartPanel;
    public GameObject m_orderDetailsPanel;
    public GameObject m_orderCompletedPanel;

    public GameObject m_statusText;

    public Text m_itemsList;
    public Text m_finalPrice;
    public Text m_numCartItems;
    public Text m_orderFinalPrice;
    public Text m_customerName;
    public Text m_customerAddress;
    public Text m_phoneNumber;
    public Text m_orderCompleted;

    private bool cartToggled = false;

    private void Start()
    {
        m_customer = new Customer();
        m_customer.SetPhoneNumber("1234567890");
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
        if (!cartToggled)
        {
            if (m_menuPanel.activeSelf)
                m_menuPanel.SetActive(!m_menuPanel.activeSelf);
            if (m_menuExpand.activeSelf)
                m_menuExpand.SetActive(!m_menuExpand.activeSelf);
            cartToggled = true;

            string items = "";
            foreach(string it in m_customer.itemNames)
            {
                items += it + "\n\n";
            }

            m_itemsList.text = items;
            m_finalPrice.text = "Your Amount: " + m_customer.GetPrice().ToString(); 
        }
        else
        {
            m_menuPanel.SetActive(!m_menuPanel.activeSelf);
            cartToggled = false;
        }
    }

    public void OrderDetailsToggle()
    {
        m_cartPanel.SetActive(!m_cartPanel.activeSelf);
        m_orderFinalPrice.text = "Your Amount: " + m_customer.GetPrice().ToString();
        m_orderDetailsPanel.SetActive(!m_orderDetailsPanel.activeSelf);
    }

    public void AddItemToCart()
    {
        if (m_lastItem != null)
        {
            Item _item = m_lastItem.GetComponent<Item>();
            Debug.Log(_item.name);
            if (_item != null && m_customer != null)
            {
                m_customer.AddItem(_item);
                m_customer.SetPrice(m_customer.GetPrice() + _item.price);
                m_customer.itemNames.Add(_item.name);
                m_numCartItems.text = m_customer.CartSize().ToString();
                StartCoroutine(displayStatus(_item.name + " added to cart!"));
            }
        }
        else
        {
            StartCoroutine(displayStatus("No item selected!"));
        }
    }

    public void Checkout()
    {
        //settings customer values
        m_customer.jsonPrice = m_customer.GetPrice().ToString();
        m_customer.name = m_customerName.text;
        m_customer.address = m_customerAddress.text;
        m_customer.phoneNumber = m_phoneNumber.text;

        m_orderCompleted.text = "Thank you for your purchase " + m_customer.name + "! \n\nWe will be sending you updates about your order @" + m_customer.phoneNumber + ".";

        string jsonString = JsonUtility.ToJson(m_customer);
        StartCoroutine(POSTRequest.PostRequest("https://aqueous-sea-55584.herokuapp.com/form", jsonString));
        Debug.Log("Checked out. Thank you!");
        m_customer.ClearCart();
        m_orderCompletedPanel.SetActive(true);
        m_orderDetailsPanel.SetActive(!m_orderDetailsPanel.activeSelf);
    }

    IEnumerator displayStatus(string message)
    {
        if (m_menuPanel.activeSelf)
        {
            Vector3 temp = new Vector3(-250.0f, 0.0f, 0.0f);
            m_statusText.transform.localPosition = temp;
        }
        else
        {
            Vector3 temp = new Vector3(0.0f, 250.0f, 0.0f);
            m_statusText.transform.localPosition = temp;
        }
        m_statusText.GetComponentInChildren<Text>().text = message;
        m_statusText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        m_statusText.SetActive(false);
    }

    public void BackOrder()
    {
        m_cartPanel.SetActive(!m_cartPanel.activeSelf);
        m_orderDetailsPanel.SetActive(!m_orderDetailsPanel.activeSelf);
    }

    public void LoadScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }
}