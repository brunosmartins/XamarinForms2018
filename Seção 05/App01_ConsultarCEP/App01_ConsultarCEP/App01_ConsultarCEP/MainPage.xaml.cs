using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            var cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end == null)
                        DisplayAlert("Erro", "Endereço não encontrado para o CEP " + cep, "Ok");
                    else
                        RESULTADO.Text = string.Format(@"Endereço: {0}, {1} {2}", end.localidade, end.uf, end.logradouro);
                }
                catch (Exception ex)
                {
                    DisplayAlert("Erro", ex.Message, "Ok");
                }
            }

        }

        private bool isValidCEP(string cep)
        {
            var valido = true;
            var msg = "";

            if (cep.Length != 8)
            {
                msg += "O CEP deve conter 8 caracteres.";
                valido = false;
            }

            int novoCEP = 0;

            if (!int.TryParse(cep, out novoCEP))
            {
                msg += Environment.NewLine + "O CEP deve conter apenas números.";
                valido = false;
            }

            if (!valido)
            {
                DisplayAlert("CEP Inválido", msg, "Ok");
            }

            return valido;
        }
    }
}
