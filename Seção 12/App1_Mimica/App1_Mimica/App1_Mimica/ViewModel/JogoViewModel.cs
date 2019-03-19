using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using App1_Mimica.Model;

namespace App1_Mimica.ViewModel
{
    public class JogoViewModel : INotifyPropertyChanged
    {
        private Grupo Grupo { get; set; }

        public string NomeGrupo { get; set; }
        public string NumeroGrupo { get; set; }

        public byte _PalavraPontuacao;
        public byte PalavraPontuacao { get { return _PalavraPontuacao; } set { _PalavraPontuacao = value; OnPropertyChanged("PalavraPontuacao"); } }
        public string _Palavra;
        public string Palavra { get { return _Palavra; } set { _Palavra = value; OnPropertyChanged("Palavra"); } }
        public string _TextoContagem;
        public string TextoContagem { get { return _TextoContagem; } set { _TextoContagem = value; OnPropertyChanged("TextoContagem"); } }

        public bool _IsVisibleContainerContagem;
        public bool IsVisibleContainerContagem { get { return _IsVisibleContainerContagem; } set { _IsVisibleContainerContagem = value; OnPropertyChanged("IsVisibleContainerContagem"); } }
        public bool _IsVisibleBtnIniciar;
        public bool IsVisibleBtnIniciar { get { return _IsVisibleBtnIniciar; } set { _IsVisibleBtnIniciar = value; OnPropertyChanged("IsVisibleBtnIniciar"); } }
        public bool _IsVisibleBtnMostrar;
        public bool IsVisibleBtnMostrar { get { return _IsVisibleBtnMostrar; } set { _IsVisibleBtnMostrar = value; OnPropertyChanged("IsVisibleBtnMostrar"); } }

        public Command MostrarPalavra { get; set; }
        public Command Acertou { get; set; }
        public Command Errou { get; set; }
        public Command Iniciar { get; set; }

        public JogoViewModel(Grupo grupo)
        {
            Grupo = grupo;

            NomeGrupo = grupo.Nome;

            if (grupo == Armazenamento.Armazenamento.Jogo.Grupo1)
            {
                NumeroGrupo = "Grupo 1 ";
            }
            else
            {
                NumeroGrupo = "Grupo 2 ";
            }

            IsVisibleContainerContagem = false;
            IsVisibleBtnIniciar = false;
            IsVisibleBtnMostrar = true;

            Palavra = "**********";

            MostrarPalavra = new Command(MostrarPalavraAction);
            Acertou = new Command(AcertouAction);
            Errou = new Command(ErrouAction);
            Iniciar = new Command(IniciarAction);
        }

        private void MostrarPalavraAction()
        {
            PalavraPontuacao = 3;
            Palavra = "Sentar";

            var numNivel = Armazenamento.Armazenamento.Jogo.NivelNumerico;
            
            if (numNivel == 0)
            {
                Random rd = new Random();
                numNivel = rd.Next(1, 3);

                int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[numNivel - 1].Length);
                Palavra = Armazenamento.Armazenamento.Palavras[numNivel - 1][ind];
                PalavraPontuacao = (byte)(numNivel == 1 ? 1 : (numNivel == 2 ? 3 : 5));
            }

            if (numNivel == 1)
            {
                Random rd = new Random();
                int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[numNivel - 1].Length);
                Palavra = Armazenamento.Armazenamento.Palavras[numNivel - 1][ind];
                PalavraPontuacao = 1;
            }

            if (numNivel == 2)
            {
                Random rd = new Random();
                int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[numNivel - 1].Length);
                Palavra = Armazenamento.Armazenamento.Palavras[numNivel - 1][ind];
                PalavraPontuacao = 3;
            }

            if (numNivel == 3)
            {
                Random rd = new Random();
                int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[numNivel - 1].Length);
                Palavra = Armazenamento.Armazenamento.Palavras[numNivel - 1][ind];
                PalavraPontuacao = 5;
            }

            IsVisibleBtnMostrar = false;
            IsVisibleBtnIniciar = true;
        }

        private void IniciarAction()
        {
            IsVisibleBtnIniciar = false;
            IsVisibleContainerContagem = true;

            int i = Armazenamento.Armazenamento.Jogo.TempoPalavra;
            TextoContagem = i.ToString();
            i--;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TextoContagem = i.ToString();
                i--;

                if (i < 0)
                {
                    TextoContagem = "Esgotou o tempo";
                }

                return true;
            });
        }

        private void ErrouAction()
        {
            GoProximoGrupo();
        }

        private void AcertouAction()
        {
            Grupo.Pontuacao += PalavraPontuacao;
            GoProximoGrupo();
        }

        private void GoProximoGrupo()
        {
            Grupo grupo;
            if (Armazenamento.Armazenamento.Jogo.Grupo1 == Grupo)
            {
                grupo = Armazenamento.Armazenamento.Jogo.Grupo2;
            }
            else
            {
                grupo = Armazenamento.Armazenamento.Jogo.Grupo1;
                Armazenamento.Armazenamento.RodadaAtual++;
            }

            if (Armazenamento.Armazenamento.RodadaAtual > Armazenamento.Armazenamento.Jogo.Rodadas)
            {
                App.Current.MainPage = new View.Resultado();
            }
            else
                App.Current.MainPage = new View.Jogo(grupo);
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
