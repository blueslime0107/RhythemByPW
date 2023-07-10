using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.IO;
using System;
using NAudio;
using NAudio.Wave;
using NLayer;
//using NLayer.NAudioSupport;

public class BmsRead : MonoBehaviour 
{

    // ��Ʈ ��ü
    // 0 : ShortNote
    // 1 : LongNote
    [SerializeField] NoteHitBase noteHitBase;
    [SerializeField] GameObject[] Note = null;
    // ��ȯ ��ġ �� �θ�ü
    [SerializeField] Transform ParentTrans = null;
    [SerializeField] Transform[] tfNoteAppear = null;
    // ����� ��Ʈ
    [SerializeField] GameObject AudioNote;

    
    // ���� �Ŵ��� ������Ʈ
    [SerializeField] GameObject GM;

    // bms ���� �̸�
    /*[SerializeField]*/ string bmsListFile;
    List<string> bmsFileName = new List<string>();
    [SerializeField] GameObject SongList;
    [SerializeField] GameObject SongListButton;
    private int SongNum = 0;

    // ������ ���� �� ����� ����
    string value, temp_value;
    char temp_char;

    // ��Ʈ Ÿ��, ��ġ ����
    int type = 0, dfjk = 0;
    float pos = 0;
    float channel_length = 32;

    // �ճ�Ʈ ���� ����
    float[] Head_Pos = new float[4];
    float[] Tail_Pos = new float[4];
    float Long_length = 0;
    bool[] Head_Find = new bool[4];
    bool[] Tail_Find = new bool[4];




    // bpm
    float bpm = 0;

    // ��Ʈ ��
    float value_length = 0;

    // ù ��Ʈ ��ġ
    float firstPos;
    bool firstFind = false;


    // ����� ����
    string bmsSongHere;
    List<string> bmsSong = new List<string>();
    public List<AudioClip> Audio = new();
    AudioClip tempClip;


    private void Start()
    {
        // ���ϸ� ����
        bmsListFile = "BMSfileList";
        bmsSongHere = "BMSsongHere";

        // BMS ���ϸ� �˾Ƴ���
        BmsListFileRead();
        // BMS ���� ã��
        BmsListArray();
        // ����� ã��
        StartCoroutine(GetAudio());
    }

    // BMS ����Ʈ ������ �о���̴� �Լ�
    public void BmsListFileRead()
    {
        
        FileInfo bmsListExist = new FileInfo(Application.streamingAssetsPath + "/BmsFiles/" + bmsListFile);
        if (bmsListExist.Exists == false)
        {
            FileStream Listfile = new FileStream(Application.streamingAssetsPath + "/BmsFiles/" + bmsListFile, FileMode.Create); // ����Ʈ ������ �������� �����Ƿ� 
            Debug.Log(bmsListFile + " File doesn't Exist");
            Listfile.Close(); // ���� �ݱ�

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ����
#else
            Application.Quit();  // ���ø����̼ǿ��� ���� ����
#endif

        }
        else
        {
            // �� ����Ʈ ������ �о���̱�
            FileStream Listfile = new FileStream(Application.streamingAssetsPath + "/BmsFiles/" + bmsListFile, FileMode.Open);
            StreamReader bmsListStreamReader = new StreamReader(Listfile);

            SongNum = 0;
            while (bmsListStreamReader.EndOfStream == false)
            {
                bmsFileName.Add(bmsListStreamReader.ReadLine()); // �̸� �о���̱�            
                Debug.Log(bmsFileName[SongNum]);
                SongNum++;

                if (bmsListStreamReader.EndOfStream) break; // ������ ���̶�� ���� �ݱ�

            }
        }
    }


