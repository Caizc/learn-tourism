using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

	// Manager 单例
	public static Manager Instance;
	public TouristCheck currentCheck;
	// 当前场景的光照
	public Light currentLight;

	void Awake ()
	{
		// 实现单例
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogError ("Only One Manager is Allowed");
		}
	}

	public void StartNewScene (TouristCheck touristCheck)
	{
		if (currentCheck == null) {
			currentCheck = touristCheck;
		} else if (currentCheck != touristCheck) {
			currentCheck.Reset ();
			currentCheck = touristCheck;
		}

		currentLight = GameObject.Find ("Directional Light").GetComponent<Light> ();
	}


	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
