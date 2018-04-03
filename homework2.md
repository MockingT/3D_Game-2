# 简单题
### 一. 游戏对象运动的本质： ###  
- 游戏对象运动的本质是在游戏对象在每一帧中的位置，大小和旋转发生的转变。即Transform中的position，rotation和大小。  
### 二. 请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）: ###  
- 使用向量Vector3的方法:  

		using System.Collections;  
		using System.Collections.Generic;  
		using UnityEngine;  
		public class NewBehaviourScript : MonoBehaviour  
		{  
    		public float speed = 0.01f;
    		void Start()
    		{
    				    
    		}
    		void Update()
    		{
        		Vector3 move = new Vector3(Time.deltaTime * 1, 0, Time.deltaTime * (speed / 100));
        		speed++; 
        		this.transform.position += move;// add move to position
			}
		}
- 直接改变物体postion中的数值:  

		using System.Collections;  
		using System.Collections.Generic;  
		using UnityEngine;  
		public class NewBehaviourScript : MonoBehaviour  
		{  
    		public float speed = 0.01f;
    		void Start()
    		{
        
    		}
    		void Update()
    		{
        		this.transform.Translate(new Vector3(Time.deltaTime * 1, 0, Time.deltaTime * (speed / 100)));
        		// directly use "Translate" method
    		}
		}  
- 修改Transform属性：  

		using System.Collections;  
		using System.Collections.Generic;  
		using UnityEngine;  
		public class NewBehaviourScript : MonoBehaviour  
		{  
    		public float speed = 0.01f;
    		void Start()
    		{
        
    		}
    		void Update()
    		{
        		this.transform.position += Vector3.up * Time.deltaTime * (speed++ / 100);
        		// go up
        		this.transform.position += Vector3.left * Time.deltaTime * 1;
        		// go left 
    		}
		}  
