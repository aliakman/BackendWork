using Datas;
using Helpers;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class AutoProducerController : MonoBehaviour
    {
        [Header("UI Fields")]
        [SerializeField] private Image productionFilledImage;
        [SerializeField] private TMP_Text productionAmountText;

        [SerializeField] private Image currentFilledImage;
        [SerializeField] private TMP_Text currentTimeText;

        private GameData gameData;
        private float tmpProductionSpeed;

        private void OnEnable()
        {
            EventManager.LoadDatas += LoadData;
            EventManager.AddProduct += AddProduct;
        }

        private void OnDisable()
        {
            EventManager.LoadDatas -= LoadData;
            EventManager.AddProduct -= AddProduct;
        }

        private void Update()
        {
            CalculateCurrentProduction();
        }

        private void CalculateCurrentProduction() //Anlik oto uretim hesaplamasi yapilir
        {
            if (gameData.UserData.ProductAmount >= gameData.UserData.MaxCapacity) return;

            if (tmpProductionSpeed > 0)
            {
                tmpProductionSpeed -= Time.deltaTime;
            }
            else
            {
                AddProduct(1);
                tmpProductionSpeed = gameData.UserData.ProductionSpeedAsSeconds;
            }

            currentFilledImage.fillAmount = ((float)gameData.UserData.ProductionSpeedAsSeconds - (float)tmpProductionSpeed) / (float)gameData.UserData.ProductionSpeedAsSeconds;
            currentTimeText.text = HelperFunctions.GetTimeString(Mathf.CeilToInt(tmpProductionSpeed));
        }

        private void LoadData()
        {
            gameData = EventManager.GameData?.Invoke();

            tmpProductionSpeed = gameData.UserData.ProductionSpeedAsSeconds;

            productionAmountText.text = $"{gameData.UserData.ProductAmount}/{gameData.UserData.MaxCapacity}";
            productionFilledImage.fillAmount = (float)gameData.UserData.ProductAmount / (float)gameData.UserData.MaxCapacity;

            CalculateOfflineProductEarning();
        }

        private void CalculateOfflineProductEarning() //Offline iken gecen surede otomatik olarak ne kadar  product kazanildigi hesaplanip kaydedilir
        {
            int diffSeconds = EventManager.TimeDifference.Invoke();

            int productAmount = Mathf.FloorToInt(diffSeconds / gameData.UserData.ProductionSpeedAsSeconds);
            AddProduct(productAmount);
            tmpProductionSpeed = diffSeconds % gameData.UserData.ProductionSpeedAsSeconds;
        }

        private void AddProduct(int v) //Product eklenir
        {
            if (gameData.UserData.ProductAmount + v < 0)
            {
                gameData.UserData.ProductAmount = 0;

            }
            else if (gameData.UserData.ProductAmount + v > gameData.UserData.MaxCapacity)
            {
                gameData.UserData.ProductAmount = gameData.UserData.MaxCapacity;

            }
            else
            {
                gameData.UserData.ProductAmount += v;
            }

            productionAmountText.text = $"{gameData.UserData.ProductAmount}/{gameData.UserData.MaxCapacity}";
            productionFilledImage.fillAmount = (float)gameData.UserData.ProductAmount / (float)gameData.UserData.MaxCapacity;

            gameData.SaveUserData();
        }
    }
}