using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Monster[] monsters;

    void OnEnable()
    {
        monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().name);

        if (MonstersAreAllDead())
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                GoToNextLevel("Level2");
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                GoToNextLevel("GameOver");
            }
        }
    }

    void GoToNextLevel(string nextLevelName)
    {
        Debug.Log("Go to level" + nextLevelName);
        SceneManager.LoadScene(nextLevelName);
    }

    bool MonstersAreAllDead()
    {
        foreach (var monster in monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }

        return true;
    }
}
