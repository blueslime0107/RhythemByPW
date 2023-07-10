// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class LNHead : MonoBehaviour
// {
     
//     LongNote LN;
    
//     void OnEnable()
//     {
//         LN = GetComponentInParent<LongNote>();
//     }

//     // �浹�ϸ� �پ�� �غ�
//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("HitBlock"))
//         {
//             LN.CanBePressed = false;
//         }
//         other.GetComponent<Hit>().uncollision = true;
//     }
// }
