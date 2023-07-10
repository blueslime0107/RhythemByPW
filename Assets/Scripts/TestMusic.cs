using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusic : MonoBehaviour
{

    /*
    ��Ʈ ���� ����
    00/000/0/0

    - ���� �ڸ� : ��Ʈ ����
    0 : ShortNote
    1 : LongNote

    - ���� �ڸ� : ��Ʈ ���� ��ġ
    0 : D
    1 : F
    2 : J
    3 : K
    4 : S
    5 : L

    - ���� �ڸ� ~ �ʸ��� �ڸ�
    ��Ʈ ���� : 1�ʿ� 4ĭ
    �ִ� ���� : 960ĭ (4��)

    ex) xx/1000/0/0
    ShortNote�� D�ڸ��� Y������ 100.0��ŭ �̵��ؼ� ����

    ex) 04/0400/4/1
    ���̰� 4�� LongNote�� S�ڸ��� Y������ 40.0��ŭ �̵��ؼ� ����

    */

    int[] TestMusicNoteInf = new int[18];

    private const int KeyNum = 6;

    private void Awake()
    {
        //Mselect = GetComponent<MusicSelect>();
        TestMusicNoteInf[0] = 4004001; TestMusicNoteInf[1] = 8010; TestMusicNoteInf[2] = 12020; TestMusicNoteInf[3] = 16030;

        TestMusicNoteInf[4] = 20010; TestMusicNoteInf[5] = 24000; TestMusicNoteInf[6] = 28040; TestMusicNoteInf[7] = 32030;

        TestMusicNoteInf[8] = 36030; TestMusicNoteInf[9] = 40050; TestMusicNoteInf[10] = 44010; TestMusicNoteInf[11] = 48000;

        TestMusicNoteInf[12] = 52050; TestMusicNoteInf[13] = 54010; TestMusicNoteInf[14] = 56000; TestMusicNoteInf[15] = 58030;

        TestMusicNoteInf[16] = 4060041; TestMusicNoteInf[17] = 6062031;


    }

    /*
    void Update()
    {
        if (GameManager.Instance.MusicNum == 1)
        {
            // ��Ʈ ����
            GameManager.Instance.NoteCreate(TestMusicNoteInf);
            // Ű ���� ����
            GameManager.Instance.NumofKey = KeyNum;
            // �ð� ����
            GameManager.Instance.TimeLimit = 18d / (GameManager.Instance.NoteSpeed / 4);

            // ���� ���� �ʱ�ȭ
            GameManager.Instance.ScoreManager = 0;
            // �޺� �ʱ�ȭ
            GameManager.Instance.ComboManager = 0;
            GameManager.Instance.BestCombo = 0;

            // ���� ȭ�� ���
            GameManager.Instance.OnMain = false;
            // ���� ����
            GameManager.Instance.MusicPlaying = true;
            // �� ���� �Ұ�
            GameManager.Instance.MusicSelectAble = false;
            // �� ���� �ʱ�ȭ
            GameManager.Instance.MusicNum = 0;
        }
        

    }
    */

}
