using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoPeteBehaviour : MonoBehaviour {

    // Types
    enum DIR { IDLE, UP, DOWN, LEFT, RIGHT}

    // Privates
    private DIR m_eDir = DIR.IDLE;
    private float m_fMoved = 0.0f;
    private float m_fSpeed = 7.0f;
    private bool m_bPrevKeyPressed = false;
	private int m_iLockedBeat = -1;
    private int m_iLastJumpedBeat = -1;
    private bool m_bAlive = true;

    private BeatMaster m_pBeatMaster;
    private GridMaster m_pGridMaster;
    private GUIMaster m_pGUIMaster;

    // Use this for initialization
    void OnEnable()
    {
        GameObject bmGO = GameObject.FindWithTag("Music");
        m_pBeatMaster = bmGO.GetComponent<BeatMaster>();
        m_fSpeed = m_pBeatMaster.getDiscoPeteSpeedDependingOnMusic();

        GameObject gmGO = GameObject.FindWithTag("GridMaster");
        m_pGridMaster = gmGO.GetComponent<GridMaster>();
        m_pGridMaster.SetDiscoPeteToStart();

        GameObject guiGO = GameObject.FindWithTag("GUIMaster");
        if(guiGO != null)
            m_pGUIMaster = guiGO.GetComponent<GUIMaster>();

        m_pBeatMaster.beatEvent += BeatMasterOnBeatEvent;
        m_pBeatMaster.onJumpChancePassedEvent += BeatMasterOnJumpChancePassedEvent;
    }

    void OnDisable()
    {
        if (m_pBeatMaster != null)
        {
            m_pBeatMaster.beatEvent -= BeatMasterOnBeatEvent;
            m_pBeatMaster.onJumpChancePassedEvent -= BeatMasterOnJumpChancePassedEvent;
        }
    }

    // Update is called once per frame
    void Update () {

        if(m_bAlive)
        {
            ItlUpdateDirection();
            ItlMovePete();
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                m_bAlive = true;

                if(m_pGUIMaster != null)
                    m_pGUIMaster.HideText();


                m_pGridMaster.Reset();
                m_pGridMaster.SetDiscoPeteToStart();
            }
        }
	}

    public void Say(string text)
    {
        Debug.Log("DISCOPETE says: " + text);
    }

    public void Die()
    {
        Debug.Log("DISCOPETE IS DEAD!");

        m_bAlive = false;

        if(m_pGUIMaster != null)
            m_pGUIMaster.ShowText("YOU DIED!", "Press R to restart");
    }

    private void BeatMasterOnBeatEvent()
    {

    }

    private void BeatMasterOnJumpChancePassedEvent()
    {
        if(m_iLastJumpedBeat < m_pBeatMaster.NearestBeat)
        {
            //Debug.Log("# STAY");

            m_pGridMaster.OnDiscoPeteStaysOnTile(this, Mathf.FloorToInt(transform.position.x + 0.5f), Mathf.FloorToInt(transform.position.z + 0.5f));
        }
    }

    private void ItlUpdateDirection()
    {
        DIR eCurrDir = ItlGetDirFromInput();

        if(m_eDir == DIR.IDLE && // currently idle
            eCurrDir != DIR.IDLE && // want to jump
            !m_bPrevKeyPressed && // jump only allowed if previously no key was pressed
            m_pBeatMaster.NearestBeat > m_iLastJumpedBeat) // jump only allowed once per beat
        {
            if (m_pBeatMaster.allowsJump()) // Check if the beatmaster allows us to jump
            {
                if (m_iLockedBeat != m_pBeatMaster.NearestBeat) // check if movement is not locked
                {
                    Debug.Log("--- JUMP BEGIN");
                    m_eDir = eCurrDir; // change direction
                    m_iLastJumpedBeat = m_pBeatMaster.NearestBeat; // set last beat where pete jumped
                    ItlSetRotationFromDir(); // apply the rotation corresponding to the current direction
                }
            }
            else
            {
                m_iLockedBeat = m_pBeatMaster.NextBeat;
            }

            if (m_eDir != DIR.IDLE)
            {
                m_pGridMaster.OnDiscoPeteLeavesTile(this, Mathf.FloorToInt(transform.position.x + 0.5f), Mathf.FloorToInt(transform.position.z + 0.5f));
            }
        }

        m_bPrevKeyPressed = (eCurrDir != DIR.IDLE);
    }

    private void ItlMovePete()
    {
        if(m_eDir != DIR.IDLE)
        {
            float fDelta = m_fSpeed * Time.deltaTime;
            if (m_fMoved + fDelta > 1.0f)
                fDelta = 1.0f - m_fMoved;

            if(m_eDir == DIR.RIGHT)
            {
                transform.Translate(Vector3.right * fDelta, Space.World);
            }
            else if(m_eDir == DIR.LEFT)
            {
                transform.Translate(Vector3.left * fDelta, Space.World);
            }
            else if (m_eDir == DIR.UP)
            {
                transform.Translate(Vector3.forward * fDelta, Space.World);
            }
            else if (m_eDir == DIR.DOWN)
            {
                transform.Translate(Vector3.back * fDelta, Space.World);
            }

            m_fMoved += fDelta;

            // Jumping
            transform.position = new Vector3(transform.position.x, Mathf.Sin(m_fMoved * Mathf.PI) + 0.5f, transform.position.z);

            if (m_fMoved >= 1.0f)
            {
                m_fMoved = 0.0f;

                transform.position = new Vector3(Mathf.Floor(transform.position.x + 0.5f), 0.5f, Mathf.Floor(transform.position.z + 0.5f));
                m_eDir = DIR.IDLE;

                Debug.Log("--- JUMP END");

                m_pGridMaster.OnDiscoPeteLanded(this, Mathf.FloorToInt(transform.position.x + 0.5f), Mathf.FloorToInt(transform.position.z + 0.5f));
            }

        }
    }

    private DIR ItlGetDirFromInput()
    {
        DIR eDir = DIR.IDLE;

        float fHorizontal = Input.GetAxisRaw("Horizontal");
        float fVertical = Input.GetAxisRaw("Vertical");

        if (fHorizontal > Mathf.Epsilon)
        {
            //Debug.Log("DIR.RIGHT");
            eDir = DIR.RIGHT;
        }
        else if (fHorizontal < -Mathf.Epsilon)
        {
            //Debug.Log("DIR.LEFT");
            eDir = DIR.LEFT;
        }
        else if (fVertical > Mathf.Epsilon)
        {
            //Debug.Log("DIR.UP");
            eDir = DIR.UP;
        }
        else if (fVertical < -Mathf.Epsilon)
        {
            //Debug.Log("DIR.DOWN");
            eDir = DIR.DOWN;
        }

        return eDir;
    }

    private void ItlSetRotationFromDir()
    {
        switch(m_eDir)
        {
            case DIR.RIGHT:
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                break;
            case DIR.LEFT:
                transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                break;
            case DIR.DOWN:
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                break;
            case DIR.UP:
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
                break;
        }
    }
}
