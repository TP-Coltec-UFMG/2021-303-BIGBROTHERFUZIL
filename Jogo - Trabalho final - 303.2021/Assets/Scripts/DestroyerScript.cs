using UnityEngine;
using System.Collections;

/// <summary>
/// Classe DestroyerScript
/// Classe genérica que pode ser usada para destruir 
///     qualquer objeto do jogo
/// </summary>>
public class DestroyerScript : MonoBehaviour 
{
	// Função que detecta uma colisão
	void OnTriggerEnter2D (Collider2D anyCollider)
	{
		if (anyCollider.gameObject.transform.parent)
		{
			// Destroi o objeto
			Destroy(anyCollider.gameObject.transform.parent.gameObject);
		}
		else
		{
			// Destroi o objeto
			Destroy(anyCollider.gameObject);
		}
	}
}
