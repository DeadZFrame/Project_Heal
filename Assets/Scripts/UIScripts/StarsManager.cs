using UnityEngine;
using UnityEngine.UI;

public class StarsManager : MonoBehaviour
{
    private Image[] _stars;
    public Sprite starSprite;
    
    private LevelManager _levelManager;
    
    private void Awake()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _stars = gameObject.GetComponentsInChildren<Image>();
    }

    public void ChangeSprite()
    {
        _stars[_levelManager.starsForThisLevel - 1].sprite = starSprite;
    }
}
