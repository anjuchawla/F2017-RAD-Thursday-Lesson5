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
            //quarter, half and full pound prices for regular, decaffienated and special blend
            decimal[,] price = { {2.60m,2.90m,3.25m },
                { 4.90m,5.60m, 6.10m},
                { 8.75m, 9.75m, 11.25m} };

            int noOfRows = price.GetLength(0);
            int noOfColumns  = price.GetLength(1);


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
                    salesPrice = price[column, row];
                    txtPrice.Text = salesPrice.ToString("c");
                    //save the rest of the transaction
                    sale.type = transactionsCoffeeSales[transactionNumber].type = cboType.Text; //(String) cboType.Items[column]  
                    sale.price = transactionsCoffeeSales[transactionNumber].price = salesPrice;
                    transactionsCoffeeSalesList.Add(sale);
                    transactionNumber++;
                                       
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
                MessageBox.Show("Only " + MaximumTransactions + " transactions can be saved",
                    "Maximum Transactions Done", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }



        }

        private void RadioButtonQuantity_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = (RadioButton)sender;
            selectedButtonName = selectedRadioButton.Name;
        }
        /// <summary>
        /// The design of the transactions to be printed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //setting up the print page for preview and print
            Font printFont = new Font("Arial", 12);
            Font headingFont = new Font("Arial Black", 14, FontStyle.Bold);
            float lineHeightFloat = printFont.GetHeight();
            float col1HorizontalPrintLocationFloat = e.MarginBounds.Left;
            float col2HorizontalPrintLocationFloat = 300;
            float col3HorizontalPrintLocationFloat;
            float verticalPrintLocationFloat = e.MarginBounds.Top;
            SizeF fontSizeF = new SizeF();
            string formattedPriceString;


            //print the heading
            e.Graphics.DrawString("Coffee Bulk Sales Report", headingFont, Brushes.Black,
                col2HorizontalPrintLocationFloat, verticalPrintLocationFloat);
            verticalPrintLocationFloat += lineHeightFloat * 2;
            //headings to columns
            e.Graphics.DrawString("Quantity", headingFont, Brushes.Black,
               col1HorizontalPrintLocationFloat, verticalPrintLocationFloat);
            e.Graphics.DrawString("Type", headingFont, Brushes.Black,
               col2HorizontalPrintLocationFloat, verticalPrintLocationFloat);
            e.Graphics.DrawString("Price", headingFont, Brushes.Black,
               500, verticalPrintLocationFloat);

            verticalPrintLocationFloat += lineHeightFloat * 2;

            //print the transactions
            // can use a for each loop
            foreach (CoffeeSale transaction in transactionsCoffeeSalesList)
            {
                //refer with transaction.quantityString
                //print the quantity
                e.Graphics.DrawString(transaction.quantity, printFont,
                    Brushes.Black, col1HorizontalPrintLocationFloat, verticalPrintLocationFloat);
                //print the coffee type
                e.Graphics.DrawString(transaction.type, printFont,
                   Brushes.Black, col2HorizontalPrintLocationFloat, verticalPrintLocationFloat);
                //format and right align the price
                formattedPriceString = transaction.price.ToString("c");
                //measure string in this fone
                fontSizeF = e.Graphics.MeasureString(formattedPriceString, printFont);
                //subtract width from column position
                col3HorizontalPrintLocationFloat = 550 - fontSizeF.Width;
                //print the price
                e.Graphics.DrawString(formattedPriceString, printFont,
                  Brushes.Black, col3HorizontalPrintLocationFloat, verticalPrintLocationFloat);

                //go to next printing position
                verticalPrintLocationFloat += lineHeightFloat;
            }
            //foreach can be used for an array
            for (int index = 0; index < transactionNumber  ; index++)
            {
                //print the quantity
                e.Graphics.DrawString(transactionsCoffeeSales[index].quantity, printFont,
                    Brushes.Black, col1HorizontalPrintLocationFloat, verticalPrintLocationFloat);
                //print the coffee type
                e.Graphics.DrawString(transactionsCoffeeSales[index].type, printFont,
                   Brushes.Black, col2HorizontalPrintLocationFloat, verticalPrintLocationFloat);
                //format and right align the price
                formattedPriceString = transactionsCoffeeSales[index].price.ToString("c");
                //measure string in this fone
                fontSizeF = e.Graphics.MeasureString(formattedPriceString, printFont);
                //subtract width from column position
                col3HorizontalPrintLocationFloat = 550 - fontSizeF.Width;
                //print the price
                e.Graphics.DrawString(formattedPriceString, printFont,
                  Brushes.Black, col3HorizontalPrintLocationFloat, verticalPrintLocationFloat);

                //go to next printing position
                verticalPrintLocationFloat += lineHeightFloat;


            }

        }
        /// <summary>
        /// Print the transactions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //show a preview 
            if(transactionNumber > 0)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
                //straight to printer
                //printDocument1.Print();

            }
            else
            {
                MessageBox.Show("No transactipons to print", "Transaction Report",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
/// <summary>
/// Prints the transaction report if user chooses to, then close the application
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Do you  want to print the transaction report?",
                "Transaction Report Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
            if (confirm == DialogResult.Yes)
                btnPrint_Click(sender, e);

            //exit  the application
            Application.Exit();
        }
        /// <summary>
        /// Clear the input and output and show form in default start state
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            rdoQuarterPound.Checked = true;
            selectedButtonName = "rdoQuarterPound";
            cboType.SelectedIndex = -1;
            txtPrice.Text = String.Empty;

        }
    }
}
