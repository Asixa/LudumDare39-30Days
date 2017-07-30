using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudMove : MonoBehaviour {
    public float Speed;
    
   	 

	void Update () {
        if (BackgroudManager.GetStatus())
        {
            transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
        }
	}
}

