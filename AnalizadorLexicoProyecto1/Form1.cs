using System.Text.RegularExpressions;
using Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace AnalizadorLexicoProyecto1
{
    public partial class Form1 : Form
    {
        private readonly Token tokens = new();
        private List<string> identifiers = new List<string>();
        private Dictionary<string, int> functions = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();
        }

        static bool IsType(string text) 
        {
            return text == "booleano" || text == "entero" || text == "cadena";
        }

        static string SeparateText(string input)
        {
            string pattern = @"([\{\}\(\)\[\]\+\-\*/;\""\,])";

            string result = Regex.Replace(input, pattern, " $1 ");
            result = Regex.Replace(result, @"\s+", " ");

            return result.Trim();
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
        private bool ValidateCondition(string condition, int lineNumber, string errors)
        {
            // Verifica balance de paréntesis
            if (!BalancedBrackets(condition))
                return false;

            // Expresión regular para identificar términos de la condición
            string pattern = @"(\b\w+\b|\&\&|\|\||==|!=|<=|>=|<|>)";
            var matches = Regex.Matches(condition, pattern);

            bool lastWasOperator = true; // Controla el orden entre operadores y operandos
            foreach (Match match in matches)
            {
                string term = match.Value;

                if (term == "&&" || term == "||" || term == "==" || term == "!=" || term == "<=" || term == ">=" || term == "<" || term == ">")
                {
                    if (lastWasOperator) return false; // Dos operadores consecutivos son inválidos
                    lastWasOperator = true;
                }
                else
                {
                    // Verifica que el término sea un identificador o un número válido
                    if (!tokens.CheckIdentifiers(term) && !tokens.CheckNumbers(term))
                        return false;

                    // Verifica que el identificador esté en la lista de variables previamente asignadas
                    if (!tokens.CheckNumbers(term) && !identifiers.Contains(term))
                    {
                        errors += $"Error en la línea {lineNumber}: La variable '{term}' no ha sido asignada antes de su uso en la condición.\n";
                        return false;
                    }

                    lastWasOperator = false;
                }
            }

            return !lastWasOperator; // La expresión debe terminar con un operando
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

            // validacion de tokens
            foreach (string line in File.ReadAllLines(lURL.Text))
                {   
                string line1 = SeparateText(line);
                if (!string.IsNullOrEmpty(line1))
                {
                    string[] words = line1.Split(' ');
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

            // balanceo de ()[]{}
            if (!BalancedBrackets(richTextBox1.Text))
            {
                errors += "Error sintáctico, se esperaba }, ] o ).\n";
            }

            i = 1;


            // validacion de variables
            foreach (string line in File.ReadAllLines(lURL.Text))
            {
                string line1 = SeparateText(line);
                if (!string.IsNullOrEmpty(line))
                {
                    string[] words = line1.Split(' ');
                    if (words.Length == 1 && tokens.CheckReservedWords(words[0]))
                    {
                        errors += $"Expresion sin objetivo en la linea {i}\n";
                    } else
                    {
                        if (words.Length == 3 && words[words.Length - 1] == ";") {
                            if (tokens.CheckReservedWords(words[0]) && tokens.CheckReservedWords(words[1]))
                            {
                                errors += $"Una palabra reservada esta repetida en la linea {i}\n";
                            } else if (IsType(words[0]) && tokens.CheckIdentifiers(words[1])) {
                                identifiers.Add(words[1]);
                            }
                        } else if (words.Length == 5)
                        {
                            if (IsType(words[0]) && tokens.CheckIdentifiers(words[1]) && words[2] == "=" && words[4] == ";") {
                                identifiers.Add(words[1]);
                            }
                        } 
                    }
                }
                i++;
            }
            
            i = 1;
            // validacion de ifs
            foreach (string line in File.ReadAllLines(lURL.Text))
            {
                
                string line1 = SeparateText(line);
                if (!string.IsNullOrEmpty(line1))
                {
                    string[] words = line1.Split(' ');
                    
                    // Validación para "si"
                    if (words[0] == "si")
                    {
                        if (words.Length < 4 || words[1] != "(" || words[words.Length - 2] != ")" || words[words.Length - 1] != "{")
                        {
                            MessageBox.Show(words.ToString());
                            errors += $"Error en la sintaxis de 'si' en la línea {i}.\n";
                        }
                        else
                        {
                            string condition = string.Join(" ", words.Skip(2).Take(words.Length - 4)); // Extrae la condición entre paréntesis
                            if (!ValidateCondition(condition, i, errors)) // Llama a la función de validación de la condición
                            {
                                errors += $"Error en la condición de 'si' en la línea {i}. Condición inválida: {condition} \n";
                            }
                        }
                    }
                    // Validación para "mientras"
                    else if (words[0] == "mientras")
                    {
                        if (words.Length < 4 || words[1] != "(" || words[words.Length - 2] != ")" || words[words.Length - 1] != "{")
                        {
                            errors += $"Error en la sintaxis de 'mientras' en la línea {i}.\n";
                        }
                        else
                        {
                            string condition = string.Join(" ", words.Skip(2).Take(words.Length - 4)); // Extrae la condición entre paréntesis
                            if (!ValidateCondition(condition, i, errors)) // Llama a la función de validación de la condición
                            {
                                errors += $"Error en la condición de 'mientras' en la línea {i}. Condición inválida: {condition} \n";
                            }
                        }
                    }
                }
                i++;
            }

            i = 1;
            // validacion de funciones y su numero de argumentos
            foreach (string line in File.ReadAllLines(lURL.Text))
            {
                string line1 = SeparateText(line);
                if (!string.IsNullOrEmpty(line))
                {
                    string[] words = line1.Split(" ");
                    if (words.Length >= 5)
                    {
                        if (IsType(words[0]) && tokens.CheckIdentifiers(words[1]) && words[2] == "(")
                        {
                            int cantParams = 0;
                            for (int j = 3; j < words.Length; j++)
                            {
                                if (j == words.Length - 1)
                                {
                                    errors += $"Se esperaba ) en la linea {i} \n";
                                    break;
                                }
                                if (words[j] == ")")
                                {
                                    break;
                                } else if (tokens.CheckIdentifiers(words[j])) {
                                    cantParams++;
                                }
                            }
                            if (words[words.Length - 1] == "{")
                            {
                                functions[words[1]] = cantParams;
                            } else
                            {
                                errors += "Se esperaba { para abrir la funcion en la linea " + i.ToString() + "\n";
                            }
                        }
                    }
                    
                }
                i++;
            }

            i = 1;
            // validacion de la cantidad de parametros que recibe una funcion
            foreach (string line in File.ReadAllLines(lURL.Text))
            {
                string line1 = SeparateText(line);
                if (!string.IsNullOrEmpty(line))
                {
                    string[] words = line1.Split(" ");
                    if (words.Length > 0)
                    {
                        if (functions.ContainsKey(words[0]) && words[1] == "(")
                        {
                            int cantParams = 0;
                            for (int j = 2; j < words.Length; j++)
                            {
                                if (j == words.Length - 1)
                                {
                                    errors += $"Se esperaba ) en la linea {i} \n";
                                    break;
                                }
                                if (words[j] == ")")
                                {
                                    break;
                                }
                                else if (tokens.CheckIdentifiers(words[j]))
                                {
                                    cantParams++;
                                }
                            }
                            if (cantParams != functions[words[0]])
                            {
                                errors += $"En la linea {i}, se esperaban {functions[words[0]]} parametros, pero se recibieron {cantParams}";
                            }
                        }
                    }
                }
                i++;
            }

                if (errors != "")
            {
                dataGridView1.Rows.Clear();
            }

            richTextBox2.Text = errors;


        }
    }
}
