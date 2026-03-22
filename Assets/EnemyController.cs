using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public GameObject bulletPrefab;
  public float fireRate = 1f;

  //移動関連
  public float moveSpeed = 2f;
  public float moveRange = 3f;
  private float startX;

  //HP関連
  public int maxHP = 5;
  private int currentHP;

  void Start()
  {
    //transform.position→このオブジェクトの今の①
    startX = transform.position.x;
    //startcoroutine→繰り返し処理の開始用
    //start関数が終わったとしてもStartCoroutineこれの引数は起動し続ける
    StartCoroutine(FireBullet());

    //HPを最大にセット
    currentHP = maxHP;
  }

  void Update()
  {
    //Mathf.Sin→波のように繰り返す値を返す (-1→0→1→0→-1)
    //moveRangeで幅を調整できる
    float x = startX + Mathf.Sin(Time.time * moveSpeed) * moveRange;
    //transformは３Dで管理されてるからvector3にしないといけない
    transform.position = new Vector3(x, transform.position.y,0);
  }
  //触れた瞬間に勝手に発動する→OnTriggerEnter2D　otherのなかに触れたものの情報が
  void OnTriggerEnter2D(Collider2D other)
  {
    if(other.CompareTag("PlayerBullet"))
    {
      //unityに決められた関数 Destroy(○○)→　○○を消す
      Destroy(other.gameObject);
      TakeDamage(1);
    }
  }

  //ダメージと残りHPの計算
  void TakeDamage(int damage)
  {
    currentHP -=damage;

    if(currentHP <= 0)
    {
      //敵が１体倒されたと報告するようなイメージ
      FindObjectOfType<WaveManager>().OnEnemyDefeated();
      Destroy(gameObject);
    }
  }

  System.Collections.IEnumerator FireBullet()
  {
    while(true)
    {
      FireBulletInDirection(new Vector2(0f, -1f));
      FireBulletInDirection(new Vector2(-0.5f, -1f));
      FireBulletInDirection(new Vector2(0.5f, -1f));
      //yieldは繰り返しの中でfirerateの時間待ってから繰り返していく
      yield return new WaitForSeconds(fireRate);
    }
  }

  void FireBulletInDirection(Vector2 direction)
  {
    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    rb.linearVelocity = direction.normalized * 5f;
  }
}
