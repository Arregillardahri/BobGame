using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 3;
    public int money { get; private set; }

    

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;

        LoadLevel(1, 1);

        money = 0;
    }

    private void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void Nextlevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void Resetlevel(float delay)
    {
        CancelInvoke(nameof(Resetlevel));
        Invoke(nameof(Resetlevel), delay);
    }

    public void Resetlevel()
    {
        lives--;

        if ( lives > 0 )
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        NewGame();
        
    }

    public void AddMoney()
    {
        money++;

        if (money == 100)
        {
            AddLife();
            money = 0;
        }
    }

    public void AddLife()
    {
        lives++;
    }
}
    

    
