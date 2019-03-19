using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1_Vagas.Modelos;
using App1_Vagas.Banco;

namespace App1_Vagas.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastroVaga : ContentPage
	{
        public bool NovaVaga = false;
        private Vaga Vaga { get; set; }

        public CadastroVaga (Vaga vaga, bool novaVaga)
		{
			InitializeComponent ();
            this.Vaga = vaga;
            this.NovaVaga = novaVaga;
            
            NomeVaga.Text = vaga.NomeVaga;
            Empresa.Text = vaga.Empresa;
            Quantidade.Text = vaga.Quantidade.ToString();
            Cidade.Text = vaga.Cidade;
            Salario.Text = vaga.Salario.ToString();
            Descricao.Text = vaga.Descricao;
            TipoContratacao.IsToggled = (vaga.TipoContratacao == "CLT") ? false : true;
            Telefone.Text = vaga.Telefone;
            Email.Text = vaga.Email;
        }

        private void SalvarAction(object sender, EventArgs e)
        {
            //TODO - Validar Dados do Cadastro
            
            Vaga.NomeVaga = NomeVaga.Text;
            Vaga.Quantidade = short.Parse(Quantidade.Text);
            Vaga.Salario = double.Parse(Salario.Text);
            Vaga.Empresa = Empresa.Text;
            Vaga.Cidade = Cidade.Text;
            Vaga.Descricao = Descricao.Text;
            Vaga.TipoContratacao = (TipoContratacao.IsToggled) ? "PJ" : "CLT";
            Vaga.Telefone = Telefone.Text;
            Vaga.Email = Email.Text;

            Database database = new Database();

            if (NovaVaga)
                database.Cadastro(Vaga);
            else
                database.Atualizacao(Vaga);


            App.Current.MainPage = new NavigationPage(new ConsultaVagas());
        }
    }
}