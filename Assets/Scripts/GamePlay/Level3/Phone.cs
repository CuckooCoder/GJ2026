using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
	public ScrollRect scrollRect;

	public List<GameObject> messages;
	int curMessageIndex = 0;

	public void NewMessage()
	{
		if (curMessageIndex < messages.Count)
		{
			messages[curMessageIndex].SetActive(true);
			ScrollToBottomImmediately();
			curMessageIndex++;
		}
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
}
