using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


//Static class is a class that can't be instantiated. 
//We don't want to accidently create multiple versions of our save system
public static class SaveSystem
{

    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //This is where we locate a directory to store our data in a file. The file type can be set to anything.
        string path = Application.persistentDataPath + "/player.cool";

        //Can use file stream to read and write data from a file. File mode decides whether we want to open up or create a file.
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        //Insert data in file
        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Load data
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.cool";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            //read from stream
            PlayerData data = formatter.Deserialize(stream) as PlayerData;  // "as playerData" typecasts our stream into the same type
            stream.Close();
            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }


}
