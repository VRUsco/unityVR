using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class LocalizationManager : MonoBehaviour
{
    static readonly Dictionary<string, string> myData = new Dictionary<string, string>();
    
    static Dictionary<string, AudioClip> songs = new Dictionary<string, AudioClip>();


    void Awake()
    {
        var culture = System.Globalization.CultureInfo.CurrentCulture;
        string language = culture.ToString().ToLower().Replace("-", "_");
        if (myData.Count == 0 && songs.Count == 0)
        {
            FileRead(language);
            GetSongsFromFolder(language);
        }
    }

    public static void ChangeLanguage()
    {
        myData.Clear();
        songs.Clear();
    }

    public static void FileRead(string language)
    {
        string textFile = @"Assets/Language/mission_" + language + ".txt";
        if (File.Exists(textFile))
        {
            string[] lines = File.ReadAllLines(textFile);

            foreach (string line in lines)
            {
                int p = line.IndexOf("=");
                string key = line.Substring(0, p);
                string value = line.Substring(p + 1);
                key = key.Trim();
                value = value.Trim();
                myData.Add(key, value);
            }
        }
    }

    public static string Localize(string text)
    {
        string res = myData[text];
        return res;
    }

    public void GetSongsFromFolder(string language)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(@"Assets/Sounds/" + language);

        foreach (FileInfo songFile in directoryInfo.GetFiles())
        {
            StartCoroutine(ConvertFilesToAudioClip(songFile));
        }

    }
    private IEnumerator ConvertFilesToAudioClip(FileInfo songFile)
    {
        songs.Clear();
        if (songFile.Name.Contains("meta"))
            yield break;
        else
        {
            string songName = songFile.FullName.ToString();
            string url = string.Format("file://{0}", songName);
            using (UnityWebRequest web = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                yield return web.SendWebRequest();
                if (!web.isNetworkError && !web.isHttpError)
                {
                    var clip = DownloadHandlerAudioClip.GetContent(web);
                    if (clip != null)
                    {
                        string KeyFile = songFile.Name.ToString();

                        KeyFile = KeyFile.Replace(".mp3", "");
                        if (songs.Count > 0)
                        {
                            songs.Add(KeyFile, clip);
                        }
                        
                    }
                }
            }
        }
    }

    public static AudioClip LocalizeAudio(string text)
    {
        AudioClip res = songs[text];
        return res;
    }
}
