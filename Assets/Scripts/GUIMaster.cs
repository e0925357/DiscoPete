using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMaster : MonoBehaviour
{
	[SerializeField]
	private float xOvershoot = 100f;
	[SerializeField]
	private float screenWidth = 1024f;
	[SerializeField]
	private float swipeSpeed = 100f;
	[SerializeField]
	private float swipeSpeed2 = 100f;
	[SerializeField]
    private Text textLine1;
	[SerializeField]
	private RectTransform panel1;

	private Vector3 panel1Target1;
	private Vector3 panel1Target2;

	[Space]
	[SerializeField]
    private Text textLine2;
	[SerializeField]
	private RectTransform panel2;

	private Vector3 panel2Target1;
	private Vector3 panel2Target2;
	private Coroutine animationCoroutine1;
	private Coroutine animationCoroutine2;

	// Use this for initialization
	void Start ()
	{
		HideText();

		panel1Target1 = panel1.position - new Vector3(xOvershoot, 0, 0);
		panel1Target2 = panel1.position + new Vector3(xOvershoot, 0, 0);

		panel2Target1 = panel2.position + new Vector3(xOvershoot, 0, 0);
		panel2Target2 = panel2.position - new Vector3(xOvershoot, 0, 0);
	}

    public void ShowText(string line1, string line2)
    {
        if (textLine1 != null)
        {
            textLine1.text = line1;
            textLine1.enabled = true;

	        panel1.GetComponent<Image>().enabled = true;
	        panel1.position = new Vector3(0, panel1.position.y, panel1.position.z);

	        animationCoroutine1 = StartCoroutine(AnimatePanels(panel1, panel1Target1, panel1Target2));
		}

        if (textLine2 != null)
        {
            textLine2.text = line2;
            textLine2.enabled = true;

	        panel2.GetComponent<Image>().enabled = true;
	        panel2.position = new Vector3(screenWidth, panel2.position.y, panel2.position.z);

	        animationCoroutine2 = StartCoroutine(AnimatePanels(panel2, panel2Target1, panel2Target2));
		}

    }

	IEnumerator AnimatePanels(RectTransform panel, Vector3 target1, Vector3 target2)
	{
		bool shouldMove = true;

		while (shouldMove)
		{
			panel.position = Vector3.MoveTowards(panel.position, target1, swipeSpeed * Time.deltaTime);

			shouldMove = (panel.position - target1).sqrMagnitude > 1f;

			yield return new WaitForEndOfFrame();
		}

		while (true)
		{
			Vector3 delta = target2 - panel.position;
			float deltaMag = delta.magnitude;
			float t = deltaMag / (xOvershoot * 2);

			panel.position = Vector3.MoveTowards(panel.position, target2, Mathf.Max(swipeSpeed2 * t*t * Time.deltaTime, 0.2f * Time.deltaTime));

			yield return new WaitForEndOfFrame();
		}
	}

    public void HideText()
    {
		if (textLine1 != null)
		    textLine1.enabled = false;

	    if (panel1 != null)
		    panel1.GetComponent<Image>().enabled = false;

	    if (textLine2 != null)
		    textLine2.enabled = false;

	    if (panel2 != null)
		    panel2.GetComponent<Image>().enabled = false;

	    if (animationCoroutine1 != null)
	    {
		    StopCoroutine(animationCoroutine1);
		    animationCoroutine1 = null;
	    }

	    if (animationCoroutine2 != null)
	    {
		    StopCoroutine(animationCoroutine2);
		    animationCoroutine2 = null;
	    }
	}
	
}
