using Tokens;
namespace AnalizadorLexicoProyecto1
{
    public partial class Form1 : Form
    {
        private readonly Token tokens = new();

        public Form1()
        {
            InitializeComponent();
        }



        static bool BalancedBrackets(string expression)
        {
            Stack<char> stack = new Stack<char>();
            int lines = 0;

            foreach (char ch in expression)
            {
                if (ch == '(' || ch == '[' || ch == '{')
                {
                    stack.Push(ch);
                }

                else if (ch == ')' || ch == ']' || ch == '}')
                {
                    if (stack.Count == 0) return false;

                    char top = stack.Pop();


                    if ((ch == ')' && top != '(') ||
                        (ch == ']' && top != '[') ||
                        (ch == '}' && top != '{'))
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    
                    string root = openFileDialog1.FileName;
                    lURL.Text = root;

                    int i = 0;
                    string content = "";
                    
                    foreach (string line in File.ReadAllLines(root))
                    {
                        i++;
                        content += i.ToString() + " - "  + line + "\n";
                    }
                    
                    richTextBox1.Text = content;
                    button2.Enabled = true;

                }
            }

            catch
            {
                MessageBox.Show("El archivo tiene un formato incorrecto o es inaccesible.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lURL.Text == "")
            {
                MessageBox.Show("Parece que no has cargado ningún archivo.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int i = 1;
            string errors = "";

            foreach (string line in File.ReadAllLines(lURL.Text))
            {   
                
                if (!string.IsNullOrEmpty(line))
                {
                    string[] words = line.Split(' ');
                    foreach (string word in words)
                    {
                        if (word == "")
                        {
                            continue;
                        }
                        if (tokens.CheckReservedWords(word.ToLower()))
                        {
                            tokens.Refresh(dataGridView1, word, "PALABRAS RESERVADAS");
                        }
                        else if (tokens.CheckOperators(word))
                        {
                            tokens.Refresh(dataGridView1, word, "OPERADORES");
                        }
                        else if (tokens.CheckSigns(word))
                        {
                            tokens.Refresh(dataGridView1, word, "SIGNOS");
                        }
                        else if (tokens.CheckNumbers(word))
                        {
                            tokens.Refresh(dataGridView1, word, "NÚMEROS");
                        }
                        else if (tokens.CheckIdentifiers(word))
                        {
                            tokens.Refresh(dataGridView1, word, "IDENTIFICADORES");
                        }
                        else
                        {
                            errors += $"La línea {i} es errónea.\n";
                            break;
                        }
                    }
                }
                i++;


            }

            if (!BalancedBrackets(richTextBox1.Text))
            {
                errors += "Error sintáctico, se esperaba }, ] o ).";
            }

            /*
            foreach (string line in File.ReadAllLines(lURL.Text))
            {

                if (!string.IsNullOrEmpty(line))
                {
                    string[] words = line.Split(' ');
                    foreach (string word in words)
                    {
                        if (word == "")
                        {
                            continue;
                        } else if (word == ""){
                            word
                        }
                        else
                        {
                            errors += $"La línea {i} es errónea.\n";
                            break;
                        }
                    }
                }
                i++;


            }
            */


            if (errors != "")
            {
                dataGridView1.Rows.Clear();
            }

            richTextBox2.Text = errors;


        }
    }
}
