using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public GameObject hungryCat;
    public GameObject fullCat;

    public RectTransform front;

    float full = 5.0f;    // 최대 체력
    float energy = 0.0f;  // 현재 체력

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // 나중에 GameManager로 옮겨야함

        // 고양이 랜덤 위치 생성
        float x = Random.Range(-9.0f, 9.0f);
        float y = 30.0f;
        transform.position = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        if(energy < full) // 현재 체력 < 전체 체력일 때 아래로 내려가기
        {
            transform.position += Vector3.down * 0.05f;
        }
        else  // 체력바가 다 찬 상태일 때 옆으로 이동하기(왼쪽일 땐 왼쪽으로, 오른쪽일 땐 오른쪽으로 이동)
        {
            // 중앙 기준으로 오른쪽에 있을 때
            if(transform.position.x > 0)
            {
                transform.position += Vector3.right * 0.05f; // 오른쪽으로 이동
            }
            else // x가 0이거나 x(중앙)보다 작을 때
            {
                transform.position += Vector3.left * 0.05f; // 왼쪽으로 이동
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            if(energy < full) // 현재 체력 < 전체 체력
            {
                // 체력바 게이지 상승
                energy += 1.0f;
                front.localScale = new Vector3(energy / full, 1.0f, 1.0f); // 현재 체력을 최대 체력으로 나누어서 비율을 계산

                // 고양이에게 맞은 Food는 파괴
                Destroy(collision.gameObject);

                // 게이지가 다 차고 바로 fullCat으로 바뀌게 하기 위해서 여기에 코드 작성
                if (energy == 5.0f) // 체력바가 다 찬 상태
                {    
                    hungryCat.SetActive(false);
                    fullCat.SetActive(true);
                }
            }
        }
    }
}
