using UnityEngine;
using System.Collections;

public class PlayerController : GunController {

	public PlayerController(Gun g) : base(g){}

	public override void CheckInput() {
		// movement
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			gun.move(Dir.UP);
		}
		else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			gun.move(Dir.DOWN);
		}
		
		// shoot
		if(Input.GetKeyDown(KeyCode.Space)){
			gun.shoot();
		}
	}
}
