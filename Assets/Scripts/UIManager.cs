using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restart;
    [SerializeField]
    private GameManager _gameManager;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _scoreText.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _restart.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is Null");
        }
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
        if (currentLives <= 0)
        {
            GameOverRoutine();
        }
    }
    public void GameOverRoutine()
    {
        StartCoroutine(GameOverFlicker());
        _restart.gameObject.SetActive(true);
        _gameManager.GameOver();
    }
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
