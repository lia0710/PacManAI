using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AStarPathfinding;
using UnityEngine.Events;
using DG.Tweening;

public class GhostController : MonoBehaviour
{
	public Vector2 ReturnLocation = new Vector2(0, 0);

	private Animator _animator;
	public Transform PacMan;
	public float speed;

	[SerializeField] private Vector2 moveToLocation; // Allows editing in the inspector but forces method call to set

	private bool generatePath = true;
	private bool nextPoint = true;
	public int pathIndex = 0;
	public List<Vector3> path = new List<Vector3>();

	private bool pathCompleted = false;
	public UnityEvent pathCompletedEvent = new UnityEvent();
	public UnityEvent moveCompletedEvent = new UnityEvent();
	public UnityEvent killedEvent = new UnityEvent();

	public bool isDead = false;
	public Vector3 position;

	void Start()
	{
		_animator = GetComponent<Animator>();
		GameDirector.Instance.GameStateChanged.AddListener(GameStateChanged);
	}

    private void OnDestroy()
    {
		GameDirector.Instance.GameStateChanged.RemoveListener(GameStateChanged);
    }

    public void SetMoveToLocation(Vector2 location)
	{
		moveToLocation = location;
		generatePath = true;
		pathCompleted = false;
	}

	public void Update()
	{
		if (GameDirector.Instance.gameOver == true)
		{
			return;
		}
		position = gameObject.transform.position;

		// Leave this here. Because the Pathfinding is in a DLL it doesn't get setup right away.
		// Call SetMoveToLocation if you want to set a new position for the Ghost.
		// BUT
		// Be aware that if you do this every frame it could cause a slowdown and you shouldn't anyway
		// You also can't change where your going instantly. You must wait till you get to your current
		// seek point
		if (generatePath == true && nextPoint == true)
		{
			generatePath = false;
			pathIndex = 0;
			PathFinding.Instance.generatePath(transform.position, moveToLocation, path);
			pathIndex++; // Go to next point. The first is where you are
		}

		if (pathIndex < path.Count && nextPoint == true)
		{
			nextPoint = false;
			transform.DOMove(new Vector3(path[pathIndex].x, path[pathIndex].y, 0), speed).OnComplete(onMoveCompleted).SetEase(Ease.Linear);
			pathIndex++;
		}
		else if (pathIndex == path.Count && pathCompleted == false && nextPoint == true)
		{
			pathCompleted = true;
			pathCompletedEvent.Invoke();
        }
        if (!isDead) { _animator.SetBool("IsDead", false); }
    }

	public void onMoveCompleted()
	{
		nextPoint = true;
		moveCompletedEvent.Invoke();
	}

	public void Kill()
	{
		_animator.SetBool("IsDead", true);
		isDead = true;
		killedEvent.Invoke();
	}

	public void GameStateChanged(GameDirector.States _state)
	{
		switch (_state)
		{
			case GameDirector.States.enState_Normal:
				_animator.SetBool("IsGhost", false);
				break;

			case GameDirector.States.enState_PacmanInvincible:
				_animator.SetBool("IsGhost", true);
				break;

			case GameDirector.States.enState_GameOver:
				break;
		}
	}
}
