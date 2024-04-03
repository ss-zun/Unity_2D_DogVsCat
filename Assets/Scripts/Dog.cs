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

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 스크린상의 마우스 좌표를 월드 좌표로 변환
        float x = mousePos.x;

        // 강아지가 화면 밖으로 나가지 않게 x값 제한
        if(x > 8.5f)
        {
            x = 8.5f;
        }
        if(x < -8.5f)
        {
            x = -8.5f;
        }

        transform.position = new Vector2(x, transform.position.y); // x값만 움직이고 y값은 현재 강아지의 y 위치
    }

    // 강아지의 Food 생성
    void MakeFood()
    {
        float x = transform.position.x;
        float y = transform.position.y + 2.0f;
        Instantiate(food, new Vector2(x, y), Quaternion.identity); // Quaternion.identity : 별도의 회전값을 주지 않겠다는 의미
    }
}
