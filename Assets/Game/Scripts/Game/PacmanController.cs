using UnityEngine;
using System.Collections;
using AStarPathfinding;
using DG.Tweening;

public class PacmanController : MonoBehaviour
{
	public enum States
	{
		enState_AttemptMove,
		enState_Moving,
	};
	public States state = States.enState_AttemptMove;

	public enum MoveDirection
	{
		Left,
		Right,
		Up,
		Down
	};

	public Vector2[] MoveDirections = new Vector2[] {	new Vector2(-1, 0),
														new Vector2(1, 0),
														new Vector2(0, 1),
														new Vector2(0, -1) };

	public bool inputChanged = true;
	public MoveDirection inputDirection = MoveDirection.Left;
	public MoveDirection moveDirection = MoveDirection.Left;

	public float speed = 10;

	private Animator _animator;

	void Start ()
	{
		_animator = GetComponent<Animator>();
		onMoveCompleted();
		GameDirector.Instance.pacmanController = this;
	}

    private void OnDestroy()
    {
		GameDirector.Instance.pacmanController = null;
	}

	void FixedUpdate ()
	{
		if (GameDirector.Instance.gameOver == false)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				updateInput(MoveDirection.Left, "Pacman-Left-Animation", -1, 0);
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				updateInput(MoveDirection.Right, "Pacman-Right-Animation", 1, 0);
			}
			else if (Input.GetKey(KeyCode.UpArrow))
			{
				updateInput(MoveDirection.Up, "Pacman-Up-Animation", 0, 1);
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				updateInput(MoveDirection.Down, "Pacman-Down-Animation", 0, -1);
			}
		}
	}

	private void updateInput(MoveDirection _direction, string animationName, int _x, int _y)
	{
		inputDirection = _direction;
		if (state == States.enState_AttemptMove)
		{
			tryMove(inputDirection);
		}
	}

	private bool tryMove(MoveDirection _direction)
	{
        Vector2 currentTileLocation = transform.position;
        Vector2 moveToTile = currentTileLocation + MoveDirections[(int)_direction];
        if (PathFinding.Instance.CollisionMap.CheckCollision((int)moveToTile.x, (int)(moveToTile.y)) == false)
        {
            switch (_direction)
            {
                case MoveDirection.Left:
                    _animator.Play("Pacman-Left-Animation");
                    break;
                case MoveDirection.Right:
                    _animator.Play("Pacman-Right-Animation");
                    break;
                case MoveDirection.Up:
                    _animator.Play("Pacman-Up-Animation");
                    break;
                case MoveDirection.Down:
                    _animator.Play("Pacman-Down-Animation");
                    break;
            }

            state = States.enState_Moving;
			transform.DOMove(new Vector3(moveToTile.x, moveToTile.y, 0), speed).OnComplete(onMoveCompleted).SetEase(Ease.Linear);
            return true;
        }

        return false;
    }

    public void onMoveCompleted()
	{
		if (GameDirector.Instance.gameOver == false)
		{
			state = States.enState_AttemptMove;
			if (tryMove(inputDirection) == false)
			{
				tryMove(moveDirection);
			}
			else
			{
				moveDirection = inputDirection;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		PelletController _pellet = other.GetComponent<PelletController>();
		if (_pellet != null)
		{
			GameDirector.Instance.IncreaseScore(_pellet.Value);
			Destroy(_pellet.gameObject);
		}

		PowerPelletController _powerPellet = other.GetComponent<PowerPelletController>();
		if (_powerPellet != null)
		{
			GameDirector.Instance.ChangeGameState(GameDirector.States.enState_PacmanInvincible);
			Destroy(_powerPellet.gameObject);
		}

		GhostController _ghost = other.GetComponent<GhostController>();
		if (_ghost != null)
		{
			// TODO: We are going to auto kill based off the Game Director needs to be resolved
			if (GameDirector.Instance.state == GameDirector.States.enState_Normal && !_ghost.isDead)
			{
				GameDirector.Instance.ChangeGameState(GameDirector.States.enState_GameOver);
				gameObject.SetActive(false);
			}
			else if (GameDirector.Instance.state == GameDirector.States.enState_PacmanInvincible)
			{
				_ghost.Kill();
			}
		}
	}
}
