using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteLine{
    public Transform hit;
    public List<Note> note = new List<Note>();
    public Note longNote;
    public bool isLongPressing;
    public float longEffectDelay;
}

public class NoteHitBase : MonoBehaviour
{
    [SerializeField] GameObject HitEffect;
    public List<GameObject> HitEffects = new List<GameObject>();
    public int hitEffectLimit;

    [Space(10f)]

    public float noTim;
    public float perfect;
    public float good;
    public float cool;
    public float bad;

    public NoteLine[] noteLines = new NoteLine[4];

    Note noteObj;

    void Start(){
        for(int i=0;i<hitEffectLimit;i++){
            GameObject obj = Instantiate(HitEffect);
            obj.SetActive(false);
            HitEffects.Add(obj);
        }
    }

    void Update(){
        if (!GameManager.Instance.MusicPlaying){return;}
        foreach(NoteLine noteline in noteLines){
            if(noteline.isLongPressing){
                noteline.longEffectDelay += Time.deltaTime;
                if(noteline.longEffectDelay > 0.2f){
                PlayEffect(noteline.longNote.transform);
                noteline.longEffectDelay = 0;
                }
                GameManager.Instance.ScoreManager += 10;
            }
            if(noteline.note.Count <= 0){continue;}
            if(noteline.note[0].gameObject.transform.position.y - noteline.hit.position.y <= perfect){
                if(noteline.note[0].gameObject.CompareTag("LongNoteTail") && noteline.isLongPressing){
                    Destroy(noteline.longNote.transform.parent.gameObject);
                    noteline.note.RemoveAt(0);
                    noteline.isLongPressing = false;
                    return;
                }
            }
            if(noteline.note[0].gameObject.transform.position.y - noteline.hit.position.y <= bad){
                if(noteline.note[0].gameObject.CompareTag("LongNoteHead")){
                    noteline.longNote = noteline.note[0]; 
                    noteline.note.RemoveAt(0);
                    noteline.note.RemoveAt(0);
                }
                else if(noteline.note[0].gameObject.CompareTag("LongNoteTail")){
                    noteline.isLongPressing = false;
                    Destroy(noteline.longNote.transform.parent.gameObject);
                    noteline.note.RemoveAt(0);
                    
                }
                else{
                Destroy(noteline.note[0].gameObject); 
                noteline.note.RemoveAt(0); 
                GameManager.Instance.ComboManager -= GameManager.Instance.ComboManager;
                GameManager.Instance.TimingOutput(5);
                }
            }
        
        }

    }


    public void checkHit(int lineIndex){
        if(noteLines[lineIndex].note.Count <= 0){return;}
        
        float hitY =0;
        float noteY =0;
        ////////// get line's first note Y and hit Y
        hitY = noteLines[lineIndex].hit.position.y;
        noteY = noteLines[lineIndex].note[0].gameObject.transform.position.y;
        noteObj = noteLines[lineIndex].note[0];

        float dist = noteY - hitY;

        /// if dist is over 2 don't destroy the note
        if(dist > noTim){return;}
        // 
        if(dist > perfect){
            GameManager.Instance.TimingOutput(1);
            GameManager.Instance.ScoreManager += 100;
        }else if(dist > good){
            GameManager.Instance.TimingOutput(2);
            GameManager.Instance.ScoreManager += 75;
        }else if(dist > cool){
            GameManager.Instance.TimingOutput(3);
            GameManager.Instance.ScoreManager += 55;
        }else if(dist > bad){
            GameManager.Instance.TimingOutput(4);
            GameManager.Instance.ScoreManager += 40;
        }
        GameManager.Instance.ComboManager++;

        if(noteObj.CompareTag("LongNoteHead")){
            noteObj.MoveSpeed = 0;
            noteObj.gameObject.transform.position =  noteLines[lineIndex].hit.transform.position;
            StartCoroutine(noteObj.testRect.startSrink());
            noteLines[lineIndex].longNote = noteObj;

            noteLines[lineIndex].note.Remove(noteObj);
            noteLines[lineIndex].isLongPressing = true;
            PlayEffect(noteObj.transform);
            return;
        }
        



        PlayEffect(noteObj.transform);
        Destroy(noteObj.gameObject);
        noteLines[lineIndex].note.Remove(noteObj);
    }
    
    
    
    
    public void keyReleased(int lineIndex){
        
        if(noteLines[lineIndex].isLongPressing){
            noteLines[lineIndex].isLongPressing = false;
            StopCoroutine(noteLines[lineIndex].longNote.testRect.startSrink());
        }
    }







    public void Reset(){
        noteLines[0].note = new List<Note>();
        noteLines[1].note = new List<Note>();
        noteLines[2].note = new List<Note>();
        noteLines[3].note = new List<Note>();
    }
    public void PlayEffect(Transform targetTransform)
    {
        // 가장 앞에 있는 비활성화된 파티클 오브젝트 찾기
        for(int i=0;i<HitEffects.Count;i++){
            if(!HitEffects[i].activeSelf){
                HitEffects[i].transform.position = targetTransform.position;
                HitEffects[i].SetActive(true);
                StartCoroutine(DeactivateParticle(HitEffects[i]));
                return;
            }
        }
    }

    private IEnumerator DeactivateParticle(GameObject particle)
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        particle.SetActive(false);
    }

}
