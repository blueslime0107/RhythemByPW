// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Hit : MonoBehaviour
// {


//     [SerializeField] double TimeFlow = 0;
//     public bool uncollision = true;
//     double Notespeed;

//     public  

//     LongNote LongNoteHit;

//     // ����Ʈ
//     [SerializeField] GameObject HitEffect;
//     GameObject CollObj;
//     List<GameObject> EffectPool;

//     private void Start()
//     {
//         EffectPool = new();
//         for(int i = 0; i < 3; i++)
//         {           
//             EffectPool.Add(Instantiate(HitEffect, new Vector3(-6,0,0), Quaternion.identity));
//         }
//     }

//     private void OnEnable()
//     {
//         Notespeed = (double)GameManager.Instance.NoteSpeed;
//         TimeFlow = 0;
//         uncollision = true;
//     }
    
//     private void Update()
//     {
//         if (uncollision)
//         {
//             TimeFlow += Time.deltaTime;
//             if (TimeFlow >= 0.5f)
//             {
//                 //StopAllCoroutines();
//                 gameObject.SetActive(false);
//             }
//         }
        

//     }

//     // ��Ʈ�� ��Ʈ���� �Ÿ�
//     double distance;
//     double resultdistance;
//     void myCollide()
//     {
//         // ���� �ڽ� �� ��� ��ü �޾ƿ���
//         Collider[] colls = Physics.OverlapBox(transform.position + new Vector3(0,0,-2.5f), transform.lossyScale, Quaternion.identity);
//         //Debug.Log("Box Size : " + transform.lossyScale);
//         resultdistance = 100;
//         //GameObject CollisionObject = null;
//         double tempdistance;
//         for (int i = 0; i < colls.Length; i++)
//         {
//             if (colls[i].gameObject != gameObject)
//             {
//                 // �Ÿ� �޾ƿ���
//                 tempdistance = colls[i].transform.position.y + 0.5f/*- transform.position.y*/;

//                 // �ִ� �Ÿ��� ���� ��Ʈ�� �Ÿ� ��
//                 if (tempdistance < resultdistance)
//                 {
//                     // ���� ��Ʈ�� �Ÿ��� �� �۴ٸ�
//                     // �ִ� �Ÿ� �� ��ü ����
//                     resultdistance = tempdistance;
//                     CollObj = colls[i].gameObject;
//                 }
//             }
            
//         }


//         // �ִ� �Ÿ��� ��Ʈ ��ü ��ȯ
//         distance = resultdistance;
//         //CollObj = CollisionObject;
//         /*
// #if UNITY_EDITOR
//         if (CollisionObject != null) Debug.Log("Distance : " + distance + "  Object : " + CollisionObject.transform.position);
//         else Debug.Log("Non GameObject");
// #endif
//         */
//     }
//     /*
//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.blue;
//         Gizmos.DrawWireCube(transform.position + new Vector3(0, 0, -2.5f), transform.lossyScale);
//     }
//     */
    


//     private void OnTriggerEnter(Collider collision)
//     {
//         if (!uncollision) return;

//         myCollide();

//         // ����Ʈ ����
//         if (CollObj.CompareTag("ShortNote") /*&& uncollision*/)
//         {
// #if UNITY_EDITOR
//             Debug.Log("����Ʈ ��Ʈ");
// #endif
//             uncollision = false;
//             //StopAllCoroutines();
            
//             //distance = collision.transform.position.y - transform.position.y;
//             if (distance <= (Notespeed / 100 * 18)
//                 && distance >= (Notespeed / 100 * -9))
//             {
//                 CollObj.GetComponent<ShortNote>().ShortMoveSpeed = 0;
//                 if (distance <= (Notespeed / 100 * 15)
//                     && distance >= (Notespeed / 1000 * -75))
//                 {
//                     if (distance <= (Notespeed / 100 * 10)
//                         && distance >= (Notespeed / 100 * -6))
//                     {
//                         if (distance <= (Notespeed / 100 * 6)
//                             && distance >= (Notespeed / 100 * -4))
//                         {
//                             // ����Ʈ
//                             GameManager.Instance.TimingOutput(1);
//                             GameManager.Instance.ScoreManager += 100;
//                         }
//                         else
//                         {
//                             // ��
//                             GameManager.Instance.TimingOutput(2);
//                             GameManager.Instance.ScoreManager += 75;
//                         }

//                     }
//                     else
//                     {
//                         // ��
//                         GameManager.Instance.TimingOutput(3);
//                         GameManager.Instance.ScoreManager += 55;
//                     }
//                 }
//                 else
//                 {
//                     // ���
//                     GameManager.Instance.TimingOutput(4);
//                     GameManager.Instance.ScoreManager += 40;
//                 }

//                 // �޺� ����
//                 GameManager.Instance.ComboManager++;

//                 // ����Ʈ ����
//                 GameObject Effect = Instantiate(HitEffect, CollObj.transform.position, Quaternion.identity);
//                 /*
//                 if (EffectPool.Count > 0)
//                 {
//                     StartCoroutine(Effecting(EffectPool[0]));                                        
//                 }
//                 */

//                 Destroy(CollObj.gameObject);
//                 //CollObj.gameObject.SetActive(false);
//                 gameObject.SetActive(false);
//             }



//         }
//         else if (CollObj.CompareTag("LongNote") /*&& uncollision*/)
//         {
// #if UNITY_EDITOR
//             Debug.Log("�ճ�Ʈ ��Ʈ");
// #endif
//             uncollision = false;
//             TimeFlow = 0;
//             //StopAllCoroutines();

//             //GameObject CollObj = myCollide();
//             //double distance = CollObj.transform.position.y - transform.position.y;
//             if (distance <= (Notespeed / 100 * 18)
//                 && distance >= (Notespeed / 100 * -9))
//             {
                
//                 LongNoteHit = CollObj.GetComponentInParent<LongNote>();
//                 LongNoteHit.LongMoveSpeed = 0;
//                 if (distance <= (Notespeed / 100 * 15)
//                     && distance >= (Notespeed / 1000 * -75))
//                 {
//                     if (distance <= (Notespeed / 100 * 10)
//                         && distance >= (Notespeed / 100 * -6))
//                     {
//                         if (distance <= (Notespeed / 100 * 6)
//                             && distance >= (Notespeed / 100 * -4))
//                         {
//                             // ����Ʈ
//                             LongNoteHit.timing = 1;
//                             GameManager.Instance.ScoreManager += 100;
//                         }
//                         else
//                         {
//                             // ��
//                             LongNoteHit.timing = 2;
//                             GameManager.Instance.ScoreManager += 75;
//                         }

//                     }
//                     else
//                     {
//                         // ��
//                         LongNoteHit.timing = 3;
//                         GameManager.Instance.ScoreManager += 55;
//                     }
//                 }
//                 else
//                 {
//                     // ���
//                     LongNoteHit.timing = 4;
//                     GameManager.Instance.ScoreManager += 40;
//                 }
//                 GameManager.Instance.TimingOutput(LongNoteHit.timing);

//                 // �޺� ����
//                 GameManager.Instance.ComboManager++;

//                 LongNoteHit.Pressed = true;
//                 LongNoteHit.CanBePressed = true;

//                 // ����Ʈ ����
//                 GameObject Effect = Instantiate(HitEffect, CollObj.transform.position, Quaternion.identity);
//                 /*
//                 if (EffectPool.Count > 0)
//                 {
//                     StartCoroutine(Effecting(EffectPool[0]));
//                 }
//                 */
//             }



//         }
//     }


//     private void OnTriggerExit(Collider other)
//     {
//         gameObject.SetActive(false);
//     }
//     /*
//     // ����Ʈ ȿ��
//     IEnumerator Effecting(GameObject effect)
//     {
//         // ��Ÿ����
//         effect.SetActive(true);
//         // ��ġ �ű��
//         effect.transform.position = CollObj.transform.position;
//         // �����ϱ�
//         EffectPool.Remove(effect);
//         // 1�� ��ٸ� �� 
//         yield return new WaitForSeconds(1f);
//         // �ٽ� ����ֱ�
//         EffectPool.Add(effect);
//     }
//     */
//     /*
//     public IEnumerator ExpnadHit()
//     {
//         for(int i = 0; i < 4; i++)
//         {            
//             switch (i)
//             {
//                 case 0:
//                     // ����Ʈ
//                     transform.localScale = new Vector3(transform.localScale.x, (float)(Notespeed / 100 * 6),transform.localScale.z);
//                     Debug.Log("Hitblock Y" + transform.localScale.y);
//                     break;
//                 case 1:
//                     // ��
//                     transform.localScale = new Vector3(transform.localScale.x, (float)(Notespeed / 100 * 10), transform.localScale.z);
//                     Debug.Log("Hitblock Y" + transform.localScale.y);
//                     break;
//                 case 2:
//                     // ��
//                     transform.localScale = new Vector3(transform.localScale.x, (float)(Notespeed / 100 * 15), transform.localScale.z);
//                     Debug.Log("Hitblock Y" + transform.localScale.y);
//                     break;
//                 case 3:
//                     // ���
//                     transform.localScale = new Vector3(transform.localScale.x, (float)(Notespeed / 100 * 18), transform.localScale.z);
//                     Debug.Log("Hitblock Y" + transform.localScale.y);
//                     break;
//             }
//             yield return new WaitForSeconds(0.003f);
//         }
        
//     }
//     */

// }
