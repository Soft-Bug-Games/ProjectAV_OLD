using UnityEngine;

public class Pixelator : Gun
{
	private Projectile mProjectile;
	
	public override void Fire()
	{
		SetFireTimer(FireTimer() - Time.deltaTime);
		if(Input.GetMouseButtonDown(0) && FireTimer() <= 0.0f)
		{
			base.Fire();

            SpawnProjectile();
		}
	}

	void SpawnProjectile()
    {
        mProjectile = (Instantiate(ProjectilePrefab(), ProjectileSpawnPosition().position, ProjectileSpawnPosition().rotation)).GetComponent<Projectile>();
        mProjectile.SetColour(HeatIndicatorColour());
        mProjectile.SetTailColour(HeatIndicatorColour());
        mProjectile.SetLightColour(HeatIndicatorColour());
    }
}