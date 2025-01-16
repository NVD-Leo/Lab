using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Cube; // Prefab của khối lập phương
    public float TimeSpawn = 2f; // Thời gian chờ giữa các lần tạo
    public Vector3 spawnAreaMin = new Vector3(-5, 0, -5); // Góc dưới bên trái khu vực sinh
    public Vector3 spawnAreaMax = new Vector3(5, 5, 5);   // Góc trên bên phải khu vực sinh
    public List<Vector3> targetPositions; // Danh sách vị trí đích để di chuyển
    public float moveDuration = 1f; // Thời gian di chuyển mỗi đối tượng

    private List<GameObject> createdCubes = new List<GameObject>(); // Danh sách lưu các Cube đã tạo

    void Start()
    {
        StartCoroutine(SpawnAndMoveCubes());
    }

    IEnumerator SpawnAndMoveCubes()
    {
        // Tạo và lưu đối tượng Cube
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(TimeSpawn);

            // Sinh vị trí ngẫu nhiên trong khu vực
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            float z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // Tạo đối tượng và lưu lại
            GameObject newCube = Instantiate(Cube, spawnPosition, Quaternion.identity);
            createdCubes.Add(newCube);
        }

        // Di chuyển tuần tự từng đối tượng Cube đến vị trí đích
        for (int i = 0; i < createdCubes.Count; i++)
        {
            if (i < targetPositions.Count)
            {
                yield return StartCoroutine(MoveToPosition(createdCubes[i], targetPositions[i]));
            }
        }
    }

    IEnumerator MoveToPosition(GameObject obj, Vector3 targetPosition)
    {
        Vector3 startPosition = obj.transform.position;
        float elapsedTime = 0f;

        // Di chuyển đối tượng trong thời gian `moveDuration`
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            yield return null; // Chờ đến frame tiếp theo
        }

        // Đảm bảo đối tượng đến đúng vị trí đích
        obj.transform.position = targetPosition;
    }
}