using System;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Devices;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using File = System.IO.File;
using CheckBox = System.Windows.Forms.CheckBox;
using TextBox = System.Windows.Forms.TextBox;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Reflection.Emit;
using System.Drawing.Imaging;
using System.Runtime.Intrinsics.X86;
using System.Net;

namespace ytdl_gui
{
    public partial class Form1 : Form
    {
        int languageSelected;
        string iniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/translate/en.ini");
        Dictionary<string, string> translations = new Dictionary<string, string>();

        private string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/config.ini");
        private string queuePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/qsave.ini");
        string localVersionFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/update/version.txt");

        private Form7 form7;
        private async void showSplashScreen()
        {
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            form7 = new Form7();
            form7.Show();
            form7.label1.Text = "Sprawdzam aktualizacje...";
            CheckForUpdates();
            form7.label1.Text = "Ładowanie ustawień...";
            await Task.Delay(2500);
            form7.label1.Text = "Uruchamianie aplikacji...";
            await Task.Delay(500);
            form7.Close();
            this.ShowInTaskbar = true;
            this.Opacity = 1;
        }

        public Form1()
        {
            showSplashScreen();
            InitializeComponent();
            CheckForUpdatesShowDialog();
            LoadConfig();
            bkgSet();

            if (langSel == 1)
            {
                languageSelected = 1;
                lang1.Enabled = true;
                lang2.Enabled = false;
                enlabel.BackColor = Color.FromArgb(171, 250, 208);
                lang2.BackColor = Color.FromArgb(171, 250, 208);
                pllabel.BackColor = Color.White;
                lang1.BackColor = Color.White;
            }
            if (langSel == 0)
            {
                languageSelected = 0;
                lang1.Enabled = false;
                lang2.Enabled = true;
                enlabel.BackColor = Color.White;
                lang2.BackColor = Color.White;
                pllabel.BackColor = Color.FromArgb(171, 250, 208);
                lang1.BackColor = Color.FromArgb(171, 250, 208);
            }

            if (comboBox1.SelectedItem != "MP4" && comboBox1.SelectedItem != "WEBM")
            {
                checkBox30.Enabled = false;
                checkBox30.Checked = false;
            }
            else
            {
                checkBox30.Enabled = true;
            }

            if (checkBox31.Checked == true)
            {
                groupBox11.BackColor = Color.White;
                date1label.Enabled = true;
                date2label.Enabled = true;
                date3label.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
                isDateOption1.Enabled = true;
                isDateOption2.Enabled = true;
                isDateOption3.Enabled = true;
            }
            else
            {
                groupBox11.BackColor = Color.Gainsboro;
                date1label.Enabled = false;
                date2label.Enabled = false;
                date3label.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
                isDateOption1.Enabled = false;
                isDateOption2.Enabled = false;
                isDateOption3.Enabled = false;
            }

            if (checkBox8.Checked == true)
            {
                checkBox9.Enabled = false;
                checkBox9.Checked = false;
            }
            else
            {
                checkBox9.Enabled = true;
            }

            if (checkBox9.Checked == true)
            {
                checkBox8.Enabled = false;
                checkBox8.Checked = false;
            }
            else
            {
                checkBox8.Enabled = true;
            }

            if (checkBox11.Checked == true)
            {
                textBox15.Enabled = true;
            }
            else
            {
                textBox15.Enabled = false;

            }

            if (checkBox12.Checked == true)
            {
                checkBox11.Enabled = true;
            }
            else
            {
                checkBox11.Enabled = false;
            }

            if (checkBox24.Checked == true)
            {
                comboBox10.Enabled = true;
            }
            else
            {
                comboBox10.Enabled = false;
            }
            SetLanguage();
            SaveConfig();
            label50.Enabled = false;
            label50.Visible = false;
            flowLayoutPanel1.Enabled = false;
            flowLayoutPanel1.Visible = false;
            panel2.Enabled = false;
            panel2.Visible = false;
            panel3.Enabled = false;
            panel3.Visible = false;
            button2.Enabled = false;
            button2.Visible = false;
            button10.Enabled = false;
            button10.Visible = false;
            updatenewInfo.ForeColor = Color.White;
            flowLayoutPanel1.PerformLayout();
            //settings
            bkg1.Click += new EventHandler(ControlChanged);
            bkg2.Click += new EventHandler(ControlChanged);
            bkg3.Click += new EventHandler(ControlChanged);
            bkg4.Click += new EventHandler(ControlChanged);
            bkg5.Click += new EventHandler(ControlChanged);
            bkg6.Click += new EventHandler(ControlChanged);
            bkg7.Click += new EventHandler(ControlChanged);
            checkBox1.CheckedChanged += new EventHandler(ControlChanged);
            checkBox2.CheckedChanged += new EventHandler(ControlChanged);
            checkBox3.CheckedChanged += new EventHandler(ControlChanged);
            checkBox4.CheckedChanged += new EventHandler(ControlChanged);
            checkBox5.CheckedChanged += new EventHandler(ControlChanged);
            checkBox6.CheckedChanged += new EventHandler(ControlChanged);
            checkBox7.CheckedChanged += new EventHandler(ControlChanged);
            checkBox8.CheckedChanged += new EventHandler(ControlChanged);
            checkBox9.CheckedChanged += new EventHandler(ControlChanged);
            checkBox10.CheckedChanged += new EventHandler(ControlChanged);
            checkBox11.CheckedChanged += new EventHandler(ControlChanged);
            checkBox12.CheckedChanged += new EventHandler(ControlChanged);
            checkBox13.CheckedChanged += new EventHandler(ControlChanged);
            checkBox14.CheckedChanged += new EventHandler(ControlChanged);
            checkBox15.CheckedChanged += new EventHandler(ControlChanged);
            checkBox16.CheckedChanged += new EventHandler(ControlChanged);
            checkBox17.CheckedChanged += new EventHandler(ControlChanged);
            checkBox18.CheckedChanged += new EventHandler(ControlChanged);
            checkBox19.CheckedChanged += new EventHandler(ControlChanged);
            checkBox20.CheckedChanged += new EventHandler(ControlChanged);
            checkBox21.CheckedChanged += new EventHandler(ControlChanged);
            checkBox22.CheckedChanged += new EventHandler(ControlChanged);
            checkBox23.CheckedChanged += new EventHandler(ControlChanged);
            checkBox24.CheckedChanged += new EventHandler(ControlChanged);
            checkBox25.CheckedChanged += new EventHandler(ControlChanged);
            checkBox27.CheckedChanged += new EventHandler(ControlChanged);
            checkBox28.CheckedChanged += new EventHandler(ControlChanged);
            checkBox29.CheckedChanged += new EventHandler(ControlChanged);
            checkBox30.CheckedChanged += new EventHandler(ControlChanged);
            checkBox31.CheckedChanged += new EventHandler(ControlChanged);
            isDateOption1.CheckedChanged += new EventHandler(ControlChanged);
            isDateOption2.CheckedChanged += new EventHandler(ControlChanged);
            isDateOption3.CheckedChanged += new EventHandler(ControlChanged);
            textBox1.TextChanged += new EventHandler(ControlChanged);
            textBox2.TextChanged += new EventHandler(ControlChanged);
            textBox3.TextChanged += new EventHandler(ControlChanged);
            textBox4.TextChanged += new EventHandler(ControlChanged);
            textBox5.TextChanged += new EventHandler(ControlChanged);
            textBox6.TextChanged += new EventHandler(ControlChanged);
            textBox7.TextChanged += new EventHandler(ControlChanged);
            textBox8.TextChanged += new EventHandler(ControlChanged);
            textBox9.TextChanged += new EventHandler(ControlChanged);
            textBox10.TextChanged += new EventHandler(ControlChanged);
            textBox11.TextChanged += new EventHandler(ControlChanged);
            textBox12.TextChanged += new EventHandler(ControlChanged);
            textBox13.TextChanged += new EventHandler(ControlChanged);
            textBox14.TextChanged += new EventHandler(ControlChanged);
            textBox15.TextChanged += new EventHandler(ControlChanged);
            textBox16.TextChanged += new EventHandler(ControlChanged);
            textBox17.TextChanged += new EventHandler(ControlChanged);
            textBox18.TextChanged += new EventHandler(ControlChanged);
            textBox20.TextChanged += new EventHandler(ControlChanged);
            comboBox1.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox2.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox3.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox4.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox5.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox6.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox7.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox8.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox9.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox10.SelectedIndexChanged += new EventHandler(ControlChanged);
            comboBox11.SelectedIndexChanged += new EventHandler(ControlChanged);
            dateTimePicker1.ValueChanged += new EventHandler(ControlChanged);
            dateTimePicker2.ValueChanged += new EventHandler(ControlChanged);
            dateTimePicker3.ValueChanged += new EventHandler(ControlChanged);
            this.textBox19.Enter += new System.EventHandler(this.textBox19_Enter);
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
            checkForFiles();
        }


        private string binFile1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./bin/ffmpeg.exe");
        private string binFile2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./bin/ffprobe.exe");
        private string binFile3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./bin/spotdl.exe");
        private string binFile4 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./bin/ytdlp.exe");
        private string binFile5 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./bin/pcsmgt.bat");
        private string binFile6 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./UpdaterMain.exe");

