using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;

namespace ConsultorioDental.formularios
{
    public partial class menucitas : Form
    {
        private DataTable tabla;
        private string buscar = "Busqueda por ID.....";
        public menucitas()
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
            tabla.Columns.Add("Paciente", typeof(string));
            tabla.Columns.Add("Dentista", typeof(string));
            tabla.Columns.Add("Motivo", typeof(string));
            tabla.Columns.Add("fecha", typeof(DateTime));
            tabla.Columns.Add("hora", typeof(string));
            tabla.Columns.Add("tiemporestante", typeof(string));
            tabla.Columns.Add("Estado", typeof(string));
        }

        public class CitaDto
        {
            public int CitaID { get; set; } 
            public int? PacienteID { get; set; }
            public string PacienteNombre { get; set; }  
            public string PacienteApellido { get; set; } 
            public int? DentistaID { get; set; }
            public string DentistaNombre { get; set; }  
            public string DentistaApellido { get; set; } 
            public DateTime Fecha { get; set; }
            public TimeSpan Hora { get; set; }
            public int? MotivoID { get; set; }
            public string Motivo { get; set; } 
            public int DiasRestantes { get; set; }
            public int HorasRestantes { get; set; }
            public string Estado { get; set; }
        }

        public void cargardatagridview()
        {
            try
            {
                using (var db = new Db.dbconsultorioEntities1())
                {
                   
                    tabla.Clear();

                    var citas = db.Database.SqlQuery<CitaDto>("EXEC procesarcitas").ToList();

                    if (citas != null && citas.Any())
                    {
                        foreach (var datos in citas)
                        {

                            
                            int diarestante = datos.DiasRestantes; 
                            int horarestante = datos.HorasRestantes; 
                            //el siguiente codigo le daremos formato de 12horas,AM,PM a nuestro dato hora de la base de datos 
                            TimeSpan valorhora = datos.Hora;
                            int horas= valorhora.Hours;
                            int minutos = valorhora.Minutes;
                            string periodo = (horas>=12)?"PM":"AM";
                            int mostrarhoras = horas % 12;
                            mostrarhoras = (mostrarhoras==0)?12:mostrarhoras;
                            string formatohora = $"{mostrarhoras:D2}:{minutos:D2} {periodo}";
                            tabla.Rows.Add(
                                datos.CitaID,
                                $"{datos.PacienteNombre}  {datos.PacienteApellido}", 
                                $"{datos.DentistaNombre}  {datos.DentistaApellido}",   
                                datos.Motivo,
                                datos.Fecha,
                                formatohora,
                                $"{diarestante} Días {horarestante}  Horas restantes",
                                datos.Estado
                            );
                        }
                        cargardatos();
                    }
                    else
                    {
                        MessageBox.Show("No citas encontradas.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
        }



        private void cargardatos()
        {
            dataGridView1.Rows.Clear();

            foreach (DataRow row in tabla.Rows)
            {
                int fila = dataGridView1.Rows.Add();
                dataGridView1.Rows[fila].Cells["ID"].Value = row["ID"];
                dataGridView1.Rows[fila].Cells["Paciente"].Value = row["paciente"];
                dataGridView1.Rows[fila].Cells["Dentista"].Value = row["dentista"];
                dataGridView1.Rows[fila].Cells["Motivo"].Value = row["motivo"];
                dataGridView1.Rows[fila].Cells["fecha"].Value = row["fecha"];
                dataGridView1.Rows[fila].Cells["hora"].Value = row["hora"];
                dataGridView1.Rows[fila].Cells["tiemporestante"].Value = row["tiemporestante"];
                dataGridView1.Rows[fila].Cells["Estado"].Value = row["estado"];
               
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
        private void menucitas_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cargardatagridview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form openform = Application.OpenForms["registrocitas"];
            if (openform == null)
            {
                Operaciones.registrocitas registro = new Operaciones.registrocitas(this);
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
                            dataGridView1.Rows[num].Cells["Paciente"].Value = filas["paciente"];
                            dataGridView1.Rows[num].Cells["Dentista"].Value = filas["dentista"];
                            dataGridView1.Rows[num].Cells["Motivo"].Value = filas["motivo"];
                            dataGridView1.Rows[num].Cells["Fecha"].Value = filas["fecha"];
                            dataGridView1.Rows[num].Cells["Hora"].Value = filas["hora"];
                            dataGridView1.Rows[num].Cells["tiemporestante"].Value = filas["tiemporestante"];
                            dataGridView1.Rows[num].Cells["Estado"].Value = filas["estado"];
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
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
                        dataGridView1.Rows[fila].Cells["Paciente"].Value = vista["paciente"];
                        dataGridView1.Rows[fila].Cells["Dentista"].Value = vista["dentista"];
                        dataGridView1.Rows[fila].Cells["Motivo"].Value = vista["motivo"];
                        dataGridView1.Rows[fila].Cells["Fecha"].Value = vista["fecha"];
                        dataGridView1.Rows[fila].Cells["Hora"].Value = vista["hora"];
                        dataGridView1.Rows[fila].Cells["tiemporestante"].Value = vista["tiemporestante"];
                        dataGridView1.Rows[fila].Cells["Estado"].Value = vista["estado"];
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
            if (e.ColumnIndex == 8 || e.ColumnIndex == 9)
            {
                this.Cursor = Cursors.Hand;

            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog exportar = new SaveFileDialog();
                exportar.Filter = "CSV files (*.csv) |*.csv|All files (*.*) | *.*";
                exportar.Title = "Guardar el archivo csv";
                if (exportar.ShowDialog() == DialogResult.OK)
                {
                    string rutaarchivo = exportar.FileName;
                    StringBuilder contenido = new StringBuilder();

                    foreach (DataGridViewColumn columna in dataGridView1.Columns)
                    {
                        contenido.Append($"\"{columna.HeaderText.Trim()}\"");
                        contenido.Append(",");
                    }
                    contenido.Length--;
                    contenido.AppendLine();

                    foreach (DataGridViewRow fila in dataGridView1.Rows)
                    {
                        for (int i = 0; i < fila.Cells.Count; i++)
                        {
                            contenido.Append($"\"{fila.Cells[i].Value?.ToString().Replace("\"", "\"\"").Trim()}\"");
                            contenido.Append(",");
                        }
                        contenido.Length--;
                        contenido.AppendLine();
                    }
                    File.WriteAllText(rutaarchivo, contenido.ToString(), Encoding.UTF8);
                    MessageBox.Show("Datos exportados correctamente en la siguiente direccion:" + rutaarchivo);
                }
            }
            else
            {
                MessageBox.Show("No hay datos que exportar");
            }
        }
       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int pacienteid;
            int dentistaid;
            int motivoid;
            if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
            {
                    if (e.ColumnIndex == 8)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    string paciente = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string dentista = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string motivo = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    DateTime fecha = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    DateTime hora = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    string tiemporestante = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    string estado = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    try
                    {
                        var db = new Db.dbconsultorioEntities1();
                        var ids = db.citas.Find(id);

                         pacienteid = ids.pacienteid;
                         dentistaid = ids.dentistaid;
                         motivoid = ids.motivoid;

                         Form openform = Application.OpenForms["actualizarcitas"];
                        if (openform==null) {
                            Operaciones.actualizarcitas actualizar = new Operaciones.actualizarcitas(this);
                            actualizar.datos(id, paciente, dentista, motivo, fecha, hora, estado, pacienteid, dentistaid, motivoid,tiemporestante);
                            actualizar.Show();
                        }
                        else
                        {
                            openform.BringToFront();
                        }
                    }
                    catch (Exception t)
                    {
                        MessageBox.Show($"error{t}");
                    }

                    
                }
                else if (e.ColumnIndex == 9)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    try
                    {
                        var db = new Db.dbconsultorioEntities1(); ;
                        var delete = db.citas.Find(id);
                        DialogResult rs = MessageBox.Show("Desea eliminar esta Cita?", "Aviso", MessageBoxButtons.OKCancel);
                        switch (rs)
                        {
                            case DialogResult.OK:
                                if (delete != null)
                                {
                                    db.citas.Remove(delete);
                                    db.SaveChanges();
                                    button2.PerformClick();

                                    MessageBox.Show("Cita correctamente eliminada");
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
            }
        }
    }
}
