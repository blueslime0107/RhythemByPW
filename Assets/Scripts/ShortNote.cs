using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortNote : MonoBehaviour
{

    public float ShortMoveSpeed;
    

    private void Start()
    {
        ShortMoveSpeed = GameManager.Instance.NoteSpeed;
    }

    void Update()
    {
        if (GameManager.Instance.MusicPlaying)
        {
            transform.position += Vector3.down * ShortMoveSpeed * Time.deltaTime;
        }
        if (GameManager.Instance.OnSelecting) Destroy(gameObject);

    }

    


}
