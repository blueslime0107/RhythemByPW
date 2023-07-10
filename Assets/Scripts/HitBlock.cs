using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBlock : MonoBehaviour
{
    [SerializeField] NoteHitBase noteHitBase;
    [SerializeField] int lineIndex;
    [SerializeField] KeyCode KeyButton;

    // [SerializeField] GameObject Hitblock;
    // //[SerializeField] GameObject UpHitblock;
    // //[SerializeField] GameObject DownHitblock;
    [SerializeField] GameObject VisibleHitBlock;

    public bool Pressed = false;
    public int count;

    private void Update()
    {

        if (GameManager.Instance.MusicPlaying)
        {
        if(Input.GetKey(KeyButton)){
            if(count <= 0){ // GetKeyDown
                noteHitBase.checkHit(lineIndex);
            }
            count++;
            // GetKey
            VisibleHitBlock.SetActive(true);

        }
        else{
            if(count > 0){ // GetKeyUp
                VisibleHitBlock.SetActive(false);
                noteHitBase.keyReleased(lineIndex);
                count = 0;
            }
            else{
            }
        }

        }
    }
}
