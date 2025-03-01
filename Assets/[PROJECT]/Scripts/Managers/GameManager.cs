using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.Scripts.GameManager += GetGameManager;
        }

        private void OnDisable()
        {
            EventManager.Scripts.GameManager -= GetGameManager;
        }

        private GameManager GetGameManager() { return this; }
    }
}