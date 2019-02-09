using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVTest : MonoBehaviour
{
    private void Awake()
    {
        List<byte> _byteList = new List<byte>();
        string path = Path.Combine(Application.streamingAssetsPath, "Test.csv");
        using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            bool isEnd = false;
            int count = 0;
            while (isEnd == false)
            {
                byte value = (byte)file.ReadByte();
                _byteList.Add(value);
                isEnd =(int)value == -1 ? true : false;

             ;
            }
            file.Close();
        }


        string path2 = Path.Combine(Application.streamingAssetsPath, "Test2.csv");
        using (FileStream file2 = new FileStream(path2, FileMode.CreateNew, FileAccess.Write))
        {
            for (int i = 0; i < _byteList.Count; i++)
            {
                file2.WriteByte(_byteList[i]);
            }
            file2.Close();
        }
    }
}
