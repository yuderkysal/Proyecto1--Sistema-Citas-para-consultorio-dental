using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultorioDental.formularios
{
    public partial class menumotivo : Form
    {
        private DataTable tabla;
        private string buscar = "Busqueda por ID.....";
        public menumotivo()
        {
            InitializeComponent();
            creaciondatatable();
            cargardatagridview();
            textboxbuscar();
            textBox1.GotFocus += textbox1_gotfocus;
            textBox1.LostFocus += textbox1_lostfocus;
        }
        private void creaciondatatable()
        {
            tabla = new DataTable();
            tabla.Columns.Add("ID", typeof(int));
            tabla.Columns.Add("Motivo", typeof(string));
       
        }

        public void cargardatagridview()
        {

            try
            {
                var db = new Db.dbconsultorioEntities1();
                tabla.Clear();
                var motivo = db.motivoes.ToList();
                if (motivo != null)
                {
                    foreach (var datos in motivo)
                    {
                        tabla.Rows.Add(
                             datos.motivoid,
                             datos.motivo1);

                    }
                    cargardatos();
                }
                else
                {

                }


            }
            catch (Exception t)
            {
                MessageBox.Show("Error:" + t.Message);
            }
        }
        private void cargardatos()
        {
            dataGridView1.Rows.Clear();

            foreach (DataRow row in tabla.Rows)
            {
                int fila = dataGridView1.Rows.Add();
                dataGridView1.Rows[fila].Cells["ID"].Value = row["ID"];
                dataGridView1.Rows[fila].Cells["Motivo"].Value = row["motivo"];
                
            }

        }
        private void textbox1_gotfocus(object sender, EventArgs e)
        {
            if (textBox1.Text == buscar)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox1_lostfocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = buscar;
                textBox1.ForeColor = Color.Gray;
            }
        }
        private void textboxbuscar()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = buscar;
                textBox1.ForeColor = Color.Gray;
            }
            else if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = buscar;
                textBox1.ForeColor = Color.Gray;
            }
        }
        private void menumotivo_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form openform = Application.OpenForms["registromotivo"];
            if (openform == null)
            {
                Operaciones.registromotivo registro = new Operaciones.registromotivo(this);
                registro.Show();
            }
            else
            {
                openform.BringToFront();
            }
        }
       
        

        private void button1_Click(object sender, EventArgs e)
        {
            int ts;
            if (int.TryParse(textBox1.Text, out ts))
            {

                dataGridView1.Rows.Clear();
                DataRow[] fila = tabla.Select($"ID={ts}");

                try
                {
                    if (fila.Length > 0)
                    {
                        foreach (DataRow filas in fila)
                        {
                            int num = dataGridView1.Rows.Add();
                            dataGridView1.Rows[num].Cells["ID"].Value = filas["ID"];
                            dataGridView1.Rows[num].Cells["Motivo"].Value = filas["motivo"];
                            
                        }

                    }
                    else
                    {
                        MessageBox.Show("Dato no encontrado");
                    }
                    textBox1.Clear();
                    textBox1.Select();
                }
                catch (Exception p)
                {
                    MessageBox.Show($"Error:{p}");
                }
            }
            else
            {
                MessageBox.Show("Digite un ID valido");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cargardatagridview();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                if (tabla == null)
                {
                    MessageBox.Show("el datatable no esta iniciado");
                }
                else
                {
                    if (comboBox1.SelectedItem?.ToString() == "Orden ascendente")
                    {
                        tabla.DefaultView.Sort = "ID ASC";
                    }
                    else if (comboBox1.SelectedItem?.ToString() == "Orden descendente")
                    {
                        tabla.DefaultView.Sort = "ID DESC";
                    }
                    dataGridView1.Rows.Clear();
                    foreach (DataRowView vista in tabla.DefaultView)
                    {
                        int fila = dataGridView1.Rows.Add();
                        dataGridView1.Rows[fila].Cells["ID"].Value = vista["ID"];
                        dataGridView1.Rows[fila].Cells["Motivo"].Value = vista["motivo"];
                       
                    }


                }
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                this.Cursor = Cursors.Hand;

            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                    Form openform = Application.OpenForms["actualizarmotivo"];
                    if (openform==null)
                    {
                        Operaciones.actualizarmotivo actualizar = new Operaciones.actualizarmotivo(this);
                        actualizar.datos(id, nombre);
                        actualizar.Show();
                    }
                    else
                    {
                        openform.BringToFront();
                    }
                   
                }
                else if (e.ColumnIndex == 3)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    try
                    {
                        var db = new Db.dbconsultorioEntities1(); ;
                        var delete = db.motivoes.Find(id);
                        DialogResult rs = MessageBox.Show("Desea eliminar este Motivo?", "Aviso", MessageBoxButtons.OKCancel);
                        switch (rs)
                        {
                            case DialogResult.OK:
                                if (delete != null)
                                {
                                    db.motivoes.Remove(delete);
                                    db.SaveChanges();
                                    button2.PerformClick();

                                    MessageBox.Show("Motivo correctamente eliminado");
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
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
        }
    }
}
