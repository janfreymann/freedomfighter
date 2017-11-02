using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyerAmmoManager : MonoBehaviour {

	public Image ammoPrefab;
	public Queue<Image> ammoItems;

	// Use this for initialization
	void Start () {
		GameMaster gm = GameMaster.getInstance ();
		gm.registerFlyerAmmoManager (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public bool checkAndDropFlyer() {
		if ((ammoItems.Count > 0) && (Time.timeScale > 0)) {
			//remove right most ammo item
			Image img = ammoItems.Dequeue();
			Destroy (img);
			return true;
		} else {
			return false;
		}
	}

	public void AddAmmo(int count) {
		//start anchored x, y at 30/25
		// width/height 70
		if(ammoItems == null) { ammoItems = new Queue<Image>(); }
		for (int i = 0; i < count; i++) {
			Image ammo = Instantiate (ammoPrefab);
			ammo.rectTransform.parent = GetComponent<RectTransform> ();

			ammo.rectTransform.anchoredPosition = new Vector2 (((count-ammoItems.Count) * 30), 25);
			ammo.rectTransform.sizeDelta = new Vector2 (70, 70);
			ammoItems.Enqueue (ammo);
		}
	}
}
