using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using mygame;  
  
public class View : MonoBehaviour {  
  
    SSDirector one;  
    Movement action;
    float width;
    float height;
    float castw(float a)
    {
        return (Screen.width - width) / a;
    }
    float casth(float a)
    {
        return (Screen.height - height) / a;
    }

    // Use this for initialization  
    void Start () {  
        one = SSDirector.GetInstance();  
        action = SSDirector.GetInstance() as Movement;  
    }  
  
    private void OnGUI()  
    {  
        GUI.skin.label.fontSize = 20;
        width = Screen.width / 12;
        height = Screen.width / 12;
        
        if (one.state == State.Win)  
        {  
            if(GUI.Button(new Rect(castw(2f), casth(2f), width, height), "WIN"))  
            {  
                action.reset();  
            }  
        }  
        if (one.state == State.Lose)  
        {  
            if(GUI.Button(new Rect(castw(2f), casth(2f), width, height), "LOSE"))  
            {  
                action.reset();  
            }  
        }  
        if(GUI.Button(new Rect(castw(2f), casth(6f), width, height), "GO"))  
        {  
            action.boat_move();  
        }  
        if (GUI.Button(new Rect(castw(2.5f), casth(1f), width, height), "OFF"))  
        {  
            action.Left_off_boat();  
        }  
        if (GUI.Button(new Rect(castw(1.6f), casth(1f), width, height), "OFF"))  
        {  
            action.Right_off_boat();  
        }  
        if (GUI.Button(new Rect(castw(1.2f), casth(4f), width, height), "ON"))  
        {  
            action.priest_end();  
        }  
        if (GUI.Button(new Rect(castw(1f), casth(4f), width, height), "ON"))  
        {  
            action.devil_end();  
        }  
        if (GUI.Button(new Rect(castw(10f), casth(4f), width, height), "ON"))  
        {  
            action.devil_start();  
        }  
        if (GUI.Button(new Rect(castw(4.5f), casth(4f), width, height), "ON"))  
        {  
            action.priest_start();  
        }  
    }  
}  
