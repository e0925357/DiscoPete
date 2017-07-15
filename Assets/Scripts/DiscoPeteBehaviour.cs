using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoPeteBehaviour : MonoBehaviour {

    // Types
    enum DIR { IDLE, UP, DOWN, LEFT, RIGHT}

    // Privates
    private DIR eDir = DIR.IDLE;
    private float fMoved = 0.0f;
    private float fSpeed = 5.0f;
    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        UpdateDirection();
        MovePete();
	}

    void UpdateDirection()
    {
        if(eDir == DIR.IDLE)
        {
            float fHorizontal = Input.GetAxisRaw("Horizontal");
            float fVertical = Input.GetAxisRaw("Vertical");

//            animator.SetTrigger("JUMP");

            if(fHorizontal > 0)
            {
                Debug.Log("DIR.RIGHT");
                eDir = DIR.RIGHT;
            }
            else if(fHorizontal < 0)
            {
                Debug.Log("DIR.LEFT");
                eDir = DIR.LEFT;
            }
            else if(fVertical > 0)
            {
                Debug.Log("DIR.UP");
                eDir = DIR.UP;
            }
            else if(fVertical < 0)
            {
                Debug.Log("DIR.DOWN");
                eDir = DIR.DOWN;
            }
        }
    }

    void MovePete()
    {
        if(eDir != DIR.IDLE)
        {
            float fDelta = fSpeed * Time.deltaTime;
            if (fMoved + fDelta > 1.0f)
                fDelta = 1.0f - fMoved;

            if(eDir == DIR.RIGHT)
            {
                transform.Translate(Vector3.right * fDelta);
            }
            else if(eDir == DIR.LEFT)
            {
                transform.Translate(Vector3.left * fDelta);
            }
            else if (eDir == DIR.UP)
            {
                transform.Translate(Vector3.forward * fDelta);
            }
            else if (eDir == DIR.DOWN)
            {
                transform.Translate(Vector3.back * fDelta);
            }

            fMoved += fDelta;

            if (fMoved == 1.0f)
            {
                fMoved = 0.0f;
                eDir = DIR.IDLE;
            }
        }
    }
}
