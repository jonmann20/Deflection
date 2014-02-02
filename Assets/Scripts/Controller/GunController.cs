using UnityEngine;
using System.Collections;

public abstract class GunController : MonoBehaviour {
	public Gun gun;
	public GunController(Gun g){
		gun = g;
	}

	public abstract void CheckInput();
}
