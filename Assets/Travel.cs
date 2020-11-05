using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Travel : MonoBehaviour
{
    // This when attatched to an object, causes a scene change based on the number where the 0 is
    void OnTriggerEnter (Collider other)
    {
        SceneManager.LoadScene(2);
    }


}
