using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public bool invertLook;
	public bool toggleAim;
	public bool toggleCrouch;

	private bool mIsCrouched;
	private bool mIsGrounded;
	private bool mIsSprinting;
	private bool mIsStunned;
	private bool mIsTPSActive;
	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float mCrouchMultiplier;
	private float mFinalSpeed;
	[SerializeField]
	private float mJumpSpeed;
	[SerializeField]
	[Range(1.0f, 10.0f)]
	private float mRotationSpeed;
	[SerializeField]
	private float mSpeed;
	[SerializeField]
	[Range(1.0f, 2.0f)]
	private float mSprintMultiplier;
	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float mStunnedMultiplier;
	private float mYAxis;
	private Player mObject;
	[SerializeField]
	private Vector2 mLookConstraints;
	private Vector3 mVelocity;

	public bool IsCrouched()
	{
		return mIsCrouched;
	}
	public bool IsGrounded()
	{
		return mIsGrounded;
	}
	public bool IsSprinting()
	{
		return mIsSprinting;
	}
	public bool IsStunned()
	{
		return mIsStunned;
	}
	public bool IsTPSActive()
	{
		return mIsTPSActive;
	}
	public float CrouchMultiplier()
	{
		return mCrouchMultiplier;
	}
	public float FinalSpeed()
	{
		return mFinalSpeed;
	}
	public float JumpSpeed()
	{
		return mJumpSpeed;
	}
	public float RotationSpeed()
	{
		return mRotationSpeed;
	}
	public float Speed()
	{
		return mSpeed;
	}
	public float SprintMultiplier()
	{
		return mSprintMultiplier;
	}
	public float StunnedMultiplier()
	{
		return mStunnedMultiplier;
	}
	public float YAxis()
	{
		return mYAxis;
	}
	public Player PlayerObject()
	{
		return mObject;
	}
	public Vector2 LookConstraints()
	{
		return mLookConstraints;
	}
	public Vector3 Velocity()
	{
		return mVelocity;
	}
	public void Crouched(bool flag)
	{
		mIsCrouched = flag;
	}
	public void Grounded(bool flag)
	{
		mIsGrounded = flag;
	}
	public void SetCrouchMultiplier(float value)
	{
		if(value > 1.0f)
		{
			Debug.LogWarning("[Controller.cs] Not possible to move faster while crouched. Truncating value to 100%");
			value = 1.0f;
		}
		else if(value < 0.0)
		{
			Debug.LogWarning("[Controller.cs] Not possible to move in the opposite direction. Increasing value to 0%");
			value = 0.0f;
		}
		mCrouchMultiplier = value;
	}
	public void SetFinalSpeed(float value)
	{
		mFinalSpeed = value;
	}
	public void SetJumpSpeed(float value)
	{
		mJumpSpeed = value;
	}
	public void SetLookConstraints(float minValue, float maxValue)
	{
		SetLookConstraints(new Vector2(minValue, maxValue));
	}
	public void SetLookConstraints(Vector2 vec)
	{
		mLookConstraints = vec;
	}
	public void SetPlayerObject(Player player)
	{
		mObject = player;
	}
	public void SetRotationSpeed(float value)
	{
		mRotationSpeed = value;
	}
	public void SetSpeed(float value)
	{
		mSpeed = value;
	}
	public void SetSprintMultiplier(float value)
	{
		if(value < 1.0f)
		{
			Debug.LogWarning("[Controller.cs] Not possible to sprinter slower than normal speed. Increasing value to 100%");
			value = 1.0f;
		}
		else if(value > 2.0f)
		{
			Debug.LogWarning("[Controller.cs] Over double speed seems a little excessive, doesn't it? Truncating the value to 200%");
			value = 2.0f;
		}
		mSprintMultiplier = value;
	}
	public void SetStunnedMultiplier(float value)
	{
		if(value < 0.0f)
		{
			value = 0.0f;
		}
		else if(value > 1.0f)
		{
			value = 1.0f;
		}
		mStunnedMultiplier = value;
	}
	public void SetYAxis(float value)
	{
		mYAxis = value;
	}
	public void SetVelocity(float x = 0.0f, float y = 0.0f, float z = 0.0f)
	{
		SetVelocity(new Vector3(x, y, z));
	}
	public void SetVelocity(Vector3 vec)
	{
		mVelocity = vec;
	}
	public void Sprinting(bool flag)
	{
		mIsSprinting = flag;
	}
	public void Stunned(bool flag)
	{
		mIsStunned = flag;
	}
	public void ToggleTPSMode(bool flag)
	{
		mIsTPSActive = flag;
	}

	// Use this for initialization
	void Awake()
	{
		SetPlayerObject(GetComponent<Player>());
		if(!PlayerObject() && GameManager.DebugMode())
		{
			Debug.LogError("[Controller.cs] Can't reference the Player script!");
		}
	}
	void Crouch()
	{
		if(toggleCrouch)
		{
			if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
			{
				Sprinting(false);
				Crouched(!IsCrouched());
			}
		}
		else
		{
			if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				Sprinting(false);
				Crouched(true);
			}
			else
			{
				Crouched(false);
			}
		}
	}
	void DebugLogic()
	{
		if(IsTPSActive())
		{
			Debug.Log("[Controller.cs] Camera Mode: 3rd Person");
		}
		else
		{
			Debug.Log("[Controller.cs] Camera Mode: 1st Person");
		}

		Debug.Log("[Controller.cs] Crouched: " + IsCrouched().ToString());
		Debug.Log("[Controller.cs] Grounded: " + IsGrounded().ToString());
		Debug.Log("[Controller.cs] Sprinting: " + IsSprinting().ToString());
		Debug.Log("[Controller.cs] Stunned: " + IsStunned().ToString());

		Debug.Log("[Controller.cs] Current Velocity: " + Velocity().ToString());
	}
	void FireWeapon()
	{
		if(!PlayerObject().Hand().Weapon().IsOverheated())
		{
			PlayerObject().Hand().Weapon().Fire();
		}
	}
	void GroundCheck()
	{
		if(Physics.Raycast(mObject.Position(), -Vector3.up, 1.5f))
		{
			mIsGrounded = true;
		}
		else
		{
			mIsGrounded = false;
		}
	}
	void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Crouched(false);
			//Grounded(false);
			Sprinting(false);

			SetVelocity(new Vector3(Velocity().x, JumpSpeed(), Velocity().z));
			//mVelocity.y = JumpSpeed();
		}
	}
	void Look()
	{
		mObject.Transform().Rotate(0.0f, Input.GetAxis("Mouse X") * RotationSpeed(), 0.0f);
		PlayerObject().Hand().transform.forward = PlayerObject().Camera().transform.forward;

		if(invertLook)
		{
			SetYAxis(YAxis() + -Input.GetAxis("Mouse Y") * RotationSpeed());
		}
		else
		{
			SetYAxis(YAxis() + Input.GetAxis("Mouse Y") * RotationSpeed());
		}
		SetYAxis(Mathf.Clamp(YAxis(), LookConstraints().x, LookConstraints().y));

		mObject.Camera().transform.localEulerAngles = new Vector3(-YAxis(), mObject.Camera().transform.localEulerAngles.y, 0.0f);
	}
	void Move()
	{
		if(IsGrounded())
		{
			Crouch();
			Sprint();

			SetVelocity(new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")));

			if(IsCrouched())
			{
				SetFinalSpeed(Speed() * CrouchMultiplier());
			}
			else if(IsSprinting())
			{
				SetFinalSpeed(Speed() * SprintMultiplier());
			}
			else if(IsStunned())
			{
				SetFinalSpeed(Speed() * StunnedMultiplier());
			}
			else
			{
				SetFinalSpeed(Speed());
			}

			Jump();

			SetVelocity(mObject.Transform().TransformDirection(Velocity()) * FinalSpeed());
		}
	}
	void Start()
	{
		//Cursor.lockState = CursorLockMode.Locked;

		mIsCrouched = false;
		mIsGrounded = false;
		mIsSprinting = false;
		mIsStunned = false;
		mFinalSpeed = 0.0f;
		SetVelocity(Vector3.zero);
	}
	void Sprint()
	{
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			Crouched(false);
			Sprinting(true);
		}
		else
		{
			Sprinting(false);
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if(GameManager.DebugMode())
		{
			DebugLogic();
		}

		GroundCheck();

		Look();
		Move();

		SetVelocity(Velocity() + Physics.gravity);

		mObject.Body().velocity = Velocity();

		FireWeapon();
	}
}