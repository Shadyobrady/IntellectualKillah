using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class AnimationControllerScript : MonoBehaviour {


	private Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
		settopunch ();
	}
	
	private void settopunch()
	{
		anim.SetBool ("Punch",true);
		anim.SetBool ("Shotgun", false);
		anim.SetBool ("FryingPan", false);
		anim.SetBool ("Bat", false);
		anim.SetBool ("AR", false);
		anim.SetBool ("Sword", false);
		anim.SetBool ("Pistol", false);
	}

	void Update () {

	}

    public void Reload()
    {
        anim.SetBool("reload", true);
        Invoke("Reloadreset", 1);
    }

    private void Reloadreset()
    {
        anim.SetBool("reload", false);
    }


	public void Weaponselect(Weapon NewWeap)
	{
		if (NewWeap.Name == "Shotgun") {
			anim.SetBool("Punch",false);
			anim.SetBool ("Shotgun", true);
			anim.SetBool ("FryingPan", false);
			anim.SetBool ("Bat", false);
			anim.SetBool ("AR", false);
			anim.SetBool ("Sword", false);
			anim.SetBool ("Pistol", false);
		}
		if (NewWeap.Name == "FryingPan") {
			anim.SetBool("Punch",false);
			anim.SetBool ("Shotgun", false);
			anim.SetBool ("FryingPan", true);
			anim.SetBool ("Bat", false);
			anim.SetBool ("AR", false);
			anim.SetBool ("Sword", false);
			anim.SetBool ("Pistol", false);
		}
		if (NewWeap.Name == "Bat") {
			anim.SetBool("Punch",false);
			anim.SetBool ("Shotgun", false);
			anim.SetBool ("FryingPan", false);
			anim.SetBool ("Bat", false);
			anim.SetBool ("AR", false);
			anim.SetBool ("Sword", false);
			anim.SetBool ("Pistol", false);
		}
		if (NewWeap.Name == "AR") {
			anim.SetBool("Punch",false);
			anim.SetBool ("Shotgun", false);
			anim.SetBool ("FryingPan", false);
			anim.SetBool ("Bat", true);
			anim.SetBool ("AR", false);
			anim.SetBool ("Sword", false);
			anim.SetBool ("Pistol", false);
		}
		if (NewWeap.Name == "Sword") {
			anim.SetBool("Punch",false);
			anim.SetBool ("Shotgun", false);
			anim.SetBool ("FryingPan", false);
			anim.SetBool ("Bat", false);
			anim.SetBool ("AR", false);
			anim.SetBool ("Sword", true);
			anim.SetBool ("Pistol", false);
		}
		if (NewWeap.Name == "Pistol") {
			anim.SetBool("Punch",false);
			anim.SetBool ("Shotgun", false);
			anim.SetBool ("FryingPan", false);
			anim.SetBool ("Bat", false);
			anim.SetBool ("AR", false);
			anim.SetBool ("Sword", false);
			anim.SetBool ("Pistol", true);
		}
	}
}
