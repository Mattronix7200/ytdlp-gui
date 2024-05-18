namespace ytdl_gui
{
    partial class Form6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form6));
            splitter1 = new Splitter();
            listBox1 = new ListBox();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            label1 = new Label();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button2 = new Button();
            label6 = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // splitter1
            // 
            splitter1.BackColor = SystemColors.ScrollBar;
            splitter1.Location = new Point(0, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(75, 301);
            splitter1.TabIndex = 0;
            splitter1.TabStop = false;
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 20;
            listBox1.Items.AddRange(new object[] { "Domyślne ustawienia", "Pobieranie muzyki - MP3", "Pobieranie muzyki - ALAC", "Pobieranie filmu - najlepsza jakość", "Pobieranie filmu z ograniczeniem daty", "Pobieranie filmu z ograniczeniem rozmiaru", "Pobieranie co 2 filmu z playlisty", "Pobieranie filmu z napisami polskimi", "Pobieranie filmu z napisami angielskimi" });
            listBox1.Location = new Point(91, 15);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(431, 244);
            listBox1.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Enabled = false;
            textBox1.Location = new Point(142, 266);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(284, 23);
            textBox1.TabIndex = 3;
            textBox1.WordWrap = false;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ScrollBar;
            pictureBox1.BackgroundImage = Properties.Resources.import;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Location = new Point(14, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(38, 41);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.ScrollBar;
            pictureBox2.BackgroundImage = Properties.Resources.export;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Location = new Point(14, 85);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(38, 41);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = SystemColors.ScrollBar;
            pictureBox3.BackgroundImage = Properties.Resources.add_file;
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Location = new Point(14, 162);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(38, 41);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = SystemColors.ScrollBar;
            pictureBox4.BackgroundImage = Properties.Resources.import__1_;
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.Cursor = Cursors.Hand;
            pictureBox4.Location = new Point(14, 233);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(38, 39);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 7;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(91, 270);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 8;
            label1.Text = "Nazwa:";
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.Enabled = false;
            button1.Location = new Point(484, 266);
            button1.Name = "button1";
            button1.Size = new Size(38, 23);
            button1.TabIndex = 9;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ScrollBar;
            label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(11, 60);
            label2.Name = "label2";
            label2.Size = new Size(49, 13);
            label2.TabIndex = 10;
            label2.Text = "IMPORT";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ScrollBar;
            label3.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label3.ForeColor = SystemColors.ControlDarkDark;
            label3.Location = new Point(11, 137);
            label3.Name = "label3";
            label3.Size = new Size(54, 13);
            label3.TabIndex = 11;
            label3.Text = "EKSPORT";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ScrollBar;
            label4.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label4.ForeColor = SystemColors.ControlDarkDark;
            label4.Location = new Point(10, 210);
            label4.Name = "label4";
            label4.Size = new Size(42, 13);
            label4.TabIndex = 12;
            label4.Text = "NOWY";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ScrollBar;
            label5.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label5.ForeColor = SystemColors.ControlDarkDark;
            label5.Location = new Point(9, 279);
            label5.Name = "label5";
            label5.Size = new Size(57, 13);
            label5.TabIndex = 13;
            label5.Text = "WCZYTAJ";
            // 
            // button2
            // 
            button2.Cursor = Cursors.Hand;
            button2.Location = new Point(432, 266);
            button2.Name = "button2";
            button2.Size = new Size(46, 23);
            button2.TabIndex = 14;
            button2.Text = "Usuń";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label6
            // 
            label6.BackColor = Color.Salmon;
            label6.Enabled = false;
            label6.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label6.ForeColor = Color.SeaShell;
            label6.Location = new Point(91, 237);
            label6.Name = "label6";
            label6.Size = new Size(431, 21);
            label6.TabIndex = 15;
            label6.Text = "Podaj nazwę dla pliku konfiguracyjnego!";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.Visible = false;
            // 
            // label7
            // 
            label7.BackColor = Color.SlateGray;
            label7.Enabled = false;
            label7.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label7.ForeColor = Color.SeaShell;
            label7.Location = new Point(91, 237);
            label7.Name = "label7";
            label7.Size = new Size(431, 21);
            label7.TabIndex = 16;
            label7.Text = "Aby utworzyć kopię bieżącej konfiguracji, podaj nazwę i naciśnij OK.";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            label7.Visible = false;
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(534, 301);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(button2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(listBox1);
            Controls.Add(splitter1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(550, 340);
            MinimizeBox = false;
            MinimumSize = new Size(550, 340);
            Name = "Form6";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Edytor plików konfiguracyjnych";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Splitter splitter1;
        private ListBox listBox1;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Label label1;
        private Button button1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button button2;
        private Label label6;
        private Label label7;
    }
}