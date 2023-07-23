using System;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    [SerializeField] NetworkRunner _networkRunnerPrefab;

    private NetworkRunner _networkRunner;
    
    private void Start()
    {
        _networkRunner = Instantiate(_networkRunnerPrefab);
        _networkRunner.name = "Network Runner";

        var clientTask = InitializeNetworkRunner(_networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(),
            SceneManager.GetActiveScene().buildIndex, null);
        
        Debug.Log("Server NetworkRunner Started");
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address,
        SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour))
            .OfType<INetworkSceneManager>().FirstOrDefault();
        
        if (sceneManager == null)
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        
        runner.ProvideInput = true;
        
        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            SessionName = "TestRoom",
            Address = address,
            SceneManager = sceneManager,
            Scene = scene,
            Initialized = initialized,
        });
    }
}