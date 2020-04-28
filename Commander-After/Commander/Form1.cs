using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commander
{
    public partial class Form1 : Form
    {
        private Task tarea;
        CancellationTokenSource cancellationSource = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtComando.Text == "cancelar")
            {
                cancellationSource.Cancel();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            CancellationToken token = cancellationSource.Token;

            txtEstado.Text = "Ejecutando...";
            tarea = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }
                }
            }, token);

            Task tarea2 = tarea.ContinueWith((antecesor) =>
            {
                if (antecesor.Exception != null)
                {
                    txtEstado.Text = antecesor.Exception.Message;
                } else if (antecesor.IsCanceled)
                {
                    txtEstado.Text = "Cancelado";
                } else
                {
                    txtEstado.Text = "Finalizó";
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
