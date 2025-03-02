# HayDayLike

Backend Gelistirici Gorevleri icin BackendDev adinda bir sahne olusturdum. (Proje oldukca basit sadece yazilim yetkinliklerini gostermeyi amaclar sekilde yapildi.)

Her sey dataya yazilir ve datadan getirilir sekilde yapildi.

Zamanin kaydedilmesi System.DateTime ile degil de ornegin Firebase gibi bir yapidan cekilip kaydedilmesi hacklenme olayinin onune gecer. Ancak su an bu yapi hazir olmadigi icin simdilik System.DateTime yapisini kullandim.

Kullanilan mimariler; Observer Pattern, Flyweight Pattern. Manager ve Controller yapisi ile namespace'lere dikkat edildi.

Dataları [PROJECT]/Datas klasörünün icindeki ScriptableObject'leri manipule edebilirsiniz.

Data sistemi icin normal ScriptableObject'ler ile DataModel ve UserModel yapisi ile calisiyorum. DataModel tarafi developer tarafindan girilebilen kisim. UserDataModel tarafi ise hem developer hem de player tarafindan manipule edilebilen taraf. Ayrica bu yapinin icinde remoteconfig ile manipule de yapilabilir.

Auto Producer durumunu, sahnede UIController/ProductCanvas objesinde AutoProducerController adinda scripti ile kontrol ediyorum. Oradan kontrol edebilirsiniz.

1- Level sayisini butonlarla artirip azaltabilirsiniz.

2- Isim degisikligini "Name" yazan alana tikladiktan sonra yapabilirsiniz.

3- Money sayisini butonlarla artirip azaltabilirsiniz.


4- Toplam uretilmis urun sayisi yer alir. (Oyundan cikip tekrar girdiginizde arada gece sure otomatik uretim suresine bolunup toplam uretilen urun sayisina eklenir.)

5- O an otomatik uretilen urunun suresi yer alir.


6- Oyundan cikip tekrar girene kadar gecen surelerin yer aldigi alan.

![Ekran görüntüsü 2025-03-01 164823](https://github.com/user-attachments/assets/ed01cbfc-0810-42eb-8707-cd1f6f022d1d)


Datalari default haline getirmek icin tiklamaniz gereken yer.
![Ekran görüntüsü 2025-03-01 165149](https://github.com/user-attachments/assets/6cc5534e-6f95-45e1-b03f-4d270d909a40)
