using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] private float BossHP;
    [SerializeField] private GameObject boss;
    public TextMeshProUGUI hPTrack;
    
    void Update()
    {
        hPTrack.text ="KING SLIMLY HP : " + BossHP.ToString();
        if (BossHP == 0) 
        {
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            BossHP--;
        }
    }
}
