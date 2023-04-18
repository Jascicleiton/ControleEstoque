using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class CSVWriter : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    string filename = "";

    [System.Serializable]
    public class Player
    {
        public string name;
        public int number;
    }

    [System.Serializable]
    public class PlayerList
    {
        public Player[] player;
    }

    public PlayerList myPlayerList = new PlayerList();
    // Start is called before the first frame update
    void Start()
    {
        filename = Application.persistentDataPath + "/calhambeque.csv";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            WriteCSV();
        }
    }

    public void WriteCSV()
    {
        if(myPlayerList.player.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Name, Number");
            tw.Close();

            tw = new StreamWriter(filename, true);
            for (int i = 0; i < myPlayerList.player.Length; i++)
            {
                tw.WriteLine(myPlayerList.player[i].name + "," + myPlayerList.player[i].number);
            }
            tw.Close();
            text.text = filename;
        }
    }
}
