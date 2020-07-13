namespace WBD_GUI
{
    partial class Form3
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
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboPayment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewProducts
            // 
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Location = new System.Drawing.Point(86, 69);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.Size = new System.Drawing.Size(638, 294);
            this.dataGridViewProducts.TabIndex = 0;
            this.dataGridViewProducts.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewProducts_RowHeaderMouseClick);
            // 
            // btnAddOrder
            // 
            this.btnAddOrder.Location = new System.Drawing.Point(532, 368);
            this.btnAddOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(76, 27);
            this.btnAddOrder.TabIndex = 22;
            this.btnAddOrder.Text = "Zamów";
            this.btnAddOrder.UseVisualStyleBackColor = true;
            this.btnAddOrder.Click += new System.EventHandler(this.btnAddOrder_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(648, 368);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 27);
            this.button1.TabIndex = 23;
            this.button1.Text = "Anuluj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboPayment
            // 
            this.comboPayment.Enabled = false;
            this.comboPayment.FormattingEnabled = true;
            this.comboPayment.Location = new System.Drawing.Point(179, 379);
            this.comboPayment.Name = "comboPayment";
            this.comboPayment.Size = new System.Drawing.Size(138, 21);
            this.comboPayment.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(83, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Sposób płatności";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(336, 382);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Ilość sztuk";
            // 
            // txtAmount
            // 
            this.txtAmount.Enabled = false;
            this.txtAmount.Location = new System.Drawing.Point(399, 379);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(100, 20);
            this.txtAmount.TabIndex = 27;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.txtSearchProduct);
            this.groupBox10.Controls.Add(this.label41);
            this.groupBox10.Location = new System.Drawing.Point(86, 6);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(638, 57);
            this.groupBox10.TabIndex = 28;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Wyszukiwanie";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Location = new System.Drawing.Point(113, 23);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(200, 20);
            this.txtSearchProduct.TabIndex = 13;
            this.txtSearchProduct.TextChanged += new System.EventHandler(this.txtSearchProduct_TextChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(26, 26);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(39, 13);
            this.label41.TabIndex = 12;
            this.label41.Text = "Szukaj";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 437);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboPayment);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddOrder);
            this.Controls.Add(this.dataGridViewProducts);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboPayment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Label label41;
    }
}