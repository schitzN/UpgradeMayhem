using UnityEngine;
using System.Collections;

public class MoveWall : MonoBehaviour {

	private float _runSpeed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (this.transform.position.x - Time.deltaTime * this._runSpeed, this.transform.position.y, this.transform.position.z);

		if(this.transform.position.x <= -13.2f)
			this.transform.position = new Vector3 (11.65f, this.transform.position.y, this.transform.position.z);
	}
}
