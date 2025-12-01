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
    public partial class registrocitas : Form
    {
        private formularios.menucitas _dtm;
       
        public registrocitas(formularios.menucitas dtm)
        {
            InitializeComponent();
            _dtm = dtm;
           

        }

        private void registrocitas_Load(object sender, EventArgs e)
        {
            
        }
        public void paciente (int id,string nombre)
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

        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form openform = Application.OpenForms["añadirdatos"];
            if (openform == null)
            {
                string pacientes = button2.Text;
                Operaciones.añadirdatos add = new Operaciones.añadirdatos(this,null);
                add.combo(pacientes);
                add.Show();
            }
            else
            {
                openform.BringToFront();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form openform = Application.OpenForms["añadirdatos"];
            if (openform == null)
            {
                string motivos = button5.Text;
                Operaciones.añadirdatos add = new Operaciones.añadirdatos(this,null);
                add.combo(motivos);
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
                string dentistas = button4.Text;
                Operaciones.añadirdatos add = new Operaciones.añadirdatos(this,null);
                add.combo(dentistas);
                add.Show();
            }
            else
            {
                openform.BringToFront();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)&&!string.IsNullOrWhiteSpace(textBox3.Text))
            {

                try
                {
                    
                    DialogResult rs = MessageBox.Show("Desea Registrar esta Cita?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            DateTime tiempo = dateTimePicker2.Value;
                            TimeSpan horacita = new TimeSpan(tiempo.Hour,tiempo.Minute,0);
                            var context = new Db.dbconsultorioEntities1();
                            int pacienteId = Convert.ToInt32(textBox1.Text);
                            int dentistaId = Convert.ToInt32(textBox3.Text);
                            int motivoId = Convert.ToInt32(textBox5.Text);
                            var pacienteExists = context.pacientes.Any(p => p.pacienteid == pacienteId);
                            var dentistaExists = context.dentistas.Any(d => d.dentistaid == dentistaId);
                            var motivoExists = context.motivoes.Any(m => m.motivoid == motivoId);

                            if (!pacienteExists || !dentistaExists || !motivoExists)
                            {
                                MessageBox.Show("Algunos de los id no existen");
                                return;  
                            }


                            var citas = new Db.cita
                            {
                                pacienteid = Convert.ToInt32(textBox1.Text),
                                hora = horacita,
                                dentistaid = Convert.ToInt32(textBox3.Text),
                                motivoid = Convert.ToInt32(textBox5.Text),
                                Fecha = dateTimePicker1.Value,
                            };
                            context.citas.Add(citas);
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
    }
}
