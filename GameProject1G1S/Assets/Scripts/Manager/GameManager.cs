using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private StageData stageData;
    [SerializeField] private StagePooler enemyPooler;
    [SerializeField] private StagePooler bossPooler;
    [SerializeField] private TilemapRenderer tilemap;
    [SerializeField] private TextMeshProUGUI textLeftEnemy;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private List<GameObject> stageList = new List<GameObject>();
    private Vector2 spawnPosition;
    private PlayerHP playerHP;
    private GameObject currentStage;
    private int stageNumber;
    private int stageOpened;
    private int currentSpawnEnemy;
    private int currentDeadEnemy;
    private int maxSpawnEnemy;
    private int currentSpawnBoss;
    private int currentDeadBoss;
    private int maxSpawnBoss;
    private int score;
    private float spawnDelay;
    private float phaseChangeDelay;
    private bool spawnPositionSelector;
    private bool isSpawnEnemy;
    private bool isBossPhase;
    private bool isSpawnBoss;

    public int StageNumber
    {
        get { return stageNumber; }
        set { stageNumber = value; }
    }
    public int CurrentDeadEnemy
    {
        get { return currentDeadEnemy; }
        set { currentDeadEnemy = value; }
    }
    public int CurrentDeadBoss
    {
        get { return currentDeadBoss; }
        set { currentDeadBoss = value; }
    }
    public int CurrentSpawnEnemy
    {
        get { return currentSpawnEnemy; }
        set { currentSpawnEnemy = value; }
    }
    public int CurrentSpawnBoss
    {
        get { return currentSpawnBoss; }
        set { currentSpawnBoss = value; }
    }
    public int MaxSpawnEnemy => maxSpawnEnemy;
    public int MaxSpawnBoss => maxSpawnBoss;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        AudioManager.Instance.PlayImpulse();

        stageNumber = PlayerPrefs.GetInt("StageNumber", 1);
        stageOpened = PlayerPrefs.GetInt("StageOpened", 1);
        currentStage = Instantiate(stageList[stageNumber - 1]);
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
    }

    private void Start()
    {
        maxSpawnEnemy = stageNumber * 100;
        maxSpawnBoss = stageNumber * 50;
        score = 0;

        if (stageNumber != 10)
        {
            spawnDelay = 0.3f;
        }
        else
        {
            spawnDelay = 0.1f;
        }

        phaseChangeDelay = 1f;
        StartCoroutine("SpawnEnemy");
    }

    private void Update()
    {
        if (currentDeadEnemy < maxSpawnEnemy)
        {
            textLeftEnemy.text = $"Left Enemy: {maxSpawnEnemy - currentDeadEnemy}";
        }

        if (currentDeadBoss < maxSpawnBoss && isBossPhase == true)
        {
            textLeftEnemy.text = $"Left Boss: {maxSpawnBoss - currentDeadBoss}";
        }

        textScore.text = $"Score: {score}";

        ResultScene();
    }

    private void ResultScene()
    {
        if (currentDeadBoss == maxSpawnBoss && playerHP.CurrentHP > 0)
        {
            Destroy(currentStage);

            if (stageNumber == stageOpened && stageNumber != 10)
            {
                PlayerPrefs.SetInt("StageOpened", ++stageOpened);
            }
            else
            {
                PlayerPrefs.SetInt("StageOpened", stageOpened);
            }

            PlayerPrefs.SetInt("CurrentScore", score);

            if (score > PlayerPrefs.GetInt($"Stage{stageNumber}HighScore", 0))
            {
                PlayerPrefs.SetInt($"Stage{stageNumber}HighScore", score);
            }

            SceneManager.LoadScene("ClearScene");
        }
        else if (playerHP.CurrentHP <= 0)
        {
            Destroy(currentStage);
            PlayerPrefs.SetInt("StageOpened", stageOpened);
            PlayerPrefs.SetInt("CurrentScore", score);

            if (score > PlayerPrefs.GetInt($"Stage{stageNumber}HighScore", 0))
            {
                PlayerPrefs.SetInt($"Stage{stageNumber}HighScore", score);
            }

            SceneManager.LoadScene("GameOverScene");
        }
    }

    IEnumerator SpawnEnemy()
    {
        isSpawnEnemy = true;

        while (true)
        {
            if (Time.timeScale == 0)
            {
                StartCoroutine("Shelter");
                StopCoroutine("SpawnEnemy");
            }

            if (currentSpawnEnemy >= maxSpawnEnemy)
            {
                break;
            }

            spawnPositionSelector = Random.value >= 0.5;

            if (spawnPositionSelector)
            {
                spawnPosition.x = (Random.value >= 0.5) ? stageData.LimitMax.x : stageData.LimitMin.x;
                spawnPosition.y = Random.Range(stageData.LimitMin.y, stageData.LimitMax.y);
            }
            else if (!spawnPositionSelector)
            {
                spawnPosition.x = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
                spawnPosition.y = (Random.value >= 0.5) ? stageData.LimitMax.y : stageData.LimitMin.y;
            }

            enemyPooler.SpawnObject(spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }

        while (true)
        {
            if (currentDeadEnemy == maxSpawnEnemy)
            {
                isSpawnEnemy = false;
                StartCoroutine("BossPhase");
                StopCoroutine("SpawnEnemy");
            }
        }
    }

    IEnumerator BossPhase()
    {
        isBossPhase = true;

        while (true)
        {
            if (Time.timeScale == 0)
            {
                StartCoroutine("Shelter");
                StopCoroutine("BossPhase");
            }

            tilemap.sortingOrder = 0;
            textLeftEnemy.text = $"Left Boss: {maxSpawnBoss - currentDeadBoss}";

            yield return new WaitForSeconds(phaseChangeDelay);

            if (phaseChangeDelay > 0)
            {
                tilemap.sortingOrder = 1;
                textLeftEnemy.text = "Left Enemy: 0";
                yield return new WaitForSeconds(phaseChangeDelay);
                phaseChangeDelay -= 0.1f;
            }
            else
            {
                isBossPhase = false;
                StartCoroutine("SpawnBoss");
                StopCoroutine("BossPhase");
            }
        }
    }

    IEnumerator SpawnBoss()
    {
        AudioManager.Instance.PlayMetropolis();

        isSpawnBoss = true;

        while (true)
        {
            if (Time.timeScale == 0)
            {
                StartCoroutine("Shelter");
                StopCoroutine("SpawnBoss");
            }

            if (currentSpawnBoss >= maxSpawnBoss)
            {
                isSpawnBoss = false;
                StopCoroutine("SpawnBoss");
            }

            spawnPositionSelector = Random.value >= 0.5;

            if (spawnPositionSelector)
            {
                spawnPosition.x = (Random.value >= 0.5) ? stageData.LimitMax.x : stageData.LimitMin.x;
                spawnPosition.y = Random.Range(stageData.LimitMin.y, stageData.LimitMax.y);
            }
            else if (!spawnPositionSelector)
            {
                spawnPosition.x = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
                spawnPosition.y = (Random.value >= 0.5) ? stageData.LimitMax.y : stageData.LimitMin.y;
            }

            bossPooler.SpawnObject(spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator Shelter()
    {
        while (true)
        {
            if (Time.timeScale == 1)
            {
                if (isSpawnEnemy)
                {
                    StartCoroutine("SpawnEnemy");
                }
                else if (isBossPhase)
                {
                    StartCoroutine("BossPhase");
                }
                else if (isSpawnBoss)
                {
                    StartCoroutine("SpawnBoss");
                }

                StopCoroutine("Shelter");
            }
        }
    }
}
