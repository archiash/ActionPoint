using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemSaveIO
{
    private static readonly string baseSavePath = Application.persistentDataPath;

    static ItemSaveIO()
    {
        //baseSavePath = Application.persistentDataPath;
    }

    public static void SaveItems(ItemContainerSaveData items,string fileName)
    {
        FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + fileName + ".dat",items);
    }

    public static ItemContainerSaveData LoadItems(string fileName)
    {
        string filePath = baseSavePath + "/" + fileName + ".dat";

        if(System.IO.File.Exists(filePath))
        {
            return FileReadWrite.ReadFromBinaryFile<ItemContainerSaveData>(filePath);
        }
        return null;
    }

    public static void SaveItems<T>(T items,string fileName)
    {
        FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + fileName + ".dat", items);
    }

    public static T LoadItems<T>(string fileName) where T : class
    {
        string filePath = baseSavePath + "/" + fileName + ".dat";

        if (System.IO.File.Exists(filePath))
        {
            return FileReadWrite.ReadFromBinaryFile<T>(filePath);
        }
        return null;
    }

    public static CharacterSaveData LoadCharacter(string fileName)
    {
        string filePath = baseSavePath + "/" + fileName + ".dat";

        if (System.IO.File.Exists(filePath))
        {
            return FileReadWrite.ReadFromBinaryFile<CharacterSaveData>(filePath);
        }
        return null;
    }

    public static void SaveCharacter(CharacterSaveData items, string fileName)
    {
        FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + fileName + ".dat", items);
    }

}
