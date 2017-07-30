using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour {
    public GameObject Menu;
	// Use this for initialization
	void Start () {
		
	}
    public bool Opened=false;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            A();
        }
	}
    public void A()
    {
        Opened = !Opened;
        Menu.SetActive(Opened);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

