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
    pos.x = Mathf.Clamp(pos.x, -16f, 16f);
    pos.y = Mathf.Clamp(pos.y, -4.5f, 4.5f);
    transform.position = pos;
  }

  System.Collections.IEnumerator AutoFire()
  {
    while(true)
    {
      if(nearestEnemy != null)
      {
        //Vector3-型-xyzの３D座標　　Camera.main-メインカメラを取得　　ScreenToWorldPoint-スクリーン座標をワールド座標に変換
        //Input.mousePosition-マウスのスクリーン座標を取得
        //型 変数名 = Camera.main.ScreenToWorldPoint(スクリーン座標);
        //マウスのスクリーン座標をゲーム上の座標に変換して、mousePosというVector3型の変数に入れてね
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = 0f;
        //normalized-ベクトルの長さを1に統一  GameObject-型 
        //マウスの座標からplayerの座標を引いて傾きを求める。それの速さを変えないためにnarmalizedでベクトルを固定
        //Vector2 direction = (mousePos - transform.position).normalized;
        //Instatiate-関数
        //instatiate(生成するprefab, 生成する位置, 向き)
        GameObject bullet = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
        //RigidBody-関数  GetComponent-関数-コンポーネントを取得
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        //linearVelocity-プロパティ-物理的な移動速度
        bulletRb.linearVelocity = direction * 30f;
      }
      yield return new WaitForSeconds(fireRate);
    }
  }

  //一番近い敵を探すメソッド
  GameObject FindNearestEnemy()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    GameObject nearest = null;
    float minDistance = Mathf.Infinity;

    foreach(GameObject enemy in enemies)
    {
      float distance = Vector2.Distance(transform.position, enemy.transform.position);
      if(distance < minDistance)
      {
        minDistance = distance;
        nearest = nenmy;
      }
    }
    return nearest;
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
