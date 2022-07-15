using System;
using TMPro;
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
    
    [NonSerialized] public int level;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex is not (int)SceneIndex.MainMenu and not (int)SceneIndex.CutScene)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
            _repairManager = GameObject.Find("RepairManager").GetComponent<RepairManager>();
        }
        //PlayerPrefs.DeleteAll();
    }
    
    public enum SceneIndex
    {
        MainMenu, CutScene, Garage, Tutorial, Level01
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.CutScene)
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
        ManageLevels();
    }
    
    public static void Save(string keyName,int level)
    {
        PlayerPrefs.SetInt(keyName, level);
        PlayerPrefs.Save();
    }

    public static int Load(string keyName)
    {
        return PlayerPrefs.GetInt(keyName);
    }

    public Button continueOnMainMenu, level01Menu;
    public Sprite enabledSprite, disabledSprite;
    
    private void ManageLevels()
    {
        if (level > 0)
        {
            continueOnMainMenu.interactable = true;
            level01Menu.gameObject.GetComponent<Image>().sprite = enabledSprite;
        }
        else
        {
            continueOnMainMenu.interactable = false;
            level01Menu.gameObject.GetComponent<Image>().sprite = disabledSprite;
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
    public TextMeshProUGUI[] mail;
        
    private RepairManager _repairManager;
    private DialogManager _dialogManager;
    
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
                if (mail.Length != 0)
                {
                    TextWriter.WriteText_Static(mail[0], _dialogManager.dialogue[0], .05f, true);
                    /*if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Level01)
                    {
                        mail[1].gameObject.SetActive(true);
                        TextWriter.WriteText_Static(mail[1], _dialogManager.dialogue[1], .1f, true);
                    }*/
                }
            }
        }
        else
        {
            if(_repairManager.distance > 4f)
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
                SFX_Manager.instance.Play("Embark");
                SceneManager.LoadSceneAsync(sceneIndex);
            }
        }
        else
        {
            interact.gameObject.SetActive(false);
        }
    }
}
