using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Destination
{
    village,
    Supermarket,
    Hospital,
    Military_base,
    gas_station
}

public class GameEvents : MonoBehaviour {
    public static GameEvents instance;
    public GameEvents()
    {
        instance = this;
    }

    public GameObject next_day_button,cureButton;
    public List<People> people = new List<People>();
    public Destination destination,choose1,choose2;
    public int Days = 0;
    public Window Destination_panel,Result_panel;
    public People ChoosedPeople;

    List<string> dead_people = new List<string>();

    public Camera GameEndCamera,GamingCamera;public GameObject GameOverCamera;
    public Transform GameEndCarPos;
    public GameObject GamingUI, GameEndUI,GameOverUI;public Text GameOverReason;
    public int MaxCarHealth = 5;
    public int CarHealth = 5;
    [System.Serializable]
    public struct People
    {
        public string name;
        public bool Hurt;
        public bool thirsty;
        public bool hungry;
    }
 //   public List<PeopleStatus> peoples_status = new List<PeopleStatus>();
    void Start () {
        Start_a_new_day();
        UpdatePeopleList();
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            KillPeople(people[0]);
        }
	}
    public Text Dayshow;
    public void Start_a_new_day()
    {
        Days++;
        DayshowPanel.instance.gameObject.SetActive(true);
        DayshowPanel.instance.Shows.text = "DAY " + Days;
        
        DayshowPanel.instance.anim.anim.Play();
    }
    public void On_NextDay()
    {
        next_day_button.SetActive(false);
        Random_Destination();
    }
    void Random_Destination()
    {
        if (Days != 29)
        {
            Destination_panel.type = ChooseType.Destination;
            choose1 = (Destination)((int)UnityEngine.Random.Range(0, 5));
            choose2 = (Destination)((int)UnityEngine.Random.Range(0, 5));
            while (choose2 == choose1)
            {
                choose2 = (Destination)((int)UnityEngine.Random.Range(0, 5));
            }
            Destination_panel.event_text.text = "Choose the next destination";
            Destination_panel.buttons[2].gameObject.SetActive(true);
            Destination_panel.buttons[3].gameObject.SetActive(false);
            Destination_panel.buttons[4].gameObject.SetActive(false);
            Destination_panel.buttons[1].GetComponentInChildren<Text>().text = choose1.ToString();
            Destination_panel.buttons[2].GetComponentInChildren<Text>().text = choose2.ToString();
            Destination_panel.gameObject.SetActive(true);
        }
        else
        {
            Start_a_new_day();
        }
    }
    public void ChoosePeople()
    {
        Destination_panel.type = ChooseType.People;
        Destination_panel.event_text.text = "Who do you want to go to look for supplies?";
        Destination_panel.buttons[1].GetComponentInChildren<Text>().text = "Nobody";
        Destination_panel.buttons[3].gameObject.SetActive(false);
        Destination_panel.buttons[4].gameObject.SetActive(false);
        for (int i = 0; i < people.Count; i++)
        {
            Destination_panel.buttons[i + 2].gameObject.SetActive(true);
            Destination_panel.buttons[i + 2].GetComponentInChildren<Text>().text = people[i].name;
        }

        Destination_panel.gameObject.SetActive(true);
    }

    public void show_new_day_info()
    {
        Dayshow.text = "DAY " + Days;
        Result_panel.gameObject.SetActive(true);
        if (Days == 1)
        {
            Result_panel.event_text.text = "<color=#FF0000FF>The zombie virus broke out</color>" + Environment.NewLine+"You started a journey to the safe area" + Environment.NewLine + "it will take you about 30 days";
        }
        else if (Days == 30)
        {
            GamingUI.SetActive(false);
            GameEndUI.SetActive(true);
            GamingCamera.gameObject.SetActive(false);
            GameEndCamera.gameObject.SetActive(true);
            Car.instance.transform.position = GameEndCarPos.position;
            Car.instance.StopWheel();



            Result_panel.event_text.text = "Congratulations! You have reached the safe area!" + Environment.NewLine;
            Result_panel.obj_text.text = "";
            bool jeff_alive = false, Fang_alive = false, Steve_alive=false;
            if (people.Count != 3)
            {

                Result_panel.event_text.text += "<color=#FF0000FF>";
                for (int i = 0; i < people.Count; i++)
                {
                    if (people[i].name == "Jeff") jeff_alive = true;
                    if (people[i].name == "Fang") Fang_alive = true;
                    if (people[i].name == "Steve") Steve_alive = true;
                }
                if (people.Count == 2)
                {
                    if(!jeff_alive) Result_panel.event_text.text += "but Jeff is dead";
                    if (!Fang_alive) Result_panel.event_text.text += "but Fang is dead";
                    if (!Steve_alive) Result_panel.event_text.text += "but Steve is dead";
                }
                else
                {
                    if (jeff_alive)
                    {
                        Result_panel.event_text.text += "but Fang and Steve are dead";
                    }
                    else if (Fang_alive)
                    {
                        Result_panel.event_text.text += "but Jeff and Steve are dead";
                    }
                    else if (Steve_alive)
                    {
                        Result_panel.event_text.text += "but Jeff and Fang are dead";
                    }
                }
                Result_panel.event_text.text += "</color>";

            }
            else
            {
                Result_panel.event_text.text += "All people are survived";
            }
            
        }
        else
        {
            GetYesterdayresult();
            if (GameManager.instance.my_items.first_aid_kit > 0)
            {
                cureButton.SetActive(true);
            }
            else
            {
                cureButton.SetActive(false);
            }

            if (GameManager.instance.my_items.Toolbox > 0&&CarHealth<MaxCarHealth)
            {
                RepairButton.SetActive(true);
            }
            else
            {
                RepairButton.SetActive(false);
            }

           
        }
   

    }
    void GetYesterdayresult()
    {
        Result_panel.obj_text.text = "";
        Result_panel.event_text.text = "You went to the " + destination.ToString() + Environment.NewLine;
        if (ChoosedPeople.name != "")
        {
            bool people_hurt = UnityEngine.Random.Range(0, 100) > 80;
            bool people_dead=false;
            if (people_hurt)
            {
                if (ChoosedPeople.Hurt)
                {
                    KillPeople(ChoosedPeople);
                    people_dead = true;
                }
                for (int i = 0; i < people.Count; i++)
                {
                    if (people[i].name == ChoosedPeople.name)
                    {
                        ChoosedPeople.Hurt = true;
                        people[i] = ChoosedPeople;
                        break;
                    }
                }
            }


            if (people_dead) Result_panel.event_text.text += "<color=#FF0000FF>but " + ChoosedPeople.name + " didn't come back...</color>" + Environment.NewLine;
            else if(people_hurt) Result_panel.event_text.text += "<color=#FF0000FF>but " + ChoosedPeople.name + " got hurt...</color>" + Environment.NewLine;
            else Result_panel.event_text.text += "<color=#00FF00FF>" + ChoosedPeople.name + " came back safely...</color>" + Environment.NewLine;
            switch (destination)
            {
                case Destination.gas_station:
                    {


                        // Car.instance();
                        int find_gas = UnityEngine.Random.Range(1, 4);
                        GameManager.ItemChange(Car.ObjectType.gasoline, find_gas);
                        int find_toolkit = UnityEngine.Random.Range(0, 2);
                        GameManager.ItemChange(Car.ObjectType.toolkit, find_toolkit);


                        Result_panel.obj_text.text= "<color=#00FF00FF>" +  "+ gasoline x" + find_gas + "</color>" +Environment.NewLine;
                        if (find_toolkit > 0) Result_panel.obj_text.text += "<color=#00FF00FF>" +  "+ toolkit x" + find_toolkit + "</color>" + Environment.NewLine;
                        break;
                    }
                case Destination.village:
                    {
                        int find_food = UnityEngine.Random.Range(0, 2);
                        GameManager.ItemChange(Car.ObjectType.food, find_food);
                        int find_water = UnityEngine.Random.Range(1, 3);
                        GameManager.ItemChange(Car.ObjectType.water, find_water);

                        Result_panel.obj_text.text = "<color=#00FF00FF>" +  "+ water x" + find_water + "</color>" + Environment.NewLine;
                        if (find_food > 0) Result_panel.obj_text.text += "<color=#00FF00FF>" +  "+ food x" + find_food + "</color>" + Environment.NewLine;
                        break;
                    }
                case Destination.Military_base:
                    {
                        int find_gas = UnityEngine.Random.Range(1, 3);
                        GameManager.ItemChange(Car.ObjectType.gasoline, find_gas);
                        bool find_rader=false;
                        if (!GameManager.instance.my_items.have_radar)
                        {
                            find_rader = UnityEngine.Random.Range(0, 100) > 50;
                        }

                        bool find_shovel = false;
                        if (!GameManager.instance.my_items.have_shovel)
                        {
                            find_shovel = UnityEngine.Random.Range(0, 100) > 60;
                        }

                        bool find_spare_tire = false;
                        if (!GameManager.instance.my_items.have_spare_tire)
                        {
                            find_spare_tire = UnityEngine.Random.Range(0, 100) > 50;
                        }

                        if (find_rader) GameManager.instance.my_items.have_radar = true;
                        if (find_shovel)
                        {
                            GameManager.instance.my_items.have_shovel = true;
                            MaxCarHealth += 3;
                            CarHealth += 3;
                        }
                        if (find_spare_tire) GameManager.instance.my_items.have_spare_tire = true;
                      
                        Result_panel.obj_text.text = "<color=#00FF00FF>" + "+ gasoline x" + find_gas + "</color>" + Environment.NewLine;
                        if (find_rader) Result_panel.obj_text.text += "<color=#00FF00FF>" + "+ rader x1" + "</color>" + Environment.NewLine;
                        if (find_shovel) Result_panel.obj_text.text += "<color=#00FF00FF>" + "+ shovel x1" + "</color>" + Environment.NewLine;
                        if (find_spare_tire) Result_panel.obj_text.text += "<color=#00FF00FF>" + "+ spare tire x1" + "</color>" + Environment.NewLine;

                        break;
                    }
                case Destination.Hospital:
                    {
                        int find_water = UnityEngine.Random.Range(0, 1);
                        GameManager.ItemChange(Car.ObjectType.water, find_water);

                        int find_aid = UnityEngine.Random.Range(1, 3);
                        GameManager.ItemChange(Car.ObjectType.firstaid, find_aid);


                        Result_panel.obj_text.text = "<color=#00FF00FF>" +  "+ first-aid x" + find_aid + "</color>" + Environment.NewLine;
                        if (find_water>0) Result_panel.obj_text.text += "<color=#00FF00FF>" + "+ water x" + find_water + "</color>" + Environment.NewLine;

                        break;
                    }
                case Destination.Supermarket:
                    {
                        int find_food = UnityEngine.Random.Range(1, 3);
                        GameManager.ItemChange(Car.ObjectType.food, find_food);
                        int find_water = UnityEngine.Random.Range(0, 2);
                        GameManager.ItemChange(Car.ObjectType.water, find_water);

                        Result_panel.obj_text.text = "<color=#00FF00FF>" +  "+ food x" + find_food + "</color>" + Environment.NewLine;
                        if (find_water > 0) Result_panel.obj_text.text += "<color=#00FF00FF>" +  "+ water x" + find_water + "</color>" + Environment.NewLine;

                        break;
                    }
            }
        }


        GameManager.ItemChange(Car.ObjectType.gasoline, -1);
        Result_panel.obj_text.text += "<color=#FF0000FF>- gasoline x1</color>" + Environment.NewLine;

        if (UnityEngine.Random.Range(0, 100) > 60)
        {
            CarHealth--;
            Result_panel.event_text.text += "<color=#FF0000FF>The car was damaged</color>" + Environment.NewLine;

        }
        if (CarHealth <= 0)
        {
            GameOver("<color=#FF0000FF>Vehicle scrapped</color>");
            return;
        }

        if (GameManager.instance.my_items.gasoline < 0)
        {
            GameOver("<color=#FF0000FF>Running out of power</color>");
            return;
        }


        if (GameManager.instance.my_items.water > 0)
        {
            GameManager.ItemChange(Car.ObjectType.water, -1);
            for(int i = 0; i < people.Count;i++)
            {
                People p = people[i];
                p.thirsty = false;
                people[i] = p;
            }
            Result_panel.obj_text.text += "<color=#FF0000FF>- water x1</color>" + Environment.NewLine;
        }
        else
        {
            int id = UnityEngine.Random.Range(0, people.Count);
            if (people[id].thirsty)
            {
                bool left= UnityEngine.Random.Range(0, 100) > 70;
                if (left)
                {
                    Result_panel.event_text.text += "<color=#FF0000FF>" + people[id].name + " left because he was too thirsty...</color>" + Environment.NewLine;
                    KillPeople(people[id]);
                    
                }
            }
            else
            {
                People p = people[id];
                p.thirsty = true;
                people[id] = p;
                Result_panel.event_text.text += "<color=#FF0000FF>" + people[id].name + " is thirsty...</color>" + Environment.NewLine;
            }
        }

        if (GameManager.instance.my_items.food > 0)
        {
            GameManager.ItemChange(Car.ObjectType.food, -1);
            for (int i = 0; i < people.Count; i++)
            {
                People p = people[i];
                p.hungry = false;
                people[i] = p;
            }
            Result_panel.obj_text.text+= "<color=#FF0000FF>- food x1</color>" + Environment.NewLine;
        }
        else
        {
            int id = UnityEngine.Random.Range(0, people.Count);
            if (people[id].hungry)
            {
                bool left = UnityEngine.Random.Range(0, 100) > 70;
                if (left)
                {
                    
                    Result_panel.event_text.text += "<color=#FF0000FF>" + people[id].name + " left because he was too hungry...</color>" + Environment.NewLine;
                    KillPeople(people[id]);
                }
            }
            else
            {
                People p = people[id];
                p.hungry = true;
                people[id] = p;
                Result_panel.event_text.text += "<color=#FF0000FF>" + people[id].name + " is hungry...</color>" + Environment.NewLine;
            }
        }

        UpdatePeopleList();
    }

    public enum ChooseType
    {
        Destination,
        People,
        Cure
    }

    public void Choosed(int id,ChooseType t)
    {
        if (t == ChooseType.Destination)
        {
            switch (id)
            {
                case 1:
                    {
                        destination = choose1;
                        break;
                    }
                case 2:
                    {
                        destination = choose2;
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                case 4:
                    {
                        break;
                    }
            }
            ChoosePeople();
        }
        else if (t == ChooseType.People)
        {
            if (id == 1) ChoosedPeople.name = "";
            else
            {
                ChoosedPeople = people[id-2];
            }
            Destination_panel.gameObject.SetActive(false);
            Start_a_new_day();
        }
        else if (t == ChooseType.Cure)
        {
            if (id == 1)
            {
                Destination_panel.gameObject.SetActive(false);
                return;
            }
            else
            {
                GameManager.ItemChange(Car.ObjectType.firstaid, -1);
                People p = people[id - 2];p.Hurt = false;
                people[id - 2] = p;
                Destination_panel.gameObject.SetActive(false);
                UpdatePeopleList();
            }
            cureButton.SetActive(false);
        }
    }


    #region People

    public Transform People_list;
    public List<Transform> PeopleIcons = new List<Transform>();
    public List<Transform> PeopleIcons_prefab = new List<Transform>();
    public void AddPeople(People name)
    {
        if(!people.Contains(name))
        people.Add(name);
        UpdatePeopleList();

    }
    public void KillPeople(People name)
    {
        if (!people.Contains(name)) return;
            people.Remove(name);
        if (people.Count == 0)
        {
            GameOver("<color=#FF0000FF>All people are dead</color>");
            
            print("游戏结束--所以角色死亡");
        }
        UpdatePeopleList();
    }

    public void cure_people()
    {
        Destination_panel.event_text.text = "who do you want to cure";
        Destination_panel.buttons[1].GetComponentInChildren<Text>().text = "Nobody";
        Destination_panel.buttons[2].gameObject.SetActive(false);
        Destination_panel.buttons[3].gameObject.SetActive(false);
        Destination_panel.buttons[4].gameObject.SetActive(false);
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Hurt)
            {
                Destination_panel.buttons[i + 2].gameObject.SetActive(true);
                Destination_panel.buttons[i + 2].GetComponentInChildren<Text>().text = people[i].name;
            }
        }
        Destination_panel.type = ChooseType.Cure;
        Destination_panel.gameObject.SetActive(true);
    }

    public void UpdatePeopleList()
    {
        for (int i = 0; i < PeopleIcons.Count; i++)
        {
            Destroy(PeopleIcons[i].gameObject);
        }
        PeopleIcons.Clear();

        for(int i = 0; i < 3; i++)
        {
            Car.instance.people[i].sprite = null;
        }

        for (int i = 0; i < people.Count; i++)
        {
            GameObject g;
            switch (people[i].name)
            {
                case "Jeff":
                    {
                        g = Instantiate(PeopleIcons_prefab[0].gameObject);
                        break;
                    }
                case "Steve":
                    {
                        g = Instantiate(PeopleIcons_prefab[1].gameObject);
                        break;
                    }
                case "Fang":
                    {
                        g = Instantiate(PeopleIcons_prefab[2].gameObject);
                        break;
                    }
                default:
                    {
                        g = new GameObject();
                        break;
                    }
            }
            PeopleIcons.Add(g.transform);
            g.transform.parent = People_list;
            PeopleIcon icon = g.GetComponent<PeopleIcon>();
            Car.instance.people[i].sprite = icon.sprite;
            icon.status.text = "";
            if (people[i].hungry) icon.status.text += "Hungry" + Environment.NewLine;
            if (people[i].thirsty) icon.status.text += "Thirsty" + Environment.NewLine;
            if (people[i].Hurt) icon.status.text += "Hurt" + Environment.NewLine;
        }

        
    }
    #endregion
    public GameObject RepairButton;
    public void RepairCar()
    {

        CarHealth++;Car.instance.Update_UI();
        RepairButton.SetActive(false);
    }

    public void GameOver(string reason)
    {
        Destroy(DayshowPanel.instance.gameObject, 180);
        GamingCamera.gameObject.SetActive(false);
        GamingUI.SetActive(false);
        GameOverCamera.SetActive(true);
        GameOverUI.SetActive(true);
        Result_panel.gameObject.SetActive(true);
        Result_panel.event_text.text = "<color=#FF0000FF>GAME OVER</color>"+Environment.NewLine+ reason;
        Result_panel.obj_text.text = "";
    } 
}
