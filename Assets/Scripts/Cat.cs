using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public GameObject hungryCat;
    public GameObject fullCat;

    public RectTransform front;

    public int type;

    float full = 5.0f;    // 최대 체력
    float energy = 0.0f;  // 현재 체력
    float speed = 0.05f;

    bool isFull = false;

    // Start is called before the first frame update
    void Start()
    {
        // 고양이 랜덤 위치 생성
        float x = Random.Range(-9.0f, 9.0f);
        float y = 30.0f;
        transform.position = new Vector2(x, y);

        if(type == 1) // NormalCat
        {
            speed = 0.05f;
            full = 5f;
        }
        else if(type == 2) // FullCat
        {
            speed = 0.02f;
            full = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(energy < full) // 현재 체력 < 전체 체력일 때 아래로 내려가기
        {
            transform.position += Vector3.down * speed;

            // 배부르지 않은 고양이가 생선가게에 닿았을 때
            if(transform.position.y < -16.0f)
            {
                GameManager.Instance.GameOver(); // 게임 종료
            }
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
                    // 점수를 더할 때 배가 부른 고양이가 Food에 또 맞게 되면 발생하는 문제 방어
                    if (!isFull)
                    {
                        isFull = true;
                        hungryCat.SetActive(false);
                        fullCat.SetActive(true);
                        Destroy(gameObject, 3.0f); // 3초 후 고양이 파괴
                        GameManager.Instance.AddScore();
                    }
                }
            }
        }
    }
}
