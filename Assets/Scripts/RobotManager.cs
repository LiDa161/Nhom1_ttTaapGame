using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RobotCustomerSpawnInfo
{
    public GameObject prefab;
    public Vector3 position;
    public GameObject instance; // Track the runtime object
}

public class RobotManager : MonoBehaviour
{
    public static RobotManager Instance { get; private set; }

    private List<RobotCustomerSpawnInfo> spawnInfos = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CacheInitialRobots();
    }

    private void CacheInitialRobots()
    {
        spawnInfos.Clear();

        RobotCustomer[] robots = FindObjectsByType<RobotCustomer>(FindObjectsSortMode.None);
        foreach (var robot in robots)
        {
            var refComp = robot.GetComponent<OriginalPrefabReference>();
            if (refComp != null && refComp.prefab != null)
            {
                spawnInfos.Add(new RobotCustomerSpawnInfo
                {
                    prefab = refComp.prefab,
                    position = robot.transform.position,
                    instance = robot.gameObject
                });
            }
        }
    }

    public void RespawnAllCustomers()
    {
        foreach (var info in spawnInfos)
        {
            if (info.instance != null)
            {
                info.instance.transform.position = info.position;
                info.instance.SetActive(true);

                var robotScript = info.instance.GetComponent<RobotCustomer>();
                if (robotScript != null)
                {
                    robotScript.ResetEmotionToNormal();
                }
            }
            else
            {
                GameObject newBot = Instantiate(info.prefab, info.position, Quaternion.identity);
                info.instance = newBot;
            }
        }
    }
}