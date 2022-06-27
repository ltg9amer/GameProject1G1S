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
    private float spawnDelay;
    private float phaseChangeDelay;
    private bool spawnPositionSelector;
    private bool isBossPhase;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        stageNumber = PlayerPrefs.GetInt("StageNumber", 1);
        stageOpened = PlayerPrefs.GetInt("StageOpened", 1);
        currentStage = Instantiate(stageList[stageNumber - 1]);
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
    }

    private void Start()
    {
        if (stageNumber != 10)
        {
            maxSpawnEnemy = stageNumber * 50;
            maxSpawnBoss = stageNumber * 25;
            spawnDelay = 1f;
            spawnDelay -= spawnDelay * ((float)stageNumber / 10) - 0.1f;
        }
        else
        {
            maxSpawnEnemy = stageNumber * 100;
            maxSpawnBoss = stageNumber * 50;
            spawnDelay = 1f;
            spawnDelay -= spawnDelay * ((float)stageNumber / 10) - 0.05f;
        }

        phaseChangeDelay = 1f;
        StartCoroutine(SpawnEnemy());
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

            SceneManager.LoadScene("ClearScene");
        }
        else if (currentDeadBoss <= maxSpawnBoss && playerHP.CurrentHP <= 0)
        {
            Destroy(currentStage);
            PlayerPrefs.SetInt("StageOpened", stageOpened);
            SceneManager.LoadScene("GameOverScene");
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
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

            if (currentSpawnEnemy == maxSpawnEnemy)
            {
                break;
            }
        }

        while (true)
        {
            yield return null;

            if (currentDeadEnemy == maxSpawnEnemy)
            {
                StartCoroutine(BossPhase());
                break;
            }
        }
    }

    IEnumerator BossPhase()
    {
        while (true)
        {
            tilemap.sortingOrder = 0;
            textLeftEnemy.text = $"Left Boss: {maxSpawnBoss - currentDeadBoss}";

            yield return new WaitForSeconds(phaseChangeDelay);

            if (phaseChangeDelay > 0)
            {
                tilemap.sortingOrder = 1;
                textLeftEnemy.text = $"Left Enemy: {maxSpawnEnemy - currentDeadEnemy}";
                yield return new WaitForSeconds(phaseChangeDelay);
                phaseChangeDelay -= 0.1f;
            }
            else
            {
                StartCoroutine(SpawnBoss());
                isBossPhase = true;
                break;
            }
        }
    }

    IEnumerator SpawnBoss()
    {
        while (true)
        {
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

            if (currentSpawnBoss == maxSpawnBoss)
            {
                break;
            }
        }
    }
}
