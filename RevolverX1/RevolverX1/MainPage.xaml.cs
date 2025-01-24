using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RevolverX1
{
    public partial class MainPage : ContentPage
    {
        private const string ESP8266_IP = "http://192.168.231.49";

        public MainPage()
        {
            InitializeComponent();
            var btnEncender = new Button
            {
                Text = "Encender Luz",
                BackgroundColor = Color.LightGreen
            };
            btnEncender.Clicked += async (sender, args) => await EnviarComando("/encender");

            var btnApagar = new Button
            {
                Text = "Apagar Luz",
                BackgroundColor = Color.LightPink
            };
            btnApagar.Clicked += async (sender, args) => await EnviarComando("/apagar");

            var stackLayout = new StackLayout
            {
                Padding = 20,
                Children = { btnEncender, btnApagar }
            };

            Content = stackLayout;
        }

        private async System.Threading.Tasks.Task EnviarComando(string ruta)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"{ESP8266_IP}{ruta}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Éxito", "Comando enviado correctamente", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo enviar el comando", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Error al conectar con el ESP8266: {ex.Message}", "OK");
                }
            }
        }
    }  
}