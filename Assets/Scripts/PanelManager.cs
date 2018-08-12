using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

	public GameObject BreakPanel;
	public GameObject WinPanel;
	public GameObject GameOverPanel;
	private bool stopped;

	public void OnGameOver() {
		if (IsBreak) {
			HideBreak();
		}
		Time.timeScale = 0;
		UnlockMouse();
		GameOverPanel.SetActive(true);
		stopped = true;
	}

	public void OnWin() {
		if (IsBreak) {
			HideBreak();
		}
		Time.timeScale = 0;
		UnlockMouse();
		WinPanel.SetActive(true);
		stopped = true;
	}


	public void OnContinue() {
		HideBreak();
	}

	void Start()
	{
		LockMouse();
	}


	private bool IsBreak;
	void Update()
	{
		if (!stopped && Input.GetKeyDown(KeyCode.Escape)) {
			if (!IsBreak) {
				ShowBreak(); 
			} else {
				HideBreak();
			}
		}
	}

	private void ShowBreak() {
		IsBreak = true;
		UnlockMouse();
		BreakPanel.SetActive(true);
		Time.timeScale = 0;
	}

	private void HideBreak() {
		IsBreak = false;
		LockMouse();
		BreakPanel.SetActive(false);
		Time.timeScale = 1;
	}

	void LockMouse()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;	
	}

	
	void UnlockMouse()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;	
	}

	
	void OnDestroy()
	{
		UnlockMouse();
		Time.timeScale = 1;
	}
}
