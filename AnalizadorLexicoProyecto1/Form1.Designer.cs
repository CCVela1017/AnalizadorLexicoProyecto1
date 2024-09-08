namespace AnalizadorLexicoProyecto1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            token = new DataGridViewTextBoxColumn();
            type = new DataGridViewTextBoxColumn();
            cantidad = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            openFileDialog1 = new OpenFileDialog();
            richTextBox2 = new RichTextBox();
            label3 = new Label();
            lURL = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Cursor = Cursors.No;
            richTextBox1.Location = new Point(14, 48);
            richTextBox1.Margin = new Padding(3, 4, 3, 4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(575, 541);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // button1
            // 
            button1.BackColor = Color.GreenYellow;
            button1.Font = new Font("Times New Roman", 12F);
            button1.Location = new Point(599, 48);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(343, 49);
            button1.TabIndex = 1;
            button1.Text = "Abrir archivo de texto";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.LightSteelBlue;
            button2.Enabled = false;
            button2.Font = new Font("Times New Roman", 12F);
            button2.Location = new Point(599, 105);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(343, 59);
            button2.TabIndex = 2;
            button2.Text = "Analizar léxico del documento";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { token, type, cantidad });
            dataGridView1.Location = new Point(599, 235);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.Size = new Size(345, 257);
            dataGridView1.TabIndex = 3;
            // 
            // token
            // 
            dataGridViewCellStyle2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            token.DefaultCellStyle = dataGridViewCellStyle2;
            token.HeaderText = "Token";
            token.MinimumWidth = 6;
            token.Name = "token";
            token.Width = 125;
            // 
            // type
            // 
            dataGridViewCellStyle3.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            type.DefaultCellStyle = dataGridViewCellStyle3;
            type.HeaderText = "Tipo";
            type.MinimumWidth = 6;
            type.Name = "type";
            type.Width = 125;
            // 
            // cantidad
            // 
            dataGridViewCellStyle4.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cantidad.DefaultCellStyle = dataGridViewCellStyle4;
            cantidad.HeaderText = "Cantidad";
            cantidad.MinimumWidth = 6;
            cantidad.Name = "cantidad";
            cantidad.Width = 125;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F);
            label1.Location = new Point(599, 192);
            label1.Name = "label1";
            label1.Size = new Size(167, 22);
            label1.TabIndex = 4;
            label1.Text = "Léxico encontrado: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(193, 12);
            label2.Name = "label2";
            label2.Size = new Size(228, 31);
            label2.TabIndex = 5;
            label2.Text = "Analizador Léxico";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(14, 629);
            richTextBox2.Margin = new Padding(3, 4, 3, 4);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(575, 127);
            richTextBox2.TabIndex = 6;
            richTextBox2.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 12F);
            label3.Location = new Point(14, 600);
            label3.Name = "label3";
            label3.Size = new Size(91, 22);
            label3.TabIndex = 7;
            label3.Text = "SALIDA: ";
            // 
            // lURL
            // 
            lURL.AutoSize = true;
            lURL.Location = new Point(625, 172);
            lURL.Name = "lURL";
            lURL.Size = new Size(0, 20);
            lURL.TabIndex = 8;
            lURL.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.AntiqueWhite;
            ClientSize = new Size(955, 773);
            Controls.Add(lURL);
            Controls.Add(label3);
            Controls.Add(richTextBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Analizador Léxico";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button button1;
        private Button button2;
        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn token;
        private DataGridViewTextBoxColumn type;
        private DataGridViewTextBoxColumn cantidad;
        private OpenFileDialog openFileDialog1;
        private RichTextBox richTextBox2;
        private Label label3;
        private Label lURL;
    }
}
