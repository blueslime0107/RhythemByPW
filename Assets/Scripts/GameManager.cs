using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public NoteHitBase noteHitBase;
    // �̱��� ��ü
    public static GameManager Instance = null;


    // ī�޶� ��ü
    [SerializeField] GameObject Camera = null;
    // ī�޶� ��ġ
    [SerializeField] GameObject PlayField;
    [SerializeField] GameObject SelectField;


    // ��Ʈ ����Ʈ
    public List<GameObject> D_note = new();
    public List<GameObject> F_note = new();
    public List<GameObject> J_note = new();
    public List<GameObject> K_note = new();

    // ��Ʈ ����Ʈ
    

    // ��Ʈ �ӵ�
    public float NoteSpeed = 4;

    // Ÿ��Ʋ ȭ���ΰ�?
    public bool OnTitle = false;
    // ���� ȭ���ΰ�?
    public bool OnSelecting = false;
    // �� ���� ���� ����
    public bool MusicSelectAble = true;
    // �� ��ȣ
    public int MusicNum;
    // Ű ����
    public int NumofKey = 4;
    // �������� ���� ��
    public bool MusicPlaying = false;


    // ����â
    [SerializeField] GameObject OptionMenu;

    // ���� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI CurrentScoreUI;
    [SerializeField] TextMeshProUGUI BestScoreUI;
    [SerializeField] TextMeshProUGUI ResultScoreUI;
    // �޺� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI CurrentComboUI;
    [SerializeField] TextMeshProUGUI BestComboUI;
    // ���� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI TimingUI;
    private string[] Timings = new string[5];
    private int[] TimingCount = new int[5];
    [SerializeField] TextMeshProUGUI[] TimingCountUI;


    // ���� Ÿ��Ʋ
    [SerializeField] GameObject Title;
    // Ÿ��Ʋ -> �� ���� ȭ�� �̵� ��ư
    [SerializeField] GameObject TitleToSelectingButton;

    // �÷��� -> �� ���� ȭ�� �̵� ��ư
    [SerializeField] GameObject PlayToSelectingButton;
    // �� ���� �� ����
    public bool SelectingMusic;
    // �� ���� â
    [SerializeField] GameObject MusicSelectScroll;
    // �� ���� -> Ÿ��Ʋ ȭ�� �̵� ��ư
    [SerializeField] GameObject SelectingToTitleButton;

    // ������
    [SerializeField] GameObject MakerText;

    // ���� ���� ��ư
    [SerializeField] GameObject GameQuitButton;

    // �÷��� ���� ��ư
    public bool PlayQuitButtonActive = false;

    // �̱��� ����
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        MusicPlaying = false;

        MusicNum = -1;
    }

    // ���� ���� -> ���� ȭ��
    private void Start()
    {

        // Ÿ��Ʋ ȭ������ ����
        OnTitle = true;
        OnSelecting = false;
        NumofKey = 8;
        CameraMove(true);

        

        // �÷��� ���� ��ư ��Ȱ��ȭ
        PlayQuitButtonActive = false;

        

        // ��ư �� ��ũ�� Ȱ��ȭ�� ��Ȱ��ȭ
        // Ȱ��ȭ
        Title.SetActive(true); // Ÿ��Ʋ ȭ�� ����
        TitleToSelectingButton.SetActive(true); // Ÿ��Ʋ ȭ�� ����
        MakerText.SetActive(true); // Ÿ��Ʋ, �� ���� ȭ�� ���� ����
        GameQuitButton.SetActive(true); // Ÿ��Ʋ, �� ���� ȭ�� ���� ����
        // ��Ȱ��ȭ
        MusicSelectScroll.SetActive(false); // �� ���� ȭ��
        SelectingToTitleButton.SetActive(false); // �� ���� ȭ��
        PlayToSelectingButton.SetActive(false); // �÷��� ��� ȭ��

        // UI �ʱ�ȭ
        CurrentScoreUI.text = ""; // �÷��� ȭ��
        BestScoreUI.text = "";// �÷��� ��� ȭ��
        ResultScoreUI.text = "";// �÷��� ��� ȭ��
        CurrentComboUI.text = ""; // �÷��� ȭ��
        BestComboUI.text = "";// �÷��� ��� ȭ��
        TimingUI.text = ""; // �÷��� ȭ��

        // �÷��� ��� ȭ��
        for (int i = 0; i < TimingCountUI.Length; i++)
        {
            TimingCountUI[i].text = "";
        }

        // Ÿ�̹� Ƚ�� �ʱ�ȭ
        for (int i = 0; i < TimingCount.Length; i++)
        {
            TimingCount[i] = 0; // �÷��� ȭ��
        }
        Timings[0] = "Perfect ";
        Timings[1] = "Good ";
        Timings[2] = "Cool ";
        Timings[3] = "Bad ";
        Timings[4] = "Miss ";

    }
    /*
    // ����Ʈ�� ��Ʈ �߰�
    public void AddNoteToList(GameObject note, int index)
    {
        switch (index)
        {
            case 0:
                D_note.Add(note);
                break;
            case 1:
                F_note.Add(note);
                break;
            case 2:
                J_note.Add(note);
                break;
            case 3:
                K_note.Add(note);
                break;
        }
        return;
    }

    IEnumerator RemoveNoteFromList()
    {
        if (D_note.Count>0) if (D_note[0] == null || D_note[0].activeSelf == false) D_note.RemoveAt(0);
        if (F_note.Count > 0) if (F_note[0] == null || F_note[0].activeSelf == false) F_note.RemoveAt(0);
        if (J_note.Count > 0) if (J_note[0] == null || J_note[0].activeSelf == false) J_note.RemoveAt(0);
        if (K_note.Count > 0) if (K_note[0] == null || K_note[0].activeSelf == false) K_note.RemoveAt(0);
        yield return new WaitForSeconds(Time.deltaTime);
    }
    */
    

    /* UI ���� */

    // ���� ����
    int CurrentScore;
    public List<int> BestScore = new List<int>();
    // ���� GetSet
    public int ScoreManager
    {
        get
        {
            return CurrentScore;
        }
        set
        {
            CurrentScore = value;
            CurrentScoreUI.text = "����\n" + CurrentScore;

            if(CurrentScore > BestScore[MusicNum])
            {
                BestScore[MusicNum] = CurrentScore;
            }
        }
    }

    // �޺� ����
    private int CurrentCombo;
    public int BestCombo = 0;
    // �޺� GetSet
    public int ComboManager
    {
        get
        {
            return CurrentCombo;
        }
        set
        {
            CurrentCombo = value;
            CurrentComboUI.text = "" + CurrentCombo;

            if(CurrentCombo > BestCombo)
            {
                BestCombo = CurrentCombo;
            }
        }
    }

    // ���� ���
    public void TimingOutput(int timing)
    {

        switch (timing)
        {
            case 1:
                TimingUI.text = "Perfect";
                break;
            case 2:
                TimingUI.text = "Good";
                break;
            case 3:
                TimingUI.text = "Cool";
                break;
            case 4:
                TimingUI.text = "Bad";
                break;
             case 5:
                TimingUI.text = "Miss";
                break;
        }
        TimingCount[(timing - 1)]++;
        if(TimingUI.text == "Miss"){return;}
        StopCoroutine(BoomTiming());
        TimingUI.transform.localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine(BoomTiming());

        return;
    }

    private IEnumerator BoomTiming()
    {
        TimingUI.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        yield return new WaitForSeconds(0.1f);
        TimingUI.transform.localScale = new Vector3(1f, 1f, 1f);
    }


    /* ���� ���� */

    // �ð� ����
    private double CurrentTime = 0d;
    public double TimeLimit = 0d;

    private void Update()
    {
        // Ÿ��Ʋ ȭ��
        if (OnTitle)
        {
            PlayToSelectingButton.SetActive(false);
        }
        // ���� ȭ��
        else if (OnSelecting)
        {
            PlayToSelectingButton.SetActive(false);
        }
        // �뷡 ���� ��
        else if (MusicPlaying)
        {
            CurrentTime += Time.deltaTime;
            //StartCoroutine(RemoveNoteFromList());
            if (CurrentTime >= (TimeLimit + 3d))
            {
                // �뷡 ����
                MusicPlaying = false;
                CurrentTime = 0;
                StartCoroutine(ShowPlayResult());
            }
        }

    }


    // ��� ����
    IEnumerator ShowPlayResult()
    {
        
        // �����޺� �� �������� UI ����
        CurrentComboUI.text = "";
        CurrentScoreUI.text = "";
        TimingUI.text = "";

        PlayQuitButtonActive = false;

        // �������, �ְ�����, �ְ��޺� ǥ��

        BestScoreUI.text = "�ְ� ���� : " + BestScore[MusicNum];

        int i = 0;
        while (i < CurrentScore)
        {
            ResultScoreUI.text = "" + i;
            i += 1231;
            yield return new WaitForSeconds(0.01f);
        }
        ResultScoreUI.text = "" + CurrentScore;

        i = 0;
        while(i < BestCombo)
        {
            BestComboUI.text = "" + i;
            i++;
            yield return new WaitForSeconds(0.01f);
        }
        BestComboUI.text = "�ְ� �޺� : " + BestCombo;

        // Ÿ�̹� Ƚ�� ǥ��
        for(int j = 0; j < TimingCount.Length; j++)
        {
            TimingCountUI[j].text = "" + Timings[j] + TimingCount[j];
        }
        


        // 2�� �� ����ȭ�� �̵� ��ư Ȱ��ȭ
        yield return new WaitForSeconds(2f);
        PlayToSelectingButton.SetActive(true);
    }

    // Ÿ��Ʋ ȭ������ �̵� - ��ư �ݹ��
    public void GoToTitle()
    {
        OnTitle = true;
        OnSelecting = false;

        // Ÿ��Ʋ Ȱ��ȭ
        Title.SetActive(true);
        // Ÿ��Ʋ -> �� ���� ��ư Ȱ��ȭ
        TitleToSelectingButton.SetActive(true);

        // �� ���� ��ũ�� ��Ȱ��ȭ
        MusicSelectScroll.SetActive(false);
        // �� ���� -> Ÿ��Ʋ ��ư ��Ȱ��ȭ
        SelectingToTitleButton.SetActive(false);

        CameraMove(true);
    }

    // ���� ȭ������ �̵� - ��ư �ݹ��
    public void GoToSelecting()
    {
        // �ð����� �ʱ�ȭ
        TimeLimit = 0;

        noteHitBase.Reset();

        // �� ���� ȭ������
        OnSelecting = true;
        OnTitle = false;

        // �÷��� -> �� ���� ��ư ��Ȱ��ȭ
        PlayToSelectingButton.SetActive(false);

        // �������, �ְ�����, �ְ��޺� UI ����
        ResultScoreUI.text = "";
        BestScoreUI.text = "";
        BestComboUI.text = "";

        // Ÿ�̹� Ƚ�� �ʱ�ȭ
        for(int i = 0; i < TimingCount.Length; i++)
        {
            TimingCountUI[i].text = "";
            TimingCount[i] = 0;
        }


        // Ÿ��Ʋ ��Ȱ��ȭ
        Title.SetActive(false);
        // Ÿ��Ʋ -> �� ���� ��ư ��Ȱ��ȭ
        TitleToSelectingButton.SetActive(false);

        // ������ �ؽ�Ʈ Ȱ��ȭ
        MakerText.SetActive(true);

        // �� ���� ��ũ�� Ȱ��ȭ
        MusicSelectScroll.SetActive(true);
        // �� ���� -> Ÿ��Ʋ ��ư Ȱ��ȭ
        SelectingToTitleButton.SetActive(true);
        // ���� ���� ��ư Ȱ��ȭ
        GameQuitButton.SetActive(true);
        // �÷��� ���� ��ư ��Ȱ��ȭ
        PlayQuitButtonActive = false;
        //PlayQuitButton.SetActive(false);


        // ȭ�� ��ȯ
        NumofKey = 8;
        CameraMove(true);

        // ��Ʈ ����Ʈ ����
        for (int i = D_note.Count - 1; i >= 0; i--)
        {
            D_note.RemoveAt(0);
        }
        for (int i = F_note.Count - 1; i >= 0; i--)
        {
            F_note.RemoveAt(0);
        }
        for (int i = J_note.Count - 1; i >= 0; i--)
        {
            J_note.RemoveAt(0);
        }
        for (int i = K_note.Count - 1; i >= 0; i--)
        {
            K_note.RemoveAt(0);
        }


        // �� ���� ����
        MusicSelectAble = true;

        return;
    }


    public void GoPlay()
    {
        // �� ���� ��ũ�� ��Ȱ��ȭ
        MusicSelectScroll.SetActive(false);
        // �� ���� -> Ÿ��Ʋ ��ư ��Ȱ��ȭ
        SelectingToTitleButton.SetActive(false);
        // ���� ���� ��ư ��Ȱ��ȭ
        GameQuitButton.SetActive(false);
        // ������ �ؽ�Ʈ ��Ȱ��ȭ
        MakerText.SetActive(false);
        // �÷��� ���� ��ư Ȱ��ȭ
        PlayQuitButtonActive = true;
        CameraMove(false);

        return;
    }

    // ī�޸� �̵�
    public void CameraMove(bool GoMain)
    {
        // true�� ����ȭ��
        if (GoMain)
        {
            Camera.transform.position = SelectField.transform.position + new Vector3(0, 4.5f, -9.5f);
        }
        // false�� �÷���ȭ��
        else
        {
            Camera.transform.position = PlayField.transform.position + new Vector3(0, 4.5f, -9.5f);
        }
        return;
    }


    // ���� ���� - ��ư �ݹ��
    public void QuitGame()
    {
        
#if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ����
#else
        Application.Quit();  // ���ø����̼ǿ��� ���� ����
#endif
        

    }

    // �÷��� ���� - ��ư �ݹ��
    public void QuitPlay()
    {
        // �뷡 ����
        MusicPlaying = false;
        TimeLimit = 0;
        CurrentTime = 0;

        // �����޺� �� �������� UI ����
        CurrentComboUI.text = "";
        CurrentScoreUI.text = "";
        TimingUI.text = "";

        // ���� ȭ�� ������
        OptionMenu.SetActive(false);

        GoToSelecting();
    }





}
