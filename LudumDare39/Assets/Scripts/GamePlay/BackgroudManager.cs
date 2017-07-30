using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudManager : MonoBehaviour {
    public bool moving;
    public static BackgroudManager instance;
    public static bool GetStatus()
    {
        return instance.moving;
    }
    

}