        private void CheckForUpdates()
        {
            string localVersion = File.ReadAllText(localVersionFilePath).Trim();
            string remoteVersionFileUrl = "https://windowsbase.pl/uploads/apps/ydlp-gui/version.txt";
            WebClient webClient = new();
            string remoteVersionFileContent = webClient.DownloadString(remoteVersionFileUrl);
            string[] remoteVersionLines = remoteVersionFileContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string remoteVersion = "";
            string updateUrl = "";
            foreach (string line in remoteVersionLines)
            {
                string[] parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    remoteVersion = parts[0].Trim('"');
                    updateUrl = parts[1].Trim('"');
                }
            }
            if (localVersion != remoteVersion)
            {
                if (languageSelected == 1)
                {
                    var result = MessageBox.Show("A newer version of this application is available. Do you want to install updates now?", "New version available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start(binFile6);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    var result = MessageBox.Show("Dostępna jest nowsza wersja tej aplikacji. Czy chcesz zainstalować aktualizacje teraz?", "Dostępna aktualizacja programu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start(binFile6);
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void CheckForUpdatesShowDialog()
        {
            string localVersion = File.ReadAllText(localVersionFilePath).Trim();
            string remoteVersionFileUrl = "https://windowsbase.pl/uploads/apps/ydlp-gui/version.txt";
            WebClient webClient = new();
            string remoteVersionFileContent = webClient.DownloadString(remoteVersionFileUrl);
            string[] remoteVersionLines = remoteVersionFileContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string remoteVersion = "";
            string updateUrl = "";
            foreach (string line in remoteVersionLines)
            {
                string[] parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    remoteVersion = parts[0].Trim('"');
                    updateUrl = parts[1].Trim('"');
                }
            }

            if (localVersion != remoteVersion)
            {
                updatenewInfo.Enabled = true;
                updatenewInfo.Visible = true;
                frameStatus.Visible = true;
                updatenewInfo.ForeColor = Color.White;
                if (languageSelected == 1)
                {
                    updatenewInfo.Text = translations["Form1.label57"];
                }
                else
                {
                    // return
                }

            }
        }

        private void lang1_Click(object sender, EventArgs e)
        {
            languageSelected = 0;
            langSel = 0;
            SaveConfig();
            SetLanguage();

            panel5.SuspendLayout();
            sel1.Visible = true;
            sel2.Visible = false;
            enlabel.BackColor = Color.White;
            lang2.BackColor = Color.White;
            pllabel.BackColor = Color.FromArgb(171, 250, 208);
            lang1.BackColor = Color.FromArgb(171, 250, 208);
            panel5.Refresh();
            panel5.ResumeLayout();

            var result = MessageBox.Show("Zamierzasz zmienić język na polski, jednak zmiana zostanie zapisana dopiero przy ponownym uruchomieniu aplikacji.\r\n\r\nCzy chcesz uruchomić ponownie teraz?", "Zmiana języka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void lang2_Click(object sender, EventArgs e)
        {
            languageSelected = 1;
            langSel = 1;
            SaveConfig();
            SetLanguage();

            panel5.SuspendLayout();
            sel2.Visible = true;
            sel1.Visible = false;
            enlabel.BackColor = Color.FromArgb(171, 250, 208);
            lang2.BackColor = Color.FromArgb(171, 250, 208);
            pllabel.BackColor = Color.White;
            lang1.BackColor = Color.White;
            panel5.Refresh();
            panel5.ResumeLayout();
        }


        private void SetLanguage()
        {
            if (languageSelected == 1)
            {
                Dictionary<string, string> translations = ReadIniFile(iniFilePath);
                ApplyTranslationsToControls(this, translations, this.Name);
                sel2.Enabled = true;
                sel2.Visible = true;
                sel1.Enabled = false;
                sel1.Visible = false;
                this.Refresh();
            }

            if (languageSelected == 0)
            {
                sel1.Enabled = true;
                sel1.Visible = true;
                sel2.Enabled = false;
                sel2.Visible = false;
                this.Refresh();
                // do nothing...
            }


        }

        private void ApplyTranslationsToControls(Control parent, Dictionary<string, string> translations, string formName)
        {
            foreach (Control c in parent.Controls)
            {
                if (formName.StartsWith("Form") && (c is TextBox || c is CheckBox ||
                    c is System.Windows.Forms.Label || c is System.Windows.Forms.GroupBox ||
                    c is System.Windows.Forms.Button || c is RichTextBox))
                {
                    string key = formName + "." + c.Name;
                    if (translations.ContainsKey(key))
                    {
                        c.Text = translations[key];
                    }
                }
                if (c.HasChildren)
                {
                    ApplyTranslationsToControls(c, translations, formName);
                }
            }
        }


        private Dictionary<string, string> ReadIniFile(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();
                            translations[key] = value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText($"Wystąpił błąd podczas ustawiania języka programu (Error: {ex.Message})");
            }

            return translations;
        }


        public void checkForFiles()
        {
            if (!File.Exists(binFile1))
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);

                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.err01"]);
                }
                else
                {
                    richTextBox1.AppendText("\r\n▲ ERR01 ▲ Krytyczny wyjątek- Nie znaleziono wymaganego składnika programu. Wymagana ponowna instalacja programu.");
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                this.Enabled = false;
            }

            else if (!File.Exists(binFile2))
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);

                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.err02"]);
                }
                else
                {
                    richTextBox1.AppendText("\r\n▲ ERR02 ▲ Krytyczny wyjątek- Nie znaleziono wymaganego składnika programu. Wymagana ponowna instalacja programu.");
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                this.Enabled = false;
            }

            else if (!File.Exists(binFile3))
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.err03"]);
                }
                else
                {
                    richTextBox1.AppendText("\r\n▲ ERR03 ▲ Krytyczny wyjątek- Nie znaleziono wymaganego składnika programu. Wymagana ponowna instalacja programu.");
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                this.Enabled = false;
            }

