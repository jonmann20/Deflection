using UnityEngine;
using System.Collections;

public class PlayerController : GunController {

    const float DT_THETA = 0.3f;

	public override void CheckInput(){
		// movement
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			move(Dir.UP);
		}
		else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			move(Dir.DOWN);
		}
		
		// shoot
		if(Input.GetKeyDown(KeyCode.Space)){
			gun.shoot();
		}
	}

    public void move(Dir dir) {
        // TODO: if < 1 degree from perfect angle, snap to target

        float dtMove = (dir == Dir.UP) ? DT_THETA : -DT_THETA;
        gun.rotateBy(dtMove);
    }
}
