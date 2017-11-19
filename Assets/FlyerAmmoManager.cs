using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyerAmmoManager : MonoBehaviour {

	public GameObject ammoPrefab;
	public Queue<GameObject> ammoItems;
	private PlayerCharacter character;

	// Use this for initialization
	void Start () {
		GameMaster gm = GameMaster.getInstance ();
		gm.registerFlyerAmmoManager (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setCharacter(PlayerCharacter c) {
		this.character = c;
	}
	public bool checkAndDropFlyer(Vector3 flyerPos) {
		if ((ammoItems.Count > 0) && (Time.timeScale > 0)) {
			//remove right most ammo item
			GameObject img = ammoItems.Dequeue();
			FlyerAmmoAnimation animation = img.GetComponent<FlyerAmmoAnimation> ();
			animation.startFlyingTo (flyerPos, character);
			//Destroy (img);
			return true;
		} else {
			return false;
		}
	}

	public void AddAmmo(int count) {
		//start anchored x, y at 30/25
		// width/height 70
		if(ammoItems == null) { ammoItems = new Queue<GameObject>(); }
		for (int i = 0; i < count; i++) {
			GameObject ammo = Instantiate (ammoPrefab);
			RectTransform rt = ammo.GetComponent<RectTransform> ();
			rt.parent = GetComponent<RectTransform> ();


			rt.anchoredPosition = new Vector2 (((count-ammoItems.Count) * 30), 25);
			rt.sizeDelta = new Vector2 (70, 70);
			ammoItems.Enqueue (ammo);
		}
	}
}
