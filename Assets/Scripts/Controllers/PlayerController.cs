using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	float _rotateAmount = 1.0f;

	BaseChar _body = null;
	float _desiredHeading = 0f;
	
	void Awake () 
	{
		_body = GetComponent<BaseChar>();
	}

	void Start()
	{
		_desiredHeading = Mathf.Atan2(_body.transform.forward.y, _body.transform.forward.x);
	}
	
	void Update () 
	{
		_body.WantsToMoveForward = Input.GetAxis("Forward") > 0f;
		_body.WantsToMoveBackward = Input.GetAxis("Forward") < 0f;

		if (Input.GetAxis("Steer") < 0f)
			_desiredHeading += _rotateAmount * Time.deltaTime;
		if (Input.GetAxis("Steer") > 0f)
			_desiredHeading -= _rotateAmount * Time.deltaTime;

		_desiredHeading = MathUtils.ClampAngle360(_desiredHeading);
		_body.SetHeading(_desiredHeading);
	}
}
