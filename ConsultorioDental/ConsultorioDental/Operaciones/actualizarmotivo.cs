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
    public partial class actualizarmotivo : Form
    {
        private formularios.menumotivo _dtm;
        public actualizarmotivo(formularios.menumotivo dtm)
        {
            InitializeComponent();
            _dtm = dtm;
        }
        public void datos(int id, string nombre)
        {
            textBox8.Text = Convert.ToString(id);
            textBox1.Text = nombre;

        }
        private void actualizarmotivo_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                int id = Convert.ToInt32(textBox8.Text);
                try
                {
                    var db = new Db.dbconsultorioEntities1(); ;
                    var actualizar = db.motivoes.Find(id);

                    DialogResult rs = MessageBox.Show("Desea actualizar este Motivo?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (actualizar != null)
                            {

                                actualizar.motivo1 = textBox1.Text;
                                db.SaveChanges();
                                _dtm.cargardatagridview();
                                this.Dispose();
                                MessageBox.Show("Dato actualizado");

                            }
                            else
                            {
                                MessageBox.Show("Motivo no encontrado");
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
