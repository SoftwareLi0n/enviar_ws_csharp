using EnviarWS.Models;
using EnviarWS.Recursos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnviarWS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Jesús is love

            string url = "https://graph.facebook.com/aqui_va_la_version_api/aqui_va_el_id_telefono/messages";
            string token = "Bearer aqui_va_tu_token";

            string codigoComprobante = cbxComprobante.Text.Split('-')[0].Trim();

            Peticion data = new Peticion
            {
                messaging_product = "whatsapp",
                to = Configuracion.telefono,
                type = "template",
                template = new Template
                {
                    name = "comprobante",
                    language = new
                    {
                        code = "es"
                    },
                    components = new List<Componente>
                    {
                        new Componente
                        {
                            type = "header",
                            parameters = new List<dynamic>
                            {
                                new
                                {
                                    type = "document",
                                    document = new
                                    {
                                        link = txtPdf.Text,
                                        filename = codigoComprobante+"-"+txtNroComprobante.Text+".pdf"
                                    }
                                }
                            }
                        },
                        new Componente
                        {
                            type = "body",
                            parameters = new List<dynamic>
                            {
                                new
                                {
                                    type = "text",
                                    text = txtNroComprobante.Text
                                },
                                new
                                {
                                    type = "text",
                                    text = codigoComprobante
                                },
                                new
                                {
                                    type = "text",
                                    text = txtTotal.Text
                                }
                            }
                        }
                    }
                }
            };

            string json = JsonConvert.SerializeObject(data);

            dynamic response = DBApi.Post(url, json, token);

            var error = response.error;

            if(error == null)
            {
                MessageBox.Show("Mensaje enviado", "Jesús", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(error.message.ToString(), "Jesús", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPdf.Text = Configuracion.pdf;
        }
    }
}
