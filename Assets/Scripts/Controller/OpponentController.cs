using UnityEngine;
using System.Collections;

public class OpponentController : MonoBehaviour {

	//int num = 0;

	GunController gun;
	
	void Start(){
		gun = (GunController)gameObject.GetComponent(typeof(GunController));
	}


	void Update() {
		if(BattleController.turn != Turn.OPPONENT){
			return;
		}


		// movement
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			gun.moveGun(Dir.DOWN);
		}
		else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			gun.moveGun(Dir.UP);
		}
		
		// shoot
		if(Input.GetKeyDown(KeyCode.Space)){
			gun.fireProjectile();
		}
	}
}
