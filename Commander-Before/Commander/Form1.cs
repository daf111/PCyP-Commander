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
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtComando.Text == "cancelar")
            {
                MessageBox.Show("Cancelado");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            tarea = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (txtComando.Text == "")
                    {
                        txtComando.Text = "Ejecutando...";
                    }
                    else
                    {
                        txtComando.Text = "";
                    }
                    Thread.SpinWait(int.MaxValue);
                }
            }, System.Threading.CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
