using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

// this did a whole lot of things in non-DOTS version but now its basically just a HUD updater

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private World _world;
    private EntityManager _entityManager;
    private EntityQuery PlayerHealthQuery;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }

    void Start()
    {
        _world = World.DefaultGameObjectInjectionWorld;
        _entityManager = _world.EntityManager;
        PlayerHealthQuery = _entityManager.CreateEntityQuery(typeof(PlayerHealth));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // <R>estart
        {
            CleanAndRestartECS();
        }

        if (Input.GetKeyDown(KeyCode.Q)) // <Q>uit
        {
            Application.Quit();
        }
    }
    
    

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 50), "FPS: " + (1 / Time.smoothDeltaTime).ToString("f0"));
    }


    public void CleanAndRestartECS()
    {
        // https://forum.unity.com/threads/reset-ecs-world-and-jobs-to-start-fresh-again.1364208/#post-8609088
        _entityManager.CompleteAllTrackedJobs();
        foreach (var system in _world.Systems)
        {
            system.Enabled = false;
        }

        _world.Dispose();
        DefaultWorldInitialization.Initialize("Default World", false);
        if (!ScriptBehaviourUpdateOrder.IsWorldInCurrentPlayerLoop(World.DefaultGameObjectInjectionWorld))
        {
            ScriptBehaviourUpdateOrder.AppendWorldToCurrentPlayerLoop(World.DefaultGameObjectInjectionWorld);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
