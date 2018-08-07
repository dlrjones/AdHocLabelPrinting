﻿using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BarcodeLib;

namespace AdHocLabelPrinting
{
    public partial class Form1 : Form
    {
        #region Class Variables
        protected static string amcConnStr = "";
        protected SqlConnection amcConn = new SqlConnection(amcConnStr);
        private ArrayList itemList = new ArrayList();
        private ArrayList itemOrder = new ArrayList(); //when parsing a list of MFG #'s, this collects the Item #'s in the same order for accessing the Hashtables
        ArrayList mfgList = new ArrayList(); //used when MFG #'s are entered to be able to use the hashtables where the key value is the item number, not the MFG #
        private Hashtable itemDesc = new Hashtable();
        private Hashtable itemDescLine2 = new Hashtable();
        private Hashtable itemMFG = new Hashtable();
        private Hashtable itemBarCode = new Hashtable();
        private int itemListIndex = 0;
		private int labelCount = 0;
        private int pageLabelCount = 0; //normally = maxLabels - the first page is the only one that may be different due to the start position
        private int maxLabels = 30;
        private int pageMaxLabels = 0;
		private int qtyToPrint = 0;
        private int qtyPrinted = 0;
        private int leftMarginCol2 = 320;
        private int leftMarginCol3 = 600;
        private Barcode barCode = null;
        private Font verdana8Font;
        private int leftOffset = -60;  //establishes the left location of the first label
        private int topOffset = -40;   //establishes the top location of the first label
		private string currentItemNo = "";
        #endregion

        public Form1()
        {
            InitializeComponent();
            //initialize the start position drop list
            for (int x = 1; x < 31; x++)
            {
                cbStartPos.Items.Add(x);
            }
            cbStartPos.SelectedIndex = 0;
        }

        //Splits the text in the input box on the cr/lf, resets
        //the data structures and fills the itemList ArrayList
        private void ParseItemList()
        {
            string[] input = tbItemInput.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            ResetForm();
            foreach (string item in input)
            {
                if (!itemList.Contains(item))
                     itemList.Add(item.Trim());
            }
        }

        //Exit point for the app
        //
        private void btnDone_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //clears the data structures used in the app
        //
        private void ResetForm()
        {
            itemMFG.Clear();
            itemDesc.Clear();
            itemDescLine2.Clear();
            itemOrder.Clear();
            itemBarCode.Clear();
            itemList = new ArrayList();
            mfgList = new ArrayList();
            itemListIndex = 0;
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            pbBarCode.Visible = false;
        }

        //clears the text boxes used on the form itself
        //
        private void ClearForm()
        {
            tbItemInput.Text = "";
            tbLabelDesc.Text = "";
            tbLabelItem.Text = "";
            tbLabelMfgCatNo.Text = "";
            cbStartPos.SelectedIndex = 0;
			tbQty.Text = "1";
            rbItems.Checked = false;
            rbMfg.Checked = false;
            currentItemNo = "";
		}

        private bool CheckFormInput()
        {
            bool goodToGo = true;            
			if (tbItemInput.Text.Length > 0)
            {
				if (rbItems.Checked)
            {
                rbMfg.Checked = false;
            }
            else if (rbMfg.Checked)
            {
                rbItems.Checked = false;
            }
            else
            {
                MessageBox.Show("Are these Item Numbers or Catalog Numbers?", "Select the Number Type");
				goodToGo = false;
            }
		}
		else
            {
                MessageBox.Show("There's Nothing To Do", "Check The Input");
                goodToGo = false;
            }
            return goodToGo;
        }
       
        //entry point into the app from the user's side
        //does a simple test to determine if the user forgot
        //to select the" MFG Catalog #" radio button 
        private void btnGo_Click(object sender, EventArgs e)
        {
          if (CheckFormInput())
            {
                bool isItem = rbItems.Checked ? true : false;
            ParseItemList();
            if (itemList.Count > 0)
            {
                if (NumberTest())
                {
                    BuildItemList();
                }
                else
                {
                    if (isItem)
                    {
                        DialogResult result = MessageBox.Show("Are these Manufacturer Catalog Numbers?",
                            "Check The Input Type", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            rbMfg.Checked = true;
                            itemListIndex = 0;
                            BuildItemList();
                        }
                        else
                            ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("These Aren't MFG Catalog #'s", "Check The Input Values");
                    }
                }
            }
        }
		}

        //called from btnGo_Click to read & parse the user's input 
        //and then create a sql query and (finally) display the results
        private void BuildItemList()
        {
            ParseItemList(); //calls ResetForm()
            BuildSQLQuery();
            if (itemList.Count > 0)
                DisplayResults();
            else
            {
                MessageBox.Show("These Numbers Aren't Valid", "Check The Input");
            }
        }

