using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace JoelMaranoExam
{
    public partial class Form1 : Form
    {
        string mycontent;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            //if (email != "" && password != "")
            //{
               GetRequest();
                txtResult.Text = mycontent;
            //}
        }

        async  void GetRequest(string url= "" )
        {
            //IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            //{

            //    new KeyValuePair<string, string>("login", txtEmail.Text),
            //    new KeyValuePair<string, string>("password", txtPassword.Text)


            //};
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("login", txtEmail.Text),
                new KeyValuePair<string, string>("password", txtPassword.Text)
            };

            var json = JsonConvert.SerializeObject(values);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //HttpContent postContent = new FormUrlEncodedContent(queries);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                  client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", "c1461646f53e0d15e1fd9d6cf5e2be4306b08982");

                //using (HttpResponseMessage responsex = await client.GetAsync("https://github.com/session"))
                //{

                //}

                using (HttpResponseMessage response = await client.PostAsync("https://api.github.com/authorizations", data))
               {
                   using (HttpContent content = response.Content)
                   {
                    mycontent = content.ReadAsStringAsync().Result;


                    }
                }
            }
        }
    }
}
