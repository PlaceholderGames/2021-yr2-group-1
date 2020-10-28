using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Travel : MonoBehaviour
{
    void OnTriggerEnter (Collider other)
    {
        SceneManager.LoadScene(0);
    }


}
