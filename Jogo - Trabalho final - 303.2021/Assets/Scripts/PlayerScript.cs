using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;

    public GameObject gameOver;
    public GameObject atacado;
    public GameObject atacou;

	public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public float textTime = 3f;
    float timeWasHit;
    float hitTime;

    public int maxHealth = 100;
    int currentHealth;

    public HealthBar healthBar;

    // Use this for initialization
    void Start () {
        gameOver.SetActive(false);
        atacado.SetActive(false);
        atacou.SetActive(false);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }
	
	// Update is called once per frame
	void Update () {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
        }

        //Check if character just started falling
        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(-5.0f, 5.0f, 5.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

		if(Time.time >= nextAttackTime){
            if (Input.GetKeyDown(KeyCode.Space)){
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.W)) {
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        
        // Disable descriptive texts
        if (Time.time > timeWasHit + textTime)
            atacado.SetActive(false);

        if (Time.time > hitTime + textTime)
            atacou.SetActive(false);
    }

	void Attack(){
        m_animator.SetTrigger("Attack");

        atacou.SetActive(true);
        atacou.GetComponent<GeneralButton>().PlaySound();
        hitTime = Time.time;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies){
            if(enemy.name != "GroundSensor"){
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damage){
        atacado.SetActive(true);
        atacado.GetComponent<GeneralButton>().PlaySound();
        timeWasHit = Time.time;

        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        m_animator.SetTrigger("Hurt");

        if(currentHealth <= 0){
            Die();
        }
    }

    public bool IsAlive(){
        return currentHealth > 0;
    }

    void OnDrawGizmosSelected(){
        if (attackPoint == null) return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Die(){

        gameOver.SetActive(true);
        gameOver.GetComponent<GeneralButton>().PlaySound();
        atacado.SetActive(false);
        atacou.SetActive(false);

        m_animator.SetBool("Death", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
