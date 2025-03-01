using Datas;
using Helpers;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private InputType inputType;
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TMP_Text namePlaceHolderText;
        private GameData gameData;

        private void OnEnable()
        {
            nameInputField.onEndEdit.AddListener(SetEditedInfo);
            EventManager.LoadDatas += LoadData;
        }
        private void OnDisable()
        {
            nameInputField.onEndEdit.RemoveListener(SetEditedInfo);
            EventManager.LoadDatas -= LoadData;
        }

        private void LoadData()
        {
            gameData = EventManager.GameData?.Invoke();
            //namePlaceHolderText.text = gameData.UserData.Username;
            nameInputField.text = gameData.UserData.Username;
        }

        private void SetEditedInfo(string v) //Playerin girdigi isim bilgisi kaydedilir
        {
            gameData.UserData.Username = v;
            gameData.SaveUserData();
        }

    }
}