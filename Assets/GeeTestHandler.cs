using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeeTestHandler : MonoBehaviour
{
	/**
	* api1，需替换成自己的服务器URL
	*/
	private static String CAPTCHA_URL = "https://www.geetest.com/demo/gt/register-slide";
	/**
     * api2，需替换成自己的服务器URL
     */
	private static String VALIDATE_URL = "https://www.geetest.com/demo/gt/validate-slide";

	public event Action OnSuccessEvent;
	public event Action OnFailedEvnet;
	public event Action OnClosedEvent;

	public bool AutoRelease = true;

	private AndroidJavaObject _androidJavaObjM;
	private AndroidJavaObject _androidJavaObj
	{
		get
		{
			if (_androidJavaObjM == null)
			{
				try
				{
					AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					_androidJavaObjM = jc.GetStatic<AndroidJavaObject>("currentActivity");

					//AndroidJavaClass jc = new AndroidJavaClass("com.example.unity_exchange.MainActivity");
					//_androidJavaObjM = jc.CallStatic<AndroidJavaObject>("GetInstance");
				}
				catch (Exception e)
				{
					Debug.LogErrorFormat("GeeTestHandler._androidJavaObj e {0}", e.ToString());
					OnFailedEvnet?.Invoke();
				}
			}

			return _androidJavaObjM;
		}
	}

	void FormAndroidMessage(string msg)
	{
		switch (msg)
		{
			case "Success":
				OnSuccessEvent?.Invoke();
				break;
			case "Failed":
				OnFailedEvnet?.Invoke();
				break;
			case "Closed":
				OnClosedEvent?.Invoke();
				break;
			default:
				break;
		}

		if (AutoRelease)
		{
			Release();
		}
	}

	public void Open()
	{
		try
		{
			_androidJavaObj.Call("Open", CAPTCHA_URL, VALIDATE_URL);
		}
		catch (Exception e)
		{
			Debug.LogErrorFormat("[GeeTestHandler.Open] e {0}", e);
			OnFailedEvnet?.Invoke();
		}
	}

	/// <summary>
	/// Setting AutoRelase = True , It Will Invoke When GeeTest Done.
	/// </summary>
	public void Release()
	{
		try
		{
			_androidJavaObj.Call("Relese");
		}
		catch(Exception e)
		{
			Debug.LogErrorFormat("[GeeTestHandler.Release] e {0}", e);
		}
	}
}
