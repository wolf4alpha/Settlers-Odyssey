using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.VisualScripting;
using System.Xml.Serialization;
using Newtonsoft.Json;

public class FileDataHandler
{
    private string DataDirectoryPath = "";
    private string DataFileName = "";

    public FileDataHandler(string dataDirectoryPath, string dataFileName)
    {
        DataDirectoryPath = dataDirectoryPath;
        DataFileName = dataFileName;
    }

    public void SaveJson(GameData _data)
    {
        string fullPath = Path.Combine(DataDirectoryPath, DataFileName);
        try { 
            Directory.CreateDirectory(DataDirectoryPath);
            string dataToStore = JsonConvert.SerializeObject(_data);

            using(FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error saving file: " + ex.Message);
        }
    }

    public GameData LoadJson()
    {
        string fullPath = Path.Combine(DataDirectoryPath, DataFileName);
        GameData loadData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                       
                    }
                }
                loadData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
                var a = loadData.characters.Count.ToString();
            }
            catch (Exception ex)
            {
                Debug.Log("Error loading file: " + ex.Message);
            }
   
        }
        else
        {
            Debug.Log("File not found: " + fullPath);
        }

        return loadData;
    }

    public void SaveXml(GameData _data)
    {
        string fullPath = Path.Combine(DataDirectoryPath, DataFileName);
        var serializer = new XmlSerializer(typeof(GameData));
        var stream = new FileStream(fullPath, FileMode.Create);
        serializer.Serialize(stream, _data);
        stream.Close();
    }
}

