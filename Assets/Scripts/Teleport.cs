﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

	public GameObject GoLeft;

	Tourism_LaserPointer slLeft;
	SteamVR_TrackedController stLeft;
	ClickedEventHandler ce;
	Transform currentTransform;
	PointerEventArgs tp;

	// Use this for initialization
	void Start ()
	{
		// 初始化变量，注册监听方法
		slLeft = GoLeft.GetComponent<Tourism_LaserPointer> ();
		stLeft = GoLeft.GetComponent<SteamVR_TrackedController> ();

		// 注册监听事件： LeftPointerIn （手柄有物体指向事件）、 LeftPointerOut （取消指向事件）、 TriggerClicked （扳机扣下事件）
		slLeft.PointerIn += LeftPointerIn;
		slLeft.PointerOut += LeftPointerOut;
		stLeft.TriggerClicked += TriggerClicked;
	}

	void LeftPointerIn (object sender, PointerEventArgs e)
	{
		// 当有物体指向时设置全局变量标识
		currentTransform = e.target;
	}

	void LeftPointerOut (object sender, PointerEventArgs e)
	{
		// 取消指向事件时将标识设置为空
		currentTransform = null;
	}

	void TriggerClicked (object sender, ClickedEventArgs e)
	{
		// 如果有指向物体则调用传送至目标位置
		if (currentTransform != null) {
			TeleportByPosition (slLeft.HitPoint);
		}
	}

	private void TeleportByPosition (Vector3 targetPosition)
	{

		//Debug.Log ("targetPosition:" + targetPosition.x + "_" + targetPosition.y + "_" + targetPosition.z);

		this.gameObject.transform.position = new Vector3 (targetPosition.x - GoLeft.transform.localPosition.x, targetPosition.y, targetPosition.z - GoLeft.transform.localPosition.z);

	}
}
