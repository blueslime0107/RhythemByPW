using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{

    public GameObject smallRectangle1;   // 작은 사각형 1
    public GameObject smallRectangle2;   // 작은 사각형 2
    public GameObject bigRectangle;      // 큰 사각형
    [SerializeField] public Note[] noteComp;

    // public float LongMoveSpeed, tempMoveSpeed;
    // public bool Pressed = false;
    // public bool CanBePressed = true;
    // public int timing = 0;
    // private bool CoCalled = false;
    

    float LongNoteLength = 1;
    // int repeatCount = 0;

    public void LengthComp(float LongLength)
    {
        smallRectangle2.transform.position = smallRectangle1.transform.position + new Vector3(0, LongLength + 0.125f, 0);
        float height = Mathf.Abs(smallRectangle2.transform.localPosition.y - smallRectangle1.transform.localPosition.y);
        bigRectangle.transform.localScale = new Vector3(bigRectangle.transform.localScale.x, height, bigRectangle.transform.localScale.z);
    }

    private void Update() {
        float y1 = smallRectangle1.transform.position.y;
        float y2 = smallRectangle2.transform.position.y;
        float bigY = (y1 + y2) / 2f;
        bigRectangle.transform.position = new Vector3(bigRectangle.transform.position.x, bigY, bigRectangle.transform.position.z);
    }

    public IEnumerator startSrink(){
        while(true){
            if(!smallRectangle1 && gameObject){
                Destroy(gameObject);
                break;
            }
            float height = Mathf.Abs(smallRectangle2.transform.localPosition.y - smallRectangle1.transform.localPosition.y);
            bigRectangle.transform.localScale = new Vector3(bigRectangle.transform.localScale.x, height, bigRectangle.transform.localScale.z);
            yield return null;
        }
    } 
    // private void Start()
    // {
    //     tempMoveSpeed = LongMoveSpeed = GameManager.Instance.NoteSpeed;        
    // }

    // bool once = true;

    // void Update()
    // {
    //     if (GameManager.Instance.MusicPlaying)
    //     {
    //         transform.position += Vector3.down * LongMoveSpeed * Time.deltaTime;
    //     }
    //     if (GameManager.Instance.OnSelecting) Destroy(gameObject);

    // }

    // void Update()
    // {
    //     // �ݺ� Ƚ�� ����
    //     if (once)
    //     {
    //         repeatCount = (int)(LongNoteLength / 8);
    //         if (repeatCount == 0) repeatCount++;
    //     }

    //     // �� ���� ���� ��
    //     if (GameManager.Instance.MusicPlaying)
    //     {
    //         // ��尡 �����Ǿ��� ��
    //         if (Pressed)
    //         {
    //             // �����̴� �ӵ� 0���� �����ϰ� ���� ���̱�
    //             LongMoveSpeed = 0;                
    //             LongNoteComp[1].transform.position -= new Vector3(0, tempMoveSpeed / 2 * Time.deltaTime, 0);
    //             StartCoroutine(Shrink());

    //             // ���� ���� ���� -> ����� ������ ����
    //             if(!CoCalled) 
    //             {
    //                 StartCoroutine(Timing());
    //                 CoCalled = true;
    //             }
                
    //         }
    //         // �����̱�
    //         transform.position += Vector3.down * LongMoveSpeed * Time.deltaTime;
    //     }
    //     if(!CanBePressed)
    //     {
    //         // ��ü �ı�
    //         StopAllCoroutines();
    //         Destroy(gameObject);
    //     }

    //     if (GameManager.Instance.OnSelecting) Destroy(gameObject);
    // }












    // IEnumerator Shrink()
    // {
    //     // ũ�� ���̱�
    //     LongNoteComp[1].transform.localScale -= new Vector3(0, tempMoveSpeed * Time.deltaTime, 0);

    //     // ������ �ٿ����ٸ� ��ü �ı��� �غ�
    //     if (LongNoteComp[1].transform.localScale.y <= 0f)
    //     {
    //         CanBePressed = false;
    //     }

    //     yield return null;
    // }

    // IEnumerator Timing()
    // {
    //     // ����� ������ ����
    //     if(timing == 1)
    //     {
    //         GameManager.Instance.ScoreManager += 30;
    //     }
    //     else if (timing == 2)
    //     {
    //         GameManager.Instance.ScoreManager += 25;
    //     }
    //     else if (timing == 3)
    //     {
    //         GameManager.Instance.ScoreManager += 20;
    //     }
    //     else if (timing == 4)
    //     {
    //         GameManager.Instance.ScoreManager += 15;
    //     }

    //     // ���� ���
    //     GameManager.Instance.TimingOutput(timing);

    //     // �޺� ����
    //     GameManager.Instance.ComboManager++;

    //     // ���� ��� 8��ŭ �����ϵ��� �ð� ����
    //     yield return new WaitForSeconds((LongNoteLength / tempMoveSpeed) / (float)repeatCount);
    //     CoCalled = false;
    // }

}
