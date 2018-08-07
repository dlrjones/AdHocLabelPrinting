using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;
using KeyMaster;
using LogDefault;
using BarcodeLib;

namespace AdHocLabelPrinting
{
    public partial class Form1 : Form
    {
        #region Class Variables

        protected static string amcConnStr =
            ConfigurationManager.ConnectionStrings["amc_userConnectionString"].ConnectionString;

        private string basePath = ConfigurationManager.AppSettings["basePath"];
        private string fileName = ConfigurationManager.AppSettings["fileName"];
        private string logFile = ConfigurationManager.AppSettings["logFile"];
        private string logFilePath = ConfigurationManager.AppSettings["logFilePath"];
        private LogManager lm = LogManager.GetInstance();
        protected SqlConnection amcConn;
        private ArrayList itemList = new ArrayList();
        //when parsing a list of MFG #'s, this collects the Item #'s in the same order for accessing the Hashtables
 		private ArrayList itemOrder = new ArrayList();
       //used when MFG #'s are entered to be able to use the hashtables where the key value is the item number, not the MFG #
       // private ArrayList mfgList = new ArrayList();
        private ArrayList mfgQty = new ArrayList();
        private Hashtable itemDesc = new Hashtable();
        private Hashtable itemDescLine2 = new Hashtable();
        private Hashtable itemMFG = new Hashtable();
        private Hashtable itemBarCode = new Hashtable();
        private Hashtable itemQty = new Hashtable();
        private int itemListIndex = 0;
        private int labelCount = 0;
        //normally = maxLabels - the first page is the only one that may be different due to the start position
        private int pageLabelCount = 0;

        private int maxLabels = 30;
        private int pageMaxLabels = 0;
        private int qtyToPrint = 0;
        private int qtyPrinted = 1;
        private int pageCount = 0;
        private int pagesPrinted = 1;
        private int leftMarginCol2 = Convert.ToInt32(ConfigurationManager.AppSettings["leftMarginCol2"]);
        private int leftMarginCol3 = Convert.ToInt32(ConfigurationManager.AppSettings["leftMarginCol3"]);
        private Barcode barCode = null;
        private Font verdana8Font;

        #region Text Box variables        
        private int rowCount = 1;
        private int rowTop = 180;   //130;
        private int itemTBoxWidth = 200;   //150;
        private int itemTBoxLeft = 35;
        private int qtyTBoxLeft = 0; //Form1 constructor sets this to  itemTBoxLeft + itemTBoxWidth + 10;
        private int rowLblWidth = 22;
        private int rowLblLeft = 10;
        private TextBox tb_1;
        private TextBox tb_2;
        private TextBox tb_3;
        private TextBox tb_4;
        private TextBox tb_5;
        private TextBox tb_6;
        private TextBox tb_7;
        private TextBox tb_8;
        private TextBox tb_9;
        private TextBox tb_10;
        private TextBox tb_11;
        private TextBox tb_12;
        private TextBox tb_13;
        private TextBox tb_14;
        private TextBox tb_15;
        private TextBox tb_16;
        private TextBox tb_17;
        private TextBox tb_18;
        private TextBox tb_19;
        private TextBox tb_20;
        private TextBox tb_21;
        private TextBox tb_22;
        private TextBox tb_23;
        private TextBox tb_24;
        private TextBox tb_25;
        private TextBox tb_26;
        private TextBox tb_27;
        private TextBox tb_28;
        private TextBox tb_29;
        private TextBox tb_30;
        private TextBox tb_31;
        private TextBox tb_32;
        private TextBox tb_33;
        private TextBox tb_34;
        private TextBox tb_35;
        private TextBox tb_36;
        private TextBox tb_37;
        private TextBox tb_38;
        private TextBox tb_39;
        private TextBox tb_40;
        #endregion

        private int leftOffset = Convert.ToInt32(ConfigurationManager.AppSettings["leftOffset"]);
            // 0;  //establishes the left location of the first label  -60

        private int topOffset = Convert.ToInt32(ConfigurationManager.AppSettings["topOffset"]);
            // 0;   //establishes the top location of the first label -40

        private string currentItemNo = "";

        #endregion

        public Form1()
        {
            string mssg = "";
            InitializeComponent();
            basePath = basePath.Equals("base") ? AppDomain.CurrentDomain.BaseDirectory : basePath;
            lm.LogFilePath = logFilePath;
            lm.LogFile = logFile;
            amcConnStr += GetKey();
            //AES connect = new AES();

            //connect.AesPath = basePath;
            //connect.ConnStr = amcConnStr;
            //connect.FileName = fileName;
            // if (connect.GetConnectionString(ref mssg))
            if (amcConnStr.Length > 0)
            {
                //amcConnStr = connect.ConnStr;                
                amcConn = new SqlConnection(amcConnStr);
                //initialize the start position drop list
                for (int x = 1; x < 31; x++)
                    cbStartPos.Items.Add(x);
                cbStartPos.SelectedIndex = 0;               
            }
            else
            {
                MessageBox.Show(mssg + Environment.NewLine + "There's no database connection" + Environment.NewLine +
                                "Contact HEMM Help", "Problem Connecting...");
                this.Text += "      NO DATABASE CONNECTION";
                lm.Write("Form1() - There's no database connection.");
            }
            qtyTBoxLeft =  itemTBoxLeft + itemTBoxWidth + 10;
           while(rowCount  < 21)
               btnAddRow_Click(new object(),new EventArgs());
        }

