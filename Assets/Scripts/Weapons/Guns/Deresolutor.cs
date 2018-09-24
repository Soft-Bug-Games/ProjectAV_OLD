using UnityEngine;

public class Deresolutor : Gun
{
	private bool mIsCharging;
	private Damage mFinalDamage;
	[SerializeField]
	private float mChargeDelay;
	private float mScale;
	private float mTimer;
	[SerializeField]
	private Projectile mGhostProjectile;
	private Projectile mProjectile;

	public override void Fire()
	{
		SetFireTimer(FireTimer() - Time.deltaTime);
		if(Input.GetMouseButton(0) && FireTimer() <= 0.0f)
		{
			SetTimer(Timer() - Time.deltaTime);
			if(Timer() <= 0.0f)
			{
				print("Charging");
				Charging(true);

				SetHeatIndicatorColour(Color.Lerp(LowHeatColour(), HighHeatColour(), CurrentHeatLevel() / 100.0f));
				MeshRenderer().material.SetColor("_Colour", HeatIndicatorColour());
				MeshRenderer().material.SetColor("_EmissionColour", HeatIndicatorColour() * EmissionIntensity());

				if(!GhostProjectile().gameObject.activeSelf)
				{
					GenerateGhostProjectile();
				}
				else
				{
					FinalDamage().SetPhysicalDamage(FinalDamage().PhysicalDamage() + (FinalDamage().PhysicalDamage() * 0.5f));
					FinalDamage().SetShieldDamage(FinalDamage().ShieldDamage() + (FinalDamage().ShieldDamage() * 0.5f));
					GhostProjectile().SetColour(HeatIndicatorColour());
					GhostProjectile().SetLightColour(HeatIndicatorColour());
				}

				SetHeatLevel(CurrentHeatLevel() + 10.0f);
				if(CurrentHeatLevel() >= 100.0f)
				{
					print("Charged shot");

					Charging(false);
					Overheated(true);

					SetTimer(ChargeDelay());
					SpawnProjectile();
				}
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			SetTimer(ChargeDelay());
			if(IsCharging())
			{
				print("Charged shot");
				Charging(false);
			}
			else
			{
				print("Regular shot");
				base.Fire();
			}

			SpawnProjectile();
		}
	}
	public override void Start()
	{
		base.Start();

		Charging(false);
		SetFinalDamage(ProjectilePrefab().GetComponent<Projectile>().Damage());
		SetScale(1.0f);

		Projectile[] projectiles = GetComponentsInChildren<Projectile>();
		foreach(Projectile projectile in projectiles)
		{
			if(projectile.IsGhost())
			{
				SetGhostProjectile(projectile);
				GhostProjectile().GetComponent<Collider>().enabled = false;
				GhostProjectile().gameObject.SetActive(false);
			}
		}
	}

	public bool IsCharging()
	{
		return mIsCharging;
	}
	public Damage FinalDamage()
	{
		return mFinalDamage;
	}
	public float ChargeDelay()
	{
		return mChargeDelay;
	}
	public float Scale()
	{
		return mScale;
	}
	public float Timer()
	{
		return mTimer;
	}
	public Projectile GhostProjectile()
	{
		return mGhostProjectile;
	}
	public Projectile Projectile()
	{
		return mProjectile;
	}
	public void Charging(bool flag)
	{
		mIsCharging = flag;
	}
	public void SetChargeDelay(float value)
	{
		mChargeDelay = value;
	}
	public void SetGhostProjectile(Projectile projectile)
	{
		mGhostProjectile = projectile;
	}
	public void SetFinalDamage(Damage damage)
	{
		mFinalDamage = damage;
	}
	public void SetProjectile(Projectile projectile)
	{
		mProjectile = projectile;
	}
	public void SetScale(float value)
	{
		mScale = value;
	}
	public void SetTimer(float value)
	{
		mTimer = value;
	}

	void GenerateGhostProjectile()
	{
		GhostProjectile().gameObject.SetActive(true);
        GhostProjectile().SetColour(HeatIndicatorColour());
        GhostProjectile().SetLightColour(HeatIndicatorColour());
	}
	void SpawnProjectile()
    {
		if(GhostProjectile().gameObject.activeSelf)
		{
			SetProjectile(GhostProjectile());
			GhostProjectile().gameObject.SetActive(false);

			Projectile().Ghost(false);
			Projectile().GetComponent<Collider>().enabled = true;
			Projectile().SetDamage(FinalDamage());
		}
        else
		{
			SetProjectile((Instantiate(ProjectilePrefab(), ProjectileSpawnPosition().position, ProjectileSpawnPosition().rotation)).GetComponent<Projectile>());
        	Projectile().SetColour(HeatIndicatorColour());
       		Projectile().SetLightColour(HeatIndicatorColour());
		  	Projectile().SetDamage(FinalDamage());
		}
    }
}