using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;
    public int life = 3;
    
    //ハートの変数用意
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public void LoseLife()
    {
        life--;
        if(life == 2)
        {
            heart3.SetActive(false);
        }
        else if(life == 1)
        {
            heart2.SetActive(false);
        }
        else if(life <= 0)
        {
            heart1.SetActive(false);
            GameOver();
        }
    }

    //GameOver関数
    public void GameOver()
    {   
        //GameOver関数が呼ばれたら表示するように
        gameOverText.SetActive(true);
        Time.timeScale = 0;
    }
}
