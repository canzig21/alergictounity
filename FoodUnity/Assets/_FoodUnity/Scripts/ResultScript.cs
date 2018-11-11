using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ResultScript : MonoBehaviour
{
	[Serializable]
	private class Item
	{
		public string[] attributes;
	}

	[Serializable]
	private class Ingredient
	{
		public string name;
	}

	public string id;
	public GameObject good;
	public GameObject bad;

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("starting");
		StartCoroutine(MakeRequest());
		Debug.Log("done");
	}

	private IEnumerator MakeRequest()
	{
		Debug.Log("request start");
		string url = $"http://ec2-52-91-0-220.compute-1.amazonaws.com:3000/getItem/{id}";
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log("network error");
		}
		else if (www.isNetworkError)
		{
			Debug.Log("other error: " + www.error);
		}
		else
		{
			Debug.Log("isdone: " + www.isDone);
			Debug.Log("good: " + www.downloadHandler.text);
			if (www.downloadHandler != null)
			{
				Item item = JsonUtility.FromJson<Item>(www.downloadHandler.text);
				if (item.attributes.Contains("vegan") == false)
				{
					ShowBad();
				}
				else
				{
					ShowGood();
				}
			}
			else
			{
				Debug.Log("no downloadhnalder");
			}
			Debug.Log("request done");
		}
	}

	private void ShowGood()
	{
		Debug.Log("ShowGood");
		bad.SetActive(false);
	}

	private void ShowBad()
	{
		Debug.Log("ShowBad");

		good.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
