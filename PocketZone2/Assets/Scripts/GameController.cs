using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Player Player;

    [SerializeField] private Item _itemPrefab;
    [SerializeField] private NavMeshSurface _navMesh;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Enemy[] _enemies;

    private int _enemyToSpawn = 3;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        LoadPlayerData();
        for (int i = 0; i < _enemyToSpawn; i++)
            SpawnEnemy(_enemies[Random.Range(0, _enemies.Length)], navMeshData.vertices[Random.Range(0, navMeshData.vertices.Length)]);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F6))
        {
            SavePlayerData();
        }
        if(Input.GetKeyDown(KeyCode.F7))
        {
            LoadPlayerData();
        }
    }

    public void SavePlayerData()
    {
        SaveLoadSystem.SavePlayer(Player);
    }

    private void LoadPlayerData()
    {
        PlayerData data = SaveLoadSystem.LoadPlayer();
        if (data != null)
        {
            foreach (ItemScriptableObject item in data.PlayerInventory)
            {
                if (item != null)
                    Player.GetInventory().AddItem(item, 1);
            }

            Player.transform.position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
        }
    }

    public void EnemyDeath() //temp
    {
        _enemyToSpawn--;

        if(_enemyToSpawn <= 0)
        {
            Invoke("PlayerWin", 5);
        }
    }

    public void PlayerWin()  //temp 
    {
        UIManager.instance.OpenWinInterface();
        SaveLoadSystem.SavePlayer(Player);
        Time.timeScale = 0;
    }

    public void SpawnItem(ItemScriptableObject item, Vector3 position)
    {
        Item newitem = Instantiate(_itemPrefab, position, Quaternion.identity);
        newitem.SetupItem(item);
    }

    public void SpawnEnemy(Enemy enemy, Vector3 position)
    {
        Instantiate(enemy, position, Quaternion.identity);
    }

    public void PlayerDeath() //temp
    {
        UIManager.instance.OpenDeathInterface();
        Time.timeScale = 0;
        SaveLoadSystem.DeletePlayerData();
    }

    public void Reload() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
