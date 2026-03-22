using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    //配列にして敵の登場位置を複数登録できるように
    public Transform[] spawnPoints;
    public TextMeshProUGUI waveText;

    private int currentWave = 0;
    private int enemiesAlive = 0;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        currentWave++;
        //waveTextはTextMeshProのコンポーネントそのものだから、テキストの中身を変えるには.textが必要
        waveText.text = "Wave" + currentWave;
        //SetActive→trueならば表示、falseなら非表示
        waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        waveText.gameObject.SetActive(false);

        enemiesAlive = currentWave;
        for(int i = 0; i < currentWave; i++)
        {   
            //出現位置の数は固定して、それぞれから出現する数を制御するためのもの
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
            //Instatiate→prefabからオブジェクトを生成する関数
            //Instatiate(何を生成するか, どこに生成するか,　向きはどうするか)
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void OnEnemyDefeated()
    {
        enemiesAlive--;

        if(enemiesAlive <= 0)
        {
            StartCoroutine(StartNextWave());
        }
    }
}
