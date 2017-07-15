using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTile : SafeTile
{

    private bool m_bWon = false;
    private GridMaster m_pGridMaster;
    private GUIMaster m_pGUIMaster;

    protected override void Start()
    {
        base.Start();
        GameObject gmGO = GameObject.FindWithTag("GridMaster");
        m_pGridMaster = gmGO.GetComponent<GridMaster>();

        GameObject guiGO = GameObject.FindWithTag("GUIMaster");
        if(guiGO != null)
            m_pGUIMaster = guiGO.GetComponent<GUIMaster>();
    }

    protected override void Update()
    {
        base.Update();
        if (m_bWon)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                m_bWon = false;

                if(m_pGUIMaster != null)
                    m_pGUIMaster.HideText();

                m_pGridMaster.Reset();
                m_pGridMaster.SetDiscoPeteToStart();
            }
        }
    }

    public override void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
    {
        Debug.Log("YOU HAVE WON!");
        m_bWon = true;

        if(m_pGUIMaster != null)
            m_pGUIMaster.ShowText("YOU WON!", "Press R to restart");
    }
}