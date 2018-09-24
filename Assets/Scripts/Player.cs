using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private Behaviour[] mLocalComponents;
	private Camera mCamera;
	private Collider mCollider;
	private Controller mController;
	private Hand mHand;
	private Rigidbody mBody;
	[SerializeField]
	private Transform mHeadPosition;

	public Behaviour[] LocalComponents()
	{
		return mLocalComponents;
	}
	public Camera Camera()
	{
		return mCamera;
	}
	public Collider Collider()
	{
		return mCollider;
	}
	public Controller Controller()
	{
		return mController;
	}
	public Hand Hand()
	{
		return mHand;
	}
	public int Layer()
	{
		return gameObject.layer;
	}
	public Rigidbody Body()
	{
		return mBody;
	}
	public Transform HeadPosition()
	{
		return mHeadPosition;
	}
	public Transform Transform()
	{
		return transform;
	}
	public Vector3 Position()
	{
		return transform.position;
	}
	public void AssignCamera(Camera camera)
	{
		mCamera = camera;
	}
	public void EnableLocalComponents()
	{
		foreach(Behaviour component in mLocalComponents)
		{
			component.enabled = true;
		}
	}
	public void DisableLocalComponents()
	{
		foreach(Behaviour component in mLocalComponents)
		{
			component.enabled = false;
		}
	}
	public void SetBody(Rigidbody body)
	{
		mBody = body;
	}
	public void SetCollider(Collider collider)
	{
		mCollider = collider;
	}
	public void SetController(Controller controller)
	{
		mController = controller;
	}
	public void SetHand(Hand hand)
	{
		mHand = hand;
	}
	public void SetHeadPositon(Transform trans)
	{
		mHeadPosition = trans;
	}
	public void SetLayer(int layer)
	{
		gameObject.layer = layer;
	}
	public void SetLayer(string layerName)
	{
		gameObject.layer = LayerMask.NameToLayer(layerName);
	}
	public void SetLocalComponents(Behaviour[] components)
	{
		mLocalComponents = components;
	}

	// Use this for initialization
	void Awake()
	{
		mCamera = GetComponentInChildren<Camera>();
		if(!mCamera && GameManager.DebugMode())
		{
			Debug.LogError("[Player.cs] No Camera found on " + gameObject.name);
		}
		mCollider = GetComponentInChildren<Collider>();
		if(!mCollider && GameManager.DebugMode())
		{
			Debug.LogError("[Player.cs] No Collider found on " + gameObject.name);
		}
		mController = GetComponent<Controller>();
		if(!mController && GameManager.DebugMode())
		{
			Debug.LogError("[Player.cs] No Controller found on " + gameObject.name);
		}
		mHand = GetComponentInChildren<Hand>();
		if(!mHand && GameManager.DebugMode())
		{
			Debug.LogError("[Player.cs] No Hand found on " + gameObject.name);
		}
		mBody = GetComponent<Rigidbody>();
		if(!mBody && GameManager.DebugMode())
		{
			Debug.LogError("[Player.cs] No Rigidbody found on " + gameObject.name);
		}

		Transform[] children = GetComponentsInChildren<Transform>();
		foreach(Transform child in children)
		{
			if(child.name == "Head")
			{
				mHeadPosition = child;
				Camera().transform.position = HeadPosition().position;
			}
		}
		if(!mHeadPosition && GameManager.DebugMode())
		{
			Debug.LogError("[Player.cs] No head position assigned to " + gameObject.name);
		}
	}
}