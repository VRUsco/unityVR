using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Dropdow : MonoBehaviour
{
    public Dropdown Drop;
    void Start()
    {
        Drop.ClearOptions();
    }

    void Update()
    {
        
    }

    public void boton()
    {
        Debug.Log(Drop.value);
        Debug.Log(Drop.options[Drop.value].text);
    }

    public void Rellenar()
    {
        lista();
    }

    public async Task lista()
    {
        var url2 = "http://localhost:5000/list";
        //var url2 = "https://jsonplaceholder.typicode.com/todos";
        HttpClient client = new HttpClient();
        var json2 = await client.GetStringAsync(url2);
        json2 = json2.Replace("\\", "");
        json2 = json2.Replace("\"[", "[");
        json2 = json2.Replace("]\"", "]");
        var myDetails = JsonConvert.DeserializeObject<List<listado>>(json2);
       
        foreach (var item in myDetails)
        {
            Dropdown.OptionData m_NewData = new Dropdown.OptionData();
            m_NewData.text = item.id.ToString();
            //m_NewData.ToString(item.usuario); 
            //m_Messages.Add(m_NewData);
            Drop.options.Add(m_NewData);

            //Debug.Log(item.id);
            //Debug.Log(item.usuario);
            //Debug.Log(item.puntaje);
            //Debug.Log("");
        }

        //https://jsonplaceholder.typicode.com/todos
    }

    public class listado
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string puntaje { get; set; }
    }
}
