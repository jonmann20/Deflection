using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GunController gun;

	void Start(){
		gun = (GunController)gameObject.GetComponent(typeof(GunController));
	}

	void Update() {
		//print (BattleController.turn);

		if(BattleController.turn != Turn.PLAYER){
			return;
		}

		// movement
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			gun.moveGun(Dir.UP);
		}
		else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			gun.moveGun(Dir.DOWN);
		}
		
		// shoot
		if(Input.GetKeyDown(KeyCode.Space)){
			gun.fireProjectile();
		}
	}
}
