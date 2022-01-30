using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    
    private Transform target;
    public float maxDistance;

    public int maxHealth = 100;
    int currentHealth;

    public float speed;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 5;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private SpriteRenderer spriteRenderer;

    public HealthBar healthBar;

    private Vector3 temp;

    void Awake()
    {
        temp = transform.Find("HealthCanvas").gameObject.transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (target.position.x < transform.position.x)
            transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        else{
            transform.localScale = new Vector3(-5.0f, 5.0f, 5.0f);
            transform.GetChild(2).localScale = new Vector3(1, 1, 1);
        }

        if (target.GetComponent<PlayerScript>().IsAlive()){
            if(Vector2.Distance(transform.position, target.position) > maxDistance){
                // animator.SetInteger("AnimState", 2);
                
                // Faz com que o inimigo ande apenas na horizontal
                Vector2 playerPos = new Vector2(target.position.x, transform.position.y);
                
                transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            }else{
                if(Time.time >= nextAttackTime){
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
        // else{
        //     animator.SetInteger("AnimState", 0);
        // }
    }

    void LateUpdate(){
        transform.Find("HealthCanvas").gameObject.transform.localScale = temp;
    }

    void Attack(){
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies){
            if (enemy.name != "GroundSensor"){
                enemy.GetComponent<PlayerScript>().TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0){
            Die();
        }
    }

    void OnDrawGizmosSelected(){
        if (attackPoint == null) return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Die(){
        animator.SetBool("Death", true);

        Destroy(this.transform.Find("HealthCanvas").gameObject);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
