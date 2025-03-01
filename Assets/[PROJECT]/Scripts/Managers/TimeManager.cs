using Datas;
using System;
using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TimeData timeData;

        [SerializeField] private int[] currentTimes = new int[3] { 0, 0, 0 };
        [SerializeField] private int[] dataTimes = new int[3] { 0, 0, 0 };
        [SerializeField] private int[] diffTimes = new int[3] { 0, 0, 0 };

        private void Awake()
        {
            timeData.Initialize();
        }

        private void OnEnable()
        {
            CalculateTimeDifference();
            EventManager.TimeDifference += TimeDifference;
        }

        private void OnDisable()
        {
            EventManager.TimeDifference -= TimeDifference;
        }

        private int TimeDifference() { return timeData.UserData.TimeDifferenceAsSeconds; }

        private void CalculateTimeDifference()
        {
            if(!timeData.UserData.IsGameOpenedFirstTime)
            {
                timeData.UserData.IsGameOpenedFirstTime = true;
                timeData.SaveUserData();
                return;
            }
            
            DateTime dateTime1 = DateTime.ParseExact(timeData.UserData.ExitTime.Split(" ")[1], "HH:mm:ss", null);
            DateTime dateTime2 = DateTime.ParseExact(DateTime.Now.ToString().Split(" ")[1], "HH:mm:ss", null);

            TimeSpan difference = dateTime2 - dateTime1;

            double secondsDifference = difference.TotalSeconds;

            timeData.UserData.TimeDifferenceAsSeconds = (int)secondsDifference;
            timeData.SaveUserData();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                timeData.UserData.ExitTime = System.DateTime.Now.ToString();
                timeData.SaveUserData();
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                timeData.UserData.ExitTime = System.DateTime.Now.ToString();
                timeData.SaveUserData();
            }
        }

        private void OnApplicationQuit()
        {
            timeData.UserData.ExitTime = System.DateTime.Now.ToString();
            timeData.SaveUserData();
        }

    }
}