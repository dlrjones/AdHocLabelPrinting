namespace AdHocLabelPrinting
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGo = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.tbItemInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbLabelDesc = new System.Windows.Forms.TextBox();
            this.tbLabelMfgCatNo = new System.Windows.Forms.TextBox();
            this.tbLabelItem = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pbBarCode = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.cbStartPos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbQty = new System.Windows.Forms.TextBox();
            this.btnPrintCurrent = new System.Windows.Forms.Button();
            this.gbPrint = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarCode)).BeginInit();
            this.panel2.SuspendLayout();
            this.gbPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.SystemColors.Control;
            this.btnGo.Location = new System.Drawing.Point(717, 15);
            this.btnGo.Margin = new System.Windows.Forms.Padding(4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(100, 70);
            this.btnGo.TabIndex = 1;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(840, 57);
            this.btnDone.Margin = new System.Windows.Forms.Padding(4);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(100, 28);
            this.btnDone.TabIndex = 10;
            this.btnDone.TabStop = false;
            this.btnDone.Text = "QUIT";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // tbItemInput
            // 
            this.tbItemInput.Location = new System.Drawing.Point(15, 34);
            this.tbItemInput.Margin = new System.Windows.Forms.Padding(4);
            this.tbItemInput.Multiline = true;
            this.tbItemInput.Name = "tbItemInput";
            this.tbItemInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbItemInput.Size = new System.Drawing.Size(217, 253);
            this.tbItemInput.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Enter One Number Per Line";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbLabelDesc);
            this.panel1.Controls.Add(this.tbLabelMfgCatNo);
            this.panel1.Controls.Add(this.tbLabelItem);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Location = new System.Drawing.Point(329, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 178);
            this.panel1.TabIndex = 6;
            // 
            // tbLabelDesc
            // 
            this.tbLabelDesc.Location = new System.Drawing.Point(21, 79);
            this.tbLabelDesc.Margin = new System.Windows.Forms.Padding(4);
            this.tbLabelDesc.Multiline = true;
            this.tbLabelDesc.Name = "tbLabelDesc";
            this.tbLabelDesc.Size = new System.Drawing.Size(311, 54);
            this.tbLabelDesc.TabIndex = 3;
            this.tbLabelDesc.TabStop = false;
            // 
            // tbLabelMfgCatNo
            // 
            this.tbLabelMfgCatNo.Location = new System.Drawing.Point(21, 47);
            this.tbLabelMfgCatNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbLabelMfgCatNo.Name = "tbLabelMfgCatNo";
            this.tbLabelMfgCatNo.Size = new System.Drawing.Size(311, 22);
            this.tbLabelMfgCatNo.TabIndex = 2;
            this.tbLabelMfgCatNo.TabStop = false;
            // 
            // tbLabelItem
            // 
            this.tbLabelItem.Location = new System.Drawing.Point(21, 15);
            this.tbLabelItem.Margin = new System.Windows.Forms.Padding(4);
            this.tbLabelItem.Name = "tbLabelItem";
            this.tbLabelItem.Size = new System.Drawing.Size(311, 22);
            this.tbLabelItem.TabIndex = 0;
            this.tbLabelItem.TabStop = false;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNext.Location = new System.Drawing.Point(243, 140);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(91, 25);
            this.btnNext.TabIndex = 8;
            this.btnNext.TabStop = false;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrev.Location = new System.Drawing.Point(21, 140);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(91, 25);
            this.btnPrev.TabIndex = 9;
            this.btnPrev.TabStop = false;
            this.btnPrev.Text = "Previous";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(840, 15);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 7;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pbBarCode
            // 
            this.pbBarCode.Location = new System.Drawing.Point(7, 14);
            this.pbBarCode.Margin = new System.Windows.Forms.Padding(4);
            this.pbBarCode.Name = "pbBarCode";
            this.pbBarCode.Size = new System.Drawing.Size(340, 49);
            this.pbBarCode.TabIndex = 10;
            this.pbBarCode.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pbBarCode);
            this.panel2.Location = new System.Drawing.Point(329, 208);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(353, 80);
            this.panel2.TabIndex = 11;
            this.panel2.TabStop = true;
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrintAll.Location = new System.Drawing.Point(13, 31);
            this.btnPrintAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(100, 28);
            this.btnPrintAll.TabIndex = 12;
            this.btnPrintAll.TabStop = false;
            this.btnPrintAll.Text = "ALL";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cbStartPos
            // 
            this.cbStartPos.FormattingEnabled = true;
            this.cbStartPos.Location = new System.Drawing.Point(845, 121);
            this.cbStartPos.Margin = new System.Windows.Forms.Padding(4);
            this.cbStartPos.Name = "cbStartPos";
            this.cbStartPos.Size = new System.Drawing.Size(72, 24);
            this.cbStartPos.TabIndex = 14;
            this.cbStartPos.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(836, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Start Position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "HMC Item #";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "MFG #";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(257, 101);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(723, 101);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "Qty To Print";
            // 
            // tbQty
            // 
            this.tbQty.Location = new System.Drawing.Point(729, 121);
            this.tbQty.Margin = new System.Windows.Forms.Padding(4);
            this.tbQty.Name = "tbQty";
            this.tbQty.Size = new System.Drawing.Size(65, 22);
            this.tbQty.TabIndex = 20;
            this.tbQty.TabStop = false;
            this.tbQty.Text = "1";
            // 
            // btnPrintCurrent
            // 
            this.btnPrintCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintCurrent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrintCurrent.Location = new System.Drawing.Point(136, 31);
            this.btnPrintCurrent.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintCurrent.Name = "btnPrintCurrent";
            this.btnPrintCurrent.Size = new System.Drawing.Size(100, 28);
            this.btnPrintCurrent.TabIndex = 23;
            this.btnPrintCurrent.TabStop = false;
            this.btnPrintCurrent.Text = "CURRENT";
            this.btnPrintCurrent.UseVisualStyleBackColor = true;
            this.btnPrintCurrent.Click += new System.EventHandler(this.btnPrintCurrent_Click);
            // 
            // gbPrint
            // 
            this.gbPrint.Controls.Add(this.btnPrintCurrent);
            this.gbPrint.Controls.Add(this.btnPrintAll);
            this.gbPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPrint.ForeColor = System.Drawing.Color.Blue;
            this.gbPrint.Location = new System.Drawing.Point(704, 176);
            this.gbPrint.Margin = new System.Windows.Forms.Padding(4);
            this.gbPrint.Name = "gbPrint";
            this.gbPrint.Padding = new System.Windows.Forms.Padding(4);
            this.gbPrint.Size = new System.Drawing.Size(251, 80);
            this.gbPrint.TabIndex = 24;
            this.gbPrint.TabStop = false;
            this.gbPrint.Text = "PRINT";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 309);
            this.Controls.Add(this.tbItemInput);
            this.Controls.Add(this.gbPrint);
            this.Controls.Add(this.tbQty);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbStartPos);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnGo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1002, 354);
            this.MinimumSize = new System.Drawing.Size(1002, 354);
            this.Name = "Form1";
            this.Text = "AdHoc Label Printing";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarCode)).EndInit();
            this.panel2.ResumeLayout(false);
            this.gbPrint.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.TextBox tbItemInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbLabelItem;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox tbLabelDesc;
        private System.Windows.Forms.TextBox tbLabelMfgCatNo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.PictureBox pbBarCode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPrintAll;
        private System.Windows.Forms.ComboBox cbStartPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbQty;
        private System.Windows.Forms.Button btnPrintCurrent;
        private System.Windows.Forms.GroupBox gbPrint;
    }
}
