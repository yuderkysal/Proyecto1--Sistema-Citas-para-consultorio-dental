using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultorioDental.Operaciones
{
    public partial class actualizarcitas : Form
    {
        private formularios.menucitas _dtm;
        public actualizarcitas(formularios.menucitas dtm)
        {
            InitializeComponent();
            _dtm = dtm;
        }

        public void datos(int id,string paciente,string dentista,string motivo,DateTime fecha,
                         DateTime hora,string estado,int pacienteid,int dentistaid,int motivoid,string tiemporestante)
        {
            textBox7.Text = Convert.ToString(id);
            textBox1.Text = Convert.ToString(pacienteid);
            textBox5.Text = Convert.ToString(motivoid);
            textBox3.Text = Convert.ToString(dentistaid);
            textBox2.Text =paciente;
            textBox6.Text = motivo;
            textBox4.Text = dentista;
            dateTimePicker1.Value = fecha;
            dateTimePicker2.Value = hora;
            label7.Text = $"Estado: {estado}";
            label8.Text = $"{tiemporestante}";




        }
        public void paciente(int id, string nombre)
        {
            textBox1.Text = Convert.ToString(id);
            textBox2.Text = nombre;

        }
        public void dentista(int id, string nombre)
        {
            textBox3.Text = Convert.ToString(id);
            textBox4.Text = nombre;

        }
        public void motivo(int id, string nombre)
        {
            textBox5.Text = Convert.ToString(id);
            textBox6.Text = nombre;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form openform = Application.OpenForms["añadirdatos"];
            if (openform == null)
            {
                string pacientes = button2.Text;
                Operaciones.añadirdatos add = new Operaciones.añadirdatos(null,this);
                add.combo(pacientes);
                add.Show();
            }
            else
            {
                openform.BringToFront();
            }
        }

        private void actualizarcitas_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form openform = Application.OpenForms["añadirdatos"];
            if (openform == null)
            {
                string motivo = button5.Text;
                Operaciones.añadirdatos add = new Operaciones.añadirdatos(null, this);
                add.combo(motivo);
                add.Show();
            }
            else
            {
                openform.BringToFront();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form openform = Application.OpenForms["añadirdatos"];
            if (openform == null)
            {
                string dentista = button4.Text;
                Operaciones.añadirdatos add = new Operaciones.añadirdatos(null, this);
                add.combo(dentista);
                add.Show();
            }
            else
            {
                openform.BringToFront();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)
                && !string.IsNullOrWhiteSpace(textBox6.Text)&& dateTimePicker1.Value!=null&& dateTimePicker2.Value!=null
                && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                int id = Convert.ToInt32(textBox7.Text);
                try
                {
                    var db = new Db.dbconsultorioEntities1(); ;
                    var actualizar = db.citas.Find(id);

                    DialogResult rs = MessageBox.Show("Desea actualizar esta Cita?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (actualizar != null)
                            {
                                DateTime tiempo = dateTimePicker2.Value;
                                TimeSpan horacita = new TimeSpan(tiempo.Hour, tiempo.Minute, 0);

                                actualizar.pacienteid=Convert.ToInt32(textBox1.Text);
                                actualizar.motivoid = Convert.ToInt32(textBox5.Text);
                                actualizar.dentistaid = Convert.ToInt32(textBox3.Text);
                                actualizar.Fecha = dateTimePicker1.Value;
                                actualizar.hora =horacita;

                                db.SaveChanges();
                                _dtm.cargardatagridview();
                                this.Dispose();
                                MessageBox.Show("Dato actualizado");

                            }
                            else
                            {
                                MessageBox.Show("Cita no encontrada");
                            }
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }
                catch (Exception p)
                {
                    MessageBox.Show($"Error:{p}");
                }

            }
            else
            {
                MessageBox.Show("verifique que todos los campos estan completos");
            }
        }
    }
  }

