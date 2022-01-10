using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Presupuestador
{
    public partial class Form1 : Form
    {
        string sFecha = DateTime.Now.ToString("dd/MM/yyyy");
        string sBasico;
        public Form1()
        {
            InitializeComponent();
            textFecha.Text = sFecha;
            sBasico = textCondiciones.Rtf;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string condicione = RtfPipe.Rtf.ToHtml(textCondiciones.Rtf);
            string sCuerpo = RtfPipe.Rtf.ToHtml(textCuerpo.Rtf);
            string sImagen = pictureBox1.ImageLocation;
            string sAdicionales = richTextBox4.Text;


            htmlChange.llenarHtml(textCliente.Text, textPresupuesto.Text, textFecha.Text, textProyecto.Text, textMonto.Text, comboMoneda.SelectedItem.ToString(), textTimming.Text, textEntrega.Text, condicione, textEncabezado.Text, sCuerpo, sImagen, saveFileDialog1.FileName);
        }


        private void button6_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog()==DialogResult.OK)
            {
                textCondiciones.SelectionFont = fontDialog1.Font;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            monthCalendar1.Enabled = true;
            monthCalendar1.Visible = true;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime fecha = monthCalendar1.SelectionStart;
            textFecha.Text = fecha.ToString("dd/MM/yyyy");
            monthCalendar1.Enabled = false;
            monthCalendar1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            if (i == 0)
            {

                pictureBox1.Load("https://i.imgur.com/0YKyhsD.png");

                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                //pictureBox1.Image = Properties.Resources.icon1;
            }
            if (i == 1)
            {
                pictureBox1.Load("https://i.imgur.com/tiUNKgF.png");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                //pictureBox1.Image = Properties.Resources.icon2; pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            if (i == 2)
            {
                pictureBox1.Load("https://i.imgur.com/LCzNOEP.png");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                //pictureBox1.Image = Properties.Resources.icon3; pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textCondiciones.Rtf = sBasico;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            monthCalendar2.Enabled = true;
            monthCalendar2.Visible = true;

        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime fecha = monthCalendar2.SelectionStart;
            textEntrega.Text = fecha.ToString("dd/MM/yyyy");
            monthCalendar2.Enabled = false;
            monthCalendar2.Visible = false;
            DateTime fechaAnterior = DateTime.ParseExact(textFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TimeSpan span = fecha.Subtract(fechaAnterior);
            textTimming.Text = span.Days.ToString() + " días";

        }
    }
}
