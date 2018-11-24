using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour {

    public GameObject m_panel;

    //params: _item: Item to display on the placemat
	public void SelectMenuItem(GameObject _item)
    {
        m_panel.SetActive(!m_panel.activeSelf);
        _item.SetActive(true);
    }
}
