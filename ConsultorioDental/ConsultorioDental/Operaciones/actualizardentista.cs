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
    public partial class actualizardentista : Form
    {
        private formularios.menudentista _dtm;
        public actualizardentista(formularios.menudentista dtm)
        {
            InitializeComponent();
            _dtm = dtm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void datos(int id,string nombre,string apellido,string especialidad,string telefono,string email,string direccion, string estado)
        {
            textBox4.Text =Convert.ToString(id);
            textBox1.Text = nombre;
            textBox2.Text = apellido;
            textBox3.Text = especialidad;
            maskedTextBox1.Text = telefono;
            textBox5.Text = email;
            textBox6.Text = direccion;
            comboBox1.SelectedItem = estado;
            
        }
        private void actualizardentista_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) && maskedTextBox1.Text.Length > 10)
            {
                int id = Convert.ToInt32(textBox4.Text);
                try
                {
                    var db = new Db.dbconsultorioEntities1(); ;
                    var actualizar = db.dentistas.Find(id);

                    DialogResult rs = MessageBox.Show("Desea actualizar este Dentista?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (actualizar != null)
                            {
                                actualizar.nombre= textBox1.Text;
                                actualizar.apellido = textBox2.Text;
                                actualizar.especialidad = textBox3.Text;
                                actualizar.telefono = maskedTextBox1.Text;
                                actualizar.email = textBox5.Text;
                                actualizar.direccion = textBox6.Text;
                                actualizar.estado =Convert.ToString( comboBox1.SelectedItem);
                                db.SaveChanges();
                               _dtm.cargardatagridview();
                                this.Dispose();
                                MessageBox.Show("Dato actualizado");

                            }
                            else
                            {
                                MessageBox.Show("Dentista no encontrado");
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
