using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gato
{
    public partial class Form1 : Form
    {
        int numeroDeJugada;

        // Hacemos una matriz para que después ver las reglas de ganador sea más fácil
        // ¿No sabes qué es una matriz, o tienes problemas con ellas en C#?
        // Aquí hay un video explicativo (15 minutos): https://www.youtube.com/watch?v=fMwJdZavPBM
        Button[,] botones;

        public Form1()
        {

            InitializeComponent();

            numeroDeJugada = 1;

            // Aquí guardamos los botones que creamos en la interfaz (Form1.cs [Design])
            // Tienen este nombre (button + número) porque es el nombre por defecto que
            // se les da al botón al crearlo. Cuando agregar un elemento en el archivo Form1.cs [Design],
            // se crea una propiedad con el nombre del elemento para que puedas referenciarlos.

            // Como creé 9 botones, aquí están. Eso sí, hay que asegurarse que el orden que aparece aquí
            // es igual al como están en la interfaz (ver propiedades de cada botón en el archivo Form1.cs)
            botones = new Button[,] { 
                { button1, button2, button3 },
                { button4, button5, button6 },
                { button7, button8, button9 },
            };

            foreach (Button button in Controls)
            {
                button.Click += button_Click;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            // "sender" es el que originó el evento...
            // Como sabemos que button_Click se llama cuando hacemos click en un cierto botón,
            // "sender" corresponderá al botón que estamos haciéndole click. Pero siempre vendrá 
            // en una variable "object"... así que tenemos que "transformalo" a Button:
            Button button = (Button)sender;

            if (numeroDeJugada % 2 == 0)
            {
                button.Text = "X";
            }
            else
            {
                button.Text = "O";
            }

            // Deshabilitamos el botón para que no podamos volver a hacerle click:
            button.Enabled = false;

            // Revisar si ya hay un ganador. Tenemos que ver las filas, columnas y diagonales
            string ganador = ObtenerGanador();

            if (ganador != null)
            {
                // Deshabilitamos todos los botones
                foreach(Button boton in botones)
                {
                    boton.Enabled = false;
                }

                // Mostramos al ganador
                MessageBox.Show($"El ganador es {ganador}");
            }

            numeroDeJugada += 1;    
        }

        private string ObtenerGanador()
        {
            // Filas!
            for (int fila = 0; fila < 3; fila++)
            {
                string celda1 = botones[fila, 0].Text;
                string celda2 = botones[fila, 1].Text;
                string celda3 = botones[fila, 2].Text;
                if (VerificarCeldas(celda1, celda2, celda3))
                {
                    return celda1; // Esto hará que el for y la función termine su ejecución
                }
            }

            // Columnas!
            for (int columna = 0; columna < 3; columna++)
            {
                string celda1 = botones[0, columna].Text;
                string celda2 = botones[1, columna].Text;
                string celda3 = botones[2, columna].Text;
                if (VerificarCeldas(celda1, celda2, celda3))
                {
                    return celda1; // Esto hará que el for y la función termine su ejecución
                }
            }

            // Diagonales...
            if (VerificarCeldas(botones[0, 0].Text, botones[1, 1].Text, botones[2, 2].Text))
            {
                return botones[0, 0].Text;
            }

            if (VerificarCeldas(botones[2, 0].Text, botones[1, 1].Text, botones[0, 2].Text))
            {
                return botones[2, 0].Text;
            }

            return null; // No hubo ganador :c
        }

        /**
         * Esta función compara que 3 celdas sean iguales y distintas de vacío
         */
        private bool VerificarCeldas(string celda1, string celda2, string celda3)
        {
            return celda1 != "" && celda1 == celda2 && celda2 == celda3;
        }
    }
}
