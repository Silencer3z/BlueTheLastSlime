using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float playerHP = 7f;
    private float MaxHP = 9f;
    [SerializeField] private float speed = 8f;
    [SerializeField] private GameObject spawnPoint;
    private float horizontal;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject restartGame;
    [SerializeField] private GameObject deathCam;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject bossGauge;

    private GameObject[] enemy;

    public TextMeshProUGUI LifeUI;
    public TextMeshProUGUI EnemyRemainUI;

    void Start()
    {
        
    }

    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        
        LifeUI.text = "Life : " + playerHP.ToString();
        EnemyRemainUI.text = "Remain : " + enemy.Length.ToString();

        
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        if (playerHP == 0) 
        {
            restartGame.SetActive(true);
            uiCanvas.SetActive(false);
            deathCam.SetActive(true);
            Destroy(this.gameObject);
            
        }
        if (enemy.Length == 0)
        {
            bossGauge.SetActive(false);
            uiCanvas.SetActive(false);
            winCanvas.SetActive(true);
        }

        if (enemy.Length ==1)
        {
            bossGauge.SetActive(true);
        }

        if (enemy == null)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerHP = playerHP - 1;
        }

        if (other.CompareTag("Border"))
        {
            playerHP = playerHP - 1;
            this.gameObject.transform.position = spawnPoint.transform.position;
        }

        if (other.CompareTag("Heal"))
        {
            if (playerHP < MaxHP)
            {
                Destroy(other.gameObject);
                playerHP ++;
            }
        }
    }
}
