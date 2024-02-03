using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameDirector : Singleton<GameDirector>
{
	public int ScoreValue = 0;
	public Text ScoreText;
	public bool gameOver = false;
	public float PowerPelletTime = 2.0f;
	private float powerPelletCounter = 0.0f;
	[HideInInspector] public List<GhostController> Ghosts = new List<GhostController>();
	[HideInInspector] public PacmanController pacmanController;

	public enum States
	{
		enState_Normal,
		enState_PacmanInvincible,
		enState_GameOver,
	};
	public States state = States.enState_Normal;

	[System.Serializable]
	public class GameStateChangedEvent : UnityEvent<GameDirector.States> { }
	public GameStateChangedEvent GameStateChanged;

	void Start()
	{
		string formatString = System.String.Format("{0:D9}", ScoreValue);
		ScoreText.text = formatString;
	}

	public void IncreaseScore(int value)
	{
		ScoreValue += value;
		string formatString = System.String.Format("{0:D9}", ScoreValue);
		ScoreText.text = formatString;
	}

	public void ChangeGameState(States _newState)
	{
		state = _newState;
		switch (state)
		{
			case States.enState_Normal:
				powerPelletCounter = 0.0f;
				break;

			case States.enState_PacmanInvincible:
				powerPelletCounter = 0.0f;
				break;

			case States.enState_GameOver:
				gameOver = true;
				break;
		}
		GameStateChanged.Invoke(state);
	}

	public void Update()
	{
		switch(state)
		{
			case States.enState_PacmanInvincible:
				powerPelletCounter += Time.deltaTime;
				if (powerPelletCounter >= PowerPelletTime)
				{
					ChangeGameState(States.enState_Normal);
				}
				break;
		}
	}
}
