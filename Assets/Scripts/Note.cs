using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float MoveSpeed;
    public LongNote testRect;
    

    private void Start()
    {
        MoveSpeed = GameManager.Instance.NoteSpeed;
    }

    void Update()
    {
        if (GameManager.Instance.MusicPlaying)
        {
            transform.position += Vector3.down * MoveSpeed * Time.deltaTime;
        }
        if (GameManager.Instance.OnSelecting) {
            if(gameObject.CompareTag("LongNoteTail")){
                Destroy(gameObject.transform.parent.gameObject);
            }
            Destroy(gameObject);
            
        }
    }
}
