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
    public partial class registromotivo : Form
    {
        private formularios.menumotivo _dtm;
        public registromotivo(formularios.menumotivo dtm)
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
        private void registromotivo_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                try
                {


                    DialogResult rs = MessageBox.Show("Desea Registrar este Motivo?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:

                            var context = new Db.dbconsultorioEntities1();
                            var motivo= new Db.motivo
                            {
                                motivo1 = textBox1.Text,
                            };
                            context.motivoes.Add(motivo);
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
