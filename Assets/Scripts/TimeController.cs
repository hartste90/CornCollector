using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

    public void handlePlayerDestroyed()
    {
        Time.timeScale = 0.2f;
    }
}
