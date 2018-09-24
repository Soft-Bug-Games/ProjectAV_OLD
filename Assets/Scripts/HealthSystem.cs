using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
	private bool mIsDead;
	private float mCurrentHealth;
	private float mCurrentShieldLevel;
	[SerializeField]
	private float mMaxHealth;
	[SerializeField]
	private float mMaxShieldLevel;

	public bool IsDead()
	{
		return mIsDead;
	}
	public float CurrentHealth()
	{
		return mCurrentHealth;
	}
	public float CurrentShieldLevel()
	{
		return mCurrentShieldLevel;
	}
	public float MaxHealth()
	{
		return mMaxHealth;
	}
	public float MaxShieldLevel()
	{
		return mMaxShieldLevel;
	}
	public void Dead(bool value)
	{
		mIsDead = value;
	}
	public void SetHealth(float value)
	{
		mCurrentHealth = value;
	}
	public void SetMaxHealth(float value)
	{
		mMaxHealth = value;
	}
	public void SetMaxShieldLevel(float value)
	{
		mMaxShieldLevel = value;
	}
	public void SetShieldLevel(float value)
	{
		mCurrentShieldLevel = value;
	}
	public void TakeDamage(Damage damage)
	{
		if(CurrentShieldLevel() > 0.0f)
		{
			SetShieldLevel(CurrentShieldLevel() - damage.ShieldDamage());

			SetHealth(CurrentHealth() - (damage.PhysicalDamage() * 0.5f));
		}
		else if(CurrentShieldLevel() < 0.0f)
		{
			SetShieldLevel(0.0f);
		}
		else
		{
			SetHealth(CurrentHealth() - damage.PhysicalDamage());
		}

		if(CurrentHealth() <= 0.0f)
		{
			Death();
		}
	}

	void Cleanup()
	{
		Destroy(gameObject);
	}
	void Death()
	{
		Dead(true);
		Cleanup();
	}
	void OnMouseOver()
	{
		print(CurrentHealth().ToString());
	}
	void Respawn()
	{

	}
	// Use this for initialization
	void Start ()
	{
		Dead(false);
		SetHealth(MaxHealth());
		SetShieldLevel(MaxShieldLevel());
	}
	
}