using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.DataFormats;
using System.IO;
using Microsoft.VisualBasic.Devices;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ytdl_gui
{
    public partial class Form6 : Form
    {
        private Form1 _form1;
        int languageSelected;
        string iniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/translate/en.ini");
        Dictionary<string, string> translations = new Dictionary<string, string>();
        private string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/config.ini");
        int langSel;

        public Form6(Form1 form1)
        {
            _form1 = form1;
            InitializeComponent();
            LoadConfig();

            if (langSel == 1)
            {
                languageSelected = 1;
                this.Text = "App profile config editor";
            }
            if (langSel == 0)
            {
                languageSelected = 0;
            }

            SetLanguage();
            UpdateListBox();
        }

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
                            case "langSel":
                                langSel = int.Parse(parts[1]);
                                break;
                        }
                    }
                }
            }
        }

        public void SetLanguage()
        {
            if (languageSelected == 1)
            {
                Dictionary<string, string> translations = ReadIniFile(iniFilePath);
                ApplyTranslationsToControls(this, translations, this.Name);
                this.Refresh();
            }

            if (languageSelected == 0)
            {
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
                else if (c is ListBox)
                {
                    ListBox listBox = c as ListBox;
                    for (int i = 0; i < listBox.Items.Count; i++)
                    {
                        string key = formName + "." + c.Name + "[" + i + "]";
                        if (translations.ContainsKey(key))
                        {
                            listBox.Items[i] = translations[key];
                        }
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
                _form1.richTextBox1.AppendText($"Wystąpił błąd podczas ustawiania języka programu. Can't set your destination language. (Error: {ex.Message})");
            }

            return translations;
        }

        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            var files = Directory.GetFiles("./data/configs");
            Dictionary<string, string> fileDescriptions = new Dictionary<string, string>();
            if (langSel == 1)
            {
                fileDescriptions = new Dictionary<string, string>()
                {
                    {"config01.ini", "Default Settings"},
                    {"config02.ini", "Download music - MP3"},
                    {"config03.ini", "Download music - ALAC"},
                    {"config04.ini", "Download movie- best quality"},
                    {"config05.ini", "Download every video fit in selected date"},
                    {"config06.ini", "Download every video fit in selected size"},
                    {"config07.ini", "Download every second video from playlist"},
                    {"config08.ini", "Download video with Polish subtitles"},
                    {"config09.ini", "Download video with English subtitles"}
                };
            }
            else
            {
                fileDescriptions = new Dictionary<string, string>()
                {
                    {"config01.ini", "Domyślne ustawienia"},
                    {"config02.ini", "Pobieranie muzyki - MP3"},
                    {"config03.ini", "Pobieranie muzyki - ALAC"},
                    {"config04.ini", "Pobieranie filmu - najlepsza jakość"},
                    {"config05.ini", "Pobieranie filmu z ograniczeniem daty"},
                    {"config06.ini", "Pobieranie filmu z ograniczeniem rozmiaru"},
                    {"config07.ini", "Pobieranie co 2 filmu z playlisty"},
                    {"config08.ini", "Pobieranie filmu z napisami polskimi"},
                    {"config09.ini", "Pobieranie filmu z napisami angielskimi"}
                };
            }

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (fileDescriptions.ContainsKey(fileName))
                {
                    listBox1.Items.Add(fileDescriptions[fileName]);
                }
                else
                {
                    listBox1.Items.Add(fileName);
                }
            }

            string lastConfigFile = System.IO.File.ReadAllText(Path.Combine("./data/lastconfig.ini"));
            string lastConfigDescription = fileDescriptions.ContainsKey(lastConfigFile) ? fileDescriptions[lastConfigFile] : lastConfigFile;
            listBox1.SelectedItem = lastConfigDescription;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                label6.Visible = true;
                label6.Enabled = true;
                label7.Enabled = false;
                label7.Visible = false;
            }
           else
            {
                string sourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/config.ini");
                string destFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/configs/", textBox1.Text + ".ini");
                System.IO.File.Copy(sourceFile, destFile, true);
                UpdateListBox();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "INI files (*.ini)|*.ini";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    var lines = File.ReadAllLines(filePath);
                    var validFile = true;
                    var requiredKeys = new List<string>
                    {
                        "langSel", "bkgSel", "checkBox1", "checkBox2", "checkBox3", "checkBox4", "checkBox5", "checkBox6",
                        "checkBox7", "checkBox8", "checkBox9", "checkBox10", "checkBox11", "checkBox12", "checkBox13",
                        "checkBox14", "checkBox15", "checkBox16", "checkBox17", "checkBox18", "checkBox19",
                        "checkBox20", "checkBox21", "checkBox22", "checkBox23", "checkBox24", "checkBox25",
                        "checkBox27", "checkBox28", "checkBox29", "checkBox30", "checkBox31", "isDateOption1",
                        "isDateOption2", "isDateOption3", "textBox1", "textBox2", "textBox3", "textBox4", "textBox5",
                        "textBox6", "textBox7", "textBox8", "textBox9", "textBox10", "textBox11", "textBox12",
                        "textBox13", "textBox14", "textBox15", "textBox16", "textBox17", "textBox18", "textBox20",
                        "comboBox1", "comboBox2", "comboBox3", "comboBox4", "comboBox5", "comboBox6", "comboBox7",
                        "comboBox8", "comboBox9", "comboBox10", "comboBox11", "dateTimePicker1", "dateTimePicker2",
                        "dateTimePicker3"
                    };
                    foreach (var line in lines)
                    {
                        var key = line.Split('=')[0];
                        if (!requiredKeys.Contains(key))
                        {
                            validFile = false;
                            break;
                        }
                    }
                    if (!validFile)
                    {
                        MessageBox.Show("Plik konfiguracyjny jest błędny.");
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(filePath))
                        {
                            sw.WriteLine($"langSel={langSel}");
                        }
                        string destPath = "./data/configs/" + Path.GetFileName(filePath);
                        File.Copy(filePath, destPath, true);
                        listBox1.Items.Add(Path.GetFileName(filePath));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wystąpił błąd podczas przetwarzania pliku: " + ex.Message);
                }
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> fileDescriptions2 = new Dictionary<string, string>();
            if (langSel == 1)
            {
                fileDescriptions2 = new Dictionary<string, string>()
                {
                    {"Default Settings", "config01.ini"},
                    {"Download music - MP3", "config02.ini"},
                    {"Download music - ALAC", "config03.ini"},
                    {"Download movie- best quality", "config04.ini"},
                    {"Download every video fit in selected date", "config05.ini"},
                    {"Download every video fit in selected size", "config06.ini"},
                    {"Download every second video from playlist", "config07.ini"},
                    {"Download video with Polish subtitles", "config08.ini"},
                    {"Download video with English subtitles", "config09.ini"}
                };
            }
            else
            {
                fileDescriptions2 = new Dictionary<string, string>()
                {
                    {"Domyślne ustawienia", "config01.ini"},
                    {"Pobieranie muzyki - MP3", "config02.ini"},
                    {"Pobieranie muzyki - ALAC", "config03.ini"},
                    {"Pobieranie filmu - najlepsza jakość", "config04.ini"},
                    {"Pobieranie filmu z ograniczeniem daty", "config05.ini"},
                    {"Pobieranie filmu z ograniczeniem rozmiaru", "config06.ini"},
                    {"Pobieranie co 2 filmu z playlisty", "config07.ini"},
                    {"Pobieranie filmu z napisami polskimi", "config08.ini"},
                    {"Pobieranie filmu z napisami angielskimi", "config09.ini"}
                };
            }

            if (listBox1.SelectedItem != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "INI File|*.ini";
                saveFileDialog.Title = "Zapisz plik konfiguracyjny";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    string selectedFile = listBox1.SelectedItem.ToString();
                    string fileName;
                    if (fileDescriptions2.ContainsKey(selectedFile))
                    {
                        fileName = fileDescriptions2[selectedFile];
                    }
                    else
                    {
                        fileName = selectedFile;
                    }
                    System.IO.File.Copy(Path.Combine("./data/configs", fileName), saveFileDialog.FileName, true);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            button1.Enabled = true;
            label7.Enabled = true;
            label7.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> fileDescriptions2 = new Dictionary<string, string>();
            if (langSel == 1)
            {
                fileDescriptions2 = new Dictionary<string, string>()
                {
                    {"Default Settings", "config01.ini"},
                    {"Download music - MP3", "config02.ini"},
                    {"Download music - ALAC", "config03.ini"},
                    {"Download movie- best quality", "config04.ini"},
                    {"Download every video fit in selected date", "config05.ini"},
                    {"Download every video fit in selected size", "config06.ini"},
                    {"Download every second video from playlist", "config07.ini"},
                    {"Download video with Polish subtitles", "config08.ini"},
                    {"Download video with English subtitles", "config09.ini"}
                };
            }
            else
            {
                fileDescriptions2 = new Dictionary<string, string>()
                {
                    {"Domyślne ustawienia", "config01.ini"},
                    {"Pobieranie muzyki - MP3", "config02.ini"},
                    {"Pobieranie muzyki - ALAC", "config03.ini"},
                    {"Pobieranie filmu - najlepsza jakość", "config04.ini"},
                    {"Pobieranie filmu z ograniczeniem daty", "config05.ini"},
                    {"Pobieranie filmu z ograniczeniem rozmiaru", "config06.ini"},
                    {"Pobieranie co 2 filmu z playlisty", "config07.ini"},
                    {"Pobieranie filmu z napisami polskimi", "config08.ini"},
                    {"Pobieranie filmu z napisami angielskimi", "config09.ini"}
                };
            }

            if (listBox1.SelectedItem != null)
            {
                string selectedFile = listBox1.SelectedItem.ToString();
                string fileName;
                if (fileDescriptions2.ContainsKey(selectedFile))
                {
                    fileName = fileDescriptions2[selectedFile];
                }
                else
                {
                    fileName = selectedFile;
                }

                string mainDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "configs");
                System.IO.File.Copy(Path.Combine(mainDir, fileName), Path.Combine(mainDir, "..", "config.ini"), true);
                System.IO.File.WriteAllText(Path.Combine(mainDir, "..", "lastconfig.ini"), fileName);
                Application.Restart();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            string configDirectory = Path.Combine(baseDirectory, "./data/configs/");

            Dictionary<string, string> fileDescriptions2 = new Dictionary<string, string>();
            if (langSel == 1)
            {
                fileDescriptions2 = new Dictionary<string, string>()
                {
                    {"Default Settings", "config01.ini"},
                    {"Download music - MP3", "config02.ini"},
                    {"Download music - ALAC", "config03.ini"},
                    {"Download movie- best quality", "config04.ini"},
                    {"Download every video fit in selected date", "config05.ini"},
                    {"Download every video fit in selected size", "config06.ini"},
                    {"Download every second video from playlist", "config07.ini"},
                    {"Download video with Polish subtitles", "config08.ini"},
                    {"Download video with English subtitles", "config09.ini"}
                };
            }
            else
            {
                fileDescriptions2 = new Dictionary<string, string>()
                {
                    {"Domyślne ustawienia", "config01.ini"},
                    {"Pobieranie muzyki - MP3", "config02.ini"},
                    {"Pobieranie muzyki - ALAC", "config03.ini"},
                    {"Pobieranie filmu - najlepsza jakość", "config04.ini"},
                    {"Pobieranie filmu z ograniczeniem daty", "config05.ini"},
                    {"Pobieranie filmu z ograniczeniem rozmiaru", "config06.ini"},
                    {"Pobieranie co 2 filmu z playlisty", "config07.ini"},
                    {"Pobieranie filmu z napisami polskimi", "config08.ini"},
                    {"Pobieranie filmu z napisami angielskimi", "config09.ini"}
                };
            }

            if (listBox1.SelectedItem != null)
            {
                string selectedFileDescription = listBox1.SelectedItem.ToString();

                if (fileDescriptions2.ContainsKey(selectedFileDescription))
                {
                    string selectedFile = fileDescriptions2[selectedFileDescription];
                    if (selectedFile.CompareTo("config01.ini") >= 0 && selectedFile.CompareTo("config09.ini") <= 0)
                    {
                        MessageBox.Show("Nie można usunąć wybranego pliku konfiguracyjnego.");
                        return;
                    }

                    string fullPath = Path.Combine(configDirectory, selectedFile);

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                        UpdateListBox();
                        MessageBox.Show("Plik konfiguracyjny został usunięty.");
                    }
                    else
                    {
                        MessageBox.Show("Wystąpił nieoczekiwany błąd podczas usuwania pliku.");
                    }
                }
                else
                {
                    string fullPathNonReversed = Path.Combine(configDirectory, selectedFileDescription);
                    File.Delete(fullPathNonReversed);
                    UpdateListBox();
                    MessageBox.Show("Plik konfiguracyjny został usunięty.");
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano żadnego pliku konfiguracyjnego.");
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label6.Visible = false;
            label6.Enabled = false;
            label7.Enabled = false;
            label7.Visible = false;
        }
    }
}
