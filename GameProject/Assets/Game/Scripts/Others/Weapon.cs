using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	#region Properties
	public float fireRate = 20f;
	public int maxLoadedAmmo = 30;
	public int maxAmmo = 150;
	public int damage = 25;
	public float distance = 200f;
	public float spread = 2f;
	public Transform fireSpot = null;
	public ParticleSystem particleSys = null;
	public Vector3 weaponInitPos = Vector3.zero;
	public Vector3 weaponInitRot = Vector3.zero;
	public Vector3 weaponZoomPos = Vector3.zero;
	public Vector3 weaponZoomRot = Vector3.zero;

	internal int nbLoadedAmmo = 0;
	internal int nbAmmoLeft = 0;

	protected float nextTimeShoot = 0f;
	protected Vector3 fireSpotRotInit = Vector3.zero;
	#endregion

	#region Weapon Methods
	internal void InitWeapon()
	{
		nbLoadedAmmo = maxLoadedAmmo;
		nbAmmoLeft = maxAmmo;
		nextTimeShoot = 0f;
		fireSpotRotInit = fireSpot.localRotation.eulerAngles;
	}

	internal virtual bool Shoot()
	{
		if(nbLoadedAmmo > 0)
		{
			if(nextTimeShoot <= Time.time)
			{
				Fire();
				nextTimeShoot = Time.time + 1f/fireRate;
				return true;
			}
		}
		return false;
	}

	internal virtual void Fire()
	{
		//Compute spread
		fireSpot.localRotation = Quaternion.Euler(fireSpotRotInit + (new Vector3 (Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * spread));

		//Do Damage and emit particles
		Ray hitRay = new Ray (fireSpot.position, fireSpot.up);

		RaycastHit hitInfo = new RaycastHit();
		if(Physics.Raycast(hitRay, out hitInfo, distance))
		{
			if(hitInfo.collider.tag == "Character")
			{
				hitInfo.collider.gameObject.GetComponent<Character>().Shooted(damage);
			}
		}

		//Fire Bullet
		particleSys.Emit (1);
		nbLoadedAmmo--;
	}

	internal virtual void Reload()
	{
		if(nbAmmoLeft > 0)
		{
			int nbAmmoToLoad = maxLoadedAmmo - nbLoadedAmmo;
			//If enough ammo
			if(nbAmmoToLoad <= nbAmmoLeft)
			{
				nbLoadedAmmo = maxLoadedAmmo;
				nbAmmoLeft -= nbAmmoToLoad;
			}
			//If not enough ammo
			else
			{
				nbLoadedAmmo += nbAmmoLeft; 
				nbAmmoLeft = 0;
			}
		}
	}
	#endregion
}
