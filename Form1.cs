using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Series
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Adicionar();
        }

        private void btn_adicionar_Click(object sender, EventArgs e)
        {
            if(tb_ano.Text != "" && tb_genero.Text != "" && tb_nome.Text != "")
            {
                Novo serie = new Novo();
                serie.nome = tb_nome.Text;
                serie.genero = tb_genero.Text;
                serie.ano = Convert.ToInt32(tb_ano.Text);
                Banco.NovoSerie(serie);

                string[] item = new string[3];
                item[0] = tb_nome.Text;
                item[1] = tb_genero.Text;
                item[2] = tb_ano.Text;
                lv_series.Items.Add(new ListViewItem(item));

                tb_nome.Clear();
                tb_genero.Clear();
                tb_ano.Clear();
            }
            else
            {
                MessageBox.Show("Preencha todos os campos");
            }
           
        }
        private void Adicionar()
        {
            lv_series.Columns.Add("Nome", 259);
            lv_series.Columns.Add("Gênero", 240);
            lv_series.Columns.Add("Ano", 100).TextAlign = HorizontalAlignment.Center;
            lv_series.View = View.Details;
            lv_series.FullRowSelect = true;
            lv_series.GridLines = true;
            lv_series.MultiSelect = false;

            DataTable dt = new DataTable();
            dt = Banco.Todos();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] item = new string[3];
                item[0] = dt.Rows[i].Field<string>("S_NOME");
                item[1] = dt.Rows[i].Field<string>("S_GENERO");
                item[2] = dt.Rows[i].Field<long>("I_ANO").ToString();
                lv_series.Items.Add(new ListViewItem(item));
            }
        }

        private void lv_series_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = lv_series.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;
            Int32 i = lv_series.Items.IndexOf(lv_series.SelectedItems[0])+1;

            if (item != null)
            {
                Atualizar s = new Atualizar();

                Button btn_ok = new Button();
                btn_ok.Text = "Atualizar";
                btn_ok.Location = new Point(534, 31);
                btn_ok.DialogResult = DialogResult.OK;
                s.AcceptButton = btn_ok;
                s.Controls.Add(btn_ok);

                Button btn_cancel = new Button();
                btn_cancel.Text = "Cancelar";
                btn_cancel.Location = new Point(534, 92);
                btn_cancel.DialogResult = DialogResult.Cancel;
                s.CancelButton = btn_cancel;
                s.Controls.Add(btn_cancel);

                TextBox tb_Fname = new TextBox();
                tb_Fname.Location = new Point(12, 32);
                tb_Fname.Size = new Size(190, 23);
                tb_Fname.Text = item.SubItems[0].Text;
                s.Controls.Add(tb_Fname);

                TextBox tb_Fgenero = new TextBox();
                tb_Fgenero.Location = new Point(237, 32);
                tb_Fgenero.Size = new Size(171, 23);
                tb_Fgenero.Text = item.SubItems[1].Text;
                s.Controls.Add(tb_Fgenero);

                TextBox tb_Fano = new TextBox();
                tb_Fano.Location = new Point(467, 32);
                tb_Fano.Size = new Size(55, 23);
                tb_Fano.Text = item.SubItems[2].Text;
                s.Controls.Add(tb_Fano);

                s.ShowDialog();

                if (s.DialogResult == DialogResult.OK)
                {
                    if (tb_Fname.Text == "" || tb_Fgenero.Text == "" || tb_Fano.Text == "")
                    {
                        MessageBox.Show("Preencha todos os campos.");
                    }
                    else
                    {
                        Novo n = new Novo();
                        n.nome = tb_Fname.Text;
                        n.genero = tb_Fgenero.Text;
                        n.ano = Convert.ToInt32(tb_Fano.Text);
                        n.index = i;
                        Banco.AtualizarSerie(n);

                        item.SubItems[0].Text = tb_Fname.Text;
                        item.SubItems[1].Text = tb_Fgenero.Text;
                        item.SubItems[2].Text = tb_Fano.Text;
                    }
                }
            }
            else
            {
                this.lv_series.SelectedItems.Clear();
                MessageBox.Show("Selecione alguma serie antes de continuar.");
            }            
        }

        private void btn_deletar_Click(object sender, EventArgs e)
        {
            if(lv_series.SelectedItems.Count == 1)
            {
                Int32 i = lv_series.Items.IndexOf(lv_series.SelectedItems[0]) + 1;

                Novo n = new Novo();
                n.index = i;
                Banco.DeletarSerie(n);
                
                lv_series.SelectedItems[0].SubItems.Clear();
            }
        }

        private void tb_ano_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
