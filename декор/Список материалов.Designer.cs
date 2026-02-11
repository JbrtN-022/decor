namespace декор
{
    partial class Список_материалов
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxMaterial = new System.Windows.Forms.ComboBox();
            this.textBoxAddKolVo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxUppKolVo = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpp = new System.Windows.Forms.Button();
            this.textBoxUppMat = new System.Windows.Forms.TextBox();
            this.buttonDel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Gabriola", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(152, 8);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(271, 36);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(217)))), ((int)(((byte)(178)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 68);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(739, 265);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gabriola", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "выбери продукцию";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxAddKolVo);
            this.groupBox1.Controls.Add(this.comboBoxMaterial);
            this.groupBox1.Location = new System.Drawing.Point(13, 345);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 225);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "добавление";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxUppMat);
            this.groupBox3.Controls.Add(this.buttonUpp);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBoxUppKolVo);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(295, 345);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 225);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "изменение";
            // 
            // comboBoxMaterial
            // 
            this.comboBoxMaterial.Font = new System.Drawing.Font("Gabriola", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxMaterial.FormattingEnabled = true;
            this.comboBoxMaterial.Location = new System.Drawing.Point(96, 39);
            this.comboBoxMaterial.Name = "comboBoxMaterial";
            this.comboBoxMaterial.Size = new System.Drawing.Size(174, 36);
            this.comboBoxMaterial.TabIndex = 0;
            // 
            // textBoxAddKolVo
            // 
            this.textBoxAddKolVo.Location = new System.Drawing.Point(96, 81);
            this.textBoxAddKolVo.Name = "textBoxAddKolVo";
            this.textBoxAddKolVo.Size = new System.Drawing.Size(174, 40);
            this.textBoxAddKolVo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = "материал";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 35);
            this.label3.TabIndex = 3;
            this.label3.Text = "количество";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 35);
            this.label4.TabIndex = 7;
            this.label4.Text = "количество";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 35);
            this.label5.TabIndex = 6;
            this.label5.Text = "материал";
            // 
            // textBoxUppKolVo
            // 
            this.textBoxUppKolVo.Location = new System.Drawing.Point(96, 98);
            this.textBoxUppKolVo.Name = "textBoxUppKolVo";
            this.textBoxUppKolVo.Size = new System.Drawing.Size(175, 40);
            this.textBoxUppKolVo.TabIndex = 5;
            this.textBoxUppKolVo.TextChanged += new System.EventHandler(this.textBoxUppKolVo_TextChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(96)))), ((int)(((byte)(51)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonAdd.Location = new System.Drawing.Point(12, 166);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(258, 38);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "добавление";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpp
            // 
            this.buttonUpp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(96)))), ((int)(((byte)(51)))));
            this.buttonUpp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpp.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonUpp.Location = new System.Drawing.Point(6, 166);
            this.buttonUpp.Name = "buttonUpp";
            this.buttonUpp.Size = new System.Drawing.Size(265, 38);
            this.buttonUpp.TabIndex = 5;
            this.buttonUpp.Text = "изменить";
            this.buttonUpp.UseVisualStyleBackColor = false;
            this.buttonUpp.Click += new System.EventHandler(this.buttonUpp_Click);
            // 
            // textBoxUppMat
            // 
            this.textBoxUppMat.Location = new System.Drawing.Point(96, 37);
            this.textBoxUppMat.Name = "textBoxUppMat";
            this.textBoxUppMat.ReadOnly = true;
            this.textBoxUppMat.Size = new System.Drawing.Size(175, 40);
            this.textBoxUppMat.TabIndex = 8;
            // 
            // buttonDel
            // 
            this.buttonDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(96)))), ((int)(((byte)(51)))));
            this.buttonDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDel.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonDel.Location = new System.Drawing.Point(578, 511);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(174, 38);
            this.buttonDel.TabIndex = 9;
            this.buttonDel.Text = "удалить";
            this.buttonDel.UseVisualStyleBackColor = false;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // Список_материалов
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 582);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox1);
            this.Font = new System.Drawing.Font("Gabriola", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.Name = "Список_материалов";
            this.Text = "Список_материалов";
            this.Load += new System.EventHandler(this.Список_материалов_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAddKolVo;
        private System.Windows.Forms.ComboBox comboBoxMaterial;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxUppKolVo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxUppMat;
        private System.Windows.Forms.Button buttonDel;
    }
}