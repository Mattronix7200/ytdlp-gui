using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace ytdl_gui
{
    public partial class Form4 : Form
    {
        private Form1 _form1;
        private string queuePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/qsave.ini");
        private BindingList<LinkData> bindingList;

        int languageSelected;
        string iniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/translate/en.ini");
        Dictionary<string, string> translations = new Dictionary<string, string>();
        private string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./data/config.ini");
        int langSel;


        public Form4(Form1 form1)
        {
            _form1 = form1;  // Przypisz form1 do _form1
            InitializeComponent();
            LoadConfig();

            if (langSel == 1)
            {
                languageSelected = 1;
                this.Text = "Download queue";
            }
            if (langSel == 0)
            {
                languageSelected = 0;
            }

            SetLanguage();

            string[] lines = File.ReadAllLines(queuePath);
            bindingList = new BindingList<LinkData>(lines.Select(x => new LinkData { Link = x }).ToList());
            dataGridView1.DataSource = bindingList;
            dataGridView1.Columns[0].DataPropertyName = "Link";
            UpdateButtons();
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

        public void RefreshData()
        {
            string[] lines = File.ReadAllLines(queuePath);
            bindingList = new BindingList<LinkData>(lines.Select(x => new LinkData { Link = x }).ToList());
            dataGridView1.DataSource = bindingList;
            dataGridView1.Columns[0].DataPropertyName = "Link";
            UpdateButtons();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void checkForConfigfile()
        {
            if (!File.Exists(queuePath))
            {
                _form1.richTextBox1.SelectionStart = _form1.richTextBox1.TextLength;
                _form1.richTextBox1.SelectionLength = 0;
                _form1.richTextBox1.SelectionColor = Color.Red;
                _form1.richTextBox1.SelectionFont = new Font("Consolas", _form1.richTextBox1.Font.Size, FontStyle.Bold);

                if (languageSelected == 1)
                {
                _form1.richTextBox1.AppendText("\r\n" + translations["Form4.err06"] + "\r\n");
                }
                else
                {
                _form1.richTextBox1.AppendText("\r\n▲ ERR06 ▲ Nie można odczytać danych do zapisu.");
                }

                _form1.richTextBox1.SelectionColor = _form1.richTextBox1.ForeColor;
                _form1.richTextBox1.SelectionFont = _form1.richTextBox1.Font;
                _form1.richTextBox1.SelectionStart = _form1.richTextBox1.Text.Length;
                _form1.richTextBox1.ScrollToCaret();
                _form1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkForConfigfile();
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count > 0)
            {
                List<int> indices = selectedRows.Cast<DataGridViewRow>()
                                                .Select(row => row.Index)
                                                .OrderByDescending(index => index)
                                                .ToList();

                foreach (int index in indices)
                {
                    bindingList.RemoveAt(index);
                }
                var lines = File.ReadAllLines(queuePath).ToList();
                foreach (int index in indices)
                {
                    lines.RemoveAt(index);
                }
                File.WriteAllLines(queuePath, lines);

                UpdateButtons();
                _form1.richTextBox1.SelectionStart = _form1.richTextBox1.TextLength;
                _form1.richTextBox1.SelectionLength = 0;
                _form1.richTextBox1.SelectionColor = Color.SteelBlue;
                _form1.richTextBox1.SelectionFont = new Font("Consolas", _form1.richTextBox1.Font.Size, FontStyle.Bold);
                if (languageSelected == 1)
                {
                    _form1.richTextBox1.AppendText("\r\n" + translations["Form4.einf20"] + "\r\n");
                }
                else
                {
                    _form1.richTextBox1.AppendText("\r\n!   Usunięto wskazane linki z kolejki.");
                }
                _form1.richTextBox1.SelectionColor = _form1.richTextBox1.ForeColor;
                _form1.richTextBox1.SelectionFont = _form1.richTextBox1.Font;
                _form1.richTextBox1.SelectionStart = _form1.richTextBox1.Text.Length;
                _form1.richTextBox1.ScrollToCaret();
            }
            else
            {
                if (languageSelected == 1)
                {
                MessageBox.Show("Please select at least one row to delete.");
                }
                else
                {
                MessageBox.Show("Wybierz przynajmniej jeden link do usunięcia aby móc zastosować zmiany.");
                }

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            checkForConfigfile();
            bindingList.Clear();
            File.WriteAllText(queuePath, string.Empty);
            UpdateButtons();
            _form1.richTextBox1.SelectionStart = _form1.richTextBox1.TextLength;
            _form1.richTextBox1.SelectionLength = 0;
            _form1.richTextBox1.SelectionColor = Color.SteelBlue;
            _form1.richTextBox1.SelectionFont = new Font("Consolas", _form1.richTextBox1.Font.Size, FontStyle.Bold);
            if (languageSelected == 1)
            {
            _form1.richTextBox1.AppendText("\r\n" + translations["Form4.einf21"] + "\r\n");
            }
            else
            {
            _form1.richTextBox1.AppendText("\r\n!   Usunięto wszystkie linki z kolejki.");
            }
            _form1.richTextBox1.SelectionColor = _form1.richTextBox1.ForeColor;
            _form1.richTextBox1.SelectionFont = _form1.richTextBox1.Font;
            _form1.richTextBox1.SelectionStart = _form1.richTextBox1.Text.Length;
            _form1.richTextBox1.ScrollToCaret();
        }

        private void UpdateButtons()
        {
            checkForConfigfile();
            bool hasData = bindingList.Count > 0;
            button1.Enabled = hasData;
            button2.Enabled = hasData;
        }
    }

    public class LinkData
    {
        public string? Link { get; set; }
    }


}
