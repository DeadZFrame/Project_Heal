using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        StartCoroutine(ExitScene(_animator.runtimeAnimatorController.animationClips[0].length));
    }

    IEnumerator ExitScene(float time)
    {
        yield return new WaitForSeconds(time + 1f);
        SceneManager.LoadScene((int)LevelManager.SceneIndex.Garage);
    }
}
