using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTile : SafeTile {

    private bool m_bWon = false;
    private GridMaster m_pGridMaster;

    protected override void Start()
    {
        base.Start();
        GameObject gmGO = GameObject.FindWithTag("GridMaster");
        m_pGridMaster = gmGO.GetComponent<GridMaster>();
    }

    protected override void Update()
    {
        base.Update();
        if (m_bWon)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                m_bWon = false;
                m_pGridMaster.Reset();
                m_pGridMaster.SetDiscoPeteToStart();
            }
        }
    }

    public override void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
    {
        Debug.Log("YOU HAVE WON!");
        m_bWon = true;
    }

   

    private void OnGUI()
    {
        if (m_bWon)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50.0f, Screen.height / 2 - 50.0f, 400f, 400f), "YOU HAVE WON!\n\nPress R to restart");
        }
    }
}
