using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelect : MonoBehaviour
{

    BmsRead BmsReader;

    private void Start()
    {
        BmsReader = GetComponent<BmsRead>();
    }

    // �� ���� �Լ� - ��ư �ݹ��
    public void MusicSelection(int SelectNum)
    {
            Debug.Log("Play Selected");
            GameManager.Instance.MusicNum = SelectNum;
            BmsReader.NoteCreate(SelectNum);
            Debug.Log("Note Instantiate Success");
            GameManager.Instance.GoPlay();
        
    }


}
