using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private GameManager m_gameManager;

    public void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
    }

    public void ManagerCall(string func)
    {
        m_gameManager.Invoke(func, 0);
    }
}
