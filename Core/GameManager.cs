using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public struct UnitInfo
{
    public GameObject prefab;
    public int price;
    public string unitRef;

    public UnitInfo(string _unitRef, GameObject _prefab, int _price)
    {
        unitRef = _unitRef;
        prefab = _prefab;
        price = _price;
    }
}

public class GameManager : MonoBehaviour
{
    public List<UnitInfo> m_units;

    //[SerializeField] public SaveManager _saveManager;
    public static GameManager m_instance;

    // TODO: improve this later
    public GameObject m_menuGame;
    //public GameObject m_menuIngame;
    public GameObject m_menuEnd;
    public GameObject m_game;
    //public GameObject m_menuMain;
    public TMP_Text m_menuGameGoldText;
    public TMP_Text m_menuEndText;
    //public Slider m_healthSlider;

    int m_gold;
    int m_score;

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
        m_menuEndText.text = "Game Over!\nScore: " + m_score;
        m_menuEnd.SetActive(true);
    }

    public void ShowUnits(GameObject spot)
    {
        m_game.AsGame().ShowUnits(spot);
    }

    public void AddGold(int value)
    {
        m_gold += value;
        UpdateGoldText();
    }

    public void AddScore(int value)
    {
        m_score += value;
    }

    public int GetGold()
    {
        return m_gold;
    }

    public int GetUnitPrice(string unitRef)
    {
        foreach(UnitInfo unit in m_units) {
            if(unit.unitRef == unitRef)
                return unit.price;
        }
        return 0;
    }

    public GameObject GetUnitPrefab(string unitRef)
    {
        foreach(UnitInfo unit in m_units) {
            if(unit.unitRef == unitRef)
                return unit.prefab;
        }
        return null;
    }

    public bool BuyUnit(string unitRef)
    {
        int price = GetUnitPrice(unitRef);
        if(price == 0 || m_gold < price)
            return false;
        AddGold(-price);
        return true;
    }

    private void CopyLocalReferences(GameManager other)
    {
        m_menuGame = other.m_menuGame;
        m_menuEnd = other.m_menuEnd;
        m_menuEndText = other.m_menuEndText;
        m_menuGameGoldText = other.m_menuGameGoldText;
        m_units = other.m_units;
        m_game = other.m_game;
    }

    private void ResetStates()
    {
        m_gold = 20;
        m_score = 0;
        ResetHealthPercent();
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        if(!m_menuGameGoldText)
            return;
        m_menuGameGoldText.text = m_gold.ToString();
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
