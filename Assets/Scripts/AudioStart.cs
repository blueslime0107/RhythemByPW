using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio;
using NLayer;

public class AudioStart : MonoBehaviour
{
    // ����� ������Ʈ
    [SerializeField] AudioSource AudioPlayer;
    public AudioClip PlaySong = null;

    // �̵��ӵ�
    float MoveSpeed;

    // �Ͻ������ΰ�?
    bool Paused = false;


    public BmsRead BmsReader;
    public int AudioNum;

    private void Start()
    {
        MoveSpeed = GameManager.Instance.NoteSpeed;
        Paused = false;
    }

    void Update()
    {
        // ����� Ŭ���� ���� ���
        if(PlaySong == null)
        {
            PlaySong = BmsReader.Audio[AudioNum];
        }

        // ���� ���� ���� ���
        if (GameManager.Instance.MusicPlaying)
        {
            // �����δ�
            transform.position += Vector3.down * MoveSpeed * Time.deltaTime;
        }
        // �� ���� ȭ�鿡 ���� ��ü �ı�
        if (GameManager.Instance.OnSelecting) Destroy(gameObject);

        // �� ������ ����µ� ������� ������� ���
        if (GameManager.Instance.MusicPlaying == false && AudioPlayer.isPlaying == true) 
        { 
            AudioPlayer.Pause();
            Paused = true;
#if UNITY_EDITOR
            Debug.Log("Audio Pause");
#endif
        }
        // ���� ���� ���ε� ������� ��� ���� �ƴ� ���
        else if(GameManager.Instance.MusicPlaying == true && AudioPlayer.isPlaying == false && Paused == true) 
        { 
            AudioPlayer.UnPause();
            Paused = false;
#if UNITY_EDITOR
            Debug.Log("Audio Unpause");
#endif
        }
    
        if(transform.position.y <= -0 && MoveSpeed != 0) PlayerAutio();

    
    
    
    }

    // ����� ��� ����
    private void PlayerAutio()
    {
        AudioPlayer.PlayOneShot(PlaySong);
#if UNITY_EDITOR
        Debug.Log("Audio Play");
#endif
        transform.position += new Vector3(0, -2, 0);
        MoveSpeed = 0;
    }

}
