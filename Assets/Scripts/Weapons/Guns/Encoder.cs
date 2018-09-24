using UnityEngine;

public class Encoder : Gun
{
	[SerializeField]
	private Damage mDamage;
	[SerializeField]
	private float mRange;
	private ParticleSystem.MainModule mPSMainModule;
	private ParticleSystem mParticleSystem;
	private RaycastHit mHit;

	public override void Fire()
	{
		SetFireTimer(FireTimer() - Time.deltaTime);
		if(Input.GetMouseButton(0) && FireTimer() <= 0.0f)
		{
			if(ParticleSystem().isStopped)
			{
				ParticleSystem().Play();
			}
			base.Fire();

			mPSMainModule.startColor = HeatIndicatorColour();

			if(Physics.Raycast(ProjectileSpawnPosition().position, transform.forward, out mHit, Range()))
			{
				if(mHit.transform.CompareTag("Player") || mHit.transform.CompareTag("Enemy"))
				{
					mHit.transform.GetComponent<HealthSystem>().TakeDamage(Damage());
				}
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			SetFireTimer(FireRate());
			if(ParticleSystem().isPlaying)
			{
				ParticleSystem().Stop();
			}
		}

		if(IsOverheated())
		{
			SetFireTimer(FireRate());
			if(ParticleSystem().isPlaying)
			{
				ParticleSystem().Stop();
			}
		}
	}
	public override void Start()
	{
		base.Start();

		SetParticleSystem(GetComponentInChildren<ParticleSystem>());
		ParticleSystem().Stop();
		mPSMainModule = ParticleSystem().main;
	}

	public Damage Damage()
	{
		return mDamage;
	}
	public float Range()
	{
		return mRange;
	}
	public ParticleSystem ParticleSystem()
	{
		return mParticleSystem;
	}
	public void SetDamage(Damage damage)
	{
		mDamage = damage;
	}
	public void SetParticleSystem(ParticleSystem particleSystem)
	{
		mParticleSystem = particleSystem;
	}
	public void SetRange(float value)
	{
		mRange = value;
	}
}
