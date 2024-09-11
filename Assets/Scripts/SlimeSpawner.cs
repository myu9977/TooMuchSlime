using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public ObjectPool slimePool; // ������ Ǯ
    public GameObject spawnPointPrefab;
    public int rows = 5;
    public int cols = 5;
    public float spacing = 1.1f;
    public float spawnProbability = 0.3f;

    private Transform[,] spawnPoints1;
    private Transform[,] spawnPoints2;
    private List<GameObject> activeSlimes = new List<GameObject>();

    public Transform background1; // ù ��° ���
    public Transform background2; // �� ��° ���

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
        Vector3 backgroundPosition = background.position; // ����� ���� ��ġ
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // ���� ����Ʈ�� ����� ������ ����
                GameObject spawnPointObject = Instantiate(spawnPointPrefab, background);

                // ��� ��ġ�� �������� ���� ����Ʈ�� ������� ��ġ ����
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
        // ���� ��� ȭ���� ���� ��踦 ��������� üũ (ȭ���� Y ��ǥ)
        if (background.position.y > Screen.height)
        {
            if (!hasExited)
            {
                hasExited = true; // ����� ó������ ȭ���� ����� �� true�� ����
                return true;
            }
        }
        else
        {
            hasExited = false; // ����� �ٽ� ȭ�� �ȿ� ������ false�� ����
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
        // ù ��° ����� ���� ����Ʈ�� ������ ����
        UpdateSlimesForSpawnPoints(spawnPoints1, background1);

        // �� ��° ����� ���� ����Ʈ�� ������ ����
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

                // �������� �ش� ����� �ڽ����� ����
                slime.transform.SetParent(background);

                slime.SetActive(true);
                activeSlimes.Add(slime);
            }
        }
    }
}
