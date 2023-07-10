// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class NoteDisappear : MonoBehaviour
// {

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("ShortNote") || other.CompareTag("LongNote"))
//         {
//             Destroy(other.gameObject);
//             GameManager.Instance.ComboManager -= GameManager.Instance.ComboManager;
//             GameManager.Instance.TimingOutput(5);
//         }
//     }


// }