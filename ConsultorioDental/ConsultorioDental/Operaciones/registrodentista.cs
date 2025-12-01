using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity.Validation;

namespace ConsultorioDental.Operaciones
{
    public partial class registrodentista : Form
    {
        private formularios.menudentista _dtm;
        public registrodentista(formularios.menudentista dtm)
        {
            InitializeComponent();
            _dtm = dtm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        
        }
        
        private void registrodentista_Load(object sender, EventArgs e)
        {
            textBox1.Select();
        }
        private void limpiar(Control crt)
        {
            
            foreach (Control ct in crt.Controls )
            {
                if (ct is TextBox)
                {
                    ct.Text = "";
                }else if (ct is MaskedTextBox)
                {
                    ct.Text = "___-___-_____";
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) && maskedTextBox1.Text.Length > 10)
            {
               
                try
                {
                   

                    DialogResult rs = MessageBox.Show("Desea Registrar este Dentista?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:

                            var context = new Db.dbconsultorioEntities1();
                            var dentistas = new Db.dentista
                            {
                                nombre = textBox1.Text,
                                apellido = textBox2.Text,
                                especialidad = textBox3.Text,
                                telefono = maskedTextBox1.Text,
                                email = textBox5.Text,
                                direccion = textBox6.Text,
                                estado = Convert.ToString("Activo"),
                                fecharegistro = DateTime.Now
                            };
                            context.dentistas.Add(dentistas);
                            context.SaveChanges();
                            _dtm.cargardatagridview();
                            limpiar(this);
                            MessageBox.Show("Datos correctamente Registrados");
                            
                          
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Debe completar todos los campos para poder registrar los datos");
            }
        }

       
    }
}
