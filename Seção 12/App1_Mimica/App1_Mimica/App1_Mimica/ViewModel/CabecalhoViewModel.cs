using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace App1_Mimica.ViewModel
{
    public class CabecalhoViewModel : INotifyPropertyChanged
    {
        public Command Sair { get; set; }
        public int Pontuacao1 { get; set; }
        public int Pontuacao2 { get; set; }
        public bool JogoIniciado { get; set; }

        public int _RodadaAtual;
        public int RodadaAtual { get { return _RodadaAtual; } set { _RodadaAtual = value; OnPropertyChanged("RodadaAtual"); } }

        public CabecalhoViewModel()
        {
            Sair = new Command(SairAction);
            RodadaAtual = Armazenamento.Armazenamento.RodadaAtual;
            JogoIniciado = false;

            if (Armazenamento.Armazenamento.Jogo != null && RodadaAtual > 0)
            {
                JogoIniciado = true;
                Pontuacao1 = Armazenamento.Armazenamento.Jogo.Grupo1.Pontuacao;
                Pontuacao2 = Armazenamento.Armazenamento.Jogo.Grupo2.Pontuacao;
            }
        }

        private void SairAction()
        {
            App.Current.MainPage = new View.Inicio();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameProperty));
            }
        }

    }
}