            else if (!File.Exists(binFile5))
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.err08"]);
                }
                else
                {
                    richTextBox1.AppendText("\r\n▲ ERR08 ▲ Krytyczny wyjątek- Nie znaleziono wymaganego składnika programu. Wymagana ponowna instalacja programu.");
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                this.Enabled = false;
            }

            else if (!File.Exists(binFile6))
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.err09"]);
                }
                else
                {
                    richTextBox1.AppendText("\r\n▲ ERR09 ▲ Krytyczny wyjątek- Nie znaleziono wymaganego składnika programu. Wymagana ponowna instalacja programu.");
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                this.Enabled = false;
            }


            else if (!File.Exists(binFile4))
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);

                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.err04"]);
                }
                else
                {
                    richTextBox1.AppendText("\r\n▲ ERR04 ▲ Krytyczny wyjątek- Nie znaleziono wymaganego składnika programu. Wymagana ponowna instalacja programu.");
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                this.Enabled = false;
            }
            else
            {
                if (languageSelected == 1)
                {
                    richTextBox1.AppendText(translations["Form1.err00"]);
                }
                else
                {
                    richTextBox1.AppendText($"Program uruchomiony prawidłowo. Oczekiwanie na akcję użytkownika.");
                }
            }
        }

        Dictionary<string, string> comboBox3Map = new Dictionary<string, string>()
        {
            {"0", "0"},
            {"1", "1"},
            {"2", "2"},
            {"3", "3"},
            {"4", "4"},
            {"5", "5"},
            {"6", "6"},
            {"7", "7"},
            {"8", "8"},
            {"9", "9"},
            {"10", "10"},
            {"16 Kbps", "16K"},
            {"32 Kbps", "32K"},
            {"96 Kbps", "96K"},
            {"128 Kbps", "128K"},
            {"192 Kbps", "192K"},
            {"256 Kbps", "256K"},
            {"320 Kbps", "320K"}
        };

        Dictionary<string, string> comboBox11Map = new Dictionary<string, string>()
        {
            { "4K (2160p)", "--postprocessor-args \"VideoConvertor:-vf scale=-1:2160\" " },
            { "2K (1440p)", "--postprocessor-args \"VideoConvertor:-vf scale=-1:1440\" " },
            { "FullHD (1080p)", "--postprocessor-args \"VideoConvertor:-vf scale=-1:1080\" " },
            { "HD (720p)", "--postprocessor-args \"VideoConvertor:-vf scale=-1:720\" " },
            { "VGA (640p)", "--postprocessor-args \"VideoConvertor:-vf scale=-1:640\" " },
            { "DVD (576p)", "--postprocessor-args \"VideoConvertor:-vf scale=-1:576\" " },
            { "SDTV (480p)", "--postprocessor-args \"VideoConvertor:-vf scale=-1:480\" " },
            { "NTSC (240p)", "--postprocessor-args \"VideoConvertor:-vf scale=-1:240\" " }
        };

        Dictionary<string, string> comboBox9Map = new Dictionary<string, string>()
        {
            { "Wszystkie języki", "all" },
            { "Polski", "pl.*" },
            { "Angielski", "en.*" },
            { "Niemiecki", "de.*" },
            { "Hiszpański", "es.*" },
            { "Rosyjski", "ru.*" },
            { "Ukraiński", "ua.*" },
            { "Francuski", "fr.*" },
            { "Włoski", "it.*" },
            { "Czeski", "cs.*" },
            { "Słowacki", "sl.*" },
            { "Chiński", "zh.*" },
            { "Japoński", "ja.*" }
        };

        Dictionary<string, string> comboBox5Map = new Dictionary<string, string>()
        {
            { "64 KB/s", "64K" },
            { "128 KB/s", "128K" },
            { "256 KB/s", "256K" },
            { "512 KB/s", "512K" },
            { "1 MB/s", "1M" },
            { "2 MB/s", "2M" },
            { "5 MB/s", "5M" },
            { "10 MB/s", "10M" },
            { "30 MB/s", "30M" },
            { "Bez limitu prędkości", "1000M" }
        };

        private void ControlChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != "MP4" && comboBox1.SelectedItem != "WEBM")
            {
                checkBox30.Enabled = false;
                checkBox30.Checked = false;
            }
            else
            {
                checkBox30.Enabled = true;
            }

            if (checkBox31.Checked == true)
            {
                groupBox11.BackColor = Color.White;
                date1label.Enabled = true;
                date2label.Enabled = true;
                date3label.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
                isDateOption1.Enabled = true;
                isDateOption2.Enabled = true;
                isDateOption3.Enabled = true;
            }
            else
            {
                groupBox11.BackColor = Color.Gainsboro;
                date1label.Enabled = false;
                date2label.Enabled = false;
                date3label.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
                isDateOption1.Enabled = false;
                isDateOption2.Enabled = false;
                isDateOption3.Enabled = false;
            }

            if (checkBox7.Checked == true)
            {
                textBox14.Enabled = true;
            }
            else
            {
                textBox14.Enabled = false;
            }

            if (checkBox27.Checked == true)
            {
                comboBox11.Enabled = true;
            }
            else
            {
                comboBox11.Enabled = false;
            }

            if (checkBox8.Checked == true)
            {
                checkBox9.Enabled = false;
                checkBox9.Checked = false;
            }
            else
            {
                checkBox9.Enabled = true;
            }

            if (checkBox9.Checked == true)
            {
                checkBox8.Enabled = false;
                checkBox8.Checked = false;
            }
            else
            {
                checkBox8.Enabled = true;
            }

            if (checkBox11.Checked == true)
            {
                textBox15.Enabled = true;
            }
            else
            {
                textBox15.Enabled = false;

            }

            if (checkBox12.Checked == true)
            {
                checkBox11.Enabled = true;
            }
            else
            {
                checkBox11.Enabled = false;
            }

            if (comboBox1.SelectedIndex == 3 || comboBox1.SelectedIndex == 4 || comboBox1.SelectedIndex == 5)
            {
                checkBox20.Enabled = true;
            }
            else
            {
                checkBox20.Enabled = false;
            }

            if (checkBox24.Checked == true)
            {
                comboBox10.Enabled = true;
            }
            else
            {
                comboBox10.Enabled = false;
            }

            if (checkBox17.Checked == true)
            {
                comboBox8.Enabled = true;
                comboBox9.Enabled = true;
            }
            else
            {
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;
            }

            if (checkBox1.Checked == true)
            {
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
            }

            if (checkBox2.Checked == true)
            {
                checkBox1.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
            }

            if (checkBox29.Checked == true)
            {
                richTextBox1.Visible = false;
                panel4.Location = new Point(0, 100);
            }
            else
            {
                richTextBox1.Visible = true;
                panel4.Location = new Point(0, 0);
            }

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
            SetLanguage();

            if (languageSelected == 1)
            {
                richTextBox1.AppendText("\r\n" + translations["Form1.errn1"]);
            }
            else
            {
                richTextBox1.AppendText($"\r\nZapisano nowe ustawienia programu.");
            }

            SaveConfig();
        }

        int bkgSel;
        int langSel;

        private void LoadConfig()
        {
            if (File.Exists(configPath))
            {
                string[] lines = File.ReadAllLines(configPath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        switch (parts[0])
                        {
                            case "bkgSel":
                                bkgSel = int.Parse(parts[1]);
                                break;
                            case "langSel":
                                langSel = int.Parse(parts[1]);
                                break;
                            case "checkBox1":
                                checkBox1.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox2":
                                checkBox2.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox3":
                                checkBox3.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox4":
                                checkBox4.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox5":
                                checkBox5.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox6":
                                checkBox6.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox7":
                                checkBox7.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox8":
                                checkBox8.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox9":
                                checkBox9.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox10":
                                checkBox10.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox11":
                                checkBox11.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox12":
                                checkBox12.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox13":
                                checkBox13.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox14":
                                checkBox14.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox15":
                                checkBox15.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox16":
                                checkBox16.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox17":
                                checkBox17.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox18":
                                checkBox18.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox19":
                                checkBox19.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox20":
                                checkBox20.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox21":
                                checkBox21.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox22":
                                checkBox22.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox23":
                                checkBox23.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox24":
                                checkBox24.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox25":
                                checkBox25.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox27":
                                checkBox27.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox28":
                                checkBox28.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox29":
                                checkBox29.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox30":
                                checkBox30.Checked = bool.Parse(parts[1]);
                                break;
                            case "checkBox31":
                                checkBox31.Checked = bool.Parse(parts[1]);
                                break;
                            case "isDateOption1":
                                isDateOption1.Checked = bool.Parse(parts[1]);
                                break;
                            case "isDateOption2":
                                isDateOption2.Checked = bool.Parse(parts[1]);
                                break;
                            case "isDateOption3":
                                isDateOption3.Checked = bool.Parse(parts[1]);
                                break;
                            case "textBox1":
                                textBox1.Text = parts[1];
                                break;
                            case "textBox2":
                                textBox2.Text = parts[1];
                                break;
                            case "textBox3":
                                textBox3.Text = parts[1];
                                break;
                            case "textBox4":
                                textBox4.Text = parts[1];
                                break;
                            case "textBox5":
                                textBox5.Text = parts[1];
                                break;
                            case "textBox6":
                                textBox6.Text = parts[1];
                                break;
                            case "textBox7":
                                textBox7.Text = parts[1];
                                break;
                            case "textBox8":
                                textBox8.Text = parts[1];
                                break;
                            case "textBox9":
                                textBox9.Text = parts[1];
                                break;
                            case "textBox10":
                                textBox10.Text = parts[1];
                                break;
                            case "textBox11":
                                textBox11.Text = parts[1];
                                break;
                            case "textBox12":
                                textBox12.Text = parts[1];
                                break;
                            case "textBox13":
                                textBox13.Text = parts[1];
                                break;
                            case "textBox14":
                                textBox14.Text = parts[1];
                                break;
                            case "textBox15":
                                textBox15.Text = parts[1];
                                break;
                            case "textBox16":
                                textBox16.Text = parts[1];
                                break;
                            case "textBox17":
                                textBox17.Text = parts[1];
                                break;
                            case "textBox18":
                                textBox18.Text = parts[1];
                                break;
                            case "textBox20":
                                textBox20.Text = parts[1];
                                break;
                            case "comboBox1":
                                comboBox1.SelectedItem = parts[1];
                                break;
                            case "comboBox2":
                                comboBox2.SelectedItem = parts[1];
                                break;
                            case "comboBox3":
                                comboBox3.SelectedItem = parts[1];
                                break;
                            case "comboBox4":
                                comboBox4.SelectedItem = parts[1];
                                break;
                            case "comboBox5":
                                comboBox5.SelectedItem = parts[1];
                                break;
                            case "comboBox6":
                                comboBox6.SelectedItem = parts[1];
                                break;
                            case "comboBox7":
                                comboBox7.SelectedItem = parts[1];
                                break;
                            case "comboBox8":
                                comboBox8.SelectedItem = parts[1];
                                break;
                            case "comboBox9":
                                comboBox9.SelectedItem = parts[1];
                                break;
                            case "comboBox10":
                                comboBox10.SelectedItem = parts[1];
                                break;
                            case "comboBox11":
                                comboBox11.SelectedItem = parts[1];
                                break;
                            case "dateTimePicker1":
                                dateTimePicker1.Value = DateTime.Parse(parts[1]);
                                break;
                            case "dateTimePicker2":
                                dateTimePicker2.Value = DateTime.Parse(parts[1]);
                                break;
                            case "dateTimePicker3":
                                dateTimePicker3.Value = DateTime.Parse(parts[1]);
                                break;
                        }
                    }
                }

                comboBox11.Enabled = checkBox27.Checked;
                comboBox10.Enabled = checkBox24.Checked;
                comboBox8.Enabled = checkBox17.Checked;
                comboBox9.Enabled = checkBox17.Checked;
                // ADDED
                textBox15.Enabled = checkBox11.Checked;
                checkBox11.Enabled = checkBox12.Checked;
                textBox14.Enabled = checkBox7.Checked;

                if (checkBox29.Checked == true)
                {
                    richTextBox1.Visible = false;
                    panel4.Location = new Point(0, 100);
                }
                else
                {
                    richTextBox1.Visible = true;
                    panel4.Location = new Point(0, 0);
                }
            }
            else
            {
                // Ustaw wartości domyślne dla kontrolek
                string defaultSaveFolder = "\"" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\"";


                //*-wymagana zmiana wartości przed pobraniem danych
                //**-aby włączyć opcję należy uwzględnić oba elementy.
                //***-wybierz tylko jedno

                comboBox1.SelectedIndex = 4; //format wideo
                comboBox2.SelectedIndex = 4; //format audio
                comboBox3.SelectedIndex = 15; //jakosc audio
                checkBox18.Checked = false; //wydobadz audio
                checkBox22.Checked = true; //metadane
                checkBox23.Checked = false; //rozdziały
                checkBox13.Checked = false; //minatury do pliku
                checkBox21.Checked = false; //minaturka do filmu
                textBox12.Text = defaultSaveFolder; //folder zapisu
                checkBox25.Checked = false; //reklamy
                textBox7.Text = string.Empty; //playlista, elementy
                textBox8.Text = string.Empty; //maks.rozmiar pliku
                textBox9.Text = string.Empty; //min.rozmiar pliku
                checkBox3.Checked = true; //pobierz playlistę całą
                checkBox4.Checked = false; //pobierz tylko wybrany film z playlisty
                textBox10.Text = string.Empty; //przerwij pobieranie gdy maks.elementy(playlista)
                comboBox4.SelectedIndex = 0; //ograniczenie elementów pobierania
                comboBox5.SelectedIndex = 9; //..prędkośći pobierania(*)
                comboBox6.SelectedIndex = 2; //..pomiń po nieudanym zapisie
                comboBox7.SelectedIndex = 2; //..pomin po nieudanym połączeniu
                textBox11.Text = "2M"; //rozmiar bufora
                checkBox5.Checked = false; //zmień dynamicznie bufora.
                textBox13.Text = string.Empty; //lista linków
                checkBox6.Checked = true; //ascii
                checkBox8.Checked = true; //nadpisywanie plików
                checkBox9.Checked = true; //wznawianie plików
                checkBox10.Checked = false; //twórz pliki tymczasowe podczas pobierania plików
                checkBox12.Checked = false; //system cache
                checkBox14.Checked = false; //podziel wideo
                checkBox15.Checked = false; //podziel audio
                checkBox16.Checked = false; //utworz plik z napisami
                checkBox17.Checked = false; //generuj napisy
                checkBox20.Checked = false; //wgraj napisy do filmu
                checkBox28.Checked = true; //konwersja audio na format
                checkBox30.Checked = false; //tylko WEBM, MP4 (szybsze pobieranie YT)
                checkBox31.Checked = false; //pobieranie z użyciem daty
                textBox1.Text = string.Empty; //proxy
                textBox2.Text = string.Empty; //geo proxy
                textBox4.Text = string.Empty; //ip klienta
                textBox5.Text = string.Empty; //user agent
                textBox6.Text = string.Empty; //xff - http
                textBox3.Text = string.Empty; //limit proxy
                checkBox1.Checked = false; //ipv4(***)
                checkBox2.Checked = false; //ipv6(***)
                textBox16.Text = string.Empty; //username
                textBox17.Text = string.Empty; //passwd
                textBox18.Text = string.Empty; //2fa
                textBox20.Text = string.Empty; //spotify cookies 
                checkBox26.Checked = false; //ukryj / pokaż - groupBox3,8,5,4
                //dateTimePicker1 = //data publikacji w
                //dateTimePicker2 = //data publikacji przed
                //dateTimePicker3 = //data publikacji po
                //comboBox8 = //format pliku z napisami
                checkBox7.Checked = false; //maks.rozmiar nazwy pliku(+textBox14 **)
                textBox14.Text = string.Empty;
                checkBox11.Checked = false; //ścieżka dla plików tymczasowych(+textBox15 **)
                textBox15.Text = string.Empty;
                //comboBox9 = //język napisów(*)
                //comboBox10 = //konwertuj napisy do innego formatu(+checkBox24 **)
                checkBox24.Checked = false;

                comboBox11.SelectedIndex = 0; // jakość wideo
                comboBox8.SelectedIndex = 3; // format napisów
                comboBox9.SelectedIndex = 1; // format napisów
                comboBox11.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;

                //comboBox11 + checkBox27 // jakośc konwersji

                bkgSel = 6; // białe tło aplikacji jako domyślne
                langSel = 0;
                checkBox29.Checked = false; // konsola wyjściowa
            }
        }

        Dictionary<Control, Color> originalForeColors = new Dictionary<Control, Color>();
        Dictionary<Control, Color> originalBackColors = new Dictionary<Control, Color>();

        void ChangeControlColors(Control control)
        {
            if (control is System.Windows.Forms.Label || control is System.Windows.Forms.CheckBox || control is System.Windows.Forms.ComboBox || control is System.Windows.Forms.GroupBox)
            {
                if (!originalForeColors.ContainsKey(control))
                {
                    originalForeColors[control] = control.ForeColor;
                }
                if (!originalBackColors.ContainsKey(control))
                {
                    originalBackColors[control] = control.BackColor;
                }
                if (control is System.Windows.Forms.GroupBox)
                {
                    control.ForeColor = Color.White;
                }
                if (control is System.Windows.Forms.CheckBox)
                {
                    control.BackColor = Color.Black;
                    control.ForeColor = Color.White;
                }
                if (control is System.Windows.Forms.ComboBox)
                {
                    control.BackColor = Color.Black;
                    control.ForeColor = Color.White;
                }
                if (control == label3 || control == label4 || control == label46 || control == label47 ||
                    control == label48 || control == label39 || control == label45 || control == label37 ||
                    control == label38 || control == label35 || control == label40 || control == label43 ||
                    control == label44 || control == label15 || control == label16 || control == label19 ||
                    control == label20 || control == label36)
                {
                    control.ForeColor = Color.DarkGray;
                }
                bkg6.BackColor = Color.White;
                control.ForeColor = Color.White;
            }

            foreach (Control c in control.Controls)
            {
                ChangeControlColors(c);
            }
        }

        void RestoreControlColors(Control control)
        {
            if (control is System.Windows.Forms.Label || control is System.Windows.Forms.CheckBox || control is System.Windows.Forms.ComboBox || control is System.Windows.Forms.GroupBox)
            {
                if (originalForeColors.ContainsKey(control))
                {
                    control.ForeColor = originalForeColors[control];
                }
                if (originalBackColors.ContainsKey(control))
                {
                    control.BackColor = originalBackColors[control];
                }
                if (control is System.Windows.Forms.Label)
                {
                    control.BackColor = Color.Transparent;
                    control.ForeColor = Color.Black;
                }
                if (control is System.Windows.Forms.CheckBox)
                {
                    control.BackColor = Color.White;
                    control.ForeColor = Color.Black;
                }
                if (control is System.Windows.Forms.ComboBox)
                {
                    control.BackColor = Color.White;
                    control.ForeColor = Color.Black;
                }
                if (control is System.Windows.Forms.GroupBox)
                {
                    control.ForeColor = Color.Black;
                }
                if (control == label3 || control == label4 || control == label46 || control == label47 ||
                    control == label48 || control == label39 || control == label45 || control == label37 ||
                    control == label38 || control == label35 || control == label40 || control == label43 ||
                    control == label44 || control == label15 || control == label16 || control == label19 ||
                    control == label20 || control == label36)
                {
                    control.ForeColor = Color.SteelBlue;
                }
                checkBox26.BackColor = Color.DarkRed;
                checkBox26.ForeColor = Color.White;
            }

            foreach (Control c in control.Controls)
            {
                RestoreControlColors(c);
            }
        }


        private void bkgSet()
        {
            if (bkgSel == 1)
            {
                foreach (Control c in this.Controls)
                {
                    RestoreControlColors(c);
                }
                this.BackgroundImage = Properties.Resources.bkg3;
                this.BackColor = Color.FromArgb(77, 54, 70);
                label51.ForeColor = Color.White;
                label51.BackColor = Color.Transparent;
                richTextBox1.BackColor = Color.Purple;
                richTextBox1.ForeColor = Color.White;
                textBox19.BackColor = Color.White;
                textBox19.ForeColor = Color.Silver;
                toolStripStatusLabel1.ForeColor = Color.White;
                toolStripStatusLabel2.ForeColor = Color.White;
                panel3.BackColor = Color.White;
                panel2.BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.g4;
                sel3.Enabled = true;
                sel3.Visible = true;

                sel4.Visible = false;
                sel4.Enabled = false;
                sel5.Visible = false;
                sel5.Enabled = false;
                sel6.Visible = false;
                sel6.Enabled = false;
                sel7.Visible = false;
                sel7.Enabled = false;
                sel8.Visible = false;
                sel8.Visible = false;
                sel9.Enabled = false;
                sel9.Enabled = false;
            }

            if (bkgSel == 2)
            {
                this.BackgroundImage = Properties.Resources.bkg;
                this.BackColor = Color.FromArgb(194, 192, 198);
                label51.ForeColor = Color.Black;
                label51.BackColor = Color.Transparent;
                richTextBox1.BackColor = Color.LightGray;
                richTextBox1.ForeColor = Color.Black;
                textBox19.BackColor = Color.White;
                textBox19.ForeColor = Color.Silver;
                toolStripStatusLabel1.ForeColor = Color.Black;
                toolStripStatusLabel2.ForeColor = Color.Black;
                panel3.BackColor = Color.White;
                panel2.BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.g42;

                foreach (Control c in this.Controls)
                {
                    RestoreControlColors(c);
                }
                sel4.Enabled = true;
                sel4.Visible = true;

                sel3.Visible = false;
                sel3.Enabled = false;
                sel5.Visible = false;
                sel5.Enabled = false;
                sel6.Visible = false;
                sel6.Enabled = false;
                sel7.Visible = false;
                sel7.Enabled = false;
                sel8.Visible = false;
                sel8.Visible = false;
                sel9.Enabled = false;
                sel9.Enabled = false;
            }

            if (bkgSel == 3)
            {
                foreach (Control c in this.Controls)
                {
                    RestoreControlColors(c);
                }

                this.BackgroundImage = Properties.Resources.bkga1;
                this.BackColor = Color.FromArgb(114, 133, 202);
                label51.ForeColor = Color.Black;
                label51.BackColor = Color.Transparent;
                richTextBox1.BackColor = Color.SlateBlue;
                richTextBox1.ForeColor = Color.White;
                textBox19.BackColor = Color.White;
                textBox19.ForeColor = Color.Silver;
                toolStripStatusLabel1.ForeColor = Color.White;
                toolStripStatusLabel2.ForeColor = Color.White;
                panel3.BackColor = Color.White;
                panel2.BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.g4;
                sel5.Enabled = true;
                sel5.Visible = true;

                sel4.Visible = false;
                sel4.Enabled = false;
                sel3.Visible = false;
                sel3.Enabled = false;
                sel6.Visible = false;
                sel6.Enabled = false;
                sel7.Visible = false;
                sel7.Enabled = false;
                sel8.Visible = false;
                sel8.Visible = false;
                sel9.Enabled = false;
                sel9.Enabled = false;
            }

            if (bkgSel == 4)
            {
                foreach (Control c in this.Controls)
                {
                    RestoreControlColors(c);
                }

                this.BackgroundImage = Properties.Resources.bkga2;
                this.BackColor = Color.FromArgb(79, 77, 116);
                label51.ForeColor = Color.White;
                label51.BackColor = Color.Transparent;
                richTextBox1.BackColor = Color.Thistle;
                richTextBox1.ForeColor = Color.Black;
                textBox19.BackColor = Color.White;
                textBox19.ForeColor = Color.Silver;
                toolStripStatusLabel1.ForeColor = Color.White;
                toolStripStatusLabel2.ForeColor = Color.White;
                panel3.BackColor = Color.White;
                panel2.BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.g4;
                sel6.Enabled = true;
                sel6.Visible = true;

                sel4.Visible = false;
                sel4.Enabled = false;
                sel5.Visible = false;
                sel5.Enabled = false;
                sel3.Visible = false;
                sel3.Enabled = false;
                sel7.Visible = false;
                sel7.Enabled = false;
                sel8.Visible = false;
                sel8.Visible = false;
                sel9.Enabled = false;
                sel9.Enabled = false;
            }

            if (bkgSel == 5)
            {
                foreach (Control c in this.Controls)
                {
                    RestoreControlColors(c);
                }

                this.BackgroundImage = Properties.Resources.bkga3;
                this.BackColor = Color.FromArgb(34, 0, 87);
                label51.ForeColor = Color.White;
                label51.BackColor = Color.Transparent;
                richTextBox1.BackColor = Color.DarkBlue;
                richTextBox1.ForeColor = Color.White;
                textBox19.BackColor = Color.White;
                textBox19.ForeColor = Color.Silver;
                toolStripStatusLabel1.ForeColor = Color.White;
                toolStripStatusLabel2.ForeColor = Color.White;
                panel3.BackColor = Color.White;
                panel2.BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.g4;
                sel7.Enabled = true;
                sel7.Visible = true;

                sel4.Visible = false;
                sel4.Enabled = false;
                sel5.Visible = false;
                sel5.Enabled = false;
                sel6.Visible = false;
                sel6.Enabled = false;
                sel3.Visible = false;
                sel3.Enabled = false;
                sel8.Visible = false;
                sel8.Visible = false;
                sel9.Enabled = false;
                sel9.Enabled = false;
            }

            if (bkgSel == 6)
            {
                this.BackColor = Color.White;
                this.BackgroundImage = null;
                richTextBox1.BackColor = Color.LightGray;
                textBox19.BackColor = Color.White;
                textBox19.ForeColor = Color.Silver;
                richTextBox1.ForeColor = Color.Black;
                label51.ForeColor = Color.Black;
                label51.BackColor = Color.Transparent;
                toolStripStatusLabel1.ForeColor = Color.Black;
                toolStripStatusLabel2.ForeColor = Color.Black;
                panel3.BackColor = Color.White;
                panel2.BackColor = Color.White;
                pictureBox1.Image = Properties.Resources.g42;

                foreach (Control c in this.Controls)
                {
                    RestoreControlColors(c);
                }

                sel8.Enabled = true;
                sel8.Visible = true;

                sel4.Visible = false;
                sel4.Enabled = false;
                sel5.Visible = false;
                sel5.Enabled = false;
                sel6.Visible = false;
                sel6.Enabled = false;
                sel7.Visible = false;
                sel7.Enabled = false;
                sel3.Visible = false;
                sel3.Visible = false;
                sel9.Enabled = false;
                sel9.Enabled = false;
            }

            if (bkgSel == 7)
            {
                foreach (Control c in this.Controls)
                {
                    ChangeControlColors(c);
                }

                this.BackColor = Color.Black;
                this.BackgroundImage = null;
                richTextBox1.BackColor = Color.Black;
                richTextBox1.ForeColor = Color.White;
                textBox19.BackColor = Color.Black;
                textBox19.ForeColor = Color.White;
                label51.ForeColor = Color.White;
                label51.BackColor = Color.Transparent;
                toolStripStatusLabel1.ForeColor = Color.White;
                toolStripStatusLabel2.ForeColor = Color.White;
                panel3.BackColor = Color.Black;
                panel2.BackColor = Color.Black;
                pictureBox1.Image = Properties.Resources.g4;
                sel9.Enabled = true;
                sel9.Visible = true;

                sel4.Visible = false;
                sel4.Enabled = false;
                sel5.Visible = false;
                sel5.Enabled = false;
                sel6.Visible = false;
                sel6.Enabled = false;
                sel7.Visible = false;
                sel7.Enabled = false;
                sel8.Visible = false;
                sel8.Visible = false;
                sel3.Enabled = false;
                sel3.Enabled = false;
            }
        }


        private void SaveConfig()
        {
            using (StreamWriter sw = new StreamWriter(configPath))
            {
                sw.WriteLine("bkgSel=" + bkgSel);
                sw.WriteLine("langSel=" + langSel);
                sw.WriteLine("checkBox1=" + checkBox1.Checked);
                sw.WriteLine("checkBox2=" + checkBox2.Checked);
                sw.WriteLine("checkBox3=" + checkBox3.Checked);
                sw.WriteLine("checkBox4=" + checkBox4.Checked);
                sw.WriteLine("checkBox5=" + checkBox5.Checked);
                sw.WriteLine("checkBox6=" + checkBox6.Checked);
                sw.WriteLine("checkBox7=" + checkBox7.Checked);
                sw.WriteLine("checkBox8=" + checkBox8.Checked);
                sw.WriteLine("checkBox9=" + checkBox9.Checked);
                sw.WriteLine("checkBox10=" + checkBox10.Checked);
                sw.WriteLine("checkBox11=" + checkBox11.Checked);
                sw.WriteLine("checkBox12=" + checkBox12.Checked);
                sw.WriteLine("checkBox13=" + checkBox13.Checked);
                sw.WriteLine("checkBox14=" + checkBox14.Checked);
                sw.WriteLine("checkBox15=" + checkBox15.Checked);
                sw.WriteLine("checkBox16=" + checkBox16.Checked);
                sw.WriteLine("checkBox17=" + checkBox17.Checked);
                sw.WriteLine("checkBox18=" + checkBox18.Checked);
                sw.WriteLine("checkBox19=" + checkBox19.Checked);
                sw.WriteLine("checkBox20=" + checkBox20.Checked);
                sw.WriteLine("checkBox21=" + checkBox21.Checked);
                sw.WriteLine("checkBox22=" + checkBox22.Checked);
                sw.WriteLine("checkBox23=" + checkBox23.Checked);
                sw.WriteLine("checkBox24=" + checkBox24.Checked);
                sw.WriteLine("checkBox25=" + checkBox25.Checked);
                sw.WriteLine("checkBox27=" + checkBox27.Checked);
                sw.WriteLine("checkBox28=" + checkBox28.Checked);
                sw.WriteLine("checkBox29=" + checkBox29.Checked);
                sw.WriteLine("checkBox30=" + checkBox30.Checked);
                sw.WriteLine("checkBox31=" + checkBox31.Checked);
                sw.WriteLine("isDateOption1=" + isDateOption1.Checked);
                sw.WriteLine("isDateOption2=" + isDateOption2.Checked);
                sw.WriteLine("isDateOption3=" + isDateOption3.Checked);
                sw.WriteLine("textBox1=" + textBox1.Text);
                sw.WriteLine("textBox2=" + textBox2.Text);
                sw.WriteLine("textBox3=" + textBox3.Text);
                sw.WriteLine("textBox4=" + textBox4.Text);
                sw.WriteLine("textBox5=" + textBox5.Text);
                sw.WriteLine("textBox6=" + textBox6.Text);
                sw.WriteLine("textBox7=" + textBox7.Text);
                sw.WriteLine("textBox8=" + textBox8.Text);
                sw.WriteLine("textBox9=" + textBox9.Text);
                sw.WriteLine("textBox10=" + textBox10.Text);
                sw.WriteLine("textBox11=" + textBox11.Text);
                sw.WriteLine("textBox12=" + textBox12.Text);
                sw.WriteLine("textBox13=" + textBox13.Text);
                sw.WriteLine("textBox14=" + textBox14.Text);
                sw.WriteLine("textBox15=" + textBox15.Text);
                sw.WriteLine("textBox16=" + textBox16.Text);
                sw.WriteLine("textBox17=" + textBox17.Text);
                sw.WriteLine("textBox18=" + textBox18.Text);
                sw.WriteLine("textBox20=" + textBox20.Text);
                sw.WriteLine("comboBox1=" + comboBox1.SelectedItem);
                sw.WriteLine("comboBox2=" + comboBox2.SelectedItem);
                sw.WriteLine("comboBox3=" + comboBox3.SelectedItem);
                sw.WriteLine("comboBox4=" + comboBox4.SelectedItem);
                sw.WriteLine("comboBox5=" + comboBox5.SelectedItem);
                sw.WriteLine("comboBox6=" + comboBox6.SelectedItem);
                sw.WriteLine("comboBox7=" + comboBox7.SelectedItem);
                sw.WriteLine("comboBox8=" + comboBox8.SelectedItem);
                sw.WriteLine("comboBox9=" + comboBox9.SelectedItem);
                sw.WriteLine("comboBox10=" + comboBox10.SelectedItem);
                sw.WriteLine("comboBox11=" + comboBox11.SelectedItem);
                sw.WriteLine("dateTimePicker1=" + dateTimePicker1.Value);
                sw.WriteLine("dateTimePicker2=" + dateTimePicker2.Value);
                sw.WriteLine("dateTimePicker3=" + dateTimePicker3.Value);
            }
        }
        private async void button13_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(binFile5);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(startInfo);
            await Task.Delay(2000);

            button13.Enabled = false;
            button13.Visible = false;
            button4.Enabled = true;
            button4.Visible = true;
            toolStripStatusLabel2.Visible = false;

            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            SetLanguage();

            if (languageSelected == 1)
            {
                richTextBox1.AppendText(translations["Form1.errn2"]);
                richTextBox1.AppendText("\r\n" + translations["Form1.errn3"]);
            }
            else
            {
                toolStripStatusLabel1.Text = $"Nastąpiło zatrzymanie pobierania";
                richTextBox1.AppendText($"\r\nUżytkownik zatrzymał pobieranie");
            }
            richTextBox1.ScrollToCaret();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            string textToAppend = "\"" + textBox19.Text + "\"";
            Regex urlCheck = new Regex(@"^http(s)?://");
            if ((!string.IsNullOrEmpty(textBox19.Text) && urlCheck.IsMatch(textToAppend)) || (File.Exists(queuePath) && new FileInfo(queuePath).Length > 0))
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.SteelBlue;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                SetLanguage();

                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.einf01"] + (textToAppend));
                }
                else
                {
                    richTextBox1.AppendText("\r\n● INFO ● Rozpoczęto pobieranie linku: " + (textToAppend));
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                List<Process> activeProcesses = new List<Process>();

                button13.Enabled = true;
                button13.Visible = true;
                button4.Enabled = false;
                button4.Visible = false;

                string? selectedVal_checkBox1 = checkBox1.Checked ? "--force-ipv4 " : null;
                string? selectedVal_checkBox2 = checkBox2.Checked ? "--force-ipv6 " : null;
                string? selectedVal_checkBox3 = checkBox3.Checked ? "--yes-playlist " : null;
                string? selectedVal_checkBox4 = checkBox4.Checked ? "--no-playlist " : null;
                string? selectedVal_checkBox5 = checkBox5.Checked ? "--no-resize-buffer " : null;
                string? selectedVal_checkBox6 = checkBox6.Checked ? "--restrict-filenames " : null;
                string? selectedVal_checkBox7 = checkBox7.Checked ? "--trim-filenames " : null;
                string? selectedVal_checkBox8 = checkBox8.Checked ? "--force-overwrites " : null;
                string? selectedVal_checkBox8_spotify = checkBox8.Checked ? "--overwrite force " : null;
                string? selectedVal_checkBox9 = checkBox9.Checked ? "--continue " : null;
                string? selectedVal_checkBox10 = checkBox10.Checked ? "--part " : null;
                //string? selectedVal_checkBox11 = checkBox11.Checked ? "" : null;
                string? selectedVal_checkBox12 = checkBox12.Checked ? "--cache-dir " : null;
                string? selectedVal_checkBox13 = checkBox13.Checked ? "--write-thumbnail " : null;
                string? selectedVal_checkBox14 = checkBox14.Checked ? "--video-multistreams " : null;
                string? selectedVal_checkBox15 = checkBox15.Checked ? "--audio-multistreams " : null;
                string? selectedVal_checkBox16 = checkBox16.Checked ? "--write-subs " : null;
                string? selectedVal_checkBox17 = checkBox17.Checked ? "--write-auto-subs " : null;
                string? selectedVal_checkBox18 = checkBox18.Checked ? "--extract-audio " : null;
                string? selectedVal_checkBox19 = checkBox19.Checked ? "--keep-video" : null;
                string? selectedVal_checkBox20 = checkBox20.Checked ? "--embed-subs " : null;
                string? selectedVal_checkBox21 = checkBox21.Checked ? "--embed-thumbnail " : null;
                string? selectedVal_checkBox22 = checkBox22.Checked ? "--embed-metadata " : null;
                string? selectedVal_checkBox23 = checkBox23.Checked ? "--embed-chapters " : null;
                string? selectedVal_checkBox25 = checkBox25.Checked ? "--sponsorblock-remove all " : null;
                string? selectedVal_checkBox28 = checkBox28.Checked ? "--remux-video " : null;
                string? selectedVal_checkBox30 = checkBox30.Checked ? "--format " : null;
                string selectedVal_textBox1 = "--proxy " + textBox1.Text + " ";
                string selectedVal_textBox1_spotify = "--proxy " + textBox1.Text + " ";
                string selectedVal_textBox2 = "--geo-verification-proxy " + textBox2.Text + " ";
                string selectedVal_textBox3 = "--socket-timeout " + textBox3.Text + " ";
                string selectedVal_textBox4 = "--source-address " + textBox4.Text + " ";
                string selectedVal_textBox5 = "--impersonate " + textBox5.Text + " ";
                string selectedVal_textBox6 = "--xff " + textBox6.Text + " ";
                string selectedVal_textBox7 = "--playlist-items " + textBox7.Text + " ";
                string selectedVal_textBox8 = "--max-filesize " + textBox8.Text + "M ";
                string selectedVal_textBox9 = "--min-filesize " + textBox9.Text + "M ";
                string selectedVal_textBox10 = "--max-downloads " + textBox10.Text + " ";
                string selectedVal_textBox11 = "--buffer-size " + textBox11.Text + " ";
                string selectedVal_textBox12 = "--paths " + textBox12.Text + " ";
                string selectedVal_textBox12_spotify = "--output " + textBox12.Text + " ";
                string selectedVal_textBox13 = "--batch-file " + textBox13.Text + " ";
                string selectedVal_textBox14 = textBox14.Text; //rozmiar nazwy pliku
                string selectedVal_textBox15 = textBox15.Text; //temp folder
                string selectedVal_textBox16 = "--username " + textBox16.Text + " ";
                string selectedVal_textBox17 = "--password " + textBox17.Text + " ";
                string selectedVal_textBox18 = "--twofactor " + textBox18.Text + " ";
                string selectedVal_textBox20 = "--cookie-file " + textBox20.Text + " --bitrate disable ";
                string selectedVal_textBox20_ytdl = "--cookies " + textBox20.Text + " ";
                string? selectedVal_comboBox3 = comboBox3.SelectedItem?.ToString();
                string? valueToStore1 = "--audio-quality " + comboBox3Map[selectedVal_comboBox3] + " ";
                string? valueToStore1_spotify = "--bitrate " + comboBox3Map[selectedVal_comboBox3] + " ";
                string? selectedVal_comboBox11 = comboBox11.SelectedItem?.ToString();
                string? valueToStore2 = comboBox11Map[selectedVal_comboBox11];
                string? selectedVal_comboBox5 = comboBox5.SelectedItem?.ToString();
                string? valueToStore3 = "--limit-rate " + comboBox5Map[selectedVal_comboBox5] + " ";
                string? selectedVal_comboBox9 = comboBox9.SelectedItem?.ToString();
                string valueToStore4 = "--sub-langs " + comboBox9Map[selectedVal_comboBox9] + " ";
                string selectedVal_comboBox1 = "--recode-video " + comboBox1.SelectedItem?.ToString() + " ";
                selectedVal_comboBox1 = selectedVal_comboBox1.ToLower();
                string valueToStore5 = comboBox1.SelectedItem?.ToString();
                valueToStore5 = valueToStore5.ToLower();
                string selectedVal_comboBox2 = "--audio-format " + comboBox2.SelectedItem?.ToString() + " ";
                string selectedVal_comboBox2_option = comboBox2.SelectedItem?.ToString() + " ";
                selectedVal_comboBox2_option = selectedVal_comboBox2_option.ToLower();
                string selectedVal_comboBox2_spotify = "--format ";
                string? format = comboBox2.SelectedItem?.ToString();

                switch (format)
                {
                    case "AAC":
                        selectedVal_comboBox2_spotify += "mp3";
                        break;
                    case "ALAC":
                        selectedVal_comboBox2_spotify += "m4a";
                        break;
                    case "VORBIS":
                        selectedVal_comboBox2_spotify += "ogg";
                        break;
                    default:
                        selectedVal_comboBox2_spotify += format;
                        break;
                }

                selectedVal_comboBox2_spotify += " ";
                selectedVal_comboBox2_spotify = selectedVal_comboBox2_spotify.ToLower();
                selectedVal_comboBox2 = selectedVal_comboBox2.ToLower();

                //string selectedVal_comboBox1 = "--recode-video " + comboBox1.SelectedItem?.ToString().ToLower() + " ";
                //string selectedVal_comboBox2 = "--audio-format " + comboBox2.SelectedItem?.ToString().ToLower() + " ";
                string selectedVal_comboBox4 = "--concurrent-fragments " + comboBox4.SelectedItem?.ToString() + " ";
                string selectedVal_comboBox6 = "--file-access-retries " + comboBox6.SelectedItem?.ToString() + " ";
                string selectedVal_comboBox7 = "--retries " + comboBox7.SelectedItem?.ToString() + " ";
                string selectedVal_comboBox8 = "--sub-format " + comboBox8.SelectedItem?.ToString() + " ";
                string selectedVal_comboBox10 = "--convert-subs " + comboBox10.SelectedItem?.ToString() + " ";
                string selectedVal_dateTimePicker1 = "--date " + dateTimePicker1.Value.ToString("yyyyMMdd") + " ";
                string selectedVal_dateTimePicker2 = "--datebefore " + dateTimePicker2.Value.ToString("yyyyMMdd") + " ";
                string selectedVal_dateTimePicker3 = "--dateafter " + dateTimePicker3.Value.ToString("yyyyMMdd") + " ";

                string finalResult = null;
                string finalResult2 = null;

                if (checkBox1.Checked == true)
                {
                    finalResult += selectedVal_checkBox1;
                }

                if (checkBox2.Checked == true)
                {
                    finalResult += selectedVal_checkBox2;
                }

                if (checkBox3.Checked == true)
                {
                    finalResult += selectedVal_checkBox3;
                }

                if (checkBox4.Checked == true)
                {
                    finalResult += selectedVal_checkBox4;
                }

                if (checkBox5.Checked == true)
                {
                    finalResult += selectedVal_checkBox5;
                }

                if (checkBox6.Checked == true)
                {
                    finalResult += selectedVal_checkBox6;
                }

                if (checkBox7.Checked == true)
                {
                    //textBox14
                    if (!System.String.IsNullOrEmpty(textBox14.Text))
                    {
                        finalResult += selectedVal_checkBox7;
                        finalResult += selectedVal_textBox14;
                    }
                    else
                    {
                        // return nothing...
                    }
                }

                //jeżeli ch8 zaznaczony- sprawdz czy ch9 jest zaznaczony, 
                //jeśli tak => pobierz wartość tylko z ch8, jeśli nie tylko z ch9.

                if (checkBox8.Checked == true)
                {
                    finalResult += selectedVal_checkBox8;
                    finalResult2 += selectedVal_checkBox8_spotify;
                }

                if (checkBox9.Checked == true)
                {
                    finalResult += selectedVal_checkBox9;
                }

                if (checkBox10.Checked == true)
                {
                    finalResult += selectedVal_checkBox10;
                }

                if (checkBox12.Checked == true)
                {
                    //textbox15
                    if (!System.String.IsNullOrEmpty(textBox15.Text))
                    {
                        finalResult += selectedVal_checkBox12;
                        finalResult += selectedVal_textBox15;
                    }
                    else
                    {
                        // return nothing...
                    }

                }

                if (checkBox13.Checked == true)
                {
                    finalResult += selectedVal_checkBox13;
                }

                if (checkBox14.Checked == true)
                {
                    finalResult += selectedVal_checkBox14;
                }

                if (checkBox15.Checked == true)
                {
                    finalResult += selectedVal_checkBox15;
                }

                if (checkBox16.Checked == true)
                {
                    finalResult += selectedVal_checkBox16;
                }

                if (checkBox17.Checked == true)
                {
                    //comboBox8
                    //comboBox9
                    finalResult += selectedVal_checkBox17;
                    finalResult += valueToStore4;
                    finalResult += selectedVal_comboBox8;
                }

                if (checkBox18.Checked == true)
                {
                    finalResult += selectedVal_checkBox18;
                }
                else
                {
                    finalResult += selectedVal_comboBox1;
                }

                if (checkBox19.Checked == true)
                {
                    finalResult += selectedVal_checkBox19;
                }

                if (checkBox20.Checked == true)
                {
                    finalResult += selectedVal_checkBox20;
                }

                if (checkBox21.Checked == true)
                {
                    finalResult += selectedVal_checkBox21;
                }

                if (checkBox22.Checked == true)
                {
                    finalResult += selectedVal_checkBox22;
                }

                if (checkBox23.Checked == true)
                {
                    finalResult += selectedVal_checkBox23;
                }

                if (checkBox24.Checked == true)
                {
                    //comboBox10
                    finalResult += selectedVal_comboBox10;
                }

                if (checkBox25.Checked == true)
                {
                    finalResult += selectedVal_checkBox25;
                }

                if (checkBox27.Checked == true)
                {
                    finalResult += valueToStore2;
                }

                if (checkBox28.Checked == true)
                {
                    finalResult += (selectedVal_checkBox28 + selectedVal_comboBox2_option);
                    finalResult2 += selectedVal_comboBox2_spotify;
                }


                if (checkBox30.Checked == true)
                {
                    finalResult += (selectedVal_checkBox30 + valueToStore5 + " ");
                }

                if (checkBox31.Checked == true)
                {
                    if (languageSelected == 1)
                    {
                        if (dateTimePicker2.Value.Date < dateTimePicker3.Value.Date)
                        {
                            MessageBox.Show("It appears that you have entered an incorrect date range. Please make corrections to the selected date range.");
                            return;
                        }
                    }
                    else
                    {
                        if (dateTimePicker2.Value.Date < dateTimePicker3.Value.Date)
                        {
                            MessageBox.Show("Wygląda na to, że podałeś niepoprawny zakres dat. Proszę wprowadzić poprawki w wybranym zakresie dat.");
                            return;
                        }
                    }

                    if (isDateOption2.Checked == true)
                    {
                        finalResult += selectedVal_dateTimePicker2;
                    }
                    if (isDateOption1.Checked == true)
                    {
                        finalResult += selectedVal_dateTimePicker1;
                    }
                    if (isDateOption3.Checked == true)
                    {
                        finalResult += selectedVal_dateTimePicker3;
                    }
                }

                finalResult += valueToStore1;
                finalResult2 += valueToStore1_spotify;
                finalResult += valueToStore3;
                finalResult += selectedVal_comboBox2;
                finalResult += selectedVal_comboBox4;
                finalResult += selectedVal_comboBox6;
                finalResult += selectedVal_comboBox7;


                if (!System.String.IsNullOrEmpty(textBox1.Text))
                {
                    finalResult += selectedVal_textBox1;
                    finalResult2 += selectedVal_textBox1_spotify;

                }

                if (!System.String.IsNullOrEmpty(textBox2.Text))
                {
                    finalResult += selectedVal_textBox2;
                }

                if (!System.String.IsNullOrEmpty(textBox3.Text))
                {
                    finalResult += selectedVal_textBox3;
                }

                if (!System.String.IsNullOrEmpty(textBox4.Text))
                {
                    finalResult += selectedVal_textBox4;
                }

                if (!System.String.IsNullOrEmpty(textBox5.Text))
                {
                    finalResult += selectedVal_textBox5;
                }

                if (!System.String.IsNullOrEmpty(textBox6.Text))
                {
                    finalResult += selectedVal_textBox6;
                }

                if (!System.String.IsNullOrEmpty(textBox7.Text))
                {
                    finalResult += selectedVal_textBox7;
                }

                if (!System.String.IsNullOrEmpty(textBox8.Text))
                {
                    finalResult += selectedVal_textBox8;
                }

                if (!System.String.IsNullOrEmpty(textBox9.Text))
                {
                    finalResult += selectedVal_textBox9;
                }

                if (!System.String.IsNullOrEmpty(textBox10.Text))
                {
                    finalResult += selectedVal_textBox10;
                }

                if (!System.String.IsNullOrEmpty(textBox11.Text))
                {
                    finalResult += selectedVal_textBox11;
                }

                if (!System.String.IsNullOrEmpty(textBox12.Text))
                {
                    finalResult += selectedVal_textBox12;
                    finalResult2 += selectedVal_textBox12_spotify;

                }

                if (!System.String.IsNullOrEmpty(textBox13.Text))
                {
                    finalResult += selectedVal_textBox13;
                }

                if (!System.String.IsNullOrEmpty(textBox16.Text))
                {
                    finalResult += selectedVal_textBox16;
                }

                if (!System.String.IsNullOrEmpty(textBox17.Text))
                {
                    finalResult += selectedVal_textBox17;
                }

                if (!System.String.IsNullOrEmpty(textBox18.Text))
                {
                    finalResult += selectedVal_textBox18;
                }

                if (!System.String.IsNullOrEmpty(textBox20.Text))
                {
                    finalResult += selectedVal_textBox20_ytdl;
                    finalResult2 += selectedVal_textBox20;
                }


                string[] links;
                if (File.Exists(queuePath) && new FileInfo(queuePath).Length > 0)
                {
                    links = File.ReadAllLines(queuePath);
                }
                else if (!string.IsNullOrEmpty(textBox19.Text))
                {
                    links = textBox19.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                }
                else
                {
                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.SelectionLength = 0;
                    richTextBox1.SelectionColor = Color.Red;
                    richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                    SetLanguage();

                    if (languageSelected == 1)
                    {
                        richTextBox1.AppendText("\r\n" + translations["Form1.err07"]);
                    }
                    else
                    {
                        richTextBox1.AppendText("\r\n▲ ERR07 ▲ Pusta kolejka. Dodaj link zanim rozpoczniesz pobieranie.");
                    }

                    richTextBox1.SelectionColor = richTextBox1.ForeColor;
                    richTextBox1.SelectionFont = richTextBox1.Font;
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();

                    button13.Enabled = false;
                    button13.Visible = false;
                    button4.Enabled = true;
                    button4.Visible = true;
                    toolStripStatusLabel2.Visible = false;

                    return;
                }

                int totalLinks = links.Length;
                int completedTasks = 0;
                SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);


                foreach (string linkURL in links)
                {
                    string ytbinSpot;
                    string processValue;
                    if (linkURL.Contains("spotify.com"))
                    {
                        ytbinSpot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\spotdl.exe ");
                        processValue = finalResult2;

                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.SelectionLength = 0;
                        richTextBox1.SelectionColor = Color.DarkOrange;
                        richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                        SetLanguage();

                        if (languageSelected == 1)
                        {
                            richTextBox1.AppendText("\r\n" + translations["Form1.einf02"]);
                            richTextBox1.SelectionColor = richTextBox1.ForeColor;
                            richTextBox1.SelectionFont = richTextBox1.Font;
                            richTextBox1.SelectionStart = richTextBox1.Text.Length;
                            richTextBox1.AppendText("\r\n" + translations["Form1.einf03"] + linkURL + " " + processValue);
                            toolStripStatusLabel1.Text = translations["Form1.einf07"];
                        }
                        else
                        {
                            richTextBox1.AppendText("\r\n● INFO ● Pobierasz audio z serwisu Spotify, z powodu zabezpieczeń DRM, jakość dzwięku zostanie ograniczona.");
                            richTextBox1.SelectionColor = richTextBox1.ForeColor;
                            richTextBox1.SelectionFont = richTextBox1.Font;
                            richTextBox1.SelectionStart = richTextBox1.Text.Length;
                            richTextBox1.AppendText($"\r\nUruchamianie zadania: {linkURL + " " + processValue}");
                            toolStripStatusLabel1.Text = $"Przygotowywanie do pobierania...";
                        }


                        toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                        toolStripProgressBar1.MarqueeAnimationSpeed = 50;

                        richTextBox1.ScrollToCaret();

                    }
                    else
                    {
                        if (linkURL.Contains("music.youtube.com"))
                        {
                            richTextBox1.SelectionStart = richTextBox1.TextLength;
                            richTextBox1.SelectionLength = 0;
                            richTextBox1.SelectionColor = Color.DarkOrange;
                            richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                            SetLanguage();

                            if (languageSelected == 1)
                            {
                                richTextBox1.AppendText("\r\n" + translations["Form1.einf04"]);
                            }
                            else
                            {
                                richTextBox1.AppendText("\r\n● INFO ● Pobierasz audio z serwisu YouTube Music, z powodu zabezpieczeń DRM, jakość dzwięku zostanie ograniczona.");
                            }

                            richTextBox1.SelectionColor = richTextBox1.ForeColor;
                            richTextBox1.SelectionFont = richTextBox1.Font;
                            richTextBox1.SelectionStart = richTextBox1.Text.Length;
                            richTextBox1.ScrollToCaret();
                        }

                        ytbinSpot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\ytdlp.exe ");
                        processValue = finalResult;

                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.SelectionLength = 0;
                        richTextBox1.SelectionColor = Color.SteelBlue;
                        richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                        SetLanguage();

                        if (languageSelected == 1)
                        {
                            richTextBox1.AppendText("\r\n" + translations["Form1.einf05"]);
                            richTextBox1.SelectionColor = richTextBox1.ForeColor;
                            richTextBox1.SelectionFont = richTextBox1.Font;
                            richTextBox1.SelectionStart = richTextBox1.Text.Length;
                            richTextBox1.AppendText("\r\n" + translations["Form1.einf06"] + linkURL + " " + processValue);
                            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                            toolStripProgressBar1.MarqueeAnimationSpeed = 50;
                            toolStripStatusLabel1.Text = translations["Form1.einf07"];
                        }
                        else
                        {
                            richTextBox1.AppendText("\r\n● INFO ● Rozpoczęto pobieranie dodanych linków.");
                            richTextBox1.SelectionColor = richTextBox1.ForeColor;
                            richTextBox1.SelectionFont = richTextBox1.Font;
                            richTextBox1.SelectionStart = richTextBox1.Text.Length;
                            richTextBox1.AppendText($"\r\nUruchamianie zadania: {linkURL + " " + processValue}");
                            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                            toolStripProgressBar1.MarqueeAnimationSpeed = 50;
                            toolStripStatusLabel1.Text = $"Przygotowywanie do pobierania...";
                        }
                        richTextBox1.ScrollToCaret();
                    }

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/C {ytbinSpot} {processValue} {linkURL}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")
                    };

                    Process process = new Process { StartInfo = startInfo };
                    activeProcesses.Add(process);

                    // Obsługa zdarzenia do przechwytywania danych wyjściowych procesu w czasie rzeczywistym
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!System.String.IsNullOrEmpty(e.Data))
                        {
                            // Aktualizuj interfejs użytkownika z wyjściowymi danymi procesu
                            richTextBox1.Invoke((MethodInvoker)(() =>
                            {
                                richTextBox1.AppendText(e.Data + Environment.NewLine);
                            }));
                        }
                    };

                    Task.Run(async () =>
                    {
                        await semaphore.WaitAsync(); // Zajmij semafor, czekając, aż będzie dostępny
                        try
                        {
                            process.Start();
                            process.BeginOutputReadLine();

                            process.OutputDataReceived += (sender, args) =>
                            {
                                if (args.Data != null && args.Data.Contains("[info] There are no subtitles for the requested languages"))
                                {
                                    richTextBox1.Invoke((MethodInvoker)(() =>
                                    {
                                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                                        richTextBox1.SelectionLength = 0;
                                        richTextBox1.SelectionColor = Color.DarkOrange;
                                        richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                                        SetLanguage();

                                        if (languageSelected == 1)
                                        {
                                            richTextBox1.AppendText(translations["Form1.einf08"] + "\r\n");
                                        }
                                        else
                                        {
                                            richTextBox1.AppendText("● INFO ● Pobierany film nie ma dostępnych lub zgodnych napisów. Pomijam...\r\n");
                                        }

                                        richTextBox1.SelectionColor = richTextBox1.ForeColor;
                                        richTextBox1.SelectionFont = richTextBox1.Font;
                                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                                        richTextBox1.ScrollToCaret();
                                    }));
                                }
                            };

                            process.OutputDataReceived += (sender, args) =>
                            {
                                if (args.Data != null && args.Data.Contains("[download]"))
                                {
                                    richTextBox1.Invoke((MethodInvoker)(() =>
                                    {
                                        toolStripStatusLabel2.Visible = true;
                                        var match = Regex.Match(args.Data, @"\[download\]\s+(.*?)% of\s+(.*?) at\s+(.*?) ETA");

                                        if (match.Success)
                                        {
                                            var percentage = match.Groups[1].Value;
                                            var size = match.Groups[2].Value;
                                            var speed = match.Groups[3].Value;
                                            SetLanguage();

                                            if (languageSelected == 1)
                                            {
                                                toolStripStatusLabel2.Text = translations["Form1.einf09"] + $"({percentage}% of {size}) Network speed: @{speed}";
                                            }
                                            else
                                            {
                                                toolStripStatusLabel2.Text = $"Pobieranie ({percentage}% z {size}) Prędkość: @{speed}";
                                            }

                                        }
                                        else
                                        {
                                            SetLanguage();

                                            if (languageSelected == 1)
                                            {
                                                toolStripStatusLabel2.Text = translations["Form1.einf14"];
                                            }
                                            else
                                            {
                                                toolStripStatusLabel2.Text = "Pobieranie ...";
                                            }
                                        }
                                    }));
                                }
                            };


                            process.OutputDataReceived += (sender, args) =>
                            {
                                if (args.Data != null && args.Data.Contains("upload date is not in range"))
                                {
                                    richTextBox1.Invoke((MethodInvoker)(() =>
                                    {
                                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                                        richTextBox1.SelectionLength = 0;
                                        richTextBox1.SelectionColor = Color.DarkOrange;
                                        richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                                        SetLanguage();

                                        if (languageSelected == 1)
                                        {
                                            richTextBox1.AppendText(translations["Form1.einf21"] + "\r\n");
                                            toolStripStatusLabel2.Text = translations["Form1.einf11"];
                                        }
                                        else
                                        {
                                            richTextBox1.AppendText("● INFO ● Nie możemy pobrać tego pliku, ponieważ data przesłania tego elementu jest poza zakresem wyboru. Pomijam...\r\n");
                                            toolStripStatusLabel2.Text = "Gotowe";
                                        }

                                        richTextBox1.SelectionColor = richTextBox1.ForeColor;
                                        richTextBox1.SelectionFont = richTextBox1.Font;
                                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                                        richTextBox1.ScrollToCaret();

                                    }));
                                }
                            };




                            process.OutputDataReceived += (sender, args) =>
                            {
                                if (args.Data != null && args.Data.Contains("[VideoConvertor]"))
                                {
                                    richTextBox1.Invoke((MethodInvoker)(() =>
                                    {
                                        toolStripStatusLabel2.Visible = true;
                                        SetLanguage();

                                        if (languageSelected == 1)
                                        {
                                            toolStripStatusLabel2.Text = translations["Form1.einf10"];
                                        }
                                        else
                                        {
                                            toolStripStatusLabel2.Text = "Finalizacja pobranego pliku  ...";
                                        }

                                    }));
                                }
                            };

                            process.OutputDataReceived += (sender, args) =>
                            {
                                if (
                                args.Data != null && args.Data.Contains("[Metadata]") ||
                                (args.Data != null && args.Data.Contains("[EmbedSubtitle]")
                                ))
                                {
                                    richTextBox1.Invoke((MethodInvoker)(() =>
                                    {
                                        toolStripStatusLabel2.Visible = true;
                                        SetLanguage();

                                        if (languageSelected == 1)
                                        {
                                            toolStripStatusLabel2.Text = translations["Form1.einf11"];
                                        }
                                        else
                                        {
                                            toolStripStatusLabel2.Text = "Gotowe.";
                                        }

                                    }));
                                }
                            };

                            process.OutputDataReceived += (sender, args) =>
                            {
                                if (args.Data != null && args.Data.Contains("[youtube]"))
                                {
                                    richTextBox1.Invoke((MethodInvoker)(() =>
                                    {
                                        toolStripStatusLabel2.Visible = true;

                                        SetLanguage();

                                        if (languageSelected == 1)
                                        {
                                            toolStripStatusLabel2.Text = translations["Form1.einf12"];
                                        }
                                        else
                                        {
                                            toolStripStatusLabel2.Text = "Parsowanie pliku ...";
                                        }

                                    }));
                                }
                            };

                            await process.WaitForExitAsync(); // Czekaj na zakończenie procesu
                            richTextBox1.Invoke((MethodInvoker)(() =>
                            {
                                // pasek postępu:
                                completedTasks++;
                                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                                int progressPercentage = (int)(((double)completedTasks / totalLinks) * 100);
                                toolStripProgressBar1.Value = progressPercentage;

                                if (toolStripProgressBar1.Value == 100 || progressPercentage == 100)
                                {
                                    button4.Enabled = true;
                                    button4.Visible = true;
                                    button13.Enabled = false;
                                    button13.Visible = false;
                                }
                                SetLanguage();

                                if (languageSelected == 1)
                                {
                                    toolStripStatusLabel2.Text = translations["Form1.einf13"] + $"{completedTasks}/{totalLinks} (Completed {progressPercentage}%";
                                    richTextBox1.AppendText("\r\n" + translations["Form1.einf15"] + "\r\n");
                                }
                                else
                                {
                                    toolStripStatusLabel1.Text = $"Pobrano elementów: {completedTasks}/{totalLinks} (Ukończono {progressPercentage}%)";
                                    richTextBox1.AppendText("\r\nUkończono pobieranie wskazanego elementu." + Environment.NewLine);
                                }

                            }));
                        }
                        finally
                        {
                            semaphore.Release(); // Zwolnij semafor po zakończeniu procesu
                        }
                    });
                }
            }
            else
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                SetLanguage();

                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.err07"]);
                }
                else
                {
                    richTextBox1.AppendText("\r\n▲ ERR07 ▲ Pusta kolejka. Dodaj link zanim rozpoczniesz pobieranie.");
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();

                button13.Enabled = false;
                button13.Visible = false;
                button4.Enabled = true;
                button4.Visible = true;
                toolStripStatusLabel2.Visible = false;
            }
        }


        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox26.Checked == true)
            {
                if (languageSelected == 1)
                {
                    var result1 = MessageBox.Show("You are about to open the advanced settings panel, which contains more complex options. If you change them unwisely, they can damage the proper operation of the program. We encourage you to change these settings only if you are sure of them and know what you are doing. \r\n\r\nAre you sure you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result1 == DialogResult.Yes)
                    {
                        label50.Enabled = true;
                        label50.Visible = true;
                        flowLayoutPanel1.Enabled = true;
                        flowLayoutPanel1.Visible = true;
                        SetLanguage();
                        checkBox26.Text = translations["Form1.ldyn01"];
                    }
                    else
                    {
                        //checkBox26.Click;
                    }
                }
                else
                {
                    var result2 = MessageBox.Show("Zamierzasz otworzyć panel zaawansowanych ustawień, które zawierają więcej złożonych opcji. W przypadku nierozważnej zmiany, mogą one uszkodzić prawidłowe działanie programu. Zachęcamy do zmiany tych ustawień, tylko wtedy gdy jesteś ich pewny i wiesz co robisz. \r\n\r\nCzy na pewno chcesz kontynnuować?", "Ostrzeżenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result2 == DialogResult.Yes)
                    {
                        label50.Enabled = true;
                        label50.Visible = true;
                        flowLayoutPanel1.Enabled = true;
                        flowLayoutPanel1.Visible = true;
                        SetLanguage();
                        checkBox26.Text = "Ukryj ustawienia zaawansowane";
                    }
                }
            }
            else
            {
                label50.Enabled = false;
                label50.Visible = false;
                flowLayoutPanel1.Enabled = false;
                flowLayoutPanel1.Visible = false;
                SetLanguage();

                if (languageSelected == 1)
                {
                    checkBox26.Text = translations["Form1.ldyn02"];
                }
                else
                {
                    checkBox26.Text = "Pokaż ustawienia zaawansowane";
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string textToAppend = "\"" + textBox19.Text + "\"";

            if (!System.String.IsNullOrEmpty(textBox19.Text))
            {
                File.AppendAllText(queuePath, textToAppend + Environment.NewLine);

                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.SteelBlue;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                SetLanguage();

                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.einf16"]);
                }
                else
                {
                    richTextBox1.AppendText("\r\n● INFO ● Dodano link: " + (textToAppend) + " do kolejki pobierania.");
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                //prepareDownloads();
            }
            else
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectionFont = new Font("Consolas", richTextBox1.Font.Size, FontStyle.Bold);
                SetLanguage();

                if (languageSelected == 1)
                {
                    richTextBox1.AppendText("\r\n" + translations["Form1.err05"]);
                }
                else
                {
                    richTextBox1.AppendText("\r\n▲ ERR05 ▲ Nie można dodać pustego linku do pobrania.");
                }

                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.SelectionFont = richTextBox1.Font;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
        }

        private Form4 form4 = null;

        public void ShowForm4()
        {
            if (form4 == null || form4.IsDisposed)
            {
                if (languageSelected == 1)
                {
                    Form4 form4 = new Form4(this);
                    SetLanguage();
                    ApplyTranslationsToControls(form4, translations, "Form4");
                    form4.Show();
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.AppendText("\r\n" + translations["Form1.einf17"]);
                }
                else
                {
                    form4 = new Form4(this);
                    form4.Show();
                    richTextBox1.AppendText($"\r\nWyświetlono okno podglądu kolejki pobierania.");
                }
                richTextBox1.ScrollToCaret();
            }
            else
            {
                form4.BringToFront();
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            ShowForm4();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        // panel 1 - głowne menu
        // panel 2 - ustawienia pobierania 
        // panel 3 - ustawienia aplikacji

        private void button1_Click(object sender, EventArgs e)
        {
            // przycisk - "edytuj ustawienia pobierania" - przenieś do panel2, powrót do panel3, włącz button2 
            panel2.Enabled = true;
            panel2.Visible = true;
            button2.Enabled = true;
            button2.Visible = true;

            panel1.Enabled = false;
            panel1.Visible = false;
            panel3.Enabled = false;
            panel3.Visible = false;

            button10.Enabled = false;
            button10.Visible = false;

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            SetLanguage();

            if (languageSelected == 1)
            {
                richTextBox1.AppendText("\r\n" + translations["Form1.einf18"]);
            }
            else
            {
                richTextBox1.AppendText($"\r\nWyświetlono panel ustawień.");
            }

            richTextBox1.ScrollToCaret();

        }
        int countertextbox19 = 0;

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            textBox19.Font = new Font("Segoe UI", 14);
            textBox19.ForeColor = Color.Black;
        }

        private void textBox19_Enter(object sender, EventArgs e)
        {
            textBox19.Font = new Font("Segoe UI", 14);
            textBox19.ForeColor = Color.Black;

            if (!System.String.IsNullOrEmpty(textBox19.Text))
            {
                if (countertextbox19 == 0)
                {
                    countertextbox19 = 1;
                    textBox19.Text = "";
                }
                else
                {
                    // return
                }
            }
        }

        private Form2 form2;
        private Form3 form3;
        private Form5 form5;
        private Form6 form6;


        private void button12_Click(object sender, EventArgs e)
        {
            Process.Start(binFile6);
            this.Close();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (form2 == null || form2.IsDisposed)
            {
                if (languageSelected == 1)
                {
                    Form2 form2 = new Form2();
                    SetLanguage();
                    ApplyTranslationsToControls(form2, translations, "Form2");
                    form2.Show();
                }
                else
                {
                    form2 = new Form2();
                    form2.Show();
                }

            }
            else
            {
                form2.BringToFront();
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            if (form3 == null || form3.IsDisposed)
            {
                if (languageSelected == 1)
                {
                    Form3 form3 = new Form3(this);
                    SetLanguage();
                    ApplyTranslationsToControls(form3, translations, "Form3");
                    form3.Show();
                }
                else
                {
                    form3 = new Form3(this);
                    form3.Show();
                }

            }
            else
            {
                form3.BringToFront();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox12.Text = fbd.SelectedPath;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox15.Text = fbd.SelectedPath;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    textBox13.Text = ofd.FileName;
                }
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    textBox20.Text = ofd.FileName;
                }
            }
        }

        // panel 1 - głowne menu
        // panel 2 - ustawienia pobierania 
        // panel 3 - ustawienia aplikacji

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            // settings - panel 3, włącz button 10

            // przycisk - "ustawień" - przenieś do panel3, powrót do panel1, włącz button10

            panel3.Enabled = true;
            panel3.Visible = true;

            panel2.Enabled = false;
            panel2.Visible = false;
            panel1.Enabled = false;
            panel1.Visible = false;

            button2.Enabled = false;
            button2.Visible = false;
            button10.Enabled = true;
            button10.Visible = true;


        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            if (form5 == null || form5.IsDisposed)
            {
                if (languageSelected == 1)
                {
                    Form5 form5 = new Form5();
                    SetLanguage();
                    ApplyTranslationsToControls(form5, translations, "Form5");
                    form5.Show();
                }
                else
                {
                    form5 = new Form5();
                    form5.Show();
                }

            }
            else
            {
                form5.BringToFront();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // PRZYCISK DO POWROTU - 1

            // przycisk - "edytuj ustawienia pobierania" - przenieś do panel2, powrót do panel3, włącz button2 

            panel3.Enabled = true;
            panel3.Visible = true;

            panel2.Enabled = false;
            panel2.Visible = false;
            panel1.Enabled = false;
            panel1.Visible = false;

            button2.Enabled = false;
            button2.Visible = false;
            button10.Enabled = true;
            button10.Visible = true;
            SetLanguage();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            if (languageSelected == 1)
            {
                richTextBox1.AppendText("\r\n" + translations["Form1.einf19"]);
            }
            else
            {
                richTextBox1.AppendText($"\r\nPowrócono do głównego okna");
            }
            richTextBox1.ScrollToCaret();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // PRZYCISK DO POWROTU - 2

            // przycisk - "ustawień" - przenieś do panel3, powrót do panel1, wyłącz button10

            panel1.Enabled = true;
            panel1.Visible = true;

            panel3.Enabled = false;
            panel3.Visible = false;
            panel2.Enabled = false;
            panel2.Visible = false;

            button10.Enabled = false;
            button10.Visible = false;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (form6 == null || form6.IsDisposed)
            {
                if (languageSelected == 1)
                {
                    Form6 form6 = new Form6(this);
                    SetLanguage();
                    ApplyTranslationsToControls(form6, translations, "Form6");
                    form6.Show();
                }
                else
                {
                    form6 = new Form6(this);
                    form6.Show();
                }

            }
            else
            {
                form6.BringToFront();
            }
        }

        private void bkg1_Click(object sender, EventArgs e)
        {
            bkgSel = 1;
            panel5.SuspendLayout();
            sel3.Visible = true;
            sel4.Visible = false;
            sel5.Visible = false;
            sel6.Visible = false;
            sel7.Visible = false;
            sel8.Visible = false;
            sel9.Visible = false;
            panel5.Refresh();
            panel5.ResumeLayout();
            bkgSet();
        }

        private void bkg2_Click(object sender, EventArgs e)
        {
            bkgSel = 2;
            panel5.SuspendLayout();
            sel4.Visible = true;
            sel3.Visible = false;
            sel5.Visible = false;
            sel6.Visible = false;
            sel7.Visible = false;
            sel8.Visible = false;
            sel9.Visible = false;
            panel5.Refresh();
            panel5.ResumeLayout();
            bkgSet();
        }

        private void bkg3_Click(object sender, EventArgs e)
        {
            bkgSel = 3;
            panel5.SuspendLayout();
            sel5.Visible = true;
            sel4.Visible = false;
            sel3.Visible = false;
            sel6.Visible = false;
            sel7.Visible = false;
            sel8.Visible = false;
            sel9.Visible = false;
            panel5.Refresh();
            panel5.ResumeLayout();
            bkgSet();
        }

        private void bkg4_Click(object sender, EventArgs e)
        {
            bkgSel = 4;
            panel5.SuspendLayout();
            sel6.Visible = true;
            sel4.Visible = false;
            sel5.Visible = false;
            sel3.Visible = false;
            sel7.Visible = false;
            sel8.Visible = false;
            sel9.Visible = false;
            panel5.Refresh();
            panel5.ResumeLayout();
            bkgSet();
        }

        private void bkg5_Click(object sender, EventArgs e)
        {
            bkgSel = 5;
            panel5.SuspendLayout();
            sel7.Visible = true;
            sel4.Visible = false;
            sel5.Visible = false;
            sel6.Visible = false;
            sel3.Visible = false;
            sel8.Visible = false;
            sel9.Visible = false;
            panel5.Refresh();
            panel5.ResumeLayout();
            bkgSet();
        }

        private void bkg6_Click(object sender, EventArgs e)
        {
            bkgSel = 6;
            panel5.SuspendLayout();
            sel8.Visible = true;
            sel4.Visible = false;
            sel5.Visible = false;
            sel6.Visible = false;
            sel7.Visible = false;
            sel3.Visible = false;
            sel9.Visible = false;
            panel5.Refresh();
            panel5.ResumeLayout();
            bkgSet();
        }

        private void bkg7_Click(object sender, EventArgs e)
        {
            bkgSel = 7;
            panel5.SuspendLayout();
            sel9.Visible = true;
            sel4.Visible = false;
            sel5.Visible = false;
            sel6.Visible = false;
            sel7.Visible = false;
            sel8.Visible = false;
            sel3.Visible = false;
            panel5.Refresh();
            panel5.ResumeLayout();
            bkgSet();
        }

    }
}