        private string GetKey()
        {
            string[] key = File.ReadAllLines(basePath + fileName);
            return StringCipher.Decrypt(key[0], "sv_pmm_jobs");
        }

        private void GetUserInput()
        {
            //ArrayList itemList = new ArrayList();
            int rows = 1;
            string itemNmbr = "";
            string qty = "";
            bool goodToGo = false;

            while (rows < rowCount)  //rowCount is set in the AddRow method
            {
                #region Item Number Text Boxes
                
                //for each row number, the odd numbered text boxes are the item number, the even numbers are the print quantity
                switch (rows)
                {
                case 1:
                        itemNmbr = tb_1.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_2.Text.Trim().Length > 0 ? tb_2.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_2.Text = "1";
                            goodToGo = true;
                        }
                        break;
                case 2:
                         itemNmbr = tb_3.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_4.Text.Trim().Length > 0 ? tb_4.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_4.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 3:
                         itemNmbr = tb_5.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_6.Text.Trim().Length > 0 ? tb_6.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_6.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 4:
                         itemNmbr = tb_7.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_8.Text.Trim().Length > 0 ? tb_8.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_8.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 5:
                         itemNmbr = tb_9.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_10.Text.Trim().Length > 0 ? tb_10.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_10.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 6:
                          itemNmbr = tb_11.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_12.Text.Trim().Length > 0 ? tb_12.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_12.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 7:
                          itemNmbr = tb_13.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_14.Text.Trim().Length > 0 ? tb_14.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_14.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 8:
                         itemNmbr = tb_15.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_16.Text.Trim().Length > 0 ? tb_16.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_16.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 9:
                         itemNmbr = tb_17.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_18.Text.Trim().Length > 0 ? tb_18.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_18.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 10:
                          itemNmbr = tb_19.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_20.Text.Trim().Length > 0 ? tb_20.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_20.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 11:
                          itemNmbr = tb_21.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_22.Text.Trim().Length > 0 ? tb_22.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_22.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 12:
                          itemNmbr = tb_23.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_24.Text.Trim().Length > 0 ? tb_24.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_24.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 13:
                          itemNmbr = tb_25.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_26.Text.Trim().Length > 0 ? tb_26.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_26.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 14:
                           itemNmbr = tb_27.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_28.Text.Trim().Length > 0 ? tb_28.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_28.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 15:
                           itemNmbr = tb_29.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_30.Text.Trim().Length > 0 ? tb_30.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_30.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 16:
                           itemNmbr = tb_31.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_32.Text.Trim().Length > 0 ? tb_32.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_32.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 17:
                           itemNmbr = tb_33.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_34.Text.Trim().Length > 0 ? tb_34.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_34.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 18:
                          itemNmbr = tb_35.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_36.Text.Trim().Length > 0 ? tb_36.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_36.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 19:
                         itemNmbr = tb_37.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_38.Text.Trim().Length > 0 ? tb_38.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_38.Text = "1";
                            goodToGo = true;
                        }
                break;
                case 20:
                         itemNmbr = tb_39.Text.Trim();
                        if (itemNmbr.Length > 0)
                        {
                            qty = tb_40.Text.Trim().Length > 0 ? tb_40.Text.Trim() : "1";
                            //default the qty to 1 when an item # is entered.
                            if (qty == "1")
                                tb_40.Text = "1";
                            goodToGo = true;
                        }
                break;
            } //end switch
            
                #endregion

