using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMaster : MonoBehaviour {

    private Text m_tOverlayLine1;
    private Text m_tOverlayLine2;

	// Use this for initialization
	void Start () {
        GameObject line1GO = GameObject.FindGameObjectWithTag("TextLine1");
        if (line1GO != null)
        {
            m_tOverlayLine1 = line1GO.GetComponent<Text>();
            if (m_tOverlayLine1 != null)
                m_tOverlayLine1.enabled = false;
        }

        GameObject line2GO = GameObject.FindGameObjectWithTag("TextLine2");
        if (line2GO != null)
        {
            m_tOverlayLine2 = line2GO.GetComponent<Text>();
            if (m_tOverlayLine2 != null)
                m_tOverlayLine2.enabled = false;
        }
    }

    public void ShowText(string line1, string line2)
    {
        if (m_tOverlayLine1 != null)
        {
            m_tOverlayLine1.text = line1;
            m_tOverlayLine1.enabled = true;
        }

        if (m_tOverlayLine2 != null)
        {
            m_tOverlayLine2.text = line2;
            m_tOverlayLine2.enabled = true;
        }
    }

    public void HideText()
    {
        if (m_tOverlayLine1 != null)
            m_tOverlayLine1.enabled = false;

        if (m_tOverlayLine2 != null)
            m_tOverlayLine2.enabled = false;
    }
	
}
