using UnityEngine;
using System.Collections;

public class OpponentController : GunController {

	public override void CheckInput() {
		// movement
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			gun.move(Dir.DOWN);
		}
		else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			gun.move(Dir.UP);
		}
		
		// shoot
		if(Input.GetKeyDown(KeyCode.Space)){
			gun.shoot();
		}
	}
}
