using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

	private bool combatIdle = false;

    // Variável float utilizada para calcular a velocidade do player no eixo X
	public float speed = 0;
	public float jumpSpeed = 0;

    // Variável Vector2 que armazena o movimento do jogador no eixos X,Y
	private Vector2 movement;
	bool jump = false;

    // Update is called once per frame
    void Update()
    {
        // Pega o Input através do teclado no eixo X através da Horizontal 
		// (Teclas A / D e Seta Esquerda / Seta Direita
		float inputX = Input.GetAxis("Horizontal");
        MovePlayer(inputX);

        if(Time.time >= nextAttackTime){
            if (Input.GetKeyDown(KeyCode.Space)){
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        // -- Handle Animations --
        //Death
        // if (Input.GetKeyDown("e")) {
        //     if(!m_isDead)
        //         m_animator.SetTrigger("Death");
        //     else
        //         m_animator.SetTrigger("Recover");

        //     m_isDead = !m_isDead;
        // }
            
        //Hurt
        else if (Input.GetKeyDown("q"))
            animator.SetTrigger("Hurt");

        else if (Input.GetKeyDown("f")){
			combatIdle = !combatIdle;
		}

		//Jump
        else if (Input.GetKeyDown("w")) {
            animator.SetTrigger("Jump");
            // m_grounded = false;
            // m_animator.SetBool("Grounded", m_grounded);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
            // m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (combatIdle)
            animator.SetInteger("AnimState", 1);

        //Idle
        else
            animator.SetInteger("AnimState", 0);
    }

    // Movimenta o player para CIMA/BAIXO
    void MovePlayer(float inputX){
		

		if (Input.GetKeyDown(KeyCode.W)){
			jump = true;
		}
		
		if (inputX > 0){
            transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
		}
        else if (inputX < 0)
            transform.localScale = new Vector3(-4.0f, 4.0f, 4.0f);
        
        // Calcula o movimento da nave, levando em consideração 
		//para cada eixo (X,Y) a velocidade * input do teclado
		movement = new Vector2(speed * inputX, 0);

        // if(inputX != 0){
		// 	Debug.Log("oi");
		// 	animator.SetInteger("AnimState", 2);}
		// }else{
		// 	animator.SetInteger("AnimState", 0);
		// }
    }

    void Attack(){
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("aaa");
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected(){
        if (attackPoint == null) return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void FixedUpdate()
	{
		// Movimenta o objeto através da velocidade
		GetComponent<Rigidbody2D>().velocity = movement;
		if (jump){			
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
			// GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			jump = false;
		};
	}	
}
