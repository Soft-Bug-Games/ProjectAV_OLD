using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField]
	protected bool mIsGhost;
	[SerializeField]
	protected Damage mDamage;
	[SerializeField]
	protected float mLifeSpan;
	[SerializeField]
	protected float mSpeed;
	protected float mTimer;
	protected Gradient mTailColour;
	protected Color mTailEndColour;
	protected Rigidbody mBody;

	public virtual void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
		{
			collision.gameObject.GetComponent<HealthSystem>().TakeDamage(Damage());
			Destroy();
		}
		else
		{
			Destroy();
		}
	}
	// Update is called once per frame
	public virtual void Update()
	{
		if(!IsGhost())
		{
			SetTimer(Timer() - Time.deltaTime);
			if(Timer() <= 0.0f)
			{
				SetTimer(LifeSpan());
				Destroy();
			}
	
			Body().MovePosition(transform.position + (transform.forward * Speed()));
		}
	}

	public bool IsGhost()
	{
		return mIsGhost;
	}
	public Color TailEndColour()
	{
		return mTailEndColour;
	}
	public Damage Damage()
	{
		return mDamage;
	}
	public float LifeSpan()
	{
		return mLifeSpan;
	}
	public float Speed()
	{
		return mSpeed;
	}
	public float Timer()
	{
		return mTimer;
	}
	public Rigidbody Body()
	{
		return mBody;
	}
	public void Destroy()
	{
		Destroy(gameObject);
	}
	public void Ghost(bool flag)
	{
		mIsGhost = flag;
	}
	public void SetBody(Rigidbody body)
	{
		mBody = body;
	}
	public void SetColour(Color colour)
	{
		GetComponent<MeshRenderer>().material.color = colour;
	}
	public void SetDamage(Damage damage)
	{
		mDamage = damage;
	}
	public void SetLifeSpan(float value)
	{
		mLifeSpan = value;
	}
	public void SetLightColour(Color colour)
	{
		GetComponentInChildren<Light>().color = colour;
	}
	public void SetSpeed(float value)
	{
		mSpeed = value;
	}
	public void SetTailColour(Color colour)
	{
		GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
		alphaKeys[0].alpha = colour.a;
		alphaKeys[0].time = 0.0f;
		alphaKeys[1].alpha = TailEndColour().a;
		alphaKeys[1].time = 1.0f;

		GradientColorKey[] colourKeys = new GradientColorKey[2];
		colourKeys[0].color = colour;
		colourKeys[0].time = 0.0f;
		colourKeys[1].color = TailEndColour();
		colourKeys[1].time = 1.0f;

		mTailColour.SetKeys(colourKeys, alphaKeys);
		GetComponentInChildren<TrailRenderer>().colorGradient = mTailColour;
	}
	public void SetTimer(float value)
	{
		mTimer = value;
	}

	// Use this for initialization
	void Awake()
	{
		SetBody(GetComponent<Rigidbody>());
		if(!Body() && GameManager.DebugMode())
		{
			Debug.LogError("[Projectile.cs] No Rigidbody found on " + gameObject.name);
		}

		SetTimer(LifeSpan());
		mTailColour = new Gradient();
	}
}