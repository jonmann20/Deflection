using UnityEngine;
using System.Collections;

public abstract class GunController : MonoBehaviour {
	protected Gun gun;

	void Awake(){
		gun = GetComponent<Gun>();
	}

	public abstract void CheckInput();
}
