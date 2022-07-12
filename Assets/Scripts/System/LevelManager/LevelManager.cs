using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform redButton;
    public Button interact;
    private Player _player;

    public GameObject levelMenu;

    private Vector3 _interactOffset;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelMenu.SetActive(false);
        }
        
        Time.timeScale = levelMenu.activeInHierarchy ? 0f : 1f;
        BigRedButton();
    }

    private void BigRedButton()
    {
        if (SceneManager.GetActiveScene().name.Equals("Garage"))
        {
            var distance = Vector3.Distance(_player.transform.position, redButton.transform.position);
            if (distance < 4f)
            {
                interact.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    levelMenu.SetActive(true);
                }
            }
            else
            {
                interact.gameObject.SetActive(false);
            }
        }
    }
}
