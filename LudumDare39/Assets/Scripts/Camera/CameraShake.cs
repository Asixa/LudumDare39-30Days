using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    Vector3 start_pos;
    public float lerp=1f;
    public float speed=0.5f;
    public float range = 0.2f;
	void Start () {
        start_pos = transform.position;
        Shake();
	}

	void Update () {
        transform.position = Vector3.Lerp(transform.position, start_pos, lerp);
	}

    public void Shake()
    {
        transform.Translate(new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0));
        Invoke("Shake", speed);
    }
}
