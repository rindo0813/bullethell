using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public GameObject bulletPrefab;
  public float fireRate = 1f;

  //移動関連
  public float moveSpeed = 2f;
  public float moveRange = 3f;
  private float startX;

  void Start()
  {
    //transform.position→このオブジェクトの今の①
    startX = transform.position.x;
    //startcoroutine→繰り返し処理の開始用
    //start関数が終わったとしてもStartCoroutineこれの引数は起動し続ける
    StartCoroutine(FireBullet());
  }

  void Update()
  {
    //Mathf.Sin→波のように繰り返す値を返す (-1→0→1→0→-1)
    //moveRangeで幅を調整できる
    float x = startX + Mathf.Sin(Time.time * moveSpeed) * moveRange;
    //transformは３Dで管理されてるからvector3にしないといけない
    transform.position = new Vector3(x, transform.position.y,0);
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
