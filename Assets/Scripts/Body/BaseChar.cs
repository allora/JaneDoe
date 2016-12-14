using UnityEngine;

public class BaseChar : BaseBody 
{
	[Header("Mobility")]
	[SerializeField]
	float _maxSpeed = 20f;

	[SerializeField]
	float _smoothTime = 0.5f;
	[SerializeField]
	float _maxRotSpeed = 500f;

	float _curRotation = 0f;
	float _desiredRotation = 0f;
	float _rotVel = 0f;

	float _desiredSpeed = 0f;
	float _curSpeed = 0f;

	Rigidbody2D _rb = null;

	public bool WantsToMoveForward {get;set;}
	public bool WantsToMoveBackward {get;set;}

	void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		_curRotation = Mathf.Atan2(_rb.transform.forward.y, _rb.transform.forward.x) * Mathf.Rad2Deg;
		_desiredRotation = _curRotation;
		_rotVel = 0f;

		_curSpeed = 0f;
		_desiredSpeed = 0f;
	}

    // Update is called once per frame
    void Update()
    {
        UpdateMotion();
    }

	public void SetHeading(float heading)
	{
		_desiredRotation = heading;
	}

	void UpdateMotion()
	{
		if (WantsToMoveForward)
			_desiredSpeed = _maxSpeed;
		else if (WantsToMoveBackward)
            _desiredSpeed = -_maxSpeed;
		else
			_desiredSpeed = 0f;
		
		_curRotation = Mathf.SmoothDampAngle(_curRotation, _desiredRotation, ref _rotVel, _smoothTime, _maxRotSpeed);
		_rb.MoveRotation(_curRotation);

        _curSpeed = _desiredSpeed;
		//_curSpeed = Mathf.SmoothDamp(_curSpeed, _desiredSpeed, ref _speedVel, _speedSmoothTime, _maxSpeed);
		Vector2 pos = new Vector2(transform.position.x, transform.position.y);
		Vector2 mov = new Vector2(-transform.right.y, transform.right.x) * _curSpeed * Time.deltaTime;

		Vector2 newPos = pos + mov;
		_rb.MovePosition(newPos);
	}
}
