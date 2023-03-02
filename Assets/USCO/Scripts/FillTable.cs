using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

public class FillTable : MonoBehaviour
{
    [SerializeField] private Button ButtonSearch;
    public TMPro.TMP_InputField User_id;
    [SerializeField] private TMPro.TMP_Text name;
    [SerializeField] private TMPro.TMP_Text lastName;
    [SerializeField] private TMPro.TMP_Text gender;
    private user User;
    private static int UserId;
    private static string resultString;

    public async Task searchAsync()
    {
        string userID = User_id.text.ToString();
        var url = "http://localhost:5000/usuario:"+ userID;
        HttpClient client = new HttpClient();
        var json = await client.GetStringAsync(url);
        json = json.Replace("\\", "");
        json = json.Replace("\"[", "");
        json = json.Replace("]\"", "");
        User = JsonConvert.DeserializeObject<user>(json);
        name.text = User.nombre;
        lastName.text = User.apellido;
        gender.text = User.genero;
        UserId = User.id;

    }
    public void fill()
    {
        searchAsync();
        ButtonSearch.interactable = false;
    }

    public static void GeneradorClave()
    {
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var Charsarr = new char[4];
        var random = new System.Random();

        for (int i = 0; i < Charsarr.Length; i++)
        {
            Charsarr[i] = characters[random.Next(characters.Length)];
        }
        resultString = new String(Charsarr);


    }

    public static void SaveNewTest(int auxiliar, int nivel, int grupo)
    {
        TestInfo Test = new TestInfo();
        GeneradorClave();
        Test.fecha_hora = DateTime.Now;
        Test.auxiliar = auxiliar;
        Test.usuario = UserId;
        Test.nivel = nivel;
        Test.grupo = grupo;
        Test.clave = resultString;

        SaveManager.DatosPrueba(Test.fecha_hora, Test.nivel, Test.clave);

        var httpClient = new HttpClient();

        var json = JsonConvert.SerializeObject(Test);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var url = "http://localhost:5000/prueba";
        httpClient.PostAsync(url, data);
    }

    public class user
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string genero { get; set; }
    }

    public class TestInfo
    {
        public DateTime fecha_hora;
        public string clave;
        public int auxiliar;
        public int usuario;
        public int nivel;
        public int  grupo;
    }
}
