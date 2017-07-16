using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DiscoPeteBehaviour : MonoBehaviour {

    // Types
    enum DIR { IDLE, UP, DOWN, LEFT, RIGHT}

	public static readonly int JUMP = Animator.StringToHash("Jump");
	public static readonly int JUMP_DURATION = Animator.StringToHash("JumpDuration");
	public static readonly int SPEED = Animator.StringToHash("Speed");
	public static readonly int BEAT = Animator.StringToHash("Beat");

	// Privates
	private DIR m_eDir = DIR.IDLE;
    private float m_fMoved = 0.0f;
    private float m_fSpeed = 7.0f;
    private bool m_bPrevKeyPressed = false;
	private int m_iLockedBeat = -1;
    private int m_iLastJumpedBeat = -1;
    private bool m_bAlive = true;
    private bool m_bAllowMovement = true;

    private BeatMaster m_pBeatMaster;
    private GridMaster m_pGridMaster;
	private Animator m_pAnimator;
    private GUIMaster m_pGUIMaster;
    private LevelAndPointBehaviour m_pLevelAndPointMaster;

	[SerializeField]
	private GameObject m_pPeteModel;

	[SerializeField]
	private GameObject deathPrefab;

	[SerializeField]
	private AudioSource winSound = null;

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

	    m_pAnimator = GetComponent<Animator>();
		m_pAnimator.SetFloat(JUMP_DURATION, m_fSpeed);
		m_pAnimator.SetFloat(SPEED, m_pBeatMaster.songInfo.Bps);

        GameObject lapGO = GameObject.FindWithTag("LevelAndPointMaster");
        m_pLevelAndPointMaster = lapGO.GetComponent<LevelAndPointBehaviour>();

        if (m_pLevelAndPointMaster == null)
            Debug.Log("DiscoPeteBehaviour: LevelAndPointMaster not found!");
    }

    void OnDisable()
    {
        if (m_pBeatMaster != null)
        {
            m_pBeatMaster.beatEvent -= BeatMasterOnBeatEvent;
            m_pBeatMaster.onJumpChancePassedEvent -= BeatMasterOnJumpChancePassedEvent;
        }
    }

	void OnTriggerEnter(Collider c)
	{
		if(m_bAlive)
			Die();
	}

    // Update is called once per frame
    void Update () {

        if(m_bAlive && m_bAllowMovement)
        {
            ItlUpdateDirection();
            ItlMovePete();
        }
	}

    public void Say(string text)
    {
        //Debug.Log("DISCOPETE says: " + text);
    }

    public void Reset()
    {
        m_bAlive = true;
        m_bAllowMovement = true;

        m_pPeteModel.SetActive(true);
    }

    public void Die()
    {
		if (m_bAlive)
		{
            m_eDir = DIR.IDLE;
            m_fMoved = 0.0f;
			Debug.Log("DISCOPETE IS DEAD!");

			m_bAlive = false;
			m_pPeteModel.SetActive(false);

			if (deathPrefab != null)
			{
				Instantiate(deathPrefab, transform.position, Quaternion.identity);
			}

            m_pLevelAndPointMaster.OnDiscoPeteDied();
		}
    }

    public void Wins()
    {
        m_bAllowMovement = false;
        m_eDir = DIR.IDLE;
        m_fMoved = 0.0f;
        //Debug.Log("YOU HAVE WON!");
        m_pLevelAndPointMaster.OnDiscoPeteFinishedCurrentLevel();

		if (winSound)
		{
			winSound.Play();
		}
    }

    private void BeatMasterOnBeatEvent()
    {
	    m_pAnimator.SetBool(BEAT, true);
	}

    private void BeatMasterOnJumpChancePassedEvent()
    {
        if(m_iLastJumpedBeat < m_pBeatMaster.NearestBeat)
        {
            //Debug.Log("# STAY");

            m_pGridMaster.OnDiscoPeteStaysOnTile(this, Mathf.FloorToInt(transform.position.x + 0.5f), Mathf.FloorToInt(transform.position.z + 0.5f));
        }

		m_pAnimator.SetBool(BEAT, false);
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
                    //Debug.Log("--- JUMP BEGIN");
					m_pAnimator.SetTrigger(JUMP);
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

                //Debug.Log("--- JUMP END");

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
