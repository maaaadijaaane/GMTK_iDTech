using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class AudioTrigger : MonoBehaviour
{
    public GameObject levelObj;
    public GameObject newObj;
    public UnityEvent onTriggerEnter;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(levelObj);
            newObj.SetActive(true);
            onTriggerEnter?.Invoke();
        }
    }
}
