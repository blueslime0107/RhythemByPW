using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusic : MonoBehaviour
{

    /*
    노트 정보 형식
    00/000/0/0

    - 일의 자리 : 노트 종류
    0 : ShortNote
    1 : LongNote

    - 십의 자리 : 노트 가로 위치
    0 : D
    1 : F
    2 : J
    3 : K
    4 : S
    5 : L

    - 백의 자리 ~ 십만의 자리
    노트 간격 : 1초에 4칸
    최대 길이 : 960칸 (4분)

    ex) xx/1000/0/0
    ShortNote를 D자리에 Y축으로 100.0만큼 이동해서 생성

    ex) 04/0400/4/1
    길이가 4인 LongNote를 S자리에 Y축으로 40.0만큼 이동해서 생성

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
            // 노트 생성
            GameManager.Instance.NoteCreate(TestMusicNoteInf);
            // 키 개수 설정
            GameManager.Instance.NumofKey = KeyNum;
            // 시간 설정
            GameManager.Instance.TimeLimit = 18d / (GameManager.Instance.NoteSpeed / 4);

            // 현재 점수 초기화
            GameManager.Instance.ScoreManager = 0;
            // 콤보 초기화
            GameManager.Instance.ComboManager = 0;
            GameManager.Instance.BestCombo = 0;

            // 메인 화면 벗어남
            GameManager.Instance.OnMain = false;
            // 음악 시작
            GameManager.Instance.MusicPlaying = true;
            // 곡 선택 불가
            GameManager.Instance.MusicSelectAble = false;
            // 곡 선택 초기화
            GameManager.Instance.MusicNum = 0;
        }
        

    }
    */

}
