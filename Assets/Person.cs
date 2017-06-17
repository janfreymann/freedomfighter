using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour {

	protected Vector3 currentTarget;

	protected float targetEpsilon = 0.05f;
	protected float charSpeed = 2f;

	protected Rigidbody _rigidbody;
	protected Collider _collider;

	protected string lastAxis;
	protected string lastDirection;
	protected bool followLastDirection;
	protected string forbiddenAxis;
	protected string lastFaxis;

	protected int collisionFrameCount;
	protected int runfreeCount;
	protected string freeDirection;

	protected Vector3 lastPosition;

	public Transform[] targets;
	public int targetIndx;

	public TilesMap tm;

	protected NavMeshAgent agent;

	//public Transform cp;

	// Use this for initialization
	public void Start () {
		_rigidbody = GetComponent<Rigidbody> ();
		_collider = GetComponent<Collider> ();
		lastAxis = "x";
		forbiddenAxis = "0";
		lastFaxis = "x";
		lastDirection = "up";
		followLastDirection = false;
		runfreeCount = 0;
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	public void Update () {
		
		//transform.rotation = Quaternion.identity;
	}

	protected void selectNextTarget() {	
		currentTarget = new Vector3(targets [targetIndx].position.x, targets [targetIndx].position.y, targets[targetIndx].position.z);
		targetIndx = (targetIndx + 1) % targets.Length;
		agent.SetDestination (currentTarget);
	}



}
