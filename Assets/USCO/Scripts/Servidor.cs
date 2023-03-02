using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using UnityEngine;

public class Servidor : MonoBehaviour
{
    void Start()
    {
        var httpClient = new HttpClient();
        var url = "http://localhost:5000/app";
        var data = new { usuario = "pedro", errores = 12 };
        var result = httpClient.PostAsync(url, AsJson(data));
        
    }

    StringContent AsJson(object o){ 
        return new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json"); 
    }


}


