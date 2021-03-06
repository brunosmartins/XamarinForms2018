﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using App1_Mimica.Model;
using Xamarin.Forms;

namespace App1_Mimica.ViewModel
{
    public class InicioViewModel : INotifyPropertyChanged
    {
        public Jogo Jogo { get; set; }
        public Command IniciarComand { get; set; }
        private string _MsgErro;
        public string MsgErro { get { return _MsgErro; } set { _MsgErro = value; OnPropertyChanged("MsgErro"); } }

        public InicioViewModel()
        {
            Armazenamento.Armazenamento.RodadaAtual = 0;
            IniciarComand = new Command(IniciarJogo);
            Jogo = new Jogo();
            Jogo.Grupo1 = new Grupo();
            Jogo.Grupo2 = new Grupo();

            Jogo.TempoPalavra = 120;
            Jogo.Rodadas = 7;
        }

        private void IniciarJogo()
        {
            string erro = "";
            if (Jogo.TempoPalavra < 10)
            {
                erro += "O tempo mínimo para o tempo da palavra é 10 segundos";
            }

            if (Jogo.Rodadas <= 0)
            {
                erro += "\nO valor mínimo para a rodada é 1";
            }

            if (erro.Length > 0)
            {
                MsgErro = erro;
            }

            Armazenamento.Armazenamento.Jogo = this.Jogo;
            Armazenamento.Armazenamento.RodadaAtual = 1;
            App.Current.MainPage = new View.Jogo(Jogo.Grupo1);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string NameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }
    }
}
