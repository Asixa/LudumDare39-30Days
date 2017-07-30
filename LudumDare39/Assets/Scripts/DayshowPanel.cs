using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayshowPanel : MonoBehaviour {
    public AnimationHelp anim;
    public AnimationClip showClip;
    public static DayshowPanel instance;
    public DayshowPanel()
    {
        instance = this;
    }
    public void Awake()
    {
        Hide();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void ShowNewDayInfo()
    {
        GameEvents.instance.show_new_day_info();
    }
    public Text Shows;
}
