using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
	SteamVR_TrackedController TrackedController;

	float padX, padY;
	bool isGripp;

	// Use this for initialization
	void Start ()
	{
		TrackedController = GetComponent<SteamVR_TrackedController> ();
		TrackedController.Gripped += Gripped;
		TrackedController.Ungripped += Ungripped;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isGripp) {
			if (TrackedController.controllerState.rAxis0.x != 0 && TrackedController.controllerState.rAxis0.y != 0) {
				Debug.Log ("TrackedController.controllerState.rAxis0.x: " + TrackedController.controllerState.rAxis0.x.ToString ());
				float angle = 90 + TrackedController.controllerState.rAxis0.x * 120;
				Manager.Instance.currentLight.transform.rotation = Quaternion.AngleAxis (angle, Vector3.right);
			}
		}	
	}

	void Gripped (object sender, ClickedEventArgs e)
	{
		isGripp = true;
	}

	void Ungripped (object sender, ClickedEventArgs e)
	{
		isGripp = false;
	}
		
}
