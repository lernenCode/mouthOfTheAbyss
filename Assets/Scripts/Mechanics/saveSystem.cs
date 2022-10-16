using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class saveSystem
{
   public static void Save(saveManager player)
   {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data Load()
    {
        string path = Application.persistentDataPath + "/player.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            return data;
        } else {Debug.LogError("save file not found in" + path); return null;}
    }
}
