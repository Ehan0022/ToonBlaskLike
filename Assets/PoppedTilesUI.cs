using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoppedTilesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;
    void Start()
    {
        tmp.text = "Popped tiles: 0"; 
        
    }

    // Update is called once per frame
    
}
