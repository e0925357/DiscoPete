using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : Assets.Scripts.AbstractTrap {

    private bool active = false;

    private Renderer m_pRenderer;
    private Collider m_pCollider;

    protected override void Start()
    {
        base.Start();
        m_pRenderer = GetComponent<Renderer>();
        m_pCollider = GetComponent<Collider>();
    }

    protected override bool Active
    {
        get
        {
            return active;
        }
        set
        {
            active = value;
            m_pRenderer.enabled = active;
            m_pCollider.enabled = active;
        }
    }
}
