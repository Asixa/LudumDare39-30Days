using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Window : MonoBehaviour {

    public Text event_text,obj_text;
    public List<Button> buttons = new List<Button>();
    public GameEvents.ChooseType type;
    public void ButtonClick(int id)
    {
        GameEvents.instance.Choosed(id,type);
    }
    
}
