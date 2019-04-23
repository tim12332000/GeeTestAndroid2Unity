using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeeTestTester : MonoBehaviour
{
	[SerializeField]
	GeeTestHandler g;
	[SerializeField]
	Text t;

	private void Awake()
	{
		g.OnSuccessEvent += OnSuccess;
		g.OnFailedEvnet += OnFailed;
		g.OnClosedEvent += OnClosed;
	}

	public void Open()
	{
		t.text += "[g.Open]";
		g.Open();
	}

	private void OnClosed()
	{
		t.text += "[OnClosed]";
	}

	private void OnFailed()
	{
		t.text += "[OnFailed]";
	}

	private void OnSuccess()
	{
		t.text += "[OnSuccess]";
	}
}
