using UnityEngine;

[System.Serializable]
public class Damage
{
	[SerializeField]
	private float mPhysicalDamage;
	[SerializeField]
	private float mShieldDamage;

	public float PhysicalDamage()
	{
		return mPhysicalDamage;
	}
	public float ShieldDamage()
	{
		return mShieldDamage;
	}
	public void SetPhysicalDamage(float value)
	{
		mPhysicalDamage = value;
	}
	public void SetShieldDamage(float value)
	{
		mShieldDamage = value;
	}
}