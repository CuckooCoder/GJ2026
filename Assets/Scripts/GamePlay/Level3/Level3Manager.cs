using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Manager : LevelManager
{
	public ScrollRect scrollRect;

	public List<GameObject> messages;
	public List<GameObject> sendMessages;
	public float messageInterval = 1f;
	public GameObject buttonRoot;
	public Animator handAnimator;
	public GameObject endPanel;
	public GameObject end1Panell;
	public GameObject end1Panel2;

	protected override void Start()
	{
		base.Start();
		StartCoroutine(ReceiveMesaages());
	}

	public void ScrollToBottomImmediately()
	{
		RectTransform content = scrollRect.content;
		// 强制立刻重建布局，更新Content真实高度
		LayoutRebuilder.ForceRebuildLayoutImmediate(content);
		Canvas.ForceUpdateCanvases();

		scrollRect.verticalNormalizedPosition = 0f;
		// 停止惯性滑动防止回弹
		scrollRect.velocity = Vector2.zero;
	}

	public void SendMessage(int index)
	{
		if(!GameManager.Instance.control)
		{
			return;
		}
		GameManager.Instance.control = false;
		buttonRoot.SetActive(false);
		StartCoroutine(SendMesaages(index));
	}

	IEnumerator ReceiveMesaages()
	{
		yield return new WaitForSeconds(messageInterval);
		for (int i = 0; i < messages.Count; i++)
		{
			messages[i].SetActive(true);
			ScrollToBottomImmediately();
			yield return new WaitForSeconds(messageInterval);
		}
		buttonRoot.SetActive(true);
	}

	IEnumerator SendMesaages(int index)
	{
		handAnimator.enabled = true;
		yield return new WaitForSeconds(messageInterval);
		sendMessages[index].SetActive(true);
		ScrollToBottomImmediately();
		yield return new WaitForSeconds(messageInterval);
		switch (index)
		{
			case 0:
				Success();
				break;
			case 1:
				Fail();
				break;
			case 2:
				Fail();
				break;
		}
	}
}