    public void BmsListArray()
    {
        
        // �о���� ������ ��ġ�ϴ� ���� ���� Ȯ��
        for (int i = 0; i < bmsFileName.Count; i++)
        {
            FileInfo bmsExist = new FileInfo(Application.streamingAssetsPath + "/BmsFiles/" + bmsFileName[i]);
            if (bmsExist.Exists == false) // �������� ���� ��
            {
                FileStream file = new FileStream(Application.streamingAssetsPath + "/BmsFiles/" + bmsFileName[i], FileMode.Create); // ������ �������� �����Ƿ� ������ ������ ���ϰ��� ����
                Debug.Log(bmsFileName[i] + " File doesn't Exist");
                file.Close(); // ���� �ݱ�
            }
            else // ������ ��
            {
                Debug.Log(bmsFileName[i] + " File Exists");

                GameObject Song = Instantiate(SongListButton, new Vector3(0, 0, 0), Quaternion.identity); // ��ư ����
                Song.transform.SetParent(SongList.transform); // �θ�ü ����
                Song.transform.localScale = new Vector3(1, 1, 1);
                int temp = i;
                Song.GetComponent<Button>().onClick.AddListener(() => GM.GetComponent<MusicSelect>().MusicSelection(temp)); // OnClick �Լ� �Ҵ�
                Song.GetComponentInChildren<TextMeshProUGUI>().text = bmsFileName[i]; // �ؽ�Ʈ ����
                Audio.Add(null);
            }

            GameManager.Instance.BestScore.Add(0); // ���ӸŴ��� ��ũ��Ʈ�� �� ���� �ִ����� ���� �Ҵ�
        }
    }

    // ����� ���� �޾ƿ���
    IEnumerator GetAudio()
{
    // bmsSongHere file path
    string audioFolderPath = Application.streamingAssetsPath + "/AudioSources/";
    string audioFilePath;

    for (int i = 0; i < bmsFileName.Count; i++)
    {
        // Construct the path to the MP3 file
        audioFilePath = audioFolderPath + bmsFileName[i] + ".mp3";

        // Load the audio file as AudioClip
        UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip(audioFilePath, AudioType.MPEG);
        yield return audioRequest.SendWebRequest();

        if (audioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(audioRequest.error);
        }
        else
        {
            Debug.Log(audioRequest);
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(audioRequest);

            // Store the audio clip in the Audio list
            Audio[i] = audioClip;
        }
    }

    Debug.Log("PlayList Ready");
}




