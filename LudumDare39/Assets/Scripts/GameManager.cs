using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    [System.Serializable]
    public struct items
    {
        public bool have_radar;
        public bool have_spare_tire;
        public bool have_shovel;
        public int food;
        public int water;
        public int Toolbox;
        public int gasoline;
        public int first_aid_kit;
    }
    public items my_items;

    public static void ItemChange(Car.ObjectType t,int count)
    {
        switch (t)
        {
            case Car.ObjectType.firstaid: { instance.my_items.first_aid_kit += count;  break; }
            case Car.ObjectType.water: { instance.my_items.water += count; break; }
            case Car.ObjectType.food: { instance.my_items.food += count; break; }
            case Car.ObjectType.toolkit: { instance.my_items.Toolbox += count; break; }
            case Car.ObjectType.gasoline: { instance.my_items.gasoline += count; break; }
        }
        Car.instance.Update_UI();
        Car.instance.objectChange(t, count);
    }

    public GameManager()
    {
        instance = this;
    }

	void Start () {
        Car.instance.objectChange(Car.ObjectType.firstaid, my_items.first_aid_kit);
        Car.instance.objectChange(Car.ObjectType.water, my_items.water);
        Car.instance.objectChange(Car.ObjectType.gasoline, my_items.gasoline);
        Car.instance.objectChange(Car.ObjectType.food, my_items.food);
        Car.instance.objectChange(Car.ObjectType.toolkit, my_items.Toolbox);
    }
	
	void Update () {
	}
}
