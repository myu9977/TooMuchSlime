using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public ObjectPool slimePool; // 슬라임 풀
    public GameObject spawnPointPrefab;
    public int rows = 5;
    public int cols = 5;
    public float spacing = 1.1f;
    public float spawnProbability = 0.3f;

    private Transform[,] spawnPoints1;
    private Transform[,] spawnPoints2;
    private List<GameObject> activeSlimes = new List<GameObject>();

    public Transform background1; // 첫 번째 배경
    public Transform background2; // 두 번째 배경

    private bool hasBackgroundExitedScreen1 = false;
    private bool hasBackgroundExitedScreen2 = false;

    private void Start()
    {
        InitializeSpawnPoints(background1, ref spawnPoints1);
        InitializeSpawnPoints(background2, ref spawnPoints2);
        UpdateSlimes();
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
                spawnPointObject.transform.position = backgroundPosition + new Vector3(i * 1.1f, j * 2.1f, 0);

                spawnPoints[i, j] = spawnPointObject.transform;
            }
        }
    }

    private void Update()
    {
        if (BackgroundExitedScreen(background1, ref hasBackgroundExitedScreen1))
        {
            DeactivateAllSlimes();
            UpdateSlimesForSpawnPoints(spawnPoints1, background1);
        }

        if (BackgroundExitedScreen(background2, ref hasBackgroundExitedScreen2))
        {
            DeactivateAllSlimes();
            UpdateSlimesForSpawnPoints(spawnPoints2, background2);
        }
    }

    private bool BackgroundExitedScreen(Transform background, ref bool hasExited)
    {
        // 예를 들어 화면의 위쪽 경계를 벗어나는지를 체크 (화면의 Y 좌표)
        if (background.position.y > Screen.height)
        {
            if (!hasExited)
            {
                hasExited = true; // 배경이 처음으로 화면을 벗어났을 때 true로 설정
                return true;
            }
        }
        else
        {
            hasExited = false; // 배경이 다시 화면 안에 들어오면 false로 리셋
        }
        return false;
    }

    private void DeactivateAllSlimes()
    {
        foreach (var slime in activeSlimes)
        {
            slime.SetActive(false);
            slimePool.ReturnObject(slime);
        }
        activeSlimes.Clear();
    }

    private void UpdateSlimes()
    {
        // 첫 번째 배경의 스폰 포인트에 슬라임 생성
        UpdateSlimesForSpawnPoints(spawnPoints1, background1);

        // 두 번째 배경의 스폰 포인트에 슬라임 생성
        UpdateSlimesForSpawnPoints(spawnPoints2, background2);
    }

    private void UpdateSlimesForSpawnPoints(Transform[,] spawnPoints, Transform background)
    {
        if (spawnPoints == null)
        {
            Debug.LogError("SpawnPoints not initialized!");
            return;
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (Random.value < spawnProbability)
            {
                GameObject slime = slimePool.GetObject();
                slime.transform.position = spawnPoint.position;

                // 슬라임을 해당 배경의 자식으로 설정
                slime.transform.SetParent(background);

                slime.SetActive(true);
                activeSlimes.Add(slime);
            }
        }
    }
}
