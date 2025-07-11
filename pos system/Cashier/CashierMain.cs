﻿using System;
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
using System.Configuration;
using System.Data.SqlTypes;



namespace pos_system.Cashier
{
    public partial class CashierMain : Form
    {


        

        public CashierMain()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                DGV_List1.CellClick += DGV_List1_CellClick;
                DGV_List2.CellContentClick += DGV_List2_CellContentClick;
                txt_search.TextChanged += txt_search_TextChanged;
            }



        }

        private void LoadProductData(string searchText = "")
        {
           

            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            string query = "SELECT ProductId, ProductName, SellingPrice FROM ProductTable";

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query += " WHERE ProductId LIKE @Search + '%'";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@Search", searchText);
                    }

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
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string search = txt_search.Text.Trim();
            LoadProductData(search);  // Filter based on search
        }

        private void CashierMain_Load(object sender, EventArgs e)
        {



            // DATABASE CONNECTION 
            if (DesignMode) return;
            LoadProductData();
            ResetBill();
           
 

            lbl_date.Text =DateTime.Now.ToLongDateString();
            lbl_time.Text = DateTime.Now.ToLongTimeString();
        }
        public void SetDiscount(decimal discount, decimal net)
        {
            txt_discount.Text = discount.ToString("F2");
            txt_net.Text = net.ToString("F2");
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
        private void InsertOrderItems(int billNo)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    foreach (DataGridViewRow row in DGV_List2.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string productId = row.Cells["Code"].Value?.ToString();
                        int qty = Convert.ToInt32(row.Cells["Qty"].Value);

                        string query = "INSERT INTO Orders (BillNo, ProductId, Qty) VALUES (@BillNo, @ProductId, @Qty)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@BillNo", billNo);
                            cmd.Parameters.AddWithValue("@ProductId", productId);
                            cmd.Parameters.AddWithValue("@Qty", qty);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //inset sale value
        private void InsertSalesRecord(int billNo)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string employeeName = lbl_user.Text.Trim();
                    decimal total = decimal.Parse(txt_total.Text.Trim());
                    decimal discount = 0;

                    // Get the EmployeeId based on employee name
                    string getEmpIdQuery = "SELECT EmployeeId FROM EmployeeTable WHERE EmployeeName = @EmployeeName";
                    string employeeId = "";

                    using (SqlCommand cmdEmp = new SqlCommand(getEmpIdQuery, conn))
                    {
                        cmdEmp.Parameters.AddWithValue("@EmployeeName", employeeName);
                        object result = cmdEmp.ExecuteScalar();
                        if (result != null)
                        {
                            employeeId = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Employee not found in EmployeeTable!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // ✅ Insert including BillNo
                    string insertQuery = @"INSERT INTO Sales (BillNo, EmployeeId, Total)
                                   VALUES (@BillNo, @EmployeeId, @Total)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BillNo", billNo);
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@Total", total);
                      

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                           
                        }
                        else
                        {
                            MessageBox.Show("Failed to add sales record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting Sales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateQty()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (DataGridViewRow row in DGV_List2.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string productId = row.Cells["Code"].Value?.ToString(); // or "ProductId"
                        int qty = Convert.ToInt32(row.Cells["Qty"].Value);

                        string updateQuery = "UPDATE ProductTable SET Quantity = Quantity - @Qty WHERE ProductId = @ProductId";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Qty", qty);
                            cmd.Parameters.AddWithValue("@ProductId", productId);

                            int affected = cmd.ExecuteNonQuery();

                            if (affected == 0)
                            {
                                MessageBox.Show($"Product ID {productId} not found or update failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product quantities:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_done_Click(object sender, EventArgs e)
        {
            if (DGV_List2.Rows.Count == 0)
            {
                MessageBox.Show("No items have been purchased.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalAmount = 0;
            foreach (DataGridViewRow row in DGV_List2.Rows)
            {
                if (!row.IsNewRow && row.Cells["Amount"].Value != null)
                {
                    if (decimal.TryParse(row.Cells["Amount"].Value.ToString(), out decimal amount))
                    {
                        totalAmount += amount;
                    }
                }
            }

            updateQty();

            txt_total.Text = totalAmount.ToString("0.00");
            txt_net.Text = totalAmount.ToString("0.00");

            if (int.TryParse(lbl_bill.Text, out int billNo))
            {
                InsertSalesRecord(billNo);  // 👉 pass the billNo to Sales
                InsertOrderItems(billNo);   // 👉 same billNo to Orders
            }
            else
            {
                MessageBox.Show("Invalid Bill Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btn_lo_Click(object sender, EventArgs e)
        {

            if(!string.IsNullOrWhiteSpace(txt_total.Text))
            {
                decimal total = Convert.ToDecimal(txt_total.Text);
                CashierAddLoyality loyaltyForm = new CashierAddLoyality(this,total);
                loyaltyForm.Show();
            }

            else
            {
                MessageBox.Show("purchase is not completed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        private void ClearFormFields()
        {
           
            txt_qty.Text = "";
            txt_total.Text = "";
            txt_net.Text = "";
            txt_cash.Text = "";
            txt_balance.Text = "";
            txt_discount.Text = "";

            lbl_qty.Text = "1";

          
            DGV_List2.Rows.Clear();

            lbl_date.Text = DateTime.Now.ToLongDateString();
            lbl_time.Text = DateTime.Now.ToLongTimeString();
        }
        private void ResetBill()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 🔍 Get latest BillNo
                    string query = "SELECT ISNULL(MAX(BillNo), 0) FROM Sales";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int lastBillNo = Convert.ToInt32(cmd.ExecuteScalar());

                    // ➕ Add 1 to create new BillNo
                    int newBillNo = lastBillNo + 1;

                    // 🔄 Refresh form controls
                    ClearFormFields(); // call method to reset UI
                    lbl_bill.Text = newBillNo.ToString(); // show new BillNo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating new BillNo:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void ptn_print_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_balance.Text))
            {

                ResetBill();



            }

            else
            {
                MessageBox.Show("Bill is not completed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void txt_cash_TextChanged_1(object sender, EventArgs e)
        {
            //this.txt_cash.TextChanged += new System.EventHandler(this.txt_cash_TextChanged);

            decimal cash, NET;

            // Parse total
            if (!decimal.TryParse(txt_net.Text.Trim(), out NET))
            {
                txt_balance.Text = "";
                return;
            }

            // Parse cash
            if (!decimal.TryParse(txt_cash.Text.Trim(), out cash))
            {
                txt_balance.Text = "";
                return;
            }

            // Calculate balance
            decimal balance = cash - NET;
            txt_balance.Text = balance.ToString("0.00");
        }

        private void DGV_List2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure the click is not on the header row and is on the correct column
            if (e.RowIndex >= 0 && DGV_List2.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DialogResult confirm = MessageBox.Show("Delete this item?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    DGV_List2.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
            private void DGV_List2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }


    }


}

