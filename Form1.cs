using System;
using System.Drawing;
using System.Windows.Forms;

namespace PointOfSaleApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public double CostOfItems()
        {
            Double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }
            return sum;
        }

        private void AddCost()
        {
            Double tax = 3.9, q;
            if (dataGridView1.Rows.Count > 0)
            {
                q = ((CostOfItems() * tax) / 100);
                lblTax.Text = String.Format("{0:c2}", q);
                lblSubTotal.Text = String.Format("{0:c2}", CostOfItems());
                lblTotal.Text = String.Format("{0:c2}", CostOfItems() + q);
                lblBarCode.Text = Convert.ToString(q + CostOfItems());
            }
        }

        private void Change()
        {
            Double tax = 3.9, q, c;
            if (dataGridView1.Rows.Count > 0)
            {
                q = ((CostOfItems() * tax) / 100) + CostOfItems();
                c = Convert.ToDouble(lblCash.Text);
                lblChange.Text = String.Format("{0:c2}", c - q);
            }
        }

        private void Order(String ProductName, Double CostOfItem)
        {
            dataGridView1.Rows.Add(ProductName, "1", CostOfItem);

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if ((string)row.Cells[0].Value == ProductName)
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value) + 1;
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostOfItem;
                }
            }

            AddCost();
        }

        Bitmap bitmap;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                lblBarCode.Text = "";
                lblCash.Text = "0";
                lblChange.Text = "";
                lblSubTotal.Text = "";
                lblTax.Text = "";
                lblTotal.Text = "";
                cboPayment.Text = "";
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboPayment.Items.Add("Cash");
            cboPayment.Items.Add("Visa Card");
            cboPayment.Items.Add("Master Card");
        }


        private void Numbers(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (lblCash.Text == "0")
            {
                lblCash.Text = "";
                lblCash.Text = b.Text;
            }
            else if (b.Text == ".")
            {
                if (!lblCash.Text.Contains("."))
                {
                    lblCash.Text = lblCash.Text + b.Text;
                }
            }
            else
            {
                lblCash.Text = lblCash.Text + b.Text;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblCash.Text = "0";
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (cboPayment.Text == "Cash")
            {
                Change();
            }
            else
            {
                lblChange.Text = "";
                lblCash.Text = "0";
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }
                AddCost();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nothing To Remove");
            }

        }


        private void BlackCoffee_Click_1(object sender, EventArgs e)
        {
            Order("Black Coffee", 2.3);
        }

        private void btnCoffee_Click(object sender, EventArgs e)
        {
            Order("Coffee", 1.9);
        }

        private void btnFrenchCoffee_Click(object sender, EventArgs e)
        {
            Order("French Coffee", 3.5);
        }

        private void btnLattee_Click(object sender, EventArgs e)
        {
            Order("Latte", 3.2);
        }

        private void btnTea_Click(object sender, EventArgs e)
        {
            Order("Tea", 1.5);
        }

        private void btnNescafe_Click(object sender, EventArgs e)
        {
            Order("Nescafe", 2.4);
        }

        private void btnCola_Click(object sender, EventArgs e)
        {
            Order("Coca Cola", 1.8);
        }

        private void btnPepsi_Click(object sender, EventArgs e)
        {
            Order("Pepsi", 1.8);
        }

        private void btnMohito_Click(object sender, EventArgs e)
        {
            Order("Mohito", 3.9);
        }

        private void btnFreshJuice_Click(object sender, EventArgs e)
        {
            Order("Fresh Juice", 3.2);
        }

        private void btnOrange_Click(object sender, EventArgs e)
        {
            Order("Orange Juice", 2.5);
        }

        private void btnSoda_Click(object sender, EventArgs e)
        {
            Order("Soda", 2.2);
        }

        private void btnChoclateShake_Click(object sender, EventArgs e)
        {
            Order("Choclate Shake", 4.5);
        }

        private void btnVanillaShake_Click(object sender, EventArgs e)
        {
            Order("Vanilla Shake", 4.2);
        }

        private void btnCocoaShake_Click(object sender, EventArgs e)
        {
            Order("Cocoa Shake", 3.9);
        }

        private void btnFruitsShake_Click(object sender, EventArgs e)
        {
            Order("Fruit Shake", 3.5);
        }

        private void btnOreoShake_Click(object sender, EventArgs e)
        {
            Order("Oreo Shake", 4.4);
        }

        private void btnIceCoffee_Click(object sender, EventArgs e)
        {
            Order("Ice Coffee", 4.7);
        }

        private void btnChoclateCake_Click(object sender, EventArgs e)
        {
            Order("Choclate Cake", 6.2);
        }

        private void btnNescafeCake_Click(object sender, EventArgs e)
        {
            Order("Nescafe Cake", 5.75);
        }

        private void btnLotusCake_Click(object sender, EventArgs e)
        {
            Order("Lotus Cake", 6.5);
        }

        private void btnVanillaCake_Click(object sender, EventArgs e)
        {
            Order("Vanilla Cake", 5.95);
        }

        private void btnCupCake_Click(object sender, EventArgs e)
        {
            Order("Cup Cake", 6.15);
        }

        private void btnCheeseCake_Click(object sender, EventArgs e)
        {
            Order("Chesee Cake", 6.22);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
