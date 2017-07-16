using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAndPointBehaviour : Assets.Scripts.AbstractBeatable {

    private uint m_nOverallPoints = 0;

    private bool m_bWonCurrentLevel = false;
    private bool m_bWonOverall = false;

    private bool m_bCounting = false;
    private uint m_nCurrentLevelBeats = 0;
    private bool m_bNoDeathBonus = true;

    private bool m_bDiscoPeteCurrentlyDead = false;

    private GUIMaster m_pGUIMaster;
    private DiscoPeteBehaviour m_pDiscoPete;
    private GridMaster m_pGridMaster;

    void Awake()
    {
        // we want this game object for all levels
        DontDestroyOnLoad(transform.gameObject);
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        ItlFindObjects();

        m_nCurrentLevelBeats = 0;
        m_bWonCurrentLevel = false;
        m_bCounting = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ItlFindObjects();
    }

    protected override void OnDisable()
    {
        Debug.Log("LevelAndPointBehaviour::OnDisable");
        base.OnDisable();
    }

    private void Update()
    {
        if (m_bWonCurrentLevel && m_bWonOverall == false)
        {
            if(Input.GetKey(KeyCode.Return))
            {
                ItlGoToNextLevel();
            }
        }
        else if(m_bDiscoPeteCurrentlyDead)
        {
            if(Input.GetKey(KeyCode.R))
            {
                ItlResetLevel();
            }
        }
    }

    // TEST
    private void OnGUI()
    {
        Scene pCurrentScene = SceneManager.GetActiveScene();
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), pCurrentScene.name + " Points: " + m_nCurrentLevelBeats);
    }

    protected override void OnBeat()
    {
        if (m_bCounting)
            ++m_nCurrentLevelBeats;
    }

    public void OnDiscoPeteJumpedFromStart()
    {
        // Start counting
        m_bCounting = true;
        m_nCurrentLevelBeats = 0;
    }

    public void OnDiscoPeteDied()
    {
        Debug.Log("LevelAndPointBehaviour::OnDiscoPeteDied");
        m_pGUIMaster.ShowText("YOU DIED!", "Press R to restart");

        m_bDiscoPeteCurrentlyDead = true;
        m_bNoDeathBonus = false;
        m_bCounting = false;
    }

    public void OnDiscoPeteFinishedCurrentLevel()
    {
        m_bWonCurrentLevel = true;
        m_bCounting = false;

        if (ItlIsThereNextLevel() == false)
        {
            m_bWonOverall = true;
            ItlDisplayOverallWinningMessage();
        }
        else
        {
            ItlDisplayGoToNextLevelMessage();
        }
    }

    private void ItlResetLevel()
    {
        m_bDiscoPeteCurrentlyDead = false;
        m_pDiscoPete.Reset();

        m_pGridMaster.Reset();
        m_pGridMaster.SetDiscoPeteToStart();

        m_pGUIMaster.HideText();
    }

    private bool ItlIsThereNextLevel()
    {
        bool bNextLevelAvailable = true;

        try
        {
            Scene pNextLevel = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch
        {
            bNextLevelAvailable = false;
        }

        return bNextLevelAvailable;
    }

    private void ItlGoToNextLevel()
    {
        // TODO
        m_bDiscoPeteCurrentlyDead = false;
        m_bNoDeathBonus = true;
        m_nCurrentLevelBeats = 0;
        m_bWonCurrentLevel = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ItlDisplayGoToNextLevelMessage()
    {
        GameObject guiGO = GameObject.FindWithTag("GUIMaster");
        GUIMaster pGUIMaster = guiGO.GetComponent<GUIMaster>();

        pGUIMaster.ShowText("LEVEL COMPLETE!", "Press RETURN for next level");
    }

    private void ItlDisplayOverallWinningMessage()
    {
        GameObject guiGO = GameObject.FindWithTag("GUIMaster");
        GUIMaster pGUIMaster = guiGO.GetComponent<GUIMaster>();
        pGUIMaster.ShowText("YOU WON!", "");
    }

    private void ItlFindObjects()
    {
        Debug.Log("LevelAndPointBehaviour::ItlFindObjects");

        GameObject guiGO = GameObject.FindWithTag("GUIMaster");
        m_pGUIMaster = guiGO.GetComponent<GUIMaster>();
        m_pGUIMaster.HideText();

        GameObject dpGO = GameObject.FindWithTag("DiscoPete");
        m_pDiscoPete = dpGO.GetComponent<DiscoPeteBehaviour>();

        GameObject gmGO = GameObject.FindWithTag("GridMaster");
        m_pGridMaster = gmGO.GetComponent<GridMaster>();

        if (m_pGUIMaster == null)
            Debug.Log("LevelAndPointBehaviour: GUIMaster not found");

        if (m_pDiscoPete == null)
            Debug.Log("LevelAndPointBehaviour: DiscoPeteBehaviour not found");

        if (m_pGridMaster == null)
            Debug.Log("LevelAndPointBehaviour: GridMaster not found");
    }
}
