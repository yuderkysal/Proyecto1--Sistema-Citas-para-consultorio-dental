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
    public partial class actualizarpacientes : Form
    {
        private formularios.menupacientes _dtm;
        public actualizarpacientes(formularios.menupacientes dtm)
        {
            InitializeComponent();
            _dtm = dtm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void datos(int id, string nombre, string apellido,string telefono, string email, string direccion, string alergias,string historial,string medicamentos,DateTime fecha)
        {
            textBox8.Text = Convert.ToString(id);
            textBox1.Text = nombre;
            textBox2.Text = apellido;
            maskedTextBox1.Text = telefono;
            textBox5.Text = email;
            textBox6.Text = direccion;
            textBox3.Text = alergias;
            textBox4.Text = historial;
            textBox7.Text = medicamentos;
            label10.Text =Convert.ToString(fecha);
            

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)
                && !string.IsNullOrWhiteSpace(textBox6.Text) && maskedTextBox1.Text.Length > 10
                && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                int id = Convert.ToInt32(textBox8.Text);
                try
                {
                    var db = new Db.dbconsultorioEntities1(); ;
                    var actualizar = db.pacientes.Find(id);

                    DialogResult rs = MessageBox.Show("Desea actualizar este Paciente?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (actualizar != null)
                            {

                                actualizar.nombre = textBox1.Text;
                                textBox2.Text = actualizar.apellido;
                                actualizar.telefono=maskedTextBox1.Text;
                                actualizar.email=textBox5.Text ;
                                actualizar.direccion=textBox6.Text;
                                actualizar.alergias=textBox3.Text;
                                actualizar.historialdental=textBox4.Text;
                                actualizar.medicamentos=textBox7.Text;

                                db.SaveChanges();
                                _dtm.cargardatagridview();
                                this.Dispose();
                                MessageBox.Show("Dato actualizado");

                            }
                            else
                            {
                                MessageBox.Show("Paciente no encontrado");
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

        private void actualizarpacientes_Load(object sender, EventArgs e)
        {

        }
    }
}
