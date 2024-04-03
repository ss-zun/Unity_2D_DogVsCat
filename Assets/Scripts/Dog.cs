using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GameObject food;

    void Start()
    {
        InvokeRepeating("MakeFood", 0f, 0.5f); // 강아지의 Food 반복 생성해주기
    }

    // 강아지의 Food 생성
    void MakeFood()
    {
        float x = transform.position.x;
        float y = transform.position.y + 2.0f;
        Instantiate(food, new Vector2(x, y), Quaternion.identity); // Quaternion.identity : 별도의 회전값을 주지 않겠다는 의미
    }
}
