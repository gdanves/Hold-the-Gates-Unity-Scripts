using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spot : MonoBehaviour, IPointerDownHandler
{
    public GameObject m_archer;
    private GameManager m_gameManager;
 
    protected virtual void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        m_gameManager.ShowUnits(gameObject);
    }
}
