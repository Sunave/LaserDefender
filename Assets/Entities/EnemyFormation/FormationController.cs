using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;

	public float width = 10f;
	public float height = 5f;
	public float speed = 3.0f;
	public float spawnDelay = 0.5f;

	private float xMin, xMax;
	private bool movingLeft = true;
	
	void Start () {
		CalculateViewBoundaries();
		SpawnUntilFull();
	}

	void SpawnEnemies () {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull () {
		Transform freePosition = NextFreePosition();
		if (freePosition) {
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay);
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

		if (AllMembersDead()) SpawnUntilFull();
	}

	bool AllMembersDead () {
		foreach(Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	Transform NextFreePosition() {
		foreach(Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount <= 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	void MoveFormation () {
		if (movingLeft) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		// Flip the direction
		if (transform.position.x <= xMin) movingLeft = false;
		else if (transform.position.x >= xMax) movingLeft = true;
	}
}
