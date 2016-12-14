using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField, Range(2,10)]
    int _loadSceneIndex = 2;

    bool _hasFirstUpdate = false;

    void Awake()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.LoadScene(_loadSceneIndex, LoadSceneMode.Additive);
        Scene stage = SceneManager.CreateScene("Stage");
        SceneManager.SetActiveScene(stage);
    }

	// Use this for initialization
	void Start ()
    {
        if (_hasFirstUpdate)
            return;

        _hasFirstUpdate = true;

        BaseBody player = Resources.Load<BaseBody>("Bodies/PlayerTest");
        SpawnManager.Spawn(player, Vector3.zero, Quaternion.identity);
    }
}
