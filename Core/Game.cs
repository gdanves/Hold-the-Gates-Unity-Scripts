using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject m_menuUnits;

    private GameManager m_gameManager;
    private GameObject m_selectedSpot;

    public void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
    }

    public void ManagerCall(string func)
    {
        m_gameManager.Invoke(func, 0);
    }

    public void ShowUnits(GameObject spot)
    {
        m_menuUnits.SetActive(true);
        m_selectedSpot = spot;
    }

    public void HideUnits()
    {
        m_menuUnits.SetActive(false);
    }

    public void BuyUnit(string unitRef)
    {
        if(!m_selectedSpot || !m_selectedSpot.activeSelf || !m_gameManager.BuyUnit(unitRef))
            return;
        Instantiate(m_gameManager.GetUnitPrefab(unitRef), m_selectedSpot.transform.position + new Vector3(0.1f, -0.8f, 0), Quaternion.identity);
        m_selectedSpot.SetActive(false);
        HideUnits();
    }
}
