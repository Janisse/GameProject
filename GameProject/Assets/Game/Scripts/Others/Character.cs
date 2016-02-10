using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	#region properties
	public CharacterMotor charactMotor = null;
	public Weapon currentWeapon = null;
	public int life = 100;
	#endregion

	#region Class Methods
	internal virtual void Init()
	{
		charactMotor.Enter ();

		//Init Weapon
		currentWeapon.InitWeapon ();
		currentWeapon.transform.localPosition = currentWeapon.weaponInitPos;
		currentWeapon.transform.localRotation = Quaternion.Euler(currentWeapon.weaponInitRot);
	}

	internal virtual void Manage() {}
	#endregion

	#region Character Methods
	internal virtual void Shoot()
	{
		currentWeapon.Shoot ();
	}

	internal virtual void Reload()
	{
		currentWeapon.Reload ();
	}

	internal virtual void EnableAim() {}

	internal virtual void DisableAim() {}

	internal virtual void Shooted(int a_damage = 0)
	{
		life -= a_damage;
		if(life <= 0)
		{
			Die();
		}
	}

	internal virtual void Die()
	{
		Destroy(gameObject);
		JEngine.Instance.eventManager.FireEvent ("PointChange", new JEventArgs(100f));
	}
	#endregion
}
