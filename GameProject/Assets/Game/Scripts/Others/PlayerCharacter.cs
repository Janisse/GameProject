using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character
{
	#region properties
	public Camera playerCamera = null;

	private PlayHUDPanel _playHUDPanel = null;
	#endregion

	#region Class Methods
	internal override void Init()
	{
		base.Init ();

		//Init HUD
		_playHUDPanel = (PlayHUDPanel)JEngine.Instance.uiManager.GetPanel ("PlayHUDPanel");
		_playHUDPanel.SetWeaponName (currentWeapon.name);
		_playHUDPanel.SetAmmoText (currentWeapon.nbLoadedAmmo.ToString() + " / " + currentWeapon.nbAmmoLeft.ToString());

	}
	#endregion

	#region Character Methods
	internal override void Shoot()
	{
		base.Shoot ();
		_playHUDPanel.SetAmmoText (currentWeapon.nbLoadedAmmo.ToString() + " / " + currentWeapon.nbAmmoLeft.ToString());
	}

	internal override void Reload()
	{
		base.Reload ();
		_playHUDPanel.SetAmmoText (currentWeapon.nbLoadedAmmo.ToString() + " / " + currentWeapon.nbAmmoLeft.ToString());
	}

	internal override void EnableAim()
	{
		currentWeapon.transform.localPosition = currentWeapon.weaponZoomPos;
		currentWeapon.transform.localRotation = Quaternion.Euler(currentWeapon.weaponZoomRot);
		playerCamera.fieldOfView = 40f;
	}

	internal override void DisableAim()
	{
		currentWeapon.transform.localPosition = currentWeapon.weaponInitPos;
		currentWeapon.transform.localRotation = Quaternion.Euler(currentWeapon.weaponInitRot);
		playerCamera.fieldOfView = 60f;
	}

	internal override void Die()
	{
		Destroy(gameObject);
		JEngine.Instance.eventManager.FireEvent ("PointChange", new JEventArgs(-50f));
	}
	#endregion
}