    // ä�� ����(BMS)�� �о�鿩 ��Ʈ ����
    public void NoteCreate(int musicNum)
    {
        // ���� ����
        FileStream file = new FileStream(Application.streamingAssetsPath + "/BmsFiles/" + bmsFileName[musicNum], FileMode.Open);
        StreamReader testStreamReader = new StreamReader(file);

        // �ճ�Ʈ ������ ���� �Լ� �ʱ�ȭ
        for (int i = 0; i < 4; i++)
        {
            Head_Pos[i] = 0;
            Tail_Pos[i] = 0;
            Head_Find[i] = false;
            Tail_Find[i] = false;
        }

        // �������Ʈ ã�� �� ����� ���� �ʱ�ȭ
        firstFind = false;

        // ���� �о���̱� ����
        while (testStreamReader.EndOfStream == false)
        {
            // �� �� �б�
            value = testStreamReader.ReadLine();


            // �� ���� �ѱ��
            if (value.Length > 1)
            {

                if (value.Substring(1, 3) == "BPM") // BPM �޾Ƶ��̱�
                {
                    bpm = float.Parse(value.Substring(5));
                    GameManager.Instance.NoteSpeed = bpm * 2 / 15; // BPM�� �̿��� ��Ʈ �ӵ� ����
                }
                else if (value.Substring(1, 3) == "KEY") GameManager.Instance.NumofKey = int.Parse(value.Substring(5)); // Ű ���� �о���̱�
                else if (value.Length > 10 && value.Substring(1, 9) == "TimeLimit") GameManager.Instance.TimeLimit = float.Parse(value.Substring(11)); // �� �ð� �о���̱�
                else
                {
                    // ��Ʈ�� ������ ������ ����(5��° �ڸ�) �о���̱�
                    type = value[4] - 48;

                    // ����Ʈ
                    if (type == 1)
                    {
                        type = 0;

                        /*
                         * 5��° �ڸ�
                         * 2 -> F
                         * 4 -> J
                         * 6 -> L
                         * 8 -> D
                        */
                        temp_char = value[5]; // ��Ʈ�� �ڸ��� ������ ����(6��° �ڸ�) �о���̱�

                        // ��Ʈ �ڸ� ����
                        if (GameManager.Instance.NumofKey == 4)
                        {
                            if (temp_char == '6') dfjk = 0;
                            else if (temp_char == '2') dfjk = 1;
                            else if (temp_char == '4') dfjk = 2;
                            else if (temp_char == '8') dfjk = 3;
                        }


                        // 10�� ������ �� == �ϳ��� ä�ο� �ϳ��� ��Ʈ�� ������ ��
                        if (value.Length < 10)
                        {
                            temp_value = value.Substring(7, 2); // ��Ʈ ��ġ�� �о���̱�

                            if (temp_value != "00") // �� ��Ʈ�� �ƴ� ���
                            {

                                pos = float.Parse(value.Substring(1, 3)) * channel_length; // ��Ʈ ��ġ���� �˸°� ��ȯ
#if UNITY_EDITOR
                                // Debug.Log(pos);
#endif
                                // ����� �÷��� ��Ʈ ��ġ == ���� �Ʒ� ��Ʈ ��ġ
                                if (firstPos > pos || !firstFind)
                                {
                                    // �����Ǹ� ã�� �ʱ�
                                    firstPos = pos;
                                    firstFind = true;
                                }

                                // ��Ʈ ����
                                GameObject t_note = Instantiate(Note[type], tfNoteAppear[dfjk].transform.position + Vector3.up * pos, Quaternion.identity);

                                noteHitBase.noteLines[dfjk].note.Add(t_note.GetComponent<Note>());

                                t_note.transform.SetParent(ParentTrans);
                                // ����Ʈ�� ��Ʈ �߰�
                                //GameManager.Instance.AddNoteToList(t_note, dfjk);
                            }


                        }
                        else // �ϳ��� ä�ο� 2�� �̻��� ��Ʈ�� ������ ��
                        {                            
                            if (type == 0)
                            {
                                // �� ä���� ��Ʈ ��
                                value_length = (value.Length - 7) / 2;
                                for (int i = 0; i < ((value.Length - 7) / 2); i++)
                                {
                                    // 2ĭ�� �߶󳻼� 00�� �ƴϸ� ����
                                    if (value.Substring((7 + 2 * i), 2) != "00")
                                    {

                                        pos = float.Parse(value.Substring(1, 3)) * channel_length + (channel_length / value_length) * i; // ��Ʈ ��ġ�� �о���̱�
#if UNITY_EDITOR
                                        // Debug.Log(pos);
#endif
                                        // ����� �÷��� ��Ʈ ��ġ == ���� �Ʒ� ��Ʈ ��ġ
                                        if (firstPos > pos || !firstFind)
                                        {
                                            // �����Ǹ� ã�� �ʱ�
                                            firstPos = pos;
                                            firstFind = true;
                                        }

                                        /////// Srot Note Generate And Push List
                                        GameObject t_note = Instantiate(Note[type], tfNoteAppear[dfjk].transform.position + Vector3.up * pos, Quaternion.identity);

                                        noteHitBase.noteLines[dfjk].note.Add(t_note.GetComponent<Note>());

                                        t_note.transform.SetParent(ParentTrans);

                                        
                                        // ����Ʈ�� ��Ʈ �߰�
                                        //GameManager.Instance.AddNoteToList(t_note, dfjk);
                                    }
                                }
                            }

                        }

                    } // ����Ʈ ��
                    // �ճ�Ʈ
                    else if(type == 5)
                    {                        
                        type = 1;

                        /*
                         * 5��° �ڸ�
                         * 2 -> F
                         * 4 -> J
                         * 6 -> L
                         * 8 -> D
                        */
                        temp_char = value[5];

                        // ��Ʈ �ڸ� ����
                        if (GameManager.Instance.NumofKey == 4)
                        {
                            if (temp_char == '6') dfjk = 0;
                            else if (temp_char == '2') dfjk = 1;
                            else if (temp_char == '4') dfjk = 2;
                            else if (temp_char == '8') dfjk = 3;
                        }


                        // 10�� ������ �� == �ϳ��� ä�ο� �ϳ��� ������ �Ǵ� �������� ������ ��
                        if (value.Length < 10)
                        {
                            temp_value = value.Substring(7, 2);

                            if (temp_value != "00")
                            {

                                pos = float.Parse(value.Substring(1, 3)) * channel_length;
                                
                                

                                // ���� ���� ��ġ ã��
                                if (!Head_Find[dfjk])
                                {
                                    Head_Pos[dfjk] = pos;
                                    Head_Find[dfjk] = true;
#if UNITY_EDITOR
                                    Debug.Log("��� ��ġ[" + dfjk + "] : " + Head_Pos[dfjk]);
#endif
                                }
                                else if (!Tail_Find[dfjk])
                                {
                                    Tail_Pos[dfjk] = pos;
                                    Tail_Find[dfjk] = true;
#if UNITY_EDITOR
                                    Debug.Log("���� ��ġ[" + dfjk + "] : " + Tail_Pos[dfjk]);
#endif
                                }

                                // ���� ���� �� �� ã���� ��� ��Ʈ ����
                                if(Head_Find[dfjk] && Tail_Find[dfjk])
                                {
                                    // ����� �÷��� ��Ʈ ��ġ == ���� �Ʒ� ��Ʈ ��ġ
                                    if (firstPos > Head_Pos[dfjk] || !firstFind)
                                    {
                                        // �����Ǹ� ã�� �ʱ�
                                        firstPos = Head_Pos[dfjk];
                                        firstFind = true;
                                    }

                                    // ��Ʈ ����
                                    GameObject t_note = Instantiate(Note[1], tfNoteAppear[dfjk].transform.position + Vector3.up * Head_Pos[dfjk], Quaternion.identity);












                                    // �ճ�Ʈ ���� ����
                                    Long_length = Tail_Pos[dfjk] - Head_Pos[dfjk];
#if UNITY_EDITOR
                                    Debug.Log("�ճ�Ʈ ����[" + dfjk + "] : " + Long_length);
#endif
                                    LongNote longNote = t_note.GetComponent<LongNote>();
                                    longNote.LengthComp(Long_length);

                                    noteHitBase.noteLines[dfjk].note.Add(longNote.smallRectangle1.GetComponent<Note>());
                                    noteHitBase.noteLines[dfjk].note.Add(longNote.smallRectangle2.GetComponent<Note>());


                                    t_note.transform.SetParent(ParentTrans);

                                    // ����Ʈ�� ��Ʈ �߰�
                                    //GameManager.Instance.AddNoteToList(t_note, dfjk);

                                    // �ճ�Ʈ �ٽ� ã��
                                    Head_Find[dfjk] = false;
                                    Tail_Find[dfjk] = false;

                                    
                                }
                                
                            }


                        }
                        else // �ϳ��� ä�ο� 2�� �̻��� ������ �Ǵ� ������ ������ ��
                        {
                            
                            // �� ä���� ��Ʈ ��
                            value_length = (value.Length - 7) / 2;

                            for (int i = 0; i < ((value.Length - 7) / 2); i++)
                            {
                                // 2ĭ�� �߶󳻼� 00�� �ƴϸ� ����
                                if (value.Substring((7 + 2 * i), 2) != "00")
                                {

                                    pos = float.Parse(value.Substring(1, 3)) * channel_length + (channel_length / value_length) * i;
                                        
                                        

                                    // ���� ���� ��ġ ã��
                                    if (!Head_Find[dfjk])
                                    {
                                        Head_Pos[dfjk] = pos;
                                        Head_Find[dfjk] = true;
#if UNITY_EDITOR
                                        Debug.Log("��� ��ġ[" + dfjk + "] : " + Head_Pos[dfjk]);
#endif
                                    }
                                    else if (!Tail_Find[dfjk])
                                    {
                                        Tail_Pos[dfjk] = pos;
                                        Tail_Find[dfjk] = true;
#if UNITY_EDITOR
                                    Debug.Log("���� ��ġ[" + dfjk + "] : " + Tail_Pos[dfjk]);
#endif
                                    }

                                    // ���� ���� �� �� ã���� ��� ��Ʈ ����
                                    if (Head_Find[dfjk] && Tail_Find[dfjk])
                                    {
                                        // ����� �÷��� ��Ʈ ��ġ == ���� �Ʒ� ��Ʈ ��ġ
                                        if (firstPos > Head_Pos[dfjk] || !firstFind)
                                        {
                                            firstPos = Head_Pos[dfjk];
                                            firstFind = true;
                                        }

                                        // ��Ʈ ����
                                        GameObject t_note = Instantiate(Note[1], tfNoteAppear[dfjk].transform.position + Vector3.up * Head_Pos[dfjk], Quaternion.identity);


                                        // �ճ�Ʈ ���� ����
                                        Long_length = Tail_Pos[dfjk] - Head_Pos[dfjk];
                                        //Debug.Log("�ճ�Ʈ ����[" + dfjk + "] : " + Long_length);
                                        LongNote longNote = t_note.GetComponent<LongNote>();
                                        longNote.LengthComp(Long_length);

                                        noteHitBase.noteLines[dfjk].note.Add(longNote.smallRectangle1.GetComponent<Note>());
                                        noteHitBase.noteLines[dfjk].note.Add(longNote.smallRectangle2.GetComponent<Note>());

                                        t_note.transform.SetParent(ParentTrans);

                                        // ����Ʈ�� ��Ʈ �߰�
                                        //GameManager.Instance.AddNoteToList(t_note, dfjk);

                                        // �ճ�Ʈ �ٽ� ã��
                                        Head_Find[dfjk] = false;
                                        Tail_Find[dfjk] = false;

                                            
                                    }

                                }
                            }
                            

                        }

                    } // �ճ�Ʈ ��




                }



            }

            
            if (testStreamReader.EndOfStream) break;
        }

        // ���� �ݱ�
        testStreamReader.Close();

        // ����� ��� ��Ʈ ����
#if UNITY_EDITOR
        Debug.Log(firstPos);
#endif
        
        GameObject audioNote = Instantiate(AudioNote, tfNoteAppear[0].transform.position + Vector3.up * firstPos + Vector3.forward * 3.5f, Quaternion.identity);
        audioNote.transform.SetParent(ParentTrans);
        //Debug.Log(Audio[musicNum].name);

        //tempClip = Audio[musicNum];
        if (audioNote.activeSelf)
        {
            audioNote.GetComponent<AudioStart>().BmsReader = this;
            audioNote.GetComponent<AudioStart>().AudioNum = musicNum;
        }



        // ���� ���� �ʱ�ȭ
        GameManager.Instance.ScoreManager = 0;
        // �޺� �ʱ�ȭ
        GameManager.Instance.ComboManager = 0;
        GameManager.Instance.BestCombo = 0;

        // ���� ȭ�� ���
        GameManager.Instance.OnSelecting = false;
        // ���� ����
        GameManager.Instance.MusicPlaying = true;
        // �� ���� �Ұ�
        GameManager.Instance.MusicSelectAble = false;
        // �� ���� �ʱ�ȭ
        GameManager.Instance.MusicNum = 0;


        return;
    }



}
