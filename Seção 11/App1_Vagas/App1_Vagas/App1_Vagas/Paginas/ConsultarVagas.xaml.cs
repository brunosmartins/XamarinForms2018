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
    public partial class ConsultaVagas : ContentPage
    {
        List<Vaga> Lista { get; set; }

        public ConsultaVagas()
        {
            InitializeComponent();

            Database database = new Database();
            Lista = database.Consultar();
            ListaVagas.ItemsSource = Lista;
            lblCount.Text = Lista.Count + " vagas cadastradas";
        }

        private void GoCadastro(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Paginas.CadastroVaga(new Vaga(), true));
        }

        private void GoMinhasVagas(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Paginas.MinhasVagasCadastradas());
        }

        private void MaisDetalheAction(object sender, EventArgs e)
        {
            Label lblDetalhe = ((Label)sender);
            var vaga = ((TapGestureRecognizer)lblDetalhe.GestureRecognizers[0]).CommandParameter as Vaga;

            Navigation.PushAsync(new Paginas.DetaheVaga(vaga));
        }

        private void PesquisarAction(object sender, TextChangedEventArgs e)
        {
            var newList = Lista.Where(a => a.NomeVaga.Contains(e.NewTextValue)).ToList();

            lblCount.Text = newList.Count + " vagas cadastradas";

            ListaVagas.ItemsSource = newList;
        }
    }
}