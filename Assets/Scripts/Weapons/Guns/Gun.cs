using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	protected bool mIsOverheated;
	protected bool mIsVenting;
	[SerializeField]
	protected Color mEmissionColour;
	protected Color mHeatIndicatorColour;
	[SerializeField]
	protected Color mHighHeatColour;
	[SerializeField]
	protected Color mLowHeatColour;
	[SerializeField]
	protected float mCoolDelay;
	[SerializeField]
	protected float mCoolRate;
	protected float mCurrentHeatLevel;
	[SerializeField]
	protected float mEmissionIntensity;
	[SerializeField]
	protected float mFireRate;
	[SerializeField]
	protected float mHeatRate;
	[SerializeField]
	protected float mOverheatTime;
	[SerializeField]
	protected float mVentRate;
	[SerializeField]
	protected GameObject mProjectilePrefab;
	protected MeshRenderer mMeshRenderer;
	[SerializeField]
	protected Transform mProjectileSpawnPosition;

	private float mCoolTimer;
	private float mFireTimer;

	public virtual void Aim()
	{}
	public virtual void Fire()
	{
		SetCoolTimer(CoolDelay());
		SetFireTimer(FireRate());

		SetHeatIndicatorColour(Color.Lerp(LowHeatColour(), HighHeatColour(), CurrentHeatLevel() / 100.0f));
		MeshRenderer().material.SetColor("_Colour", HeatIndicatorColour());
		MeshRenderer().material.SetColor("_EmissionColour", HeatIndicatorColour() * EmissionIntensity());

		SetHeatLevel(CurrentHeatLevel() + HeatRate());
		if(CurrentHeatLevel() >= 100.0f)
		{
			Overheated(true);
		}
	}
	// Use this for initialization
	public virtual void Start ()
	{
		Overheated(false);
		Venting(false);

		SetHeatLevel(0.0f);
		SetHeatIndicatorColour(LowHeatColour());

		MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
		foreach(MeshRenderer meshRenderer in meshRenderers)
		{
			if(meshRenderer.gameObject.name == "HeatChamber")
			{
				SetMeshRenderer(meshRenderer);
			}
		}
		MeshRenderer().material.SetColor("_Colour", HeatIndicatorColour());
		MeshRenderer().material.SetColor("_EmissionColour", HeatIndicatorColour() * EmissionIntensity());

		SetCoolTimer(CoolDelay());
		SetFireTimer(FireRate());
	}
	// Update is called once per frame
	public virtual void Update ()
	{
		SetCoolTimer(CoolTimer() - Time.deltaTime);
		if(CoolTimer() <= 0.0f)
		{
			Cool();
		}

		if(IsOverheated())
		{
			Overheat();
		}
		else if(IsVenting())
		{
			Vent();
		}
	}

	public bool IsOverheated()
	{
		return mIsOverheated;
	}
	public bool IsVenting()
	{
		return mIsVenting;
	}
	public Color EmissionColour()
	{
		return mEmissionColour;
	}
	public Color HeatIndicatorColour()
	{
		return mHeatIndicatorColour;
	}
	public Color HighHeatColour()
	{
		return mHighHeatColour;
	}
	public Color LowHeatColour()
	{
		return mLowHeatColour;
	}
	public float CurrentHeatLevel()
	{
		return mCurrentHeatLevel;
	}
	public float CoolDelay()
	{
		return mCoolDelay;
	}
	public float CoolRate()
	{
		return mCoolRate;
	}
	public float CoolTimer()
	{
		return mCoolTimer;
	}
	public float EmissionIntensity()
	{
		return mEmissionIntensity;
	}
	public float FireRate()
	{
		return mFireRate;
	}
	public float FireTimer()
	{
		return mFireTimer;
	}
	public float HeatRate()
	{
		return mHeatRate;
	}
	public float OverheatTime()
	{
		return mOverheatTime;
	}
	public float VentRate()
	{
		return mVentRate;
	}
	public GameObject ProjectilePrefab()
	{
		return mProjectilePrefab;
	}
	public MeshRenderer MeshRenderer()
	{
		return mMeshRenderer;
	}
	public Transform ProjectileSpawnPosition()
	{
		return mProjectileSpawnPosition;
	}
	public void Overheated(bool value)
	{
		mIsOverheated = value;
	}
	public void Venting(bool value)
	{
		mIsVenting = value;
	}
	public void SetCoolDelay(float value)
	{
		mCoolDelay = value;
	}
	public void SetCoolRate(float value)
	{
		mCoolRate = value;
	}
	public void SetCoolTimer(float value)
	{
		mCoolTimer = value;
	}
	public void SetEmissionColour(Color colour)
	{
		mEmissionColour = colour;
	}
	public void SetEmissionIntensity(float value)
	{
		mEmissionIntensity = value;
	}
	public void SetFireRate(float value)
	{
		mFireRate = value;
	}
	public void SetFireTimer(float value)
	{
		mFireTimer = value;
	}
	public void SetHeatIndicatorColour(Color colour)
	{
		mHeatIndicatorColour = colour;
	}
	public void SetHeatLevel(float value)
	{
		mCurrentHeatLevel = value;
	}
	public void SetHeatRate(float value)
	{
		mHeatRate = value;
	}
	public void SetHighHeatColour(Color colour)
	{
		mHighHeatColour = colour;
	}
	public void SetLowHeatColour(Color colour)
	{
		mLowHeatColour = colour;
	}
	public void SetMeshRenderer(MeshRenderer meshRenderer)
	{
		mMeshRenderer = meshRenderer;
	}
	public void SetOverheatTime(float value)
	{
		mOverheatTime = value;
	}
	public void SetProjectilePrefab(GameObject obj)
	{
		mProjectilePrefab = obj;
	}
	public void SetProjectileSpawnPosition(Transform transform)
	{
		mProjectileSpawnPosition = transform;
	}
	public void SetVentRate(float value)
	{
		mVentRate = value;
	}

	void Cool()
	{
		SetHeatIndicatorColour(Color.Lerp(LowHeatColour(), HighHeatColour(), CurrentHeatLevel() / 100.0f));
		MeshRenderer().material.SetColor("_Colour", HeatIndicatorColour());
		MeshRenderer().material.SetColor("_EmissionColour", HeatIndicatorColour() * EmissionIntensity());

		SetHeatLevel(CurrentHeatLevel() - (CoolRate() * Time.deltaTime));
		if(CurrentHeatLevel() <= 0.0f)
		{
			SetHeatLevel(0.0f);
		}
	}
	void Overheat()
	{
		SetHeatIndicatorColour(Color.Lerp(LowHeatColour(), HighHeatColour(), CurrentHeatLevel() / 100.0f));
		MeshRenderer().material.SetColor("_Colour", HeatIndicatorColour());
		MeshRenderer().material.SetColor("_EmissionColour", HeatIndicatorColour() * EmissionIntensity());

		SetHeatLevel(CurrentHeatLevel() - (OverheatTime() * Time.deltaTime));
		if(CurrentHeatLevel() <= 0.0f)
		{
			SetHeatLevel(0.0f);
			Overheated(false);
		}
	}
	void Vent()
	{
		SetHeatIndicatorColour(Color.Lerp(LowHeatColour(), HighHeatColour(), CurrentHeatLevel() / 100.0f));
		MeshRenderer().material.SetColor("_Colour", HeatIndicatorColour());
		MeshRenderer().material.SetColor("_EmissionColour", HeatIndicatorColour() * EmissionIntensity());

		SetHeatLevel(CurrentHeatLevel() - (VentRate() - Time.deltaTime));
		if(CurrentHeatLevel() <= 0.0f)
		{
			SetHeatLevel(0.0f);
			Venting(false);
		}
	}
}