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


    private BeatMaster m_pBeatMaster;
  //  private Animator animator;

    // Use this for initialization
    void OnEnable()
    {
        GameObject bmGO = GameObject.FindWithTag("Music");
        m_pBeatMaster = bmGO.GetComponent<BeatMaster>();
        //    animator = GetComponent<Animator>();

        m_pBeatMaster.beatEvent += BeatMasterOnBeatEvent;
    }

    void OnDisable()
    {
        if (m_pBeatMaster != null)
            m_pBeatMaster.beatEvent -= BeatMasterOnBeatEvent;
    }

    // Update is called once per frame
    void Update () {

        UpdateDirection();
        MovePete();
	}

    void UpdateDirection()
    {
        DIR eCurrDir = ItlGetDirFromInput();

        if(m_eDir == DIR.IDLE && !m_bPrevKeyPressed && m_pBeatMaster.allowsJump())
        {
            m_eDir = eCurrDir;

          //  if(eDir != DIR.IDLE)
            //    animator.SetTrigger("JUMP");
        }

        m_bPrevKeyPressed = (eCurrDir != DIR.IDLE);
    }

    void MovePete()
    {
        if(m_eDir != DIR.IDLE)
        {
            float fDelta = m_fSpeed * Time.deltaTime;
            if (m_fMoved + fDelta > 1.0f)
                fDelta = 1.0f - m_fMoved;

            if(m_eDir == DIR.RIGHT)
            {
                transform.Translate(Vector3.right * fDelta);
            }
            else if(m_eDir == DIR.LEFT)
            {
                transform.Translate(Vector3.left * fDelta);
            }
            else if (m_eDir == DIR.UP)
            {
                transform.Translate(Vector3.forward * fDelta);
            }
            else if (m_eDir == DIR.DOWN)
            {
                transform.Translate(Vector3.back * fDelta);
            }

            m_fMoved += fDelta;

            // Jumping
            transform.position = new Vector3(transform.position.x, Mathf.Sin(m_fMoved * Mathf.PI), transform.position.z);

            if (m_fMoved >= 1.0f)
            {
                m_fMoved = 0.0f;
                transform.position = new Vector3(Mathf.Floor(transform.position.x + 0.5f), 0.0f, Mathf.Floor(transform.position.z + 0.5f));
                m_eDir = DIR.IDLE;
            }
        }
    }

    private void BeatMasterOnBeatEvent()
    {
        //print("DiscoPete Beat");
       // animator.SetTrigger("BEAT");
    }

    private DIR ItlGetDirFromInput()
    {
        DIR eDir = DIR.IDLE;

        float fHorizontal = Input.GetAxisRaw("Horizontal");
        float fVertical = Input.GetAxisRaw("Vertical");

        if (fHorizontal > 0)
        {
            //Debug.Log("DIR.RIGHT");
            eDir = DIR.RIGHT;
        }
        else if (fHorizontal < 0)
        {
            //Debug.Log("DIR.LEFT");
            eDir = DIR.LEFT;
        }
        else if (fVertical > 0)
        {
            //Debug.Log("DIR.UP");
            eDir = DIR.UP;
        }
        else if (fVertical < 0)
        {
            //Debug.Log("DIR.DOWN");
            eDir = DIR.DOWN;
        }

        return eDir;
    }
}
