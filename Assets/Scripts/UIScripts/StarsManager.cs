using System;
using UnityEngine;
using UnityEngine.UI;

public class StarsManager : MonoBehaviour
{
    [NonSerialized]public Image[] stars;
    public Sprite starSprite;
    
    private LevelManager _levelManager;
    
    private void Awake()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        stars = gameObject.GetComponentsInChildren<Image>();
    }

    public void ChangeSprite()
    {
        stars[_levelManager.starsForThisLevel - 1].sprite = starSprite;
    }
}
