using UnityEngine;
using System.Collections;

/// <summary>
/// Classe Player
/// </summary>
public class PlayerScript : MonoBehaviour
{
	// Variável float utilizada para calcular a velocidade do player no eixo X
	public float speed = 0;

	public float jumpSpeed = 0;
	
	// Variável Vector2 que armazena o movimento do jogador no eixos X,Y
	private Vector2 movement;
	bool jump = false;

	// O método Update é executado a cada frame do jogo
	void Update()
	{
		// Movimenta o player para CIMA/BAIXO
		MovePlayer();
		
		// Calcula o limite do jogador na tela, 
		// não deixando sair da visão da câmera
		// CalculesLimitOfThePlayerOnTheScreen();
	}

	// Movimenta o player para CIMA/BAIXO
	void MovePlayer()
	{
		// Pega o Input através do teclado no eixo X através da Horizontal 
		// (Teclas A / D e Seta Esquerda / Seta Direita
		float inputX = Input.GetAxis("Horizontal");

		if (Input.GetKeyDown("space")){
			jump = true;
		}
		
		// Calcula o movimento da nave, levando em consideração 
		//para cada eixo (X,Y) a velocidade * input do teclado
		movement = new Vector2(speed * inputX, 0);
	}

	// // Calcula o limite do jogador na tela, 
	// // não deixando sair da visão da câmera
	// void CalculesLimitOfThePlayerOnTheScreen ()
	// {
	// 	float margin = 2.0f;
		
	// 	// Controle para a nave não sair fora da tela (superior e inferior)
	// 	// Calcula a distância do player em relação a câmera
	// 	var distance = (transform.position - Camera.main.transform.position).z;
		
	// 	// Borda superior
	// 	var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;
	// 	topBorder = topBorder + margin;
		
	// 	// Borda inferior
	// 	var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)).y;
	// 	bottomBorder = bottomBorder - margin;
		
	// 	// Controla a posição do player entre o canto superior e inferior
	// 	float posY = Mathf.Clamp(transform.position.y, topBorder, bottomBorder);
	// 	transform.position = new Vector3(transform.position.x, posY, transform.position.z);
	// }
	
	// O método FixedUpdate é executado a cada frame do jogo, 
	// e deve ser usado para objetos que tenham física (rigidbody)
	void FixedUpdate()
	{
		// Movimenta o objeto através da velocidade
		GetComponent<Rigidbody2D>().velocity = movement;
		if (jump){
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			jump = false;
		};
	}	
}
