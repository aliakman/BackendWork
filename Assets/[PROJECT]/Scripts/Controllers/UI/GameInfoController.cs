using Datas;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameInfoController : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text levelText;
        
        private GameData gameData;

        private void OnEnable()
        {
            EventManager.LoadDatas += LoadData;

            EventManager.AddMoney += AddMoney;
            EventManager.IncreaseLevel += IncreaseLevel;
            EventManager.DecreaseLevel += DecreaseLevel;
        }

        private void OnDisable()
        {
            EventManager.LoadDatas -= LoadData;

            EventManager.AddMoney -= AddMoney;
            EventManager.IncreaseLevel -= IncreaseLevel;
            EventManager.DecreaseLevel -= DecreaseLevel;
        }

        private void LoadData()
        {
            gameData = EventManager.GameData?.Invoke();
            moneyText.text = gameData.UserData.Money.ToString();
            levelText.text = gameData.UserData.Level.ToString();
        }

        public void AddMoneyViaButton(int v)
        {
            EventManager.AddMoney?.Invoke(v);
        }

        private bool AddMoney(int v) //Para hesabi
        {
            if (gameData.UserData.Money + v < 0) return false;
            gameData.UserData.Money += v;
            moneyText.text = gameData.UserData.Money.ToString();
            gameData.SaveUserData();
            return true;
        }

        public void IncreaseLevel() //Level artirimi
        {
            gameData.UserData.Level++;
            levelText.text = gameData.UserData.Level.ToString();
            gameData.SaveUserData();
        }

        public void DecreaseLevel() //Level dusurme (normalde boyle bir fonksiyon olmaz ama case senaryosunda ihtiyac olacagi icin ekledim)
        {
            if (gameData.UserData.Level - 1 < 1) return;
            gameData.UserData.Level--;
            levelText.text = gameData.UserData.Level.ToString();
            gameData.SaveUserData();
        }

    }
}