using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultorioDental
{
    public partial class Home : Form
    { //Variables para crear la seleccion de color del menu
        private Color btncolor = Color.White; 
        private Color btnmouse = Color.Blue; 
        private Button btnselecion;
        public Home()
        {
            InitializeComponent();
            //este es sera el panel por defecto al iniciar el programa
            cargarformulario(new formularios.menuprincipal());
        }
        //esta parte son las acciones agregadas a los eventos click del programa
        private void button1_Click(object sender, EventArgs e)
        {
            cargarformulario(new formularios.menuprincipal());
            label1.Text = "Menu Principal.";
            pictureBox2.Image = Properties.Resources.icons8_overview_26;

            if (btnselecion != null)
            {
                btnselecion.BackColor = btncolor;
                btnselecion.ForeColor = Color.Black;
            }
            if (sender is Button btn)
            {
                btn.BackColor = btnmouse;
                btnselecion = btn;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cargarformulario(new formularios.menudentista());
            label1.Text = "Formulario para manejo de Dentistas.";
            pictureBox2.Image = Properties.Resources.dentist;

            if (btnselecion != null)
            {
                btnselecion.BackColor = btncolor;
                btnselecion.ForeColor = Color.Black;
            }
            if (sender is Button btn)
            {
                btn.BackColor = btnmouse;
                btnselecion = btn;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cargarformulario(new formularios.menupacientes());
            label1.Text = "Formulario para manejo de Pacientes.";
            pictureBox2.Image = Properties.Resources.icons8_health_checkup_26;

            if (btnselecion != null)
            {
                btnselecion.BackColor = btncolor;
                btnselecion.ForeColor = Color.Black;
            }
            if (sender is Button btn)
            {
                btn.BackColor = btnmouse;
                btnselecion = btn;
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            cargarformulario(new formularios.menumotivo());
            label1.Text = "Formulario para manejo de Motivo.";
            pictureBox2.Image = Properties.Resources.icons8_prescription_261;

            if (btnselecion != null)
            {
                btnselecion.BackColor = btncolor;
                btnselecion.ForeColor = Color.Black;
            }
            if (sender is Button btn)
            {
                btn.BackColor = btnmouse;
                btnselecion = btn;
            }
        }



        private void button8_Click(object sender, EventArgs e)
        {
            cargarformulario(new formularios.menucitas());
            label1.Text = "Formulario para manejo de Citas.";
            pictureBox2.Image = Properties.Resources.icons8_dentist_time_26;

            if (btnselecion != null)
            {
                btnselecion.BackColor = btncolor;
                btnselecion.ForeColor = Color.Black;
            }
            if (sender is Button btn)
            {
                btn.BackColor = btnmouse;
                btnselecion = btn;
            }


        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        //estos eventos se encargan de cambiar el puntero del mouse y color de los botones cuando el mouse esta sobre ellos
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn && btn!=btnselecion)
            {
                btn.BackColor = btnmouse;
                btn.Cursor = Cursors.Hand;
                btn.ForeColor = Color.White;
            }
           
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnselecion)
            {
                btn.BackColor = btncolor;
                btn.Cursor = Cursors.Default;
                btn.ForeColor = Color.Black;
            }
          
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnselecion)
            {
                btn.BackColor = btnmouse;
                btn.Cursor = Cursors.Hand;
                btn.ForeColor = Color.White;
            }
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnselecion)
            {
                btn.BackColor = btncolor;
                btn.Cursor = Cursors.Default;
                btn.ForeColor = Color.Black;
            }
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnselecion)
            {
                btn.BackColor = btnmouse;
                btn.Cursor = Cursors.Hand;
                btn.ForeColor = Color.White;
            }
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnselecion)
            {
                btn.BackColor = btncolor;
                btn.Cursor = Cursors.Default;
                btn.ForeColor = Color.Black;
            }
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnselecion)
            {
                btn.BackColor = btnmouse;
                btn.Cursor = Cursors.Hand;
                btn.ForeColor = Color.White;
            }
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnselecion)
            {
                btn.BackColor = btncolor;
                btn.Cursor = Cursors.Default;
                btn.ForeColor = Color.Black;
            }
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {

        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnselecion)
            {
                btn.BackColor = btnmouse;
                btn.Cursor = Cursors.Hand;
                btn.ForeColor = Color.White;
            }
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnselecion)
            {
                btn.BackColor = btncolor;
                btn.Cursor = Cursors.Default;
                btn.ForeColor = Color.Black;
            }
        }
        private void button9_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {

        }
        //estos son las accciones de los botones maximizar y minimizar del formulario madre
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // esta parte se encarga de cargar los formularios al panelprincipal
        private void cargarformulario(Form formulario)
        {
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panelformulario.Controls.Add(formulario);
            panelformulario.Tag = formulario;
            formulario.BringToFront();
            formulario.Show();
        }
        //esta parte agrega un tamaño y color del borde del panel1
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Silver, 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel1.Width - 1, panel1.Height - 1);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Desea cerrar el programa","Aviso",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            switch (rs)
            {
                case DialogResult.OK:
                    Application.Exit();
                    break;
                case DialogResult.Cancel:
                    break;
            }
          
        }
    }
}
