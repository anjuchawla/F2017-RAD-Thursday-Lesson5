/*
 * Name : Anju Chawla
 * Date : October, 2017
 * This application allows the customers to buy coffee in bulk,
 * in various quantities.
 * A report that shows the sales made can be printed.
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkCoffeeSales2017
{
    public partial class frmBulkCoffeeSales : Form
    {
        private struct CoffeeSale
        {
            public String quantity;
            public String type;
            public decimal price;
        };
        //number of transactions that can be saved
        private const int MaximumTransactions = 5;
        //save the transactions in an array
        private CoffeeSale[] transactionsCoffeeSales = new CoffeeSale[MaximumTransactions];
        //save the transactions in a list
        private List<CoffeeSale> transactionsCoffeeSalesList = new List<CoffeeSale>();

        private string selectedButtonName;
        private int transactionNumber;

        public frmBulkCoffeeSales()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// The default sttings on application load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBulkCoffeeSales_Load(object sender, EventArgs e)
        {
            rdoQuarterPound.Checked = true;
            selectedButtonName = "rdoQuarterPound";


        }
        /// <summary>
        /// Calculates the price of the coffee. Saves the transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindPrice_Click(object sender, EventArgs e)
        {
            //the prices of the three types of coffees in the three weights
            //quarter, half and full pund prices for regular, decaffienated and special blend
            decimal[,] price = { {2.60m,2.90m,3.25m },
                { 4.90m,5.60m, 6.10m},
                { 8.75m, 9.75m, 11.25m} };

            int row, column;
            decimal salesPrice;
            CoffeeSale sale;

            //allow only maximum transactions

            // if(transactionNumber < MaximumTransactions )

            try
            {
                column = cboType.SelectedIndex;
                if (column != -1) //coffee type chosen
                {
                    //find the quantity 
                    switch (selectedButtonName)
                    {
                        case "rdoQuarterPound":
                            row = 0;
                            transactionsCoffeeSales[transactionNumber].quantity = "Quarter Pound";
                            sale.quantity = "Quarter Pound";
                            break;
                        case "rdoHalfPound":
                            row = 1;
                            transactionsCoffeeSales[transactionNumber].quantity = "Half Pound";
                            sale.quantity = "Half Pound";
                            break;
                        case "rdoFullPound":
                            row = 2;
                            transactionsCoffeeSales[transactionNumber].quantity = "Full Pound";
                            sale.quantity = "Full Pound";
                            break;
                        default:
                            row = 0;
                            transactionsCoffeeSales[transactionNumber].quantity = "Quarter Pound";
                            sale.quantity = "Quarter Pound";
                            break;

                    }//switch
                    //look up the price
                    salesPrice = price[row, column];
                    txtPrice.Text = salesPrice.ToString("c");
                    //save the rest of the transaction
                    sale.type = transactionsCoffeeSales[transactionNumber].type = cboType.Text; //(String) cboType.Items[column]  
                    sale.price = transactionsCoffeeSales[transactionNumber].price = salesPrice;
                    transactionsCoffeeSalesList.Add(sale);
                                       
                }
                else
                {
                    MessageBox.Show("Please choose a coffee type",
                    "Coffee Selection Missing", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    cboType.Focus();
                }

            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Only " + MaximumTransactions + " can be saved",
                    "Maximum Transactions Done", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }



        }
    }
}
