using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour {

	protected Vector3 currentTarget;

	protected float targetEpsilon = 0.05f;
	protected float charSpeed = 2f;

	protected Rigidbody2D _rigidbody;
	protected Collider2D _collider;

	protected string lastAxis;
	protected string lastDirection;
	protected bool followLastDirection;
	protected string forbiddenAxis;
	protected string lastFaxis;

	protected int collisionFrameCount;
	protected int runfreeCount;
	protected string freeDirection;

	protected Vector3 lastPosition;

	public Vector2[] targets;
	public int targetIndx;

	public TilesMap tm;

	private NavMeshAgent agent;

	//public Transform cp;

	// Use this for initialization
	public void Start () {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_collider = GetComponent<Collider2D> ();
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

	protected bool MoveToTarget() {
		

		return false; 
	}
	protected void selectNextTarget() {	
		currentTarget = new Vector3(targets [targetIndx].x, targets [targetIndx].y, 0.0f);
		targetIndx = (targetIndx + 1) % targets.Length;
		agent.SetDestination (currentTarget);
	}



}
