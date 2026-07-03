using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[Serializable]
public class InputBuffer
{
	public InputName name;
	public float time = 0.1f;
	[HideInInspector]
	public bool enable;
	[HideInInspector]
	public float value;
}

public enum InputName
{
	Jump,
	Down,
	Interact
}

public class PlayerInput : MonoBehaviour
{

	protected PlayerInputActions inputActions;

	public float horizontal;
	public List<InputBuffer> bufferList;

	protected Dictionary<InputName, InputBuffer> bufferDict;
	protected Dictionary<InputName, Coroutine> bufferCoroutineDict;

	protected virtual void Awake()
	{
#if UNITY_STANDALONE
		inputActions = new PlayerInputActions();
#endif
		bufferDict = new Dictionary<InputName, InputBuffer>();
		bufferCoroutineDict = new Dictionary<InputName, Coroutine>();
		foreach (var buffer in bufferList)
		{
			bufferDict.Add(buffer.name, buffer);
		}
	}

	private void Start()
	{
		EnableGameplayInputs();
	}

	private void OnDestroy()
	{
		DisableGameplayInputs();
	}

	public virtual void EnableGameplayInputs()
	{
#if UNITY_STANDALONE
		inputActions.GamePlay.Enable();
		inputActions.GamePlay.Horizontal.performed += OnHorizontalPerformed;
		inputActions.GamePlay.Horizontal.canceled += OnHorizontalPerformed;
		inputActions.GamePlay.Down.performed += OnDownPerformed;
		inputActions.GamePlay.Jump.performed += OnJumpPerformed;
		inputActions.GamePlay.Interact.performed += OnInteractPerformed;
#endif
	}

	public virtual void DisableGameplayInputs()
	{
#if UNITY_STANDALONE
		inputActions.GamePlay.Disable();
		inputActions.GamePlay.Horizontal.performed -= OnHorizontalPerformed;
		inputActions.GamePlay.Horizontal.canceled -= OnHorizontalPerformed;
		inputActions.GamePlay.Down.performed -= OnDownPerformed;
		inputActions.GamePlay.Jump.performed -= OnJumpPerformed;
		inputActions.GamePlay.Interact.performed -= OnInteractPerformed;
#endif
	}

	protected void OnHorizontalPerformed(InputAction.CallbackContext ctx)
	{
		float newValue = ctx.ReadValue<float>();
		if (!Mathf.Approximately(newValue, horizontal))
		{
			horizontal = newValue;
		}
	}

	protected void OnDownPerformed(InputAction.CallbackContext ctx)
	{
		if (ctx.interaction is PressInteraction)
		{
			SetInputBufferTimer(InputName.Down);
		}
	}

	protected void OnJumpPerformed(InputAction.CallbackContext ctx)
	{
		if (ctx.interaction is PressInteraction)
		{
			SetInputBufferTimer(InputName.Jump);
		}
	}

	protected void OnInteractPerformed(InputAction.CallbackContext ctx)
	{
		if (ctx.interaction is PressInteraction)
		{
			SetInputBufferTimer(InputName.Interact);
		}
	}

	public InputBuffer GetBuffer(InputName name)
	{
		InputBuffer buffer = null;
		bufferDict.TryGetValue(name, out buffer);
		return buffer;
	}

	public bool HasBuffer(InputName name)
	{
		InputBuffer buffer = GetBuffer(name);
		if (buffer != null)
		{
			return buffer.enable;
		}
		return false;
	}

	public void UseBuffer(InputName name)
	{
		InputBuffer buffer = GetBuffer(name);
		if (buffer != null)
		{
			buffer.enable = false;
		}
	}

	public void SetInputBufferTimer(InputName name, float value = 0)
	{
		StopBufferCoroutine(name);
		var coroutine = StartCoroutine(InputBufferCoroutine(name, value));
		bufferCoroutineDict[name] = coroutine;
	}

	public void StopBufferCoroutine(InputName name)
	{
		bufferCoroutineDict.TryGetValue(name, out Coroutine coroutine);
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
			bufferCoroutineDict.Remove(name);
		}
	}

	IEnumerator InputBufferCoroutine(InputName name, float value = 0)
	{
		var buffer = GetBuffer(name);
		buffer.value = value;
		buffer.enable = true;
		yield return new WaitForSeconds(buffer.time);
		buffer.enable = false;
		bufferCoroutineDict.Remove(name);
	}
}
