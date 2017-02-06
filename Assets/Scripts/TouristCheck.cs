using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouristCheck : MonoBehaviour
{
	// 需要隐藏的物体
	public GameObject DoorObject;
	// 加载场景名称
	public string SceneName;
	// 场景异步加载操作控制器
	AsyncOperation asyncOperation;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "MainCamera" && asyncOperation == null) {
			asyncOperation = SceneManager.LoadSceneAsync (SceneName, LoadSceneMode.Additive);
		}
	}

	void FixedUpdate ()
	{
		if (asyncOperation != null && asyncOperation.isDone) {
			SceneManager.SetActiveScene (SceneManager.GetSceneByName (SceneName));
			Manager.Instance.StartNewScene (this);
			asyncOperation = null;
			DoorObject.SetActive (false);
		}
	}

	public void Reset ()
	{
		DoorObject.SetActive (true);
		SceneManager.UnloadSceneAsync (SceneName);
	}
}
