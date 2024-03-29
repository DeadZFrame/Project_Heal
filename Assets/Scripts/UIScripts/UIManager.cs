using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button returnToGarage;

    private LevelManager _levelManager;

    public GameObject missionComplete, gameOver;

    private void Awake()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        returnToGarage.interactable = SceneManager.GetActiveScene().buildIndex != (int)LevelManager.SceneIndex.Garage;
        
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex is not ((int)LevelManager.SceneIndex.MainMenu and not (int)LevelManager.SceneIndex.CutScene))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            Time.timeScale = pauseMenu.activeInHierarchy ? 0f : 1f;
        }
        
        if (SceneManager.GetActiveScene().buildIndex is not (int)LevelManager.SceneIndex.MainMenu and not (int)LevelManager.SceneIndex.CutScene && _resetSaveGame)
        {
            //_levelManager.player.level = (int)LevelManager.SceneIndex.MainMenu;
            _resetSaveGame = false;
        }
        
        CheckTime();
    }

    public void CheckTime()
    {
        if (_levelManager.levelMenu.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else if (pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else if (_levelManager.mailBoxUI.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else if(gameOver.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else if(missionComplete.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private bool _resetSaveGame; 
    public void NewGame()
    {
        SceneManager.LoadScene((int)LevelManager.SceneIndex.CutScene);
        _resetSaveGame = true;
        LevelManager.Save("Level", 0);
        if (PlayerPrefs.HasKey("Level"))
        {
            _levelManager.level = LevelManager.Load("Level");
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ReturnToGarage()
    {
        SceneManager.LoadScene((int)LevelManager.SceneIndex.Garage);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }
    
    public void LoadTutorial()
    {
        _levelManager.sceneIndex = (int)LevelManager.SceneIndex.Tutorial;
        _levelManager.selectedScene = true;
        _levelManager.levelMenu.SetActive(false);
    }

    public void LoadLevel01()
    {
        if (_levelManager.level != (int)LevelManager.SceneIndex.Level01) return;
        _levelManager.sceneIndex = (int)LevelManager.SceneIndex.Level01;
        _levelManager.selectedScene = true;
        _levelManager.levelMenu.SetActive(false);
    }
}
