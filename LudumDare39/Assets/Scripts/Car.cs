using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour {
    public static Car instance;
    public Transform SpawnPoint;

    public enum ObjectType
    {
        food,water,toolkit,firstaid, gasoline
    }
    [System.Serializable]
    public struct components
    {
        public GameObject radar;
        public GameObject shovel;
        public GameObject spare_tire;
    }
    public components component;
    [System.Serializable]
    public struct item_prefab
    {
        public GameObject food;
        public GameObject water;
        public GameObject toolkit;
        public GameObject first_aid;
    }
    public item_prefab prefabs;
   [HideInInspector]
    public List<GameObject> food_i = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> water_i = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> toolkit_i = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> first_aid_i = new List<GameObject>();

    public Text[] resources_ui;
    public AnimationClip shake;
    public SpriteRenderer[] people;

    public WheelRotate w1, w2, w3;
    public Car()
    {
        instance = this;
    }
	void Start () {
        Update_UI();

    }
    public void StopWheel()
    {
        Destroy(w1);Destroy(w2);Destroy(w3);
    }
    public void Update_UI()
    {
        resources_ui[0].text = "Power x" + GameManager.instance.my_items.gasoline;
        resources_ui[1].text = "Food x" + GameManager.instance.my_items.food;
        resources_ui[2].text = "Water x" + GameManager.instance.my_items.water;
        resources_ui[3].text = "Toolkit x" + GameManager.instance.my_items.Toolbox;
        resources_ui[4].text = "First-Aid x" + GameManager.instance.my_items.first_aid_kit;
        component.radar.SetActive(GameManager.instance.my_items.have_radar);
        component.shovel.SetActive(GameManager.instance.my_items.have_shovel);
        component.spare_tire.SetActive(GameManager.instance.my_items.have_spare_tire);

        resources_ui[5].text = GameEvents.instance.CarHealth + "/" + GameEvents.instance.MaxCarHealth;
        if(GameEvents.instance.CarHealth== GameEvents.instance.MaxCarHealth)
        {
            resources_ui[5].color = Color.green;
        }
        else
        {
            resources_ui[5].color = Color.red;
        }
            if (GameManager.instance.my_items.gasoline <= 2)
        {
            resources_ui[0].transform.parent.GetComponent<AnimationHelp>().play(shake);
        }
        else
        {
            resources_ui[0].transform.parent.GetComponent<Animation>().Stop();
        }

        if (GameManager.instance.my_items.food <= 2)
        {
            resources_ui[1].transform.parent.GetComponent<AnimationHelp>().play(shake);
        }
        else
        {
            resources_ui[1].transform.parent.GetComponent<Animation>().Stop();
        }


        if (GameManager.instance.my_items.water <= 2)
        {
            resources_ui[2].transform.parent.GetComponent<AnimationHelp>().play(shake);
        }
        else
        {
            resources_ui[2].transform.parent.GetComponent<Animation>().Stop();
        }

        if (GameEvents.instance.CarHealth < GameEvents.instance.MaxCarHealth)
        {
            resources_ui[5].transform.parent.GetComponent<AnimationHelp>().play(shake);
        }
        else
        {
            resources_ui[5].transform.parent.GetComponent<Animation>().Stop();
        }
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
           GameManager.ItemChange(ObjectType.firstaid,1);
        }
	}

    public void objectChange(ObjectType t,int count)
    {
        if (count >= 0)
        {
            for (int i = 0; i < count; i++)
            {
                instanceObject(t);
            }

        }
        else
        {
            for (int i = 0; i < -count; i++)
            {
                DestoryObject(t);
            }
        }
    }

    public void instanceObject(ObjectType t)
    {
        switch (t)
        {
            case ObjectType.firstaid:
                {
                    first_aid_i.Add(Instantiate(prefabs.first_aid, SpawnPoint.position, SpawnPoint.rotation));
                    break;
                }
            case ObjectType.food:
                {
                    food_i.Add(Instantiate(prefabs.food, SpawnPoint.position, SpawnPoint.rotation));
                    break;
                }
            case ObjectType.water:
                {
                    water_i.Add(Instantiate(prefabs.water, SpawnPoint.position, SpawnPoint.rotation));
                    break;
                }
            case ObjectType.toolkit:
                {
                    toolkit_i.Add(Instantiate(prefabs.toolkit, SpawnPoint.position, SpawnPoint.rotation));
                    break;
                }
        }
    }

    public void DestoryObject(ObjectType t)
    {
        switch (t)
        {
            case ObjectType.firstaid:
                {
                    if (first_aid_i.Count > 0)
                    {
                        Destroy(first_aid_i[0]);
                        first_aid_i.Remove(first_aid_i[0]);
                    }
                  //  first_aid_i.Add(Instantiate(prefabs.first_aid, SpawnPoint.position, SpawnPoint.rotation));
                    break;
                }
            case ObjectType.food:
                {
                    Destroy(food_i[0]);
                    food_i.Remove(food_i[0]);
                    break;
                }
            case ObjectType.water:
                {
                    Destroy(water_i[0]);
                    water_i.Remove(water_i[0]);
                    break;
                }
            case ObjectType.toolkit:
                {
                    Destroy(toolkit_i[0]);
                    toolkit_i.Remove(toolkit_i[0]);
                    break;
                }
        }
    }
}
