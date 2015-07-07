using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject projectile;
	public AudioClip projectileSound;
	public AudioClip dieSound;
	
	public float health = 150f;
	public float projectileSpeed = 10f;
	public float shotPerSeconds = 0.5f;
	public int scoreValue = 150;

	private ScoreKeeper scoreKeeper;

	void Start () {
		// Find Score object in here instead of inspector, as enemies are created in runtime
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update () {
		float probabilityOfFire = Time.deltaTime * shotPerSeconds;
		if (Random.value < probabilityOfFire) {
			FireBeam();
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Die();
			}
		}
	}

	void Die () {
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
		AudioSource.PlayClipAtPoint (dieSound, transform.position, 0.5f);
	}

	void FireBeam () {
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (projectileSound, transform.position, 0.5f);
	}
}
