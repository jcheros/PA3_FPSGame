using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void restart()
    {
        GameController.Instance.RestartGame();
    }
}
