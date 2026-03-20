using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float speed = 5f;
  //自分の物理情報を入れるための変数準備して
  private Rigidbody2D rb;
  //GameManagerの箱を用意
  private GameManager gameManager;
  //弾
  public GameObject playerBulletPrefab;
  public float fireRate = 0.5f;
  void Start()
  {
    //inspectorで打ったrigidbody2D情報を入れる
    rb = GetComponent<Rigidbody2D>();
    gameManager = FindObjectOfType<GameManager>();

    //自動発射
    StartCoroutine(AutoFire());
  }

  void Update()
  {
    //Input.GetAxis("Horizontal  or  Vertical")　　　キー入力を取得する命令
    float x = Input.GetAxis("Horizontal");
    float y = Input.GetAxis("Vertical");
    rb.linearVelocity = new Vector2(x,y) * speed;

    Vector3 pos = transform.position;
    //Mathf.Clamp(値,最小値,最大値) 値を範囲内に収める
    pos.x = Mathf.Clamp(pos.x, -11f, 11f);
    pos.y = Mathf.Clamp(pos.y, -4.5f, 4.5f);
    transform.position = pos;
  }

  System.Collections.IEnumerator AutoFire()
  {
    while(true)
    {
      //instatiate(生成するPrefab, 生成する位置, 向き)
      GameObject bullet = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
      Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
      rb.linearVelocity = new Vector2(0f, 5f);
      yield return new WaitForSeconds(fireRate);
    }
  }

  //当たり判定
  //OnTriggerEnter2D→何かが当たったら実行される関数
  //Collider2D→当たり判定の情報をいれる型
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.CompareTag("Bullet"))
    {
      gameManager.LoseLife();
    }
  }
}
