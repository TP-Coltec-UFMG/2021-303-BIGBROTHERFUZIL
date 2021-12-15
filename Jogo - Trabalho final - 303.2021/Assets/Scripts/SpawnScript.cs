using UnityEngine;
using System.Collections;

/// <summary>
/// SpawnScript
/// Classe genérica responsável por criar objetos aleatórios na cena
/// </summary>
public class SpawnScript : MonoBehaviour 
{
	// Lista pública que deve conter os Prefabs que serão 
	// colocados randomicamente na cena em tempo de execução.
	// Os valores serão configurados para cada tipo de objeto através do Inspector.
	public GameObject[] obj;
	
	// Variáveis públicas que serão utilizadas para criar uma faixa indicando 
	// randômicamente a quantidade de objetos a serem colocados na cena.
	// Os valores serão configurados para cada tipo de objeto através do Inspector.
	public float spawnMin = 1.0f;
	public float spawnMax = 2.0f;
	
	// Variáveis públicas que serão utilizadas para definir 
	// os tamanhos dos objetos que serão colocados na cena.
	// Os valores serão configurados para cada tipo de objeto através do Inspector.
	public float sizeMin  = 1.0f;
	public float sizeMax  = 1.0f;
	
	// Este método deve ser utilizado para inicialização.
	// É executado uma única vez.
	void Start () 
	{
		Spawn();
	}
	
	// Método responsável por criar objetos 
	// randomicamente na cena.
	void Spawn ()
	{
		// Cria um objeto baseado na lista de objetos.
		GameObject objOriginal = obj[Random.Range(0, obj.GetLength(0))];
		
		// Define um tamanho randomicamente entre 
		// os valores mínimo e máximo.
		float size = Random.Range(sizeMin, sizeMax);
		
		// Seta o tamanho do objeto
		objOriginal.transform.localScale = new Vector3(size, size, size);
		
		// Instancia o objeto na cena
		Instantiate(objOriginal, transform.position, Quaternion.identity);
		Invoke ("Spawn", Random.Range(spawnMin, spawnMax));
	}
}

