using UnityEngine;

public class RaycasterProjectile : Projectile
{
	private Damage mFinalDamage;
	[SerializeField]
	private float mDamageMultiplier;
	[SerializeField]
	private int mMaxNumBounces;
	private int mNumBounces;

	public override void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
		{
			collision.gameObject.GetComponent<HealthSystem>().TakeDamage(FinalDamage());
			Destroy();
		}
		else if(collision.gameObject.CompareTag("Environment") && (NumberOfBounces() < MaxNumberOfBounces()))
		{
			SetNumberOfBounces(NumberOfBounces() + 1);

			foreach(ContactPoint contact in collision.contacts)
			{
				SetSpeed((Quaternion.AngleAxis(180.0f, contact.normal) * transform.forward * -1.0f).magnitude);
			}
		}
		else
		{
			Destroy();
		}
	}
	// Update is called once per frame
	public override void Update()
	{
		UpdateDamage();

		base.Update();
	}

	public Damage FinalDamage()
	{
		return mFinalDamage;
	}
	public float DamageMultiplier()
	{
		return mDamageMultiplier;
	}
	public int MaxNumberOfBounces()
	{
		return mMaxNumBounces;
	}
	public int NumberOfBounces()
	{
		return mNumBounces;
	}
	public void SetDamageMultiplier(float value)
	{
		mDamageMultiplier = value;
	}
	public void SetFinalDamage(Damage damage)
	{
		mFinalDamage = damage;
	}
	public void SetMaxNumberOfBounces(int value)
	{
		mMaxNumBounces = value;
	}
	public void SetNumberOfBounces(int value)
	{
		mNumBounces = value;
	}
	
	// Use this for initialization
	void Start()
	{
		SetNumberOfBounces(0);

		mTailEndColour = new Color(1.0f, 4.6f, 0.0f, 1.0f);
		SetFinalDamage(Damage());
	}
	void UpdateDamage()
	{
		Damage().SetPhysicalDamage(Damage().PhysicalDamage() + (Damage().PhysicalDamage() * (DamageMultiplier() * NumberOfBounces())));
		Damage().SetShieldDamage(Damage().ShieldDamage() + (Damage().ShieldDamage() * (DamageMultiplier() * NumberOfBounces())));

		SetFinalDamage(Damage());
	}
}
