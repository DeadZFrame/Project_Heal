using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform redButton ,panel, mailBox;
    public Button interact;
    [NonSerialized] public Player player;

    public GameObject levelMenu;

    private Vector3 _interactOffset;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex is not (int)SceneIndex.MainMenu and not (int)SceneIndex.CutScene)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    
    public enum SceneIndex
    {
        MainMenu, CutScene, Garage, Tutorial, Level01
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.CutScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene((int)SceneIndex.Garage);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex is not (int)SceneIndex.MainMenu and not (int)SceneIndex.CutScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                levelMenu.SetActive(false);
                mailBoxUI.SetActive(false);
                Time.timeScale = levelMenu.activeInHierarchy ? 0f : 1f;
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Garage)
        {
            BigRedButton();
            Panel();
        }
        else if(SceneManager.GetActiveScene().buildIndex is not (int)SceneIndex.MainMenu and not (int)SceneIndex.Garage and not (int)SceneIndex.CutScene)
        {
            MailBox();
            ManageStars();
        }
        if(SceneManager.GetActiveScene(). buildIndex is not (int)SceneIndex.MainMenu and not (int)SceneIndex.CutScene)
        {
            ManageLevels();
        }
    }

    public Button continueOnMainMenu, level01Menu;
    
    private void ManageLevels()
    {
        if (player.level > (int)SceneIndex.CutScene)
        {
            continueOnMainMenu.interactable = true;
        }
        else if (player.level > (int)SceneIndex.Tutorial)
        {
            level01Menu.interactable = true;
        }
        else
        {
            continueOnMainMenu.interactable = false;
            level01Menu.interactable = false;
        }
    }
    
    [NonSerialized] public int starsForThisLevel;
    public void ManageStars()
    {
        if (starsForThisLevel == 3)
        {
            starsForThisLevel = 0;
        }
    }

    public GameObject mailBoxUI;
    
    private void MailBox()
    {
        var distance = Vector3.Distance(player.transform.position, mailBox.position);
        if (distance < 4f)
        {
            interact.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Open MailBox menu and set Time.scale to zero
                mailBoxUI.SetActive(true);
            }
        }
        else
        {
            interact.gameObject.SetActive(false);
        }
    }
    
    [NonSerialized] public int sceneIndex;
    [NonSerialized] public bool selectedScene = false;
    
    private void Panel()
    {
        var distance = Vector3.Distance(player.transform.position, panel.position);
        if (distance < 6f)
        {
            interact.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                levelMenu.SetActive(true);
                Time.timeScale = levelMenu.activeInHierarchy ? 0f : 1f;
            }
        }
    }
    
    private void BigRedButton()
    {
        var distance = Vector3.Distance(player.transform.position, redButton.position);
        if (distance < 4f && selectedScene)
        {
            interact.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadSceneAsync(sceneIndex);
            }
        }
        else
        {
            interact.gameObject.SetActive(false);
        }
    }
}
