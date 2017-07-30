using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour {

	public static void Move(GameObject t,Vector3 p,float time)
    {
        t.AddComponent<TweenObject>().set(p,time,true);
    }
    public static void Rotate(GameObject t, Vector3 p, float time)
    {
        //BUG
        t.AddComponent<TweenObject>().set(p, time, false);
    }
}

public class TweenObject : MonoBehaviour
{
    public bool move;
    public void set(Vector3 p, float t,bool ismove)
    {
        move = ismove;
        target = p;
        time = t;
    }
    public Vector3 target;
    public float time;
    public float accuracy=0.01f;
    public void Update()
    {
        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * time);
           
            if ((Mathf.Abs(transform.position.x - target.x) <= accuracy) && (Mathf.Abs(transform.position.y - target.y) <= accuracy) && (Mathf.Abs(transform.position.z - target.z) <= accuracy))
            {
                transform.position = target;
                Destroy(this);
            }
        }
        else
        { //BUG
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, target, Time.deltaTime * time));
            print((Mathf.Abs(transform.rotation.eulerAngles.x - target.x))+" "+ (Mathf.Abs(transform.rotation.eulerAngles.y- target.y))+" "+ (Mathf.Abs(transform.rotation.eulerAngles.z - target.z)));
            if ((Mathf.Abs(transform.rotation.eulerAngles.x - target.x) <= accuracy) && (Mathf.Abs(transform.rotation.eulerAngles.y - target.y) <= accuracy) && (Mathf.Abs(transform.rotation.eulerAngles.z - target.z) <= accuracy))
            {
                transform.rotation =Quaternion.Euler(target);
                Destroy(this);
            }
        }
    }
}
