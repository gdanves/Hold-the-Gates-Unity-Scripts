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
        if(!m_gameManager.BuyUnit("archer"))
            return;
        Instantiate(m_gameManager.GetUnitPrefab("archer"), transform.position + new Vector3(0.1f, -0.8f, 0), Quaternion.identity);
        gameObject.SetActive(false);
    }
}
