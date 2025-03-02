using System;
using System.Globalization;
using UnityEngine; 

namespace Helpers
{
    public class HelperFunctions //Birden cok script tarafindan kullanilabilecek fonksiyonlari buraya yaz
    {
        public static int[] GetTimes(string dateString) //Zamani string cinsinden alip saat olarak veren fonksiyon
        {
            int[] times = new int[3];

            for (int i = 0; i < 3; i++)
                times[i] = int.Parse(dateString.Split(" ")[1].Split(":")[i]);

            if (dateString.Split(" ").Length > 2)
            {
                if (dateString.Split(" ")[2] == "PM")
                    times[0] += 12;
            }
            
            return times;
        }

        public static string GetTimeString(int v) //Zamani saniye cinsinden alip string olarak veren fonksiyon
        {
            int[] times = new int[3];

            times[0] = Mathf.CeilToInt(v / 3600);
            times[1] = Mathf.CeilToInt((v % 3600) / 60);
            times[2] = Mathf.CeilToInt(v % 60);

            string timeText;

            if (times[0] > 0)
            {
                if(times[0] >= 24)
                    timeText = $"{Mathf.FloorToInt(times[0] / 24)}d {Mathf.FloorToInt(times[0] % 24)}h {times[1]}m {times[2]}s";
                else
                    timeText = $"{times[0]}h {times[1]}m {times[2]}s";
            }
            else if (times[1] > 0)
            {
                timeText = $"{times[1]}m {times[2]}s";
            }
            else
            {
                timeText = $"{times[2]}s";
            }

            return timeText;
        }

        public static void GetDateTime(string v, out DateTime parsedDate) //Tarihi cihaz sistemine gore farklý formatlarda da calisacak sekilde elde eder
        {
            string[] formats = { 
                "yyyy-MM-dd HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy.MM.dd HH:mm:ss", 
                "MM-dd-yyyy HH:mm:ss", "MM/dd/yyyy HH:mm:ss", "MM.dd.yyyy HH:mm:ss",
                "dd-MM-yyyy HH:mm:ss", "dd/MM/yyyy HH:mm:ss", "dd.MM.yyyy HH:mm:ss",
                "d-MM-yyyy HH:mm:ss", "d/MM/yyyy HH:mm:ss", "d.MM.yyyy HH:mm:ss"
            }; // Ortak tarih formatlari

            try
            {
                if (DateTime.TryParseExact(v, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {

                }
                else
                {
                    Debug.LogError("Tarih elde edilemedi.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Sebep: " + e.Message + "    Elde edilemeyen tarih yerine su anki zaman geri donduruluyor.");
                parsedDate = DateTime.Now;
            }
        }
    }
}