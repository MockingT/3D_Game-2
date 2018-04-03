using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

public class Model : MonoBehaviour
{

    Stack<GameObject> start_priests = new Stack<GameObject>(); // store the priests on the start side
    Stack<GameObject> end_priests = new Stack<GameObject>(); // store the priests on the arrival
    Stack<GameObject> start_devils = new Stack<GameObject>(); // store the devils on the start side
    Stack<GameObject> end_devils = new Stack<GameObject>(); // store the devils on the arrival

    GameObject[] boat = new GameObject[2];
    GameObject boat_obj;
    SSDirector one;

    Vector3 boatStartPos = new Vector3(-11, -3, 0); // boat start from left
    Vector3 boatEndPos = new Vector3(-1, -3, 0); // boat end at right
    Vector3 shoreStartPos = new Vector3(-19, -5, 0); // the left shore
    Vector3 shoreEndPos = new Vector3(8, -5, 0);  // the right shore
    Vector3 priestsStartPos = new Vector3(-14, -1.5f, 0); 
    Vector3 priestsEndPos = new Vector3(7, -1.5f, 0);
    Vector3 devilsStartPos = new Vector3(-20, -1.5f, 0);
    Vector3 devilsEndPos = new Vector3(13, -1.5f, 0);
    // the original postion
    
    public float speed = 50f;

    // Initialization  
    void Start()
    {
        one = SSDirector.GetInstance();
        one.setModel(this);
        loadSrc();
    }

    // Update is called once per frame  
    void Update()
    {
        setposition(start_priests, priestsStartPos);
        setposition(end_priests, priestsEndPos);
        setposition(start_devils, devilsStartPos);
        setposition(end_devils, devilsEndPos);

        if (one.state == State.Start_to_End)
        {
            boat_obj.transform.position = Vector3.MoveTowards(boat_obj.transform.position, boatEndPos, Time.deltaTime * speed);
            if (boat_obj.transform.position == boatEndPos)
            {
                one.state = State.End;
            }
        }
        else if (one.state == State.End_to_Start)
        {
            boat_obj.transform.position = Vector3.MoveTowards(boat_obj.transform.position, boatStartPos, Time.deltaTime * speed);
            if (boat_obj.transform.position == boatStartPos)
            {
                one.state = State.Start;
            }
        }
        else
        {
            check();
            //check the status per frame
        }
    }

    // load the object source
    void loadSrc()
    {
        //prisets and devils  
        for (int i = 0; i < 3; i++)
        {
            start_priests.Push(Instantiate(Resources.Load("Prefabs/priest")) as GameObject);
            start_devils.Push(Instantiate(Resources.Load("Prefabs/devil")) as GameObject);
        }
        //shore
        Instantiate(Resources.Load("Prefabs/shore"), shoreStartPos, Quaternion.identity);
        Instantiate(Resources.Load("Prefabs/shore"), shoreEndPos, Quaternion.identity);
        //boat
        boat_obj = Instantiate(Resources.Load("Prefabs/boat"), boatStartPos, Quaternion.identity) as GameObject;
    }

    void setposition(Stack<GameObject> aaa, Vector3 pos)
    {
        GameObject[] temp = aaa.ToArray();
        for (int i = 0; i < aaa.Count; i++)
        {
            temp[i].transform.position = pos + new Vector3(-1.5f * i, 0, 0); 
            // seperate the priests and the evils
        }
    }

    // get on the boat
    void getOnTheboat(GameObject obj)
    {
        if (check_boat() != 0)
        {
            obj.transform.parent = boat_obj.transform;
            if (boat[0] == null)
            {
                boat[0] = obj;
                obj.transform.localPosition = new Vector3(-0.4f, 1, 0);
            }
            else
            {
                boat[1] = obj;
                obj.transform.localPosition = new Vector3(0.4f, 1, 0);
            }
        }
    }

    public void boat_move()
    {
        if (check_boat() != 2)
        {
            if (one.state == State.Start)
            {
                one.state = State.Start_to_End;
            }
            else if (one.state == State.End)
            {
                one.state = State.End_to_Start;
            }
        }
    }

    // get off the boat
    public void getOffTheboat(int side)
    {
        if (boat[side] != null)
        {
            boat[side].transform.parent = null;
            if (one.state == State.Start)
            {
                if (boat[side].tag == "Priest")
                {
                    start_priests.Push(boat[side]);
                }
                else
                {
                    start_devils.Push(boat[side]);
                }
            }
            else if (one.state == State.End)
            {
                if (boat[side].tag == "Priest")
                {
                    end_priests.Push(boat[side]);
                }
                else
                {
                    end_devils.Push(boat[side]);
                }
            }
            boat[side] = null;
        }
    }

    void check()
    {
        if (end_devils.Count == 3 && end_priests.Count == 3) // win the game
        {
            one.state = State.Win;
            return;
        }

        int bp = 0, bd = 0;
        for (int i = 0; i < 2; i++)
        {
            if (boat[i] != null && boat[i].tag == "Priest")
            {
                bp++;
            }
            else if (boat[i] != null && boat[i].tag == "Devil")
            {
                bd++;
            }
        }

        int sp = 0, sd = 0, ep = 0, ed = 0;
        if (one.state == State.Start)
        {
            sp = start_priests.Count + bp;
            ep = end_priests.Count;
            sd = start_devils.Count + bd;
            ed = end_devils.Count;
        }
        else if (one.state == State.End)
        {
            sp = start_priests.Count;
            ep = end_priests.Count + bp;
            sd = start_devils.Count;
            ed = end_devils.Count + bd;
        }

        if ((sp != 0 && sp < sd) || (ep != 0 && ep < ed))
        {
            one.state = State.Lose;
        }
    }

    // priests'move
    public void priS()
    {
        if (start_priests.Count != 0 && check_boat() != 0 && one.state == State.Start)
        {
            getOnTheboat(start_priests.Pop());
        }
    }
    public void priE()
    {
        if (end_priests.Count != 0 && check_boat() != 0 && one.state == State.End)
        {
            getOnTheboat(end_priests.Pop());
        }
    }

    // devils'move
    public void delS()
    {
        if (start_devils.Count != 0 && check_boat() != 0 && one.state == State.Start)
        {
            getOnTheboat(start_devils.Pop());
        }
    }
    public void delE()
    {
        if (end_devils.Count != 0 && check_boat() != 0 && one.state == State.End)
        {
            getOnTheboat(end_devils.Pop());
        }
    }

    // check if there is empty seat 
    int check_boat()
    {
        int num = 0, i = 0;
        for (i = 0; i < 2; i++)
        {
            if (boat[i] == null)
            {
                num++;
            }
        }
        return num;
    }

    public void Reset_game()
    {
        boat_obj.transform.position = boatStartPos;
        int num1 = end_devils.Count, num2 = end_priests.Count;
        for (int i = 0; i < num1; i++)
        {
            start_devils.Push(end_devils.Pop());
        }
        for (int i = 0; i < num2; i++)
        {
            start_priests.Push(end_priests.Pop());
        }
        getOffTheboat(0);
        getOffTheboat(1);
    }
}