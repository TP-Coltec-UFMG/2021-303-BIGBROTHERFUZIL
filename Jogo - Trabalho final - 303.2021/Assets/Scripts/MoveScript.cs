using UnityEngine;
using System.Collections;

/// <summary>
/// Classe MoveScript
/// Classe genérica que pode ser usada para movimentar 
///     qualquer objeto do jogo quando anexada a ele
/// </summary>>
public class MoveScript : MonoBehaviour
{
	// Variável Vector2 utilizada para calcular a velocidade 
	// do objeto no eixos X,Y
	public Vector2 speed = new Vector2(15, 15);
	
	// Variável Vector2 que armazena a direção do objeto no eixos X,Y
	public Vector2 direction = new Vector2(1, 0);
	
	// Variável Vector2 que armazena o movimento do objeto no eixos X,Y
	private Vector2 movement;
	
	// O método Update é executado a cada frame do jogo
	void Update()
	{
		// Calcula o movimento do objeto, levando em consideração a 
		// velocidade no eixo (X,Y) * a direção
		movement = new Vector2(speed.x*direction.x, speed.y*direction.y);
	}
	
	// O método FixedUpdate é executado a cada frame do jogo, 
	// e deve ser usado para objetos que tenham física (rigidbody)
	void FixedUpdate()
	{
		// Movimenta o objeto através da velocidade
		GetComponent<Rigidbody2D>().velocity = movement;
	}
}