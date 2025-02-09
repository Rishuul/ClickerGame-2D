using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class SaveSystem{

    public static void SaveShopData(ShopManager shopManager)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath+ "/saveshop.dat";
        FileStream file = new FileStream(path,FileMode.Create);

        ShopData shopData = new ShopData(shopManager);

        binaryFormatter.Serialize(file,shopData);

        file.Close();
    }

    public static ShopData LoadShopData()
    {
        string path = Application.persistentDataPath +"/saveshop.dat";
        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter=  new BinaryFormatter();
            FileStream file = new FileStream(path,FileMode.Open);

            ShopData shopData = binaryFormatter.Deserialize(file) as ShopData;
            file.Close();

            return shopData;
        }
        else
        {
            Debug.Log("Initialising values!");
            ShopData defaultShopData = new ShopData
            {
                boughtBurgerPlace=false,
                costOfMoreClicksPerSecond = 100

            };
            return defaultShopData;
        }
        
    }
}
