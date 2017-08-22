using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapControl : MonoBehaviour {

	private List<MiniMapActor> actors;

	public Image playerImage;
	public Image policeImage;
	public Image citizenImage;
	public Image citizenTurnedImage;


	void Awake() {
		actors = new List<MiniMapActor> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (MiniMapActor miniMapActor in actors) {
			miniMapActor.image.GetComponent<RectTransform> ().anchoredPosition = convertPosition (miniMapActor.actor.transform.localPosition);
		}
	}

	public void addActor(Person actor) {
		Debug.Log ("add actor with tag " + actor.tag);
		Image pImage = null;
		switch(actor.tag) {
			case "Player":
				pImage = Instantiate<Image> (playerImage);
				break;
			case "Citizen":
				pImage = Instantiate<Image> (citizenImage);
				break;
			case "Turned":
				pImage = Instantiate<Image> (citizenTurnedImage);
				break;
			case "Police":
				pImage = Instantiate<Image> (policeImage);
				break;
		}
	
		if (pImage != null) {
			pImage.rectTransform.parent = GetComponent<RectTransform> ();
			pImage.rectTransform.anchoredPosition = new Vector2 (40f, 40f);
			actors.Add (new MiniMapActor (actor, pImage));
		} else {
			Debug.Log ("mini map: added actor with unknown tag");
		}

	}
	public void removeActor(Person actorToRemove) {
		Debug.Log ("removeActor() " + actors.Count);
		List<MiniMapActor> tmp = new List<MiniMapActor> ();

		foreach (MiniMapActor miniMapActor in actors) {			
			if (actorToRemove.GetUuid () != miniMapActor.GetUuid ()) {
				tmp.Add (miniMapActor);
			} else {
				Destroy (miniMapActor.image);
			}

		}

		actors.Clear();
		actors.AddRange (tmp);
	}
	private Vector2 convertPosition(Vector3 worldPos) { //convert world coordinates to anchored mini map coordinates
		float normX = (worldPos.x - GodScript.boundsXmin) / (GodScript.boundsXmax-GodScript.boundsXmin);
		float normY = (worldPos.y - GodScript.boundsYmin) / (GodScript.boundsYmax-GodScript.boundsYmin);
		float mapSize = GetComponent<RectTransform> ().sizeDelta.x;
		return new Vector2 (normX * mapSize, normY * mapSize);
	}
}
public class MiniMapActor {
	public Person actor;
	public Image image;
	public MiniMapActor(Person _actor, Image _image) {
		actor = _actor;
		image = _image;
	}
	public int GetUuid() {
		if (actor != null) {
			return actor.GetUuid ();
		} else {
			return -1; //unknown...
		}
	}
		
}