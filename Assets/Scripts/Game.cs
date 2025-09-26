using UnityEngine;


class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SpawnerEnemy _spawnerEnemy;
    [SerializeField] private Window _window;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private ScoreCounter _scoreCounter;

    private Vector2 _spawnPoint;

    private void Start()
    {
        _spawnPoint = _player.transform.position;

        _player.GameOver += Pause;
        _window.StartGame += StartGame;
        _spawnerEnemy.EnemyDie += _scoreCounter.Add;
    }

    private void OnEnable()
    {
        _player.GameOver -= Pause;
        _window.StartGame -= StartGame;
        _spawnerEnemy.EnemyDie -= _scoreCounter.Add;
    }

    private void Restart()
    {
        _player.Reset();
        _player.transform.position = _spawnPoint;
        
        _spawnerEnemy.Reset();
        _scoreCounter.Reset();
    }

    private void StartGame()
    {
        Restart();
        Time.timeScale = 1;

        _inputReader.Block = false;
    }

    private void Pause()
    {
        _inputReader.Block = true;
        _window.Pause();

        Time.timeScale = 0;
    }
}