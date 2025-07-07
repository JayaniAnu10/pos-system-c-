using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pos_system.Cashier
{
    public partial class CashierMain : Form
    {

       

        public CashierMain()
        {
            InitializeComponent();
            
        }

        private void CashierMain_Load(object sender, EventArgs e)
        {
            DCV_List1.Rows.Add("P001", "Milo 180g", "250.00");
            DCV_List1.Rows.Add("P002", "Nestomalt 400g", "275.00");
            DCV_List1.Rows.Add("P003", "Sunlight Soap", "120.00");
            DCV_List1.Rows.Add("P004", "Anchor Milk", "300.00");
            DCV_List1.Rows.Add("P005", "Surf Excel", "320.00");
            DCV_List1.Rows.Add("P001", "Milo 180g", "250.00");
            DCV_List1.Rows.Add("P002", "Nestomalt 400g", "275.00");
            DCV_List1.Rows.Add("P003", "Sunlight Soap", "120.00");
            DCV_List1.Rows.Add("P004", "Anchor Milk", "300.00");
            DCV_List1.Rows.Add("P005", "Surf Excel", "320.00");
            DCV_List1.Rows.Add("P001", "Milo 180g", "250.00");
            DCV_List1.Rows.Add("P002", "Nestomalt 400g", "275.00");
            DCV_List1.Rows.Add("P003", "Sunlight Soap", "120.00");
            DCV_List1.Rows.Add("P004", "Anchor Milk", "300.00");
            DCV_List1.Rows.Add("P005", "Surf Excel", "320.00");
            DCV_List1.Rows.Add("P001", "Milo 180g", "250.00");
            DCV_List1.Rows.Add("P002", "Nestomalt 400g", "275.00");
            DCV_List1.Rows.Add("P003", "Sunlight Soap", "120.00");
            DCV_List1.Rows.Add("P004", "Anchor Milk", "300.00");
            DCV_List1.Rows.Add("P005", "Surf Excel", "320.00");

            DGV_List2.Rows.Add("1", "p110", "Surf Excel", "320.00", "10", "320.00");
            DGV_List2.Rows.Add("1", "p110", "Surf Excel", "320.00", "10", "320.00");
            DGV_List2.Rows.Add("1", "p110", "Surf Excel", "320.00", "10", "320.00");
            DGV_List2.Rows.Add("1", "p110", "Surf Excel", "320.00", "10", "320.00");
            DGV_List2.Rows.Add("1", "p110", "Surf Excel", "320.00", "10", "320.00");
            DGV_List2.Rows.Add("1", "p110", "Surf Excel", "320.00", "10", "320.00");
            DGV_List2.Rows.Add("1", "p110", "Surf Excel", "320.00", "10", "320.00");
            DGV_List2.Rows.Add("1", "p110", "Surf Excel", "320.00", "10", "320.00");


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

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

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
            CashierLoyality loyaltyForm = new CashierLoyality();
            loyaltyForm.Show();
        }
    }
}
