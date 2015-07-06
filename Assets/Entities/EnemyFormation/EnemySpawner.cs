using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	public float width = 10f;
	public float height = 5f;
	public float speed = 3.0f;

	private float xMin, xMax;
	private bool movingLeft = true;
	
	void Start () {
		CalculateViewBoundaries();
		SpawnEnemies();
	}

	void SpawnEnemies () {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void CalculateViewBoundaries () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftmost.x + (0.5f * width);
		xMax = rightmost.x - (0.5f * width);
	}

	public void OnDrawGizmos () {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	void Update () {
		MoveFormation();
	}

	void MoveFormation () {
		if (movingLeft) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		// Flip the direction
		if (transform.position.x <= xMin || transform.position.x >= xMax) movingLeft = !movingLeft;
	}
}
