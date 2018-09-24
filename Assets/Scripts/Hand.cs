using UnityEngine;

public class Hand : MonoBehaviour
{
	private Gun mWeapon;
	[SerializeField]
	private Vector3 mOffset;

	public Gun Weapon()
	{
		return mWeapon;
	}
	public Vector3 Offset()
	{
		return mOffset;
	}
	public void SetOffset(float x, float y, float z)
	{
		SetOffset(new Vector3(x, y, z));
	}
	public void SetOffset(Vector3 vec)
	{
		mOffset = vec;
	}
	public void SetWeapon(Gun weapon)
	{
		mWeapon = weapon;
	}

	void Awake()
	{
		SetWeapon(GetComponentInChildren<Gun>());
		if(!Weapon() && GameManager.DebugMode())
		{
			Debug.LogError("[Hand.cs] No weapon found!");
		}
	}
	// Use this for initialization
	void Start ()
	{
		transform.localPosition = Offset();
	}
}