using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace pos_system.Cashier
{
    public partial class CashierMain : Form
    {

       

        public CashierMain()
        {
            InitializeComponent();
            DGV_List1.CellClick += DGV_List1_CellClick;

        }

        private void CashierMain_Load(object sender, EventArgs e)
        {



            // DATABASE CONNECTION 

            try
            {
                
                string connectionString = @"Data Source=LAPTOP-7EH2LIKF\SQLEXPRESS01;Initial Catalog=POS;Integrated Security=True;";

                
                string query = "SELECT ProductId, ProductName, SellingPrice FROM ProductTable";

                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    
                    DGV_List1.DataSource = dt;

                   
                    DGV_List1.Columns["ProductId"].HeaderText = "Item Code";
                    DGV_List1.Columns["ProductName"].HeaderText = "Product Name";
                    DGV_List1.Columns["SellingPrice"].HeaderText = "Unit Price";

                    DGV_List1.Columns["ProductId"].Width = 150;
                    DGV_List1.Columns["ProductName"].Width = 245;
                    DGV_List1.Columns["SellingPrice"].Width = 150;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





            
           

            lbl_date.Text =DateTime.Now.ToLongDateString();
            lbl_time.Text = DateTime.Now.ToLongTimeString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Qty = int.Parse(lbl_qty.Text);

            if (!string.IsNullOrWhiteSpace(txt_qty.Text)) 
            {
                Qty++;

                lbl_qty.Text = Qty.ToString();
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Qty = int.Parse(lbl_qty.Text);

            if (txt_qty != null && Qty >1 )
            {
                Qty--;

                lbl_qty.Text = Qty.ToString();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_time.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CashierCreateLoyality loyaltyForm = new CashierCreateLoyality();
            loyaltyForm.Show();
        }

        private void DGV_List1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DGV_List1.Rows[e.RowIndex];
                string productName = row.Cells[1].Value.ToString(); 
                txt_qty.Text = productName;
            }
        }

        private void btn_buy_item_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txt_total.Text))
            {
                string productName = txt_qty.Text.Trim();
                int quantity = int.TryParse(lbl_qty.Text, out int qty) ? qty : 1;

                // Search matching product in DGV_List1
                string productCode = "";
                decimal unitPrice = 0;

                bool productFound = false;

                foreach (DataGridViewRow row in DGV_List1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string currentName = row.Cells["ProductName"].Value?.ToString().Trim(); // Match with DGV_List1 column name

                    if (string.Equals(currentName, productName, StringComparison.OrdinalIgnoreCase))
                    {
                        productCode = row.Cells["ProductId"].Value?.ToString();
                        unitPrice = Convert.ToDecimal(row.Cells["SellingPrice"].Value);
                        productFound = true;
                        break;
                    }
                }

                if (!productFound)
                {
                    MessageBox.Show("Product not found in list!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                decimal amount = unitPrice * quantity;
                int rowNo = DGV_List2.Rows.Count + 1;

                DGV_List2.Rows.Add(
                    rowNo,               // No
                    productCode,         // Code (from DGV_List1)
                    productName,         // Product (txt_qty)
                    unitPrice.ToString("0.00"),  // Cost (from DGV_List1)
                    quantity,            // Qty (lbl_qty)
                    amount.ToString("0.00")   // Amount

                );

                //again qty value ==1
                int Qty = int.Parse(lbl_qty.Text);
                Qty = 1;
                lbl_qty.Text = Qty.ToString();

                productName = " ";
                txt_qty.Text = productName.ToString();

            }

            else
            {
                MessageBox.Show("Already bill is calculated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        


        }

        private void txt_cash_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in DGV_List2.Rows)
            {
                if (!row.IsNewRow && row.Cells["Amount"].Value != null)
                {
                    decimal amount = 0;

                    if (decimal.TryParse(row.Cells["Amount"].Value.ToString(), out amount))
                    {
                        totalAmount += amount;
                    }
                }
            }

            // Set total and net
            txt_total.Text = totalAmount.ToString("0.00");
            txt_net.Text = totalAmount.ToString("0.00");

        }

        private void btn_lo_Click(object sender, EventArgs e)
        {
            CashierAddLoyality loyaltyForm = new CashierAddLoyality();
            loyaltyForm.Show();
        }
    }
}
