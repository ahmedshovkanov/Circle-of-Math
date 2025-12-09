using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveComponent : MonoBehaviour
{
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
	{ 

	}

    private Rigidbody2D _rb;

	[SerializeField] private float _touchSpeedModif;

	[SerializeField] float moveSpeed = 0.75f;

	Touch touch;
	Vector3 touchPosition, whereToMove;

	float previousDistanceToTouchPos, currentDistanceToTouchPos;

	void Update()
	{

		//if (isMoving)
		//	currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;

		if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				previousDistanceToTouchPos = 0;
				currentDistanceToTouchPos = 0;
				//isMoving = true;
				touchPosition = GameFlowController.instance.GlobalCameraRef.ScreenToWorldPoint(touch.position);
				//Debug.Log(touchPosition);
				touchPosition.z = 0;
				whereToMove = (touchPosition - transform.position).normalized;
				_rb.linearVelocity = new Vector2(whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
			}
		}

		if (currentDistanceToTouchPos > previousDistanceToTouchPos)
		{
			//isMoving = false;
			//rb.velocity = Vector2.zero;
		}

		//if (isMoving)
		//	previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
	}
}