                if (goodToGo)
                {
                    itemList.Add(itemNmbr); //fill the ArrayList
                    if (rbItems.Checked)                
                        itemQty.Add(itemNmbr, qty); //fill the hashtable
                    else
                    {
                      //  mfgList.Add(itemNmbr);  //probably don't need mfgList
                        mfgQty.Add(qty);
                    }
                }
                goodToGo = false;
                itemNmbr = "";
                qty = "";
                rows++;
            } // end while

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
            itemQty.Clear();
            itemList = new ArrayList();
            mfgQty = new ArrayList();
            itemListIndex = 0;
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            pbBarCode.Visible = false;
        }

        //clears the text boxes used on the form itself
        //
        private void ClearForm()
        {
            ClearUserInput();
            tbLabelDesc.Text = "";
            tbLabelItem.Text = "";
            tbLabelMfgCatNo.Text = "";
            cbStartPos.SelectedIndex = 0;
            rbItems.Checked = true;
            rbMfg.Checked = false;
            currentItemNo = "";

        }

        private void ClearUserInput()
        {
            tb_1.Text = "";
            tb_2.Text = "";
            tb_3.Text = "";
            tb_4.Text = "";
            tb_5.Text = "";
            tb_6.Text = "";
            tb_7.Text = "";
            tb_8.Text = "";
            tb_9.Text = "";
            tb_10.Text = "";
            tb_11.Text = "";
            tb_12.Text = "";
            tb_13.Text = "";
            tb_14.Text = "";
            tb_15.Text = "";
            tb_16.Text = "";
            tb_17.Text = "";
            tb_18.Text = "";
            tb_19.Text = "";
            tb_20.Text = "";
            tb_21.Text = "";
            tb_22.Text = "";
            tb_23.Text = "";
            tb_24.Text = "";
            tb_25.Text = "";
            tb_26.Text = "";
            tb_27.Text = "";
            tb_28.Text = "";
            tb_29.Text = "";
            tb_30.Text = "";
            tb_31.Text = "";
            tb_32.Text = "";
            tb_33.Text = "";
            tb_34.Text = "";
            tb_35.Text = "";
            tb_36.Text = "";
            tb_37.Text = "";
            tb_38.Text = "";
            tb_39.Text = "";
            tb_40.Text = "";
        }

        private bool CheckFormInput()
        {
            bool goodToGo = true;
            if (itemList.Count == 0)
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
            if (amcConn != null)
            {
                bool isItem = rbItems.Checked ? true : false;
                ResetForm();
                GetUserInput();
                if (itemList.Count > 0)
                {
                    btnPrintAll.Enabled = true;
                    btnPrintCurrent.Enabled = true;
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
            else
            {
                MessageBox.Show("There's no database connection" + Environment.NewLine + "Contact HEMM Help",
                    "Problem Connecting...");
                lm.Write("btnGo_Click - There is no database connection");
            }
        }

        //called from btnGo_Click to read & parse the user's input 
        //and then create a sql query and (finally) display the results
        private void BuildItemList()
        {
           //ParseItemList(); //calls ResetForm()
            BuildSQLQuery();
            if (itemList.Count > 0)
                DisplayResults();
            else
            {
                MessageBox.Show("These Numbers Aren't Valid", "Check The Input");
            }
            if (itemList.Count > 1)
            {
                btnNext.Enabled = true;
                btnPrev.Enabled = true;
            }
        }

        //Since the radio buttons default to ItemNumbers, this checks
        //to see if the input list actually are item numbers. It only
        //checks the first item in the list so there may be cases where
        //a MFG # is identical to an Item #. the Descr and MFG # for that
        //item will be different, though
        private bool NumberTest()
        {
            bool goodToGo = false;
            string sql = "";
            string testItem = itemList[0].ToString().Trim();

            if (rbItems.Checked)
                sql = "SELECT CTLG_NO, DESCR FROM ITEM WHERE ITEM_NO = '" + testItem + "'";
            else
                sql = "SELECT ITEM_NO, DESCR FROM ITEM WHERE CTLG_NO = '" + testItem + "'";

            try
            {
                SqlCommand comm = new SqlCommand(sql, amcConn);
                amcConn.Open();
                try
                {
                    SqlDataReader dr = comm.ExecuteReader();
                    if (dr.HasRows)
                    {
                        goodToGo = true;
                    }
                }
                catch (Exception ex)
                {
                    lm.Write("NumberTest - Item: " + testItem + Environment.NewLine + ex.Message);
                }
                amcConn.Close();
            }
            catch (Exception ex)
            {

                lm.Write("NumberTest (outer try block) - Item: " + testItem + Environment.NewLine + ex.Message);
            }
            return goodToGo;
        }

        //creates a new bar code object 
        //and sets its size
        private void InitBarcode(int height, int width)
        {
            barCode = new Barcode();
            barCode.Alignment = AlignmentPositions.CENTER;
            barCode.Height = height; // init: 26  print: 20
            barCode.Width = width; // init: 250  print: 200
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
            btnPrev.Enabled = (itemList.Count > 1) ? true : false;  //itemListIndex > 1
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
                   //mfgList.Add(itemList[itemListIndex]);
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
                    itemDesc.Add(itemList[itemListIndex],
                        SetDescLen(itemList[itemListIndex].ToString(), dr[1].ToString().Trim()));
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
            int mfgIndex = 0;


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
                {
                    lm.Write("BuildSQLQuery - Problem building SQL Query");
                }
            }
            //want itemList to contain a list of item #'s not mfg #'s
            if (rbMfg.Checked)
            {
                itemList.Clear();
                foreach (string item in itemOrder)
                {

                    if (!itemList.Contains(item))
                        itemList.Add(item);
                    if(!itemQty.Contains(item))
                        itemQty.Add(item, mfgQty[mfgIndex++]);//mfgQty is an array list (like itemOrder) which maintains the order that the items were entered
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
                lm.Write("ValidInput - Input Invalid.");
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
            if (itemListIndex == 1)
                itemListIndex = itemList.Count - 1;
            else
            itemListIndex -= 2;
            pbBarCode.Visible = false;
            DisplayResults();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            ResetForm();
        }

        //currentItemNo having a value in it means the user clicked the PrintAllowed button
        //The outer Print routine is stepping through each of the items in tbItemInput.
        // When it matches the one that is passed in here then that single label is printed.
        // When currentItemNo is empty then the return value is TRUE and all labels get printed.
        // Since the user may want several of the same labels printed (qtyToPrint > 0) I set qtyPrinted
        // to 0 so that same unwanted itemNo's doesn't get passed in here the qtyToPrint number of times.
        private bool PrintAllowed(string itemNo)
        {
            bool goodToGo = false;
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
            float yPos = 0;
            float leftMargin = evArgs.MarginBounds.Left + leftOffset;
            float topMargin = evArgs.MarginBounds.Top + topOffset;
            int lineCount = 0;
            int colCount = 0;
            int offset = 0;
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
                        topMargin = offset + 160;
                        break;
                    case 5:
                        colCount = 1;
                        topMargin = offset + 160;
                        leftMargin = leftMarginCol2;
                        break;
                    case 6:
                        colCount = 2;
                        topMargin = offset + 160;
                        leftMargin = leftMarginCol3;
                        break;
                    case 7:
                        topMargin = offset + 260;
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
                        topMargin = offset + 360;
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
                        topMargin = offset + 460;
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
            //Calculate the lines per page on the basis of the height of the page and the height of the font
            try
            {
                //iNumb = itemOrder[labelCount].ToString().Trim();
                //qtyToPrint = Convert.ToInt32(itemQty[iNumb]);

                while (labelCount <= itemOrder.Count && pageLabelCount++ <= pageMaxLabels)
                {
                    //increment labelCount to go onto the next label - after checking Quantity To Print  
                    // qtyToPrint: defaults to 1 but is otherwise up to the user;    qtyPrinted: begins with the qtyToPrint value and is decremented
                    if (qtyPrinted > 0)
                        qtyPrinted--;
                    else
                    {
                        if (labelCount < itemOrder.Count)
                        {
                            iNumb = itemOrder[labelCount].ToString().Trim();
                            qtyToPrint = Convert.ToInt32(itemQty[iNumb]);
                            qtyPrinted = qtyToPrint - 1;
                        }
                        labelCount++;
                    }

                    if (labelCount <= itemOrder.Count)
                    {
                       iNumb = itemOrder[labelCount - 1].ToString().Trim(); //since labels now have individual counts the iNumb can be advanced beyond the capacity of itemBarCode, this keeps it in synch
                        //if user selects PrintCurrent then the PrintAllowed routine returns TRUE when that particular 
                        //label is found in itemOrder. For PrintAll, PrintAllowed returns TRUE for all labels in itemOrder
                        if (PrintAllowed(iNumb))
                        {
                            //Calculate the starting position
                            yPos = topMargin + (lineCount*verdana8Font.GetHeight(g));
                            //Draw text
                            g.DrawString("HMC#:  " + iNumb, verdana8Font, Brushes.Black, leftMargin, yPos,
                                new StringFormat());
                            //Move to next line
                            yPos = topMargin + (++lineCount*verdana8Font.GetHeight(g));
                            g.DrawString("MFG#:  " + itemMFG[iNumb], verdana8Font, Brushes.Black, leftMargin, yPos,
                                new StringFormat());
                            yPos = topMargin + (++lineCount*verdana8Font.GetHeight(g));

                            /**BAR CODE**/
                            evArgs.Graphics.DrawImage((Image) itemBarCode[iNumb],
                                new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(yPos))); //init: leftMargin - 20

                            lineCount++;
                            yPos = topMargin + (++lineCount*verdana8Font.GetHeight(g));
                            g.DrawString(itemDesc[iNumb].ToString(), verdana8Font, Brushes.Black, leftMargin, yPos,
                                new StringFormat());
                            if (itemDescLine2.ContainsKey(iNumb))
                            {
                                yPos = topMargin + (++lineCount*verdana8Font.GetHeight(g));
                                g.DrawString(itemDescLine2[iNumb].ToString(), verdana8Font, Brushes.Black, leftMargin,
                                    yPos, new StringFormat());
                            }

                            if (++colCount == 3)
                            {
                                colCount = 0;
                                lineCount = 0;
                                leftMargin = evArgs.MarginBounds.Left + leftOffset;
                                topMargin += 100; //init: 60
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
                        } //end if (PrintAllowed(iNumb))	
                    }
                }
                if (pagesPrinted++ < pageCount)
                {
                    pageMaxLabels = maxLabels;
                    pageLabelCount = 0;
                    cbStartPos.SelectedIndex = 0;
                    evArgs.HasMorePages = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine + "Take a screenshot of the form and send it to PMMHelp",
                    "Unrecognized Error");
            }
        }

        private int CountTotalLabelQty()
        {
            int totalLabelQty = 0;

            foreach (string qty in itemQty.Values)
            {
                totalLabelQty += Convert.ToInt32(qty);
            }


            return totalLabelQty;
        }

        //the number of labels to print x the number of times to print them
        //adjusted by the start position on the first page.
        private void CalculatePageCount()
        {
            int qtyEach = CountTotalLabelQty();                                  // Convert.ToInt32(tbQty.Text.Trim());
            int startPos = Convert.ToInt32(cbStartPos.Text);
            decimal wholePages = Math.Truncate(Convert.ToDecimal(qtyEach/maxLabels));
            int labelsModMaxLabels = qtyEach % maxLabels;                 //(itemOrder.Count * qtyEach) % maxLabels;
            decimal labelCountByMaxLabels = wholePages > 0 ? wholePages : 1;                                  //(itemOrder.Count*Convert.ToInt32(tbQty.Text.Trim()))/maxLabels;
            int pageOffset = startPos > 1 ? maxLabels - (maxLabels - startPos + 1) : 0;

            pageCount = Convert.ToInt32(labelCountByMaxLabels);
            if (labelsModMaxLabels + pageOffset > maxLabels)
            {
                pageCount++;
                if ((labelsModMaxLabels + pageOffset) - maxLabels > 0)
                    pageCount++;
            }
            else if (labelsModMaxLabels > 0 && labelsModMaxLabels != qtyEach)
                pageCount++;
            pagesPrinted = 1; //we've calculated pageCount, need to reset pagesPrinted.
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
                        qtyToPrint = 0; //Convert.ToInt32(tbQty.Text.Trim());
                        qtyPrinted = qtyToPrint;
                        labelCount = 0; //increments to be same value as itemOrder.Count
                        pageLabelCount = 0; //counts how many labels have been printed to a given page
                        verdana8Font = new Font("Verdana", 8);
                        CalculatePageCount();
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

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            switch (rowCount)
            {
                #region Item Number Text Boxes
                //for each row number, the odd numbered text boxes are the item number, the even numbers are the print quantity
                
                case 1:
                    Label lbl1 = new Label();
                    lbl1.Top = rowTop + 2;
                    lbl1.Left = rowLblLeft;
                    lbl1.Width = rowLblWidth;
                    lbl1.Font = new Font(lbl1.Font.FontFamily.Name, 7);
                    lbl1.ForeColor = Color.Gray;
                    lbl1.Text = rowCount.ToString();
                    Controls.Add(lbl1);

                    tb_1 = new TextBox();                    
                    tb_1.Top = rowTop;
                    tb_1.Left = itemTBoxLeft;
                    tb_1.Width = itemTBoxWidth;
                    tb_1.Text = "";
                    Controls.Add(tb_1);

                    tb_2 = new TextBox();                     
                    tb_2.Width = 28;
                   // tb_2.Height = 20;
                     tb_2.Top = rowTop;
                     tb_2.Left = qtyTBoxLeft;
                     tb_2.Text = "";
                     Controls.Add(tb_2);
                    break;                
                case 2:
                    Label lbl2 = new Label();
                    lbl2.Top = rowTop + 2;
                    lbl2.Left = rowLblLeft;
                    lbl2.Width = rowLblWidth;
                    lbl2.Font = new Font(lbl2.Font.FontFamily.Name, 7);
                    lbl2.ForeColor = Color.Gray;
                    lbl2.Text = rowCount.ToString();
                    Controls.Add(lbl2);

                     tb_3 = new TextBox();                    
                    tb_3.Top = rowTop;
                    tb_3.Left = itemTBoxLeft;
                    tb_3.Width = itemTBoxWidth;
                    tb_3.Text = "";
                    Controls.Add(tb_3);

                    tb_4 = new TextBox();
                    tb_4.Width = 28;
                   // tb_4.Height = 20;
                     tb_4.Top = rowTop;
                     tb_4.Left = qtyTBoxLeft;
                     tb_4.Text = "";
                     Controls.Add(tb_4);
                    break;
                case 3:
                    Label lbl3 = new Label();
                    lbl3.Top = rowTop + 2;
                    lbl3.Left = rowLblLeft;
                    lbl3.Width = rowLblWidth;
                    lbl3.Font = new Font(lbl3.Font.FontFamily.Name, 7);
                    lbl3.ForeColor = Color.Gray;
                    lbl3.Text = rowCount.ToString();
                    Controls.Add(lbl3);

                     tb_5 = new TextBox();
                    tb_5.Top = rowTop;
                    tb_5.Left = itemTBoxLeft;
                    tb_5.Width = itemTBoxWidth;
                    tb_5.Text = "";
                    Controls.Add(tb_5);

                    tb_6 = new TextBox();
                    tb_6.Width = 28;
                   // tb_6.Height = 20;
                     tb_6.Top = rowTop;
                     tb_6.Left = qtyTBoxLeft;
                     tb_6.Text = "";
                     Controls.Add(tb_6);
                    break;
                case 4:
                    Label lbl4 = new Label();
                    lbl4.Top = rowTop + 2;
                    lbl4.Left = rowLblLeft;
                    lbl4.Width = rowLblWidth;
                    lbl4.Font = new Font(lbl4.Font.FontFamily.Name, 7);
                    lbl4.ForeColor = Color.Gray;
                    lbl4.Text = rowCount.ToString();
                    Controls.Add(lbl4);

                     tb_7 = new TextBox();
                    tb_7.Top = rowTop;
                    tb_7.Left = itemTBoxLeft;
                    tb_7.Width = itemTBoxWidth;
                    tb_7.Text = "";
                    Controls.Add(tb_7);

                    tb_8 = new TextBox();
                    tb_8.Width = 28;
                   // tb_8.Width = itemTBoxWidth;
                     tb_8.Top = rowTop;
                     tb_8.Left = qtyTBoxLeft;
                     tb_8.Text = "";
                     Controls.Add(tb_8);
                    break;
                case 5:
                    Label lbl5 = new Label();
                    lbl5.Top = rowTop + 2;
                    lbl5.Left = rowLblLeft;
                    lbl5.Width = rowLblWidth;
                    lbl5.Font = new Font(lbl5.Font.FontFamily.Name, 7);
                    lbl5.ForeColor = Color.Gray;
                    lbl5.Text = rowCount.ToString();
                    Controls.Add(lbl5);

                    tb_9 = new TextBox();
                    tb_9.Top = rowTop;
                    tb_9.Left = itemTBoxLeft;
                    tb_9.Width = itemTBoxWidth;
                    tb_9.Text = "";
                    Controls.Add(tb_9);

                    tb_10 = new TextBox();
                    tb_10.Width = 28;
                   // tb_10.Height = 20;
                    tb_10.Top = rowTop;
                    tb_10.Left = qtyTBoxLeft;
                    tb_10.Text = "";
                    Controls.Add(tb_10);
                    break;
                case 6:
                    Label lbl6 = new Label();
                    lbl6.Top = rowTop + 2;
                    lbl6.Left = rowLblLeft;
                    lbl6.Width = rowLblWidth;
                    lbl6.Font = new Font(lbl6.Font.FontFamily.Name, 7);
                    lbl6.ForeColor = Color.Gray;
                    lbl6.Text = rowCount.ToString();
                    Controls.Add(lbl6);

                    tb_11 = new TextBox();
                    tb_11.Top = rowTop;
                    tb_11.Left = itemTBoxLeft;
                    tb_11.Width = itemTBoxWidth;
                    tb_11.Text = "";
                    Controls.Add(tb_11);

                    tb_12 = new TextBox();
                    tb_12.Width = 28;
                  //  tb_12.Height = 20;
                    tb_12.Top = rowTop;
                    tb_12.Left = qtyTBoxLeft;
                    tb_12.Text = "";
                    Controls.Add(tb_12);
                    break;
                case 7:
                    Label lbl7 = new Label();
                    lbl7.Top = rowTop + 2;
                    lbl7.Left = rowLblLeft;
                    lbl7.Width = rowLblWidth;
                    lbl7.Font = new Font(lbl7.Font.FontFamily.Name, 7);
                    lbl7.ForeColor = Color.Gray;
                    lbl7.Text = rowCount.ToString();
                    Controls.Add(lbl7);

                    tb_13 = new TextBox();
                    tb_13.Top = rowTop;
                    tb_13.Left = itemTBoxLeft;
                    tb_13.Width = itemTBoxWidth;
                    tb_13.Text = "";
                    Controls.Add(tb_13);

                    tb_14 = new TextBox();
                    tb_14.Width = 28;
                  //  tb_14.Height = 20;
                    tb_14.Top = rowTop;
                    tb_14.Left = qtyTBoxLeft;
                    tb_14.Text = "";
                    Controls.Add(tb_14);
                    break;
                case 8:
                    Label lbl8 = new Label();
                    lbl8.Top = rowTop + 2;
                    lbl8.Left = rowLblLeft;
                    lbl8.Width = rowLblWidth;
                    lbl8.Font = new Font(lbl8.Font.FontFamily.Name, 7);
                    lbl8.ForeColor = Color.Gray;
                    lbl8.Text = rowCount.ToString();
                    Controls.Add(lbl8);

                    tb_15 = new TextBox();
                    tb_15.Top = rowTop;
                    tb_15.Left = itemTBoxLeft;
                    tb_15.Width = itemTBoxWidth;
                    tb_15.Text = "";
                    Controls.Add(tb_15);

                    tb_16 = new TextBox();
                    tb_16.Width = 28;
                   // tb_16.Height = 20;
                    tb_16.Top = rowTop;
                    tb_16.Left = qtyTBoxLeft;
                    tb_16.Text = "";
                    Controls.Add(tb_16);
                    break;
                case 9:
                    Label lbl9 = new Label();
                    lbl9.Top = rowTop + 2;
                    lbl9.Left = rowLblLeft;
                    lbl9.Width = rowLblWidth;
                    lbl9.Font = new Font(lbl9.Font.FontFamily.Name, 7);
                    lbl9.ForeColor = Color.Gray;
                    lbl9.Text = rowCount.ToString();
                    Controls.Add(lbl9);

                    tb_17 = new TextBox();
                    tb_17.Top = rowTop;
                    tb_17.Left = itemTBoxLeft;
                    tb_17.Width = itemTBoxWidth;
                    tb_17.Text = "";
                    Controls.Add(tb_17);

                    tb_18 = new TextBox();
                    tb_18.Width = 28;
                  //  tb_18.Height = 20;
                    tb_18.Top = rowTop;
                    tb_18.Left = qtyTBoxLeft;
                    tb_18.Text = "";
                    Controls.Add(tb_18);
                    break;
                case 10:
                    Label lbl10 = new Label();
                    lbl10.Top = rowTop + 2;
                    lbl10.Left = rowLblLeft;
                    lbl10.Width = rowLblWidth;
                    lbl10.Font = new Font(lbl10.Font.FontFamily.Name, 7);
                    lbl10.ForeColor = Color.Gray;
                    lbl10.Text = rowCount.ToString();
                    Controls.Add(lbl10);

                    tb_19 = new TextBox();
                    tb_19.Top = rowTop;
                    tb_19.Left = itemTBoxLeft;
                    tb_19.Width = itemTBoxWidth;
                    tb_19.Text = "";
                    Controls.Add(tb_19);

                    tb_20 = new TextBox();
                    tb_20.Width = 28;
                   // tb_20.Height = 20;
                    tb_20.Top = rowTop;
                    tb_20.Left = qtyTBoxLeft;
                    tb_20.Text = "";
                    Controls.Add(tb_20);
                    break;
                case 11:
                     Label lbl11 = new Label();
                    lbl11.Top = rowTop + 2;
                    lbl11.Left = rowLblLeft;
                    lbl11.Width = rowLblWidth;
                    lbl11.Font = new Font(lbl11.Font.FontFamily.Name, 7);
                    lbl11.ForeColor = Color.Gray;
                    lbl11.Text = rowCount.ToString();
                    Controls.Add(lbl11);

                    tb_21 = new TextBox();
                    tb_21.Top = rowTop;
                    tb_21.Left = itemTBoxLeft;
                    tb_21.Width = itemTBoxWidth;
                    tb_21.Text = "";
                    Controls.Add(tb_21);

                    tb_22 = new TextBox();
                    tb_22.Width = 28;
                   // tb_22.Height = 20;
                    tb_22.Top = rowTop;
                    tb_22.Left = qtyTBoxLeft;
                    tb_22.Text = "";
                    Controls.Add(tb_22);
                    break;
                case 12:
                     Label lbl12 = new Label();
                    lbl12.Top = rowTop + 2;
                    lbl12.Left = rowLblLeft;
                    lbl12.Width = rowLblWidth;
                    lbl12.Font = new Font(lbl12.Font.FontFamily.Name, 7);
                    lbl12.ForeColor = Color.Gray;
                    lbl12.Text = rowCount.ToString();
                    Controls.Add(lbl12);

                    tb_23 = new TextBox();
                    tb_23.Top = rowTop;
                    tb_23.Left = itemTBoxLeft;
                    tb_23.Width = itemTBoxWidth;
                    tb_23.Text = "";
                    Controls.Add(tb_23);

                    tb_24 = new TextBox();
                    tb_24.Width = 28;
                   // tb_24.Height = 20;
                    tb_24.Top = rowTop;
                    tb_24.Left = qtyTBoxLeft;
                    tb_24.Text = "";
                    Controls.Add(tb_24);
                    break;
                case 13:
                     Label lbl13 = new Label();
                    lbl13.Top = rowTop + 2;
                    lbl13.Left = rowLblLeft;
                    lbl13.Width = rowLblWidth;
                    lbl13.Font = new Font(lbl13.Font.FontFamily.Name, 7);
                    lbl13.ForeColor = Color.Gray;
                    lbl13.Text = rowCount.ToString();
                    Controls.Add(lbl13);

                    tb_25 = new TextBox();
                    tb_25.Top = rowTop;
                    tb_25.Left = itemTBoxLeft;
                    tb_25.Width = itemTBoxWidth;
                    tb_25.Text = "";
                    Controls.Add(tb_25);

                    tb_26 = new TextBox();
                    tb_26.Width = 28;
                   // tb_26.Height = 20;
                    tb_26.Top = rowTop;
                    tb_26.Left = qtyTBoxLeft;
                    tb_26.Text = "";
                    Controls.Add(tb_26);
                    break;
                case 14:
                     Label lbl14 = new Label();
                    lbl14.Top = rowTop + 2;
                    lbl14.Left = rowLblLeft;
                    lbl14.Width = rowLblWidth;
                    lbl14.Font = new Font(lbl14.Font.FontFamily.Name, 7);
                    lbl14.ForeColor = Color.Gray;
                    lbl14.Text = rowCount.ToString();
                    Controls.Add(lbl14);

                    tb_27 = new TextBox();
                    tb_27.Top = rowTop;
                    tb_27.Left = itemTBoxLeft;
                    tb_27.Width = itemTBoxWidth;
                    tb_27.Text = "";
                    Controls.Add(tb_27);

                    tb_28 = new TextBox();
                    tb_28.Width = 28;
                   // tb_28.Height = 20;
                    tb_28.Top = rowTop;
                    tb_28.Left = qtyTBoxLeft;
                    tb_28.Text = "";
                    Controls.Add(tb_28);
                    break;
                case 15:
                     Label lbl15 = new Label();
                    lbl15.Top = rowTop + 2;
                    lbl15.Left = rowLblLeft;
                    lbl15.Width = rowLblWidth;
                    lbl15.Font = new Font(lbl15.Font.FontFamily.Name, 7);
                    lbl15.ForeColor = Color.Gray;
                    lbl15.Text = rowCount.ToString();
                    Controls.Add(lbl15);

                    tb_29 = new TextBox();
                    tb_29.Top = rowTop;
                    tb_29.Left = itemTBoxLeft;
                    tb_29.Width = itemTBoxWidth;
                    tb_29.Text = "";
                    Controls.Add(tb_29);

                    tb_30 = new TextBox();
                    tb_30.Width = 28;
                   // tb_30.Height = 20;
                    tb_30.Top = rowTop;
                    tb_30.Left = qtyTBoxLeft;
                    tb_30.Text = "";
                    Controls.Add(tb_30);
                    break;
                case 16:
                     Label lbl16 = new Label();
                    lbl16.Top = rowTop + 2;
                    lbl16.Left = rowLblLeft;
                    lbl16.Width = rowLblWidth;
                    lbl16.Font = new Font(lbl16.Font.FontFamily.Name, 7);
                    lbl16.ForeColor = Color.Gray;
                    lbl16.Text = rowCount.ToString();
                    Controls.Add(lbl16);

                    tb_31 = new TextBox();
                    tb_31.Top = rowTop;
                    tb_31.Left = itemTBoxLeft;
                    tb_31.Width = itemTBoxWidth;
                    tb_31.Text = "";
                    Controls.Add(tb_31);

                    tb_32 = new TextBox();
                    tb_32.Width = 28;
                   // tb_32.Height = 20;
                    tb_32.Top = rowTop;
                    tb_32.Left = qtyTBoxLeft;
                    tb_32.Text = "";
                    Controls.Add(tb_32);
                    break;
                case 17:
                     Label lbl17 = new Label();
                    lbl17.Top = rowTop + 2;
                    lbl17.Left = rowLblLeft;
                    lbl17.Width = rowLblWidth;
                    lbl17.Font = new Font(lbl17.Font.FontFamily.Name, 7);
                    lbl17.ForeColor = Color.Gray;
                    lbl17.Text = rowCount.ToString();
                    Controls.Add(lbl17);

                    tb_33 = new TextBox();
                    tb_33.Top = rowTop;
                    tb_33.Left = itemTBoxLeft;
                    tb_33.Width = itemTBoxWidth;
                    tb_33.Text = "";
                    Controls.Add(tb_33);

                    tb_34 = new TextBox();
                    tb_34.Width = 28;
                   // tb_34.Height = 20;
                    tb_34.Top = rowTop;
                    tb_34.Left = qtyTBoxLeft;
                    tb_34.Text = "";
                    Controls.Add(tb_34);
                    break;
                case 18:
                     Label lbl18 = new Label();
                    lbl18.Top = rowTop + 2;
                    lbl18.Left = rowLblLeft;
                    lbl18.Width = rowLblWidth;
                    lbl18.Font = new Font(lbl18.Font.FontFamily.Name, 7);
                    lbl18.ForeColor = Color.Gray;
                    lbl18.Text = rowCount.ToString();
                    Controls.Add(lbl18);

                    tb_35 = new TextBox();
                    tb_35.Top = rowTop;
                    tb_35.Left = itemTBoxLeft;
                    tb_35.Width = itemTBoxWidth;
                    tb_35.Text = "";
                    Controls.Add(tb_35);

                    tb_36 = new TextBox();
                    tb_36.Width = 28;
                   // tb_36.Height = 20;
                    tb_36.Top = rowTop;
                    tb_36.Left = qtyTBoxLeft;
                    tb_36.Text = "";
                    Controls.Add(tb_36);
                    break;
                case 19:
                     Label lbl19 = new Label();
                    lbl19.Top = rowTop + 2;
                    lbl19.Left = rowLblLeft;
                    lbl19.Width = rowLblWidth;
                    lbl19.Font = new Font(lbl19.Font.FontFamily.Name, 7);
                    lbl19.ForeColor = Color.Gray;
                    lbl19.Text = rowCount.ToString();
                    Controls.Add(lbl19);

                    tb_37 = new TextBox();
                    tb_37.Top = rowTop;
                    tb_37.Left = itemTBoxLeft;
                    tb_37.Width = itemTBoxWidth;
                    tb_37.Text = "";
                    Controls.Add(tb_37);

                    tb_38 = new TextBox();
                    tb_38.Width = 28;
                   // tb_38.Height = 20;
                    tb_38.Top = rowTop;
                    tb_38.Left = qtyTBoxLeft;
                    tb_38.Text = "";
                    Controls.Add(tb_38);
                    break;
                case 20:
                     Label lbl20 = new Label();
                    lbl20.Top = rowTop + 2;
                    lbl20.Left = rowLblLeft;
                    lbl20.Width = rowLblWidth;
                    lbl20.Font = new Font(lbl20.Font.FontFamily.Name, 7);
                    lbl20.ForeColor = Color.Gray;
                    lbl20.Text = rowCount.ToString();
                    Controls.Add(lbl20);

                    tb_39 = new TextBox();
                    tb_39.Top = rowTop;
                    tb_39.Left = itemTBoxLeft;
                    tb_39.Width = itemTBoxWidth;
                    tb_39.Text = "";
                    Controls.Add(tb_39);

                    tb_40 = new TextBox();
                    tb_40.Width = 28;
                   // tb_40.Height = 20;
                    tb_40.Top = rowTop;
                    tb_40.Left = qtyTBoxLeft;
                    tb_40.Text = "";
                    Controls.Add(tb_40);
                    break;
                #endregion
            }
            rowTop += 20;
            rowCount++;
            groupBox1.SendToBack();
}

        public TextBox AddNewTextBox()
        {
           TextBox txt = new TextBox();
            this.Controls.Add(txt);
            txt.Top = rowTop;
            txt.Left = 30;
            txt.Text = "TextBox " + this.rowCount.ToString();
            return txt;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Using the Ad Hoc Label Printer  version 3" + Environment.NewLine +
                "Enter the ITEM number or the MFG number into the list on the left " + Environment.NewLine +
                "and indicate the number of copies you want printed in the QTY column." + Environment.NewLine +
                "The QTY defaults to 1 if you leave it blank." + Environment.NewLine + 
                "Click GO. The deatils of the first item in the list appear in the text" + Environment.NewLine +
                "boxes on the top right half of the form." + Environment.NewLine +
                "To Print click the ALL button to print all of the items in your list or" + Environment.NewLine +
                "click CURRENT to print only the item whose details are being displayed." + Environment.NewLine +
                "The drop list START POSITION is intended for those cases where you need" + Environment.NewLine +
                "to manually insert a sheet of labels into a printer. It is optimized for" + Environment.NewLine +
                "a specific type of sheet with 10 rows of 3 columns each. Leave it on position " +
                "1 if this doesn't work for you.");
        }
    }
}
