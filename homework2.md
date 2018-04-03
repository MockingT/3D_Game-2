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
![avatar](https://github.com/MockingT/3D_Game-2/blob/master/picture/3d4.png)