        //Since the radio buttons default to ItemNumbers, this checks
        //to see if the input list actually are item numbers. It only
        //checks the first item in the list so there may be cases where
        //a MFG # is identical to an Item #. the Descr and MFG # for that
        //item will be different, though
        private bool NumberTest()
        {
            bool goodToGo = true;
            string sql = "";
            string rtnValu = "";

            if (rbItems.Checked)
                sql = "SELECT CTLG_NO, DESCR FROM ITEM WHERE ITEM_NO = '" + itemList[0].ToString().Trim() + "'";
            else
                sql = "SELECT ITEM_NO, DESCR FROM ITEM WHERE CTLG_NO = '" + itemList[0].ToString().Trim() + "'";

            SqlCommand comm = new SqlCommand(sql, amcConn);
            amcConn.Open();
            try
            {
                SqlDataReader dr = comm.ExecuteReader();
                if (!dr.HasRows)
                {
                    goodToGo = false;
                }
            }
            catch (Exception ex)
            {
                goodToGo = false;
            }
            amcConn.Close();
            return goodToGo;
        }

        //creates a new bar code object 
        //and sets its size
        private void InitBarcode(int height, int width)
        {
            barCode = new Barcode();
            barCode.Alignment = AlignmentPositions.CENTER;
            barCode.Height = height; // init: 26  print: 20
            barCode.Width = width;  // init: 250  print: 200
        }

        //The itemList is the numbers entered into tbItemInput - in the order
        //in which they were entered. itemListIndex is a zero based counter used
        //to step through the itemList. This routine displays all of the info
        //for a given item #, including the barcode. It also makes an attempt to
        //recognize that the user entered MFG #'s but left the default rbItems
        //radio button checked.
        private void DisplayResults()
        {
            InitBarcode(26, 250);
            bool isItem = rbItems.Checked ? true : false;
            int itemCount = itemList.Count;

            if (itemListIndex >= itemCount || itemListIndex < 0)
                itemListIndex = 0;
            if (itemBarCode.Count == 0)
                LoadItemBarCode();

            string keyValu = itemList[itemListIndex++].ToString();
            if (isItem)
            {
                try
                {
                    tbLabelMfgCatNo.Text = itemMFG[keyValu].ToString().Trim();
                    tbLabelDesc.Text = itemDesc[keyValu].ToString().Trim();
                    if (itemDescLine2.ContainsKey(keyValu))
                        tbLabelDesc.Text += " " + itemDescLine2[keyValu].ToString().Trim();
                    pbBarCode.Visible = true;
                    pbBarCode.Image = barCode.Encode(TYPE.CODE39, keyValu);
                    tbLabelItem.Text = keyValu;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Take a screenshot of the form and send it to PMMHelp", "Unrecognized Error");
                }
            }
            else
            {
                try
                {
                    //if (!itemBarCode.ContainsKey(keyValu))
                    //    itemBarCode.Add(keyValu, barCode.Encode(TYPE.CODE39, keyValu));
                    tbLabelItem.Text = itemOrder[itemListIndex - 1].ToString().Trim();
                    tbLabelDesc.Text = itemDesc[itemOrder[itemListIndex - 1].ToString()].ToString().Trim();
                    if (itemDescLine2.ContainsKey(keyValu))
                        tbLabelDesc.Text += " " + itemDescLine2[keyValu].ToString().Trim();
                    pbBarCode.Visible = true;
                    pbBarCode.Image = barCode.Encode(TYPE.CODE39, itemOrder[itemListIndex - 1].ToString().Trim());
                    tbLabelMfgCatNo.Text = itemMFG[itemOrder[itemListIndex - 1].ToString()].ToString().Trim(); 
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show("Are these HMC Item Numbers Numbers?", "Check The Input Type",
                       MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        rbItems.Checked = true;
                        itemListIndex = 0;
                    }
                    else
                        ClearForm();
                }
            }
            btnNext.Enabled = (itemList.Count > 1) ? true : false;
            btnPrev.Enabled = itemListIndex > 1 ? true : false;
            barCode.Dispose();
        }

        //called once each by DisplayResults and PrintPage
        //to load the itemBarCode hashtable
        private void LoadItemBarCode()
        {
            int indx = 0;
            foreach (string keyValu in itemOrder)
            {
                if (!itemBarCode.ContainsKey(keyValu))
                    itemBarCode.Add(keyValu, barCode.Encode(TYPE.CODE39, keyValu));
            }
        }