### 三. 写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上: ###  
- 创建太阳系的GameObject（一个太阳，八大行星，一个月亮）  
![avatar](https://github.com/MockingT/3D_Game-2/blob/master/picture/3d2.png)  
- 编写下面的c#代码以实现行星围绕太阳公转，行星自转以及月亮围绕地球公转:  
其中RotateAround用于实现围绕太阳公转（三个参数分别为围绕公转点的位置，围绕公转的平面的法向量，以及速度），Rotate用于实现自转  

		using System.Collections;
		using System.Collections.Generic;
		using UnityEngine;

		public class SunSystem : MonoBehaviour {
    		public float earthSpeed = 25.0f;
    		public float mercurySpeed = 30.0f;
    		public float marsSpeed = 35.0f;
    		public float jupiterSpeed = 20.0f;
    		public float saturnSpeed = 40.0f;
    		public float uranusSpeed = 28.0f;
    		public float neptuneSpeed = 33.0f;
    		public float venusSpeed = 38.0f;
    		public float speed = 888.0f;
    		// Use this for initialization
    		void Start () {
		
			}
	
			// Update is called once per frame
			void Update () {
        		// rotate around the sun
        		GameObject.Find("earth").transform.RotateAround(GameObject.Find("sun").transform.position, new Vector3(0, 1, 1), earthSpeed * Time.deltaTime);
        		GameObject.Find("mercury").transform.RotateAround(GameObject.Find("sun").transform.position, new Vector3(0, 1, 2), mercurySpeed * Time.deltaTime);
        		GameObject.Find("mars").transform.RotateAround(GameObject.Find("sun").transform.position, new Vector3(0, 1, 4), marsSpeed * Time.deltaTime);
        		GameObject.Find("jupiter").transform.RotateAround(GameObject.Find("sun").transform.position, new Vector3(0, 1, 3), jupiterSpeed * Time.deltaTime);
        		GameObject.Find("saturn").transform.RotateAround(GameObject.Find("sun").transform.position, new Vector3(0, 1, -2), saturnSpeed * Time.deltaTime);
        		GameObject.Find("uranus").transform.RotateAround(GameObject.Find("sun").transform.position, new Vector3(0, 1, 5), uranusSpeed * Time.deltaTime);
        		GameObject.Find("neptune").transform.RotateAround(GameObject.Find("sun").transform.position, new Vector3(0, 1, -1), neptuneSpeed * Time.deltaTime);
        		GameObject.Find("venus").transform.RotateAround(GameObject.Find("sun").transform.position, new Vector3(0, 1, 2), venusSpeed * Time.deltaTime);

        		// self-rotation
        		GameObject.Find("earth").transform.Rotate(Vector3.up * Time.deltaTime * speed);
        		GameObject.Find("mercury").transform.Rotate(Vector3.up * Time.deltaTime * speed);
        		GameObject.Find("mars").transform.Rotate(Vector3.up * Time.deltaTime * speed);
        		GameObject.Find("jupiter").transform.Rotate(Vector3.up * Time.deltaTime * speed);
        		GameObject.Find("saturn").transform.Rotate(Vector3.up * Time.deltaTime * speed);
        		GameObject.Find("uranus").transform.Rotate(Vector3.up * Time.deltaTime * speed);
        		GameObject.Find("neptune").transform.Rotate(Vector3.up * Time.deltaTime * speed);
        		GameObject.Find("venus").transform.Rotate(Vector3.up * Time.deltaTime * speed);

		        // moon rotates around the earth
        		GameObject.Find("moon").transform.RotateAround(GameObject.Find("earth").transform.position, new Vector3(0, 1, 2), 80 * Time.deltaTime);
    		}
		}  
- 运行效果如下图:  
![avatar](https://github.com/MockingT/3D_Game-2/blob/master/picture/3d4.png)  
![avatar](https://github.com/MockingT/3D_Game-2/blob/master/picture/3d3.png)  
### 四. 牧师与魔鬼 ###  
- 游戏对象  

| Name | Number |
| :-: | :-: |
| 牧师 | 3 |
| 魔鬼 | 3 |
| 船只 | 1 |
| 河岸 | 2 |  

- 动作表

| Event | Condition |
| :-: | :-: |
| 开船 | 船处在左岸或右岸并且船上至少有一个人 |
| 船的右边有人下船 | 船靠岸并且右边有人 |
| 船的左边有人下船 | 船靠岸并且左边有人 |
| 牧师在开始岸上船 | 船靠岸并且有空位并且有牧师在开始岸 |
| 牧师在到达岸上船 | 船靠岸并且有空位并且有牧师在到达岸 |
| 魔鬼在开始岸上船 | 船靠岸并且有空位并且有魔鬼在开始岸 |
| 魔鬼在到达岸上船 | 船靠岸并且有空位并且有魔鬼在到达岸 |  

- 预置游戏对象  
![avatar](https://github.com/MockingT/3D_Game-2/blob/master/picture/3d5.png)  

- 运行之后的界面及效果  
![avatar](https://github.com/MockingT/3D_Game-2/blob/master/picture/3d6.png)  
![avatar](https://github.com/MockingT/3D_Game-2/blob/master/picture/3d7.png)  
![avatar](https://github.com/MockingT/3D_Game-2/blob/master/picture/3d8.png)  

- 代码部分采用MVC模式，分别有Model，Controller，View三个脚本文件，其中Controller.cs文件中引入了各个用户接口；而Model.cs文件则实现了各大类，包括牧师与魔鬼的上下船动作，船的移动以及判断输赢等，被Controller.cs中调用；最后的View.cs则是初始化界面，提供了UI，里面各个按钮供用户操作。  

- View.cs具体代码:  

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

- Model.cs具体代码:  

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
		
- Controller.cs具体代码:  

		using System.Collections;
		using System.Collections.Generic;
		using UnityEngine;
		using mygame;

		namespace mygame
		{
		    public enum State { Start, End, Win, Lose, Start_to_End, End_to_Start };
		    public interface Movement
		    {
			void priest_start();
			void priest_end();
			void devil_start();
			void devil_end();
			void boat_move();
			void Left_off_boat();
			void Right_off_boat();
			void reset();
		    }

		    public class SSDirector : System.Object, Movement
		    {
			// only one instance
			private static SSDirector _instance;
			public static SSDirector GetInstance()
			{
			    if (_instance == null)
			    {
				_instance = new SSDirector();
			    }
			    return _instance;
			}

			public Controller CurScenceCtr;
			public State state = State.Start;

			//get game_obj
			private Model game_obj;
			public Model getModel()
			{
			    return game_obj;
			}
			internal void setModel(Model someone)
			{
			    if (game_obj == null)
			    {
				game_obj = someone;
			    }
			}

			// get off boat
			public void Left_off_boat()
			{
			    game_obj.getOffTheboat(0);
			}
			public void Right_off_boat()
			{
			    game_obj.getOffTheboat(1);
			}

			// priest gets on the boat
			public void priest_start()
			{
			    game_obj.priS();
			}
			public void priest_end()
			{
			    game_obj.priE();
			}

			// devil gets on the boat
			public void devil_start()
			{
			    game_obj.delS();
			}
			public void devil_end()
			{
			    game_obj.delE();
			}

			// boat moves
			public void boat_move()
			{
			    game_obj.boat_move();
			}

			// reset
			public void reset()
			{
			    state = State.Start;
			    game_obj.Reset_game();
			}
		    }


		}

		public class Controller : MonoBehaviour
		{
		    // Use this for initialization  
		    void Start()
		    {
			SSDirector one = SSDirector.GetInstance();
		    }

		}

