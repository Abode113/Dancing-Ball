using UnityEngine;
using System.Collections;

public class CameraMotor : MonoBehaviour {

	public GameObject target;
	public float xOffset, yOffset, zOffset;
	
	// Update is called once per frame
	void Update () {
		if (!target)
			return;
		transform.position = target.transform.position + new Vector3 (xOffset, yOffset, zOffset);

		transform.LookAt (target.transform.position);
	}
}
