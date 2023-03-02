using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private TMP_Text erroresUI;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timeElapsed;
    [SerializeField] private GameObject endOfLevel;


    [SerializeField] private TMP_Text endOfLevelErrores;
    [SerializeField] private TMP_Text endOfLevelTiempo;
    public string time;
    public int errorsCount;
    private int minutes, seconds, miliseconds;
    private string directionToSave = "Cll 0 con 0";
    private static DateTime fechaInicio;
    private static int nivel;
    private static string clave;

    private PruebaId pruebaIdLegada;
    private int idPrueba;

    private int idPruebaError;
    private TipoErrorId TipoErrorIdLegada;

    private void Start()
    {
        IdPruebaAsync();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        timerText.text = getTime(timeElapsed);

        if (minutes > 9)
        {
            timerText.color = new Color(255, 0, 0, 255);
        }
    }

    [ContextMenu("GUARDAR CHAMO")]
    public void SaveAppAsync()
    {

        ResultadoInfo player = new ResultadoInfo();
        player.prueba = idPrueba;
        player.nivel = nivel;
        player.fecha_hora_inicio = fechaInicio;
        player.fecha_hora_fin = DateTime.Now;
        player.tiempo_ejecucion = time;

        var httpClient = new HttpClient();

        var json = JsonConvert.SerializeObject(player);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var url = "http://localhost:5000/resultados";
        httpClient.PostAsync(url, data);

        endOfLevelTiempo.text = time.ToString();
        endOfLevelErrores.text = errorsCount.ToString();
        endOfLevel.SetActive(true);
    }

    public static void DatosPrueba(DateTime fecha_hora_inicio, int nivelLlegada, string claveLlegada)
    {
        fechaInicio = fecha_hora_inicio;
        nivel = nivelLlegada;
        clave = claveLlegada;
    }

    public async Task IdPruebaAsync()
    {
        var url2 = "http://localhost:5000/pruebaId:\'"+clave+"\'";
        HttpClient client = new HttpClient();
        var json = await client.GetStringAsync(url2);
        json = json.Replace("[", "");
        json = json.Replace("]", "");
        pruebaIdLegada = JsonConvert.DeserializeObject<PruebaId>(json);
        idPrueba = pruebaIdLegada.id;
    }

    public class ResultadoInfo
    {
        public int prueba;
        public int nivel;
        public DateTime fecha_hora_inicio;
        public DateTime fecha_hora_fin;
        public string tiempo_ejecucion;
    }

    public class PruebaId
    {
        public int id;
    }

    public void IncreaseError(string whyerror, DateTime fecha_horaLlegada)
    {
        errorsCount++;
        PostError(fecha_horaLlegada);
        erroresUI.text = whyerror;
        Invoke("Cono", (200 * Time.deltaTime));

    }

    public void Cono()
    {
        erroresUI.text = "";
    }

    public void PostError( DateTime fecha_hora)
    {
        ErrorInfo Error = new ErrorInfo();
        Error.prueba = idPrueba;
        Error.tipo_error = idPruebaError;
        Error.fecha_hora = fecha_hora;

        var httpClient = new HttpClient();
        var json = JsonConvert.SerializeObject(Error);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var url = "http://localhost:5000/error";
        httpClient.PostAsync(url, data);
    }

    public class ErrorInfo
    {
        public int prueba;
        public int tipo_error;
        public DateTime fecha_hora;
    }

    public async Task IdTipoErrorAsync(string tipoError)//funcionando
    {
        var url2 = "http://localhost:5000/error:\'"+tipoError+"\'";
        HttpClient client = new HttpClient();
        var json = await client.GetStringAsync(url2);
        json = json.Replace("[", "");
        json = json.Replace("]", "");
        TipoErrorIdLegada = JsonConvert.DeserializeObject<TipoErrorId>(json);
        idPruebaError = TipoErrorIdLegada.id;
    }

    public class TipoErrorId
    {
        public int id;
    }

    public void updateDirection(string direction)
    {
        directionToSave = direction;
    }
    string getTime(float n)
    {
        minutes = (int)(timeElapsed / 60f);
        seconds = (int)(timeElapsed - (60f * minutes));
        miliseconds = (int)((timeElapsed - (int)timeElapsed) * 100f);
        time = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
        return time;
    }
}
/* RVadmin
RVadmin2022 */
