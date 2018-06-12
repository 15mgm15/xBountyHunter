using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using xBountyHunterShared.Models;
using Newtonsoft.Json;

namespace xBountyHunterShared.Extras
{
    public class webServicesConnection
    {
        private const string URL_WS1 = @"http://201.168.207.210/services/droidBHServices.svc/fugitivos";
        private const string URL_WS2 = @"http://201.168.207.210/services/droidBHServices.svc/atrapados";

        HttpClient client;
        Page mainPage;

        public webServicesConnection(Page page)
        {
            mainPage = page;
        }

        public void connectGET()
        {
            List<mFugitivos> fujitivos = new List<mFugitivos>();
            client = new HttpClient();
            try
            {
                HttpResponseMessage response = client.GetAsync(URL_WS1).Result;
                if(response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    List<mFugitivos> items = JsonConvert.DeserializeObject<List<mFugitivos>>(content);
                    verifyFugitivosOnDB(items);
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                if(ex.InnerException != null && ex.InnerException.Message == "Error: NameResolutionFailure")
                {
                    connectGET();
                }
                else
                {
                    mainPage.DisplayAlert("Error", "No se pudo conectar con los servicios web", "Aceptar");
                }
            }
        }

        private void verifyFugitivosOnDB(List<mFugitivos> fugitivos)
        {
            List<mFugitivos> dbFugitivos = new List<mFugitivos>();
            databaseManager db = new databaseManager();
            dbFugitivos = db.selectAll();

            foreach (var fugitivo in fugitivos)
            {
                if(!dbFugitivos.Exists(x => x.Name == fugitivo.Name))
                {
                    fugitivo.Capturado = false;
                    db.insertItem(fugitivo);
                }
            }
            db.closeConnection();
        }

        public string connectPOST(string udid)
        {
            string result = "";
            string postBody = "{\"UDIDString\":\""+udid+"\"}";
            client = new HttpClient();
            try
            {
                HttpContent bodyContent = new StringContent(postBody, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync(URL_WS2, bodyContent).Result;
                if(response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    Dictionary<string, string> jsondata = JsonConvert
                        .DeserializeObject<Dictionary<string, string>>(content);
                    result = jsondata["mensaje"];
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message == "Error: NameResolutionFailure")
                {
                    result = connectPOST(udid);
                }
                else
                {
                    mainPage.DisplayAlert("Error", "No se pudo conectar con los servicios web", "Aceptar");
                }
            }
            return result;
        }


    }
}
