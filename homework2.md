# 简单题
### 一. 游戏对象运动的本质： ###  
- 游戏对象运动的本质是在游戏对象在每一帧中的位置，大小和旋转发生的转变。即Transform中的position，rotation和大小。  
### 二. 请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）: ###  
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
