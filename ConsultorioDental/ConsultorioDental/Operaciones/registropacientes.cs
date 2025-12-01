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
    public partial class registropacientes : Form
    {
        private formularios.menupacientes _dtm;
        public registropacientes(formularios.menupacientes dtm)
        {
            InitializeComponent();
            _dtm = dtm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void limpiar(Control crt)
        {

            foreach (Control ct in crt.Controls)
            {
                if (ct is TextBox)
                {
                    ct.Text = "";
                }
                else if (ct is MaskedTextBox)
                {
                    ct.Text = "___-___-_____";
                }
            }
        }
        private void registropacientes_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) 
                && !string.IsNullOrWhiteSpace(textBox6.Text) && maskedTextBox1.Text.Length > 10 
                && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {

                try
                {


                    DialogResult rs = MessageBox.Show("Desea Registrar este Paciente?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:

                            var context = new Db.dbconsultorioEntities1();
                            var paciente = new Db.paciente
                            {
                                nombre = textBox1.Text,
                                apellido = textBox2.Text,
                                telefono = maskedTextBox1.Text,
                                email = textBox5.Text,
                                direccion = textBox6.Text,
                                alergias=textBox3.Text,
                                historialdental=textBox4.Text,
                                medicamentos=textBox7.Text,
                                fecharegistro=DateTime.Now
                            };
                            context.pacientes.Add(paciente);
                            context.SaveChanges();
                            _dtm.cargardatagridview();
                            limpiar(this);
                            MessageBox.Show("Datos correctamente Registrados");


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
                MessageBox.Show("Debe completar todos los campos para poder registrar los datos");
            }
        }
    }
}
