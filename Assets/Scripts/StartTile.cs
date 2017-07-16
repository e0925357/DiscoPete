using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : SafeTile {

    private LevelAndPointBehaviour m_pLevelMaster;

    private void OnEnable()
    {
        GameObject lapGO = GameObject.FindWithTag("LevelAndPointMaster");
        m_pLevelMaster = lapGO.GetComponent<LevelAndPointBehaviour>();
    }

    public override void OnDiscoPeteLeaves(DiscoPeteBehaviour pete)
    {
        m_pLevelMaster.OnDiscoPeteJumpedFromStart();
    }
}
