﻿namespace AdHocLabelPrinting
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
            this.rbItems = new System.Windows.Forms.RadioButton();
            this.rbMfg = new System.Windows.Forms.RadioButton();
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
            this.btnGo.Location = new System.Drawing.Point(538, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 57);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(630, 46);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 1;
            this.btnDone.Text = "QUIT";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // tbItemInput
            // 
            this.tbItemInput.Location = new System.Drawing.Point(11, 73);
            this.tbItemInput.Multiline = true;
            this.tbItemInput.Name = "tbItemInput";
            this.tbItemInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbItemInput.Size = new System.Drawing.Size(164, 161);
            this.tbItemInput.TabIndex = 2;
            // 
            // rbItems
            // 
            this.rbItems.AutoSize = true;
			this.rbItems.Checked = false;
            this.rbItems.Location = new System.Drawing.Point(11, 12);
            this.rbItems.Name = "rbItems";
            this.rbItems.Size = new System.Drawing.Size(117, 17);
            this.rbItems.TabIndex = 3;
            this.rbItems.Text = "HMC Item Numbers";
            this.rbItems.UseVisualStyleBackColor = true;
			this.rbItems.Visible = true;
            // 
            // rbMfg
            // 
            this.rbMfg.AutoSize = true;
            this.rbMfg.Location = new System.Drawing.Point(10, 30);
            this.rbMfg.Name = "rbMfg";
            this.rbMfg.Size = new System.Drawing.Size(132, 17);
            this.rbMfg.TabIndex = 4;
            this.rbMfg.Text = "MFG Catalog Numbers";
            this.rbMfg.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
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
            this.panel1.Location = new System.Drawing.Point(247, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 145);
            this.panel1.TabIndex = 6;
            // 
            // tbLabelDesc
            // 
            this.tbLabelDesc.Location = new System.Drawing.Point(16, 64);
            this.tbLabelDesc.Multiline = true;
            this.tbLabelDesc.Name = "tbLabelDesc";
            this.tbLabelDesc.Size = new System.Drawing.Size(234, 45);
            this.tbLabelDesc.TabIndex = 3;
            // 
            // tbLabelMfgCatNo
            // 
            this.tbLabelMfgCatNo.Location = new System.Drawing.Point(16, 38);
            this.tbLabelMfgCatNo.Name = "tbLabelMfgCatNo";
            this.tbLabelMfgCatNo.Size = new System.Drawing.Size(234, 20);
            this.tbLabelMfgCatNo.TabIndex = 2;
            // 
            // tbLabelItem
            // 
            this.tbLabelItem.Location = new System.Drawing.Point(16, 12);
            this.tbLabelItem.Name = "tbLabelItem";
            this.tbLabelItem.Size = new System.Drawing.Size(234, 20);
            this.tbLabelItem.TabIndex = 0;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNext.Location = new System.Drawing.Point(182, 114);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(68, 20);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Enabled = false;
            this.btnPrev.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrev.Location = new System.Drawing.Point(16, 114);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(68, 20);
            this.btnPrev.TabIndex = 9;
            this.btnPrev.Text = "Previous";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(630, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pbBarCode
            // 
            this.pbBarCode.Location = new System.Drawing.Point(5, 11);
            this.pbBarCode.Name = "pbBarCode";
            this.pbBarCode.Size = new System.Drawing.Size(255, 40);
            this.pbBarCode.TabIndex = 10;
            this.pbBarCode.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pbBarCode);
            this.panel2.Location = new System.Drawing.Point(247, 169);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(265, 65);
            this.panel2.TabIndex = 11;
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrintAll.Location = new System.Drawing.Point(10, 25);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(75, 23);
            this.btnPrintAll.TabIndex = 12;
            this.btnPrintAll.Text = "ALL";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cbStartPos
            // 
            this.cbStartPos.FormattingEnabled = true;
            this.cbStartPos.Location = new System.Drawing.Point(634, 98);
            this.cbStartPos.Name = "cbStartPos";
            this.cbStartPos.Size = new System.Drawing.Size(55, 21);
            this.cbStartPos.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(627, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Start Position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "HMC Item #";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "MFG #";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(542, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Qty To Print";
            // 
            // tbQty
            // 
            this.tbQty.Location = new System.Drawing.Point(547, 98);
            this.tbQty.Name = "tbQty";
            this.tbQty.Size = new System.Drawing.Size(50, 20);
            this.tbQty.TabIndex = 20;
            this.tbQty.Text = "1";
            // 
            // btnPrintCurrent
            // 
            this.btnPrintCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintCurrent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrintCurrent.Location = new System.Drawing.Point(102, 25);
            this.btnPrintCurrent.Name = "btnPrintCurrent";
            this.btnPrintCurrent.Size = new System.Drawing.Size(75, 23);
            this.btnPrintCurrent.TabIndex = 23;
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
            this.gbPrint.Location = new System.Drawing.Point(528, 143);
            this.gbPrint.Name = "gbPrint";
            this.gbPrint.Size = new System.Drawing.Size(188, 65);
            this.gbPrint.TabIndex = 24;
            this.gbPrint.TabStop = false;
            this.gbPrint.Text = "PRINT";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 258);
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
            this.Controls.Add(this.rbMfg);
            this.Controls.Add(this.rbItems);
            this.Controls.Add(this.tbItemInput);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnGo);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(756, 296);
            this.MinimumSize = new System.Drawing.Size(756, 296);
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
        private System.Windows.Forms.RadioButton rbItems;
        private System.Windows.Forms.RadioButton rbMfg;
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