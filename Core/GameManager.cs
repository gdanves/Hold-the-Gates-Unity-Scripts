using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //[SerializeField] public SaveManager _saveManager;
    public static GameManager m_instance;

    // TODO: improve this later
    public GameObject m_menuGame;
    //public GameObject m_menuIngame;
    public GameObject m_menuEnd;
    //public GameObject m_menuMain;
    public TMP_Text m_menuGameGoldText;
    //public Slider m_healthSlider;

    int m_gold;

    void Awake()
    {
        MakeSingleton();
    }

    public void SetHealthPercent(float value)
    {
        //m_healthSlider.value = value;
    }

    public void ResetHealthPercent()
    {
        //m_healthSlider.value = 1f;
    }

    public void NewGame()
    {
        //_saveManager.ResetCheckpoint();
        StartGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        //m_menuIngame.SetActive(false);
        //m_menuEnd.SetActive(false);
        //m_menuMain.SetActive(false);
        //m_menuGame.SetActive(true);
    }

    public void PauseGame()
    {
        //if(m_menuIngame.activeInHierarchy) {
        //    ResumeGame();
        //    return;
        //}

        Time.timeScale = 0f;
        //m_menuIngame.SetActive(true);
    }

    public void ResumeGame()
    {
        //m_menuIngame.SetActive(false);
        Time.timeScale = 1f; // TODO: change this, may be different if the player is dying
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndGame(bool won = false)
    {
        Time.timeScale = 0f;
        m_menuEnd.SetActive(true);
    }

    public void AddGold(int value)
    {
        m_gold += value;
        UpdateGoldText();
    }

    private void CopyLocalReferences(GameManager other)
    {
        m_menuGame = other.m_menuGame;
        m_menuEnd = other.m_menuEnd;
        m_menuGameGoldText = other.m_menuGameGoldText;
    }

    private void ResetStates()
    {
        m_gold = 0;
        ResetHealthPercent();
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        if(!m_menuGameGoldText)
            return;
        m_menuGameGoldText.text = "Gold: " + m_gold;
    }

    private void MakeSingleton()
    {
        if(m_instance != null) {
            m_instance.CopyLocalReferences(this);
            m_instance.ResetStates();
            Destroy(gameObject);
        } else {
            ResetStates();
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
