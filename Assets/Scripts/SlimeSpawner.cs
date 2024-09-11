using UnityEngine;
using System.Collections.Generic;

public class SlimeSpawner : MonoBehaviour
{
    public ObjectPool slimePool; // 슬라임 풀
    public GameObject spawnPointPrefab;
    public int rows = 5;
    public int cols = 5;
    public float rowSpacing = 1.1f;
    public float colSpacing = 2.1f;
    public float spawnProbability = 0.3f;

    private Transform[,] spawnPoints1;
    private Transform[,] spawnPoints2;
    private List<GameObject> activeSlimes1 = new List<GameObject>(); // 배경1 슬라임
    private List<GameObject> activeSlimes2 = new List<GameObject>(); // 배경2 슬라임

    public Transform background1; // 첫 번째 배경
    public Transform background2; // 두 번째 배경

    private bool hasBackgroundExitedScreen1 = false;
    private bool hasBackgroundExitedScreen2 = false;

    private void Start()
    {
        InitializeSpawnPoints(background1, ref spawnPoints1);
        InitializeSpawnPoints(background2, ref spawnPoints2);
        UpdateSlimesForSpawnPoints(spawnPoints1, background1, activeSlimes1);
        UpdateSlimesForSpawnPoints(spawnPoints2, background2, activeSlimes2);
    }

    private void InitializeSpawnPoints(Transform background, ref Transform[,] spawnPoints)
    {
        spawnPoints = new Transform[rows, cols];
        Vector3 backgroundPosition = background.position; // 배경의 현재 위치
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // 스폰 포인트를 배경의 하위로 생성
                GameObject spawnPointObject = Instantiate(spawnPointPrefab, background);

                // 배경 위치를 기준으로 스폰 포인트의 상대적인 위치 설정
                spawnPointObject.transform.position = backgroundPosition + new Vector3(i * rowSpacing, j * colSpacing, 0);

                spawnPoints[i, j] = spawnPointObject.transform;
            }
        }
    }

    private void Update()
    {
        if (BackgroundExitedScreen(background1, ref hasBackgroundExitedScreen1))
        {
            UpdateSlimesForSpawnPoints(spawnPoints1, background1, activeSlimes1);
        }

        if (BackgroundExitedScreen(background2, ref hasBackgroundExitedScreen2))
        {
            UpdateSlimesForSpawnPoints(spawnPoints2, background2, activeSlimes2);
        }
    }

    private bool BackgroundExitedScreen(Transform background, ref bool hasExited)
    {
        Vector3 backgroundScreenPosition = Camera.main.WorldToViewportPoint(background.position);

        if (backgroundScreenPosition.y > 1 || backgroundScreenPosition.y < -1 ||
            backgroundScreenPosition.x > 1 || backgroundScreenPosition.x < -1)
        {
            if (!hasExited)
            {
                hasExited = true;
                return true;
            }
        }
        else
        {
            hasExited = false;
        }
        return false;
    }

    private void UpdateSlimesForSpawnPoints(Transform[,] spawnPoints, Transform background, List<GameObject> activeSlimes)
    {
        if (spawnPoints == null)
        {
            return;
        }

        // 기존 슬라임 비활성화 및 반환
        foreach (var slime in activeSlimes)
        {
            slime.SetActive(false);
            slimePool.ReturnObject(slime);
        }
        activeSlimes.Clear();

        // 새로운 슬라임 생성
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (Random.value < spawnProbability)
            {
                GameObject slime = slimePool.GetObject();
                slime.transform.position = spawnPoint.position;
                slime.transform.SetParent(background);
                slime.SetActive(true);
                activeSlimes.Add(slime);
            }
        }
    }
}
