using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFlyer : MonoBehaviour {

    public GameObject AnimatedFlyer;
    public GameObject Text;
    public GameObject Overlay;
    private Animator animatorFlyer;
    private Animator animatorOverlay;

    // Use this for initialization
    void Start () {
        animatorFlyer = AnimatedFlyer.GetComponent<Animator>();
        animatorOverlay = Overlay.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowFlyer()
    {
        animatorFlyer.SetTrigger("Show");
        animatorOverlay.SetTrigger("Show");
        Text.gameObject.SetActive(true);
    }

    public void HideFlyer()
    {
        animatorFlyer.SetTrigger("Hide");
        animatorOverlay.SetTrigger("Hide");
        Text.gameObject.SetActive(false);
    }
}