        //loads the itemMFG, itemDesc and itemOrder hashtables and
        //the mfgList ArrayList for display and printing
        private void MFGInput(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, amcConn);
            amcConn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (!itemMFG.ContainsKey(dr[0]))
                {
                    mfgList.Add(itemList[itemListIndex]);
                    itemMFG.Add(dr[0].ToString().Trim(), dr[1].ToString().Trim());
                    itemDesc.Add(dr[0].ToString().Trim(), SetDescLen(dr[0].ToString().Trim(), dr[2].ToString().Trim()));
                    itemOrder.Add(dr[0].ToString().Trim());
                }
            }
            amcConn.Close();
        }

        //loads the itemMFG, itemDesc and itemOrder hashtables
        //for display and printing
        private void ItemInput(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, amcConn);
            amcConn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (!itemMFG.ContainsKey(itemList[itemListIndex]))
                {
                    itemMFG.Add(itemList[itemListIndex], dr[0].ToString().Trim());
                    itemDesc.Add(itemList[itemListIndex], SetDescLen(itemList[itemListIndex].ToString(), dr[1].ToString().Trim()));
                    itemOrder.Add(itemList[itemListIndex++].ToString().Trim());
                }
            }
            amcConn.Close();
        }

        //sizes the description to fit on two lines if necessary
        //
        private string SetDescLen(string keyValu, string desc)
        {
            string line1 = "";
            string line2 = "";
            string space = "";
            int charCount = 0;
            string[] descWords = desc.Split(" ".ToCharArray());

            foreach (string word in descWords)
            {
                if (line1.Length + word.Length <= 30)
                    line1 += space + word;
                else
                {
                    space = line2.Length == 0 ? "" : space;
                    line2 += space + word;
                }
                space = " ";
            }

            if (line2.Length > 0)
                itemDescLine2.Add(keyValu, line2);
            return line1;
        }

        //Creates the sql SELECT statement depending upon whether Item #'s
        //or MFG #'s were entered. Also invokes the routines that load the
        //Hashtables
        private void BuildSQLQuery()
        {
            string whereParam = "";
            string selectParam = "";
            bool isItem = true;
            string inValu = "";

            for (int x = 0; x < itemList.Count; x++)
            {
                inValu = itemList[x].ToString();
                try
                {
                    // int test = Convert.ToInt32(inValu);

                    if (rbItems.Checked)
                    {
                        whereParam = "ITEM_NO";
                        selectParam = "CTLG_NO";
                    }
                    else
                    {
                        whereParam = "CTLG_NO";
                        selectParam = "ITEM_NO, CTLG_NO";
                        isItem = false;
                    }

                    string sql = "SELECT " + selectParam + ", DESCR FROM ITEM WHERE " + whereParam + " = '" + inValu + "'";

                    if (ValidInput(inValu, sql))
                    {
                        if (isItem)
                            ItemInput(sql);
                        else
                        {
                            sql += " AND SUBSTRING(ITEM_NO,1,1) NOT LIKE '~'";
                            MFGInput(sql);
                        }
                    }
                    else
                    {
                        //an invalid item (or mfg) # has been found and removed 
                        //from the itemList to reset the loop counter.
                        x--;
                    }
                }
                catch (Exception ex)
                { }
            }
            //want itemList to contain a list of item #'s not mfg #'s
            if (rbMfg.Checked)
            {
                itemList.Clear();
                foreach (string item in itemOrder)
                {
                    if (!itemList.Contains(item))
                        itemList.Add(item);
                }
            }
        }

        //this uses the sql command to determine if the item # or mfg #
        //exists in the db. if not, the number is removed from the itemList
        private bool ValidInput(string inValu, string sql)
        {
            bool goodToGo = true;
            try
            {
                SqlCommand comm = new SqlCommand(sql, amcConn);
                amcConn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (!dr.HasRows)
                {
                    itemList.Remove(inValu);
                    goodToGo = false;
                }
            }
            catch (Exception ex)
            {
                goodToGo = false;
            }
            amcConn.Close();
            return goodToGo;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            pbBarCode.Visible = false;
            DisplayResults();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            itemListIndex -= 2;
            pbBarCode.Visible = false;
            DisplayResults();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            ResetForm();
        }

		private bool PrintCurrent(string itemNo)
        {
            bool goodToGo = false;
			//currentItemNo having a value in it means the user clicked the PrintCurrent button
            //The outer Print routine is stepping through  
            //each of the items in tbItemInput. When it matches the one that is passed in here
            //then the single label is printed. When currentItemNo is empty then the return 
            //value is TRUE and all labels get printed. Since the user may want several labels 
            //printed (qtyToPrint > 0) I set qtyPrinted to 0 so that same unwanted itemNo's 
            //doesn't get passed in here the qtyToPrint number of times.
            if (currentItemNo.Length > 0)
            {
                if (currentItemNo.Equals(itemNo))
                {
                    goodToGo = true;
                }
                else
                {
                    qtyPrinted = 0;
                }
            }
            else
            {
                goodToGo = true;
            }          
            return goodToGo;
        }

        private void PrintPage(object sender, PrintPageEventArgs evArgs)
        {
            Graphics g = evArgs.Graphics;
            float linesPerPage = 0;
            float yPos = 0;
			float leftMargin = evArgs.MarginBounds.Left + leftOffset;
            float topMargin = evArgs.MarginBounds.Top + topOffset;
            int lineCount = 0;
            int colCount = 0;
			int	offset = 0;  
            int startPos = Convert.ToInt32(cbStartPos.Text);
			string iNumb = "";
          
			pageMaxLabels = maxLabels - startPos;
            //the print barcode is sized to fit the labels
            InitBarcode(20, 200);
            itemBarCode.Clear();
            LoadItemBarCode();

            if (startPos > 1)
            {
                //used to establish the start position of the first label on the page.
                //NOT for fine tuning the positions of the labels for a particular 
                //printer or label  - the 'else' statement does that
                #region Switch Stmnt to set the Start Position
                switch (startPos)
                {
                    //case 1:  // this is shown for reference
                    //    topMargin = 60;
                    //    leftMargin = 40;
                    //    break;
                    case 2:
                        colCount = 1;
                        leftMargin = leftMarginCol2;
                        break;
                    case 3:
                        colCount = 2;
                       leftMargin = leftMarginCol3;
                        break;
                    case 4:
                        topMargin = offset +  160;
                        break;
                    case 5:
                        colCount = 1;
                        topMargin = offset +  160; 
                        leftMargin = leftMarginCol2;
                        break;
                    case 6:
                        colCount = 2;
                         topMargin = offset +  160;
                         leftMargin = leftMarginCol3;
                        break;
                    case 7:
                         topMargin = offset +  260;
                        break;
                    case 8:
                        colCount = 1;
                        topMargin = offset + 260;
                        leftMargin = leftMarginCol2;
                        break;
                    case 9:
                        colCount = 2;
                        topMargin = offset + 260;
                      leftMargin = leftMarginCol3;
                        break;
                    case 10:
                        topMargin = offset +  360;
                        break;
                    case 11:
                        colCount = 1;
                        topMargin = offset + 360;
                        leftMargin = leftMarginCol2;
                        break;
                    case 12:
                        colCount = 1;
                        topMargin = offset + 360;
                      leftMargin = leftMarginCol3;
                        break;
                    case 13:
                        topMargin = offset +  460;
                        break;
                    case 14:
                        colCount = 1;
                        topMargin = offset + 460;
                        leftMargin = leftMarginCol2;
                        break;
                    case 15:
                        colCount = 2;
                        topMargin = offset + 460;
                        leftMargin = leftMarginCol3;
                        break;
                    case 16:
                        topMargin = offset + 560;
                        break;
                    case 17:
                        colCount = 1;
                        topMargin = offset + 560;
                        leftMargin = leftMarginCol2;
                        break;
                    case 18:
                        colCount = 2;
                        topMargin = offset + 560;
                      leftMargin = leftMarginCol3;
                        break;
                    case 19:
                        topMargin = offset + 660;
                        break;
                    case 20:
                        colCount = 1;
                        topMargin = offset + 660;
                        leftMargin = leftMarginCol2;
                        break;
                    case 21:
                        colCount = 2;
                        topMargin = offset + 660;
                        leftMargin = leftMarginCol3;
                        break;
                    case 22:
                        topMargin = offset + 760;
                        break;
                    case 23:
                        colCount = 1;
                        topMargin = offset + 760;
                        leftMargin = leftMarginCol2;
                        break;
                    case 24:
                        colCount = 2;
                        topMargin = offset + 760;
                      leftMargin = leftMarginCol3;
                        break;
                    case 25:
                        topMargin = offset + 860;
                        break;
                    case 26:
                        colCount = 1;
                        topMargin = offset + 860;
                        leftMargin = leftMarginCol2;
                        break;
                    case 27:
                        colCount = 2;
                        topMargin = offset + 860;
                      leftMargin = leftMarginCol3;
                        break;
                    case 28:
                        topMargin = offset + 960;
                        break;
                    case 29:
                        colCount = 1;
                        topMargin = offset + 960;
                        leftMargin = leftMarginCol2;
                        break;
                    case 30:
                        colCount = 2;
                        topMargin = offset + 960;
                        leftMargin = leftMarginCol3;
                        break;
                }
                #endregion
            }
            string line = null;
            //Calculate the lines per page on the basis of the height of the page and the height of the font
            try
            {
               // foreach (string iNumb in itemOrder)  
				while (labelCount < itemOrder.Count && pageLabelCount++ <= pageMaxLabels)
                {
                   //increment labelCount after checking Quantity To Print  
                        // qtyToPrint: defaults to 1 but is otherwise up to the user;    qtyPrinted: begins with the qtyToPrint value and is decremented
                        if (qtyPrinted > 0)
                            qtyPrinted--;
                        else
                        {
                            qtyPrinted = qtyToPrint - 1;
                            labelCount++;
                        }

                    if (labelCount < itemOrder.Count)
                    {
                        iNumb = itemOrder[labelCount].ToString().Trim();
                        if (PrintCurrent(iNumb))
                        {
                    //Calculate the starting position
                    yPos = topMargin + (lineCount * verdana8Font.GetHeight(g));
                    //Draw text
                    g.DrawString("HMC#:  " + iNumb, verdana8Font, Brushes.Black, leftMargin, yPos, new StringFormat());
                    //Move to next line
                    yPos = topMargin + (++lineCount * verdana8Font.GetHeight(g));
                    g.DrawString("MFG#:  " + itemMFG[iNumb], verdana8Font, Brushes.Black, leftMargin, yPos, new StringFormat());
                    yPos = topMargin + (++lineCount * verdana8Font.GetHeight(g));

                    /**BAR CODE**/
                    evArgs.Graphics.DrawImage((Image)itemBarCode[iNumb], 
						new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(yPos)));  //init: leftMargin - 20

                    lineCount++;
                    yPos = topMargin + (++lineCount * verdana8Font.GetHeight(g));
                    g.DrawString(itemDesc[iNumb].ToString(), verdana8Font, Brushes.Black, leftMargin, yPos, new StringFormat());
                    if (itemDescLine2.ContainsKey(iNumb))
                    {
                        yPos = topMargin + (++lineCount * verdana8Font.GetHeight(g));
                        g.DrawString(itemDescLine2[iNumb].ToString(), verdana8Font, Brushes.Black, leftMargin, yPos, new StringFormat());
                    }
                    
					if (++colCount == 3)
                    {
                        colCount = 0;
                        lineCount = 0;
                        leftMargin = evArgs.MarginBounds.Left + leftOffset;
                        topMargin += 100;  //init: 60
                    }
                    else
                    {
                        //this resets the print line for the next label
                        lineCount = 0;                        

                        //this is used to position the labels on the page
                        //the three colCount values are 0, 1 and 2
                        if (colCount == 1)
                        {
                            leftMargin = leftMarginCol2;
                        }
                        else
                        {
                            leftMargin = leftMarginCol3;
                        }
                    }
				}//end if (PrintCurrent(iNumb))	
                } 
				}             
                if ((labelCount >= pageMaxLabels && itemOrder.Count > labelCount) || (itemOrder.Count > labelCount && pageLabelCount > pageMaxLabels))
                {
                    pageMaxLabels = maxLabels;
                    pageLabelCount = 0;
                    cbStartPos.SelectedIndex = 0;
                    evArgs.HasMorePages = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "Take a screenshot of the form and send it to PMMHelp", "Unrecognized Error");
            }         
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (CheckFormInput())
            {
            //itemList tells us that something has been entered by the user
            //itemOrder says that the numbers  have been validated by btnGo_Click
            if (itemList.Count > 0 && itemOrder.Count > 0)
            {
                try
                {
                    qtyToPrint = Convert.ToInt32(tbQty.Text.Trim());
                    qtyPrinted = qtyToPrint;
                    labelCount = 0; //increments to be same value as itemOrder.Count
                    pageLabelCount = 0; //counts how many labels have been printed to a given page
					verdana8Font = new Font("Verdana", 8);
                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings.PaperSize = new PaperSize("Letter", 850, 1100);
                    pd.PrintPage += new PrintPageEventHandler(this.PrintPage);
                    pd.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while printing", ex.ToString());
                }
			}
}
	}

			private void btnPrintCurrent_Click(object sender, EventArgs e)
        {
			if (CheckFormInput())
            {
				//currentItemNo holds the individual Item number being displayed
                //the fact that it's not empty triggers the magic within the Print routine
	            currentItemNo = tbLabelItem.Text.Trim();
	            btnPrint_Click(sender, e);
	            currentItemNo = ""; 
			}     
        }
    }
}
