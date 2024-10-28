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
            // Verifica balance de par�ntesis
            if (!BalancedBrackets(condition))
                return false;

            // Expresi�n regular para identificar t�rminos de la condici�n
            string pattern = @"(\b\w+\b|\&\&|\|\||==|!=|<=|>=|<|>)";
            var matches = Regex.Matches(condition, pattern);

            bool lastWasOperator = true; // Controla el orden entre operadores y operandos
            foreach (Match match in matches)
            {
                string term = match.Value;

                if (term == "&&" || term == "||" || term == "==" || term == "!=" || term == "<=" || term == ">=" || term == "<" || term == ">")
                {
                    if (lastWasOperator) return false; // Dos operadores consecutivos son inv�lidos
                    lastWasOperator = true;
                }
                else
                {
                    // Verifica que el t�rmino sea un identificador o un n�mero v�lido
                    if (!tokens.CheckIdentifiers(term) && !tokens.CheckNumbers(term))
                        return false;

                    // Verifica que el identificador est� en la lista de variables previamente asignadas
                    if (!tokens.CheckNumbers(term) && !identifiers.Contains(term))
                    {
                        errors += $"Error en la l�nea {lineNumber}: La variable '{term}' no ha sido asignada antes de su uso en la condici�n.\n";
                        return false;
                    }

                    lastWasOperator = false;
                }
            }

            return !lastWasOperator; // La expresi�n debe terminar con un operando
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
                MessageBox.Show("Parece que no has cargado ning�n archivo.", "Error",
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
                            tokens.Refresh(dataGridView1, word, "N�MEROS");
                        }
                        else if (tokens.CheckIdentifiers(word))
                        {
                            tokens.Refresh(dataGridView1, word, "IDENTIFICADORES");
                        }
                        else
                        {
                            errors += $"La l�nea {i} es err�nea.\n";
                            break;
                        }
                    }
                }
                i++;


            }

            // balanceo de ()[]{}
            if (!BalancedBrackets(richTextBox1.Text))
            {
                errors += "Error sint�ctico, se esperaba }, ] o ).\n";
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
            Stack<int> siStack = new Stack<int>(); // Pila para rastrear cada "si" pendiente de un "sino"

            foreach (string line in File.ReadAllLines(lURL.Text))
            {
                string line1 = SeparateText(line);
                if (!string.IsNullOrEmpty(line1))
                {
                    string[] words = line1.Split(' ');

                    // Validaci�n para "si"
                    if (words[0] == "si")
                    {
                        // Agregar el �ndice de la l�nea a la pila cuando se encuentra un "si"
                        siStack.Push(i);

                        if (words.Length < 4 || words[1] != "(" || words[words.Length - 2] != ")" || words[words.Length - 1] != "{")
                        {
                            errors += $"Error en la sintaxis de 'si' en la l�nea {i}. Formato esperado: si (condicion) {{...}} \n";
                        }
                        else
                        {
                            string condition = string.Join(" ", words.Skip(2).Take(words.Length - 4));

                            if (!ValidateCondition(condition, i, errors))
                            {
                                errors += $"Error en la condici�n de 'si' en la l�nea {i}. Condici�n inv�lida o contiene variables sin asignar: {condition} \n";
                            }
                        }
                    }
                    // Validaci�n para "sino"
                    else if (words[0] == "sino")
                    {
                        // Verifica que haya un "si" sin cerrar pendiente en la pila
                        if (siStack.Count == 0)
                        {
                            errors += $"Error en la sintaxis de 'sino' en la l�nea {i}. 'sino' debe seguir a un bloque 'si'.\n";
                        }
                        else
                        {
                            // Extrae el "si" correspondiente de la pila
                            siStack.Pop();

                            // Verifica que "sino" est� seguido �nicamente por "{"
                            if (words.Length != 2 || words[1] != "{")
                            {
                                errors += $"Error en la sintaxis de 'sino' en la l�nea {i}. Formato esperado: sino {{...}} \n";
                            }
                        }
                    }
                    // Validaci�n para "mientras"
                    else if (words[0] == "mientras")
                    {
                        if (words.Length < 4 || words[1] != "(" || words[words.Length - 2] != ")" || words[words.Length - 1] != "{")
                        {
                            errors += $"Error en la sintaxis de 'mientras' en la l�nea {i}. Formato esperado: mientras (condicion) {{...}} \n";
                        }
                        else
                        {
                            string condition = string.Join(" ", words.Skip(2).Take(words.Length - 4));
                            if (!ValidateCondition(condition, i, errors))
                            {
                                errors += $"Error en la condici�n de 'mientras' en la l�nea {i}. Condici�n inv�lida o contiene variables sin asignar: {condition} \n";
                            }
                        }
                    }
                }
                i++;
            }

            // Verificaci�n final: si queda alg�n "si" en la pila, significa que falt� un "sino" o "}"
            if (siStack.Count > 0)
            {
                while (siStack.Count > 0)
                {
                    int unclosedIfLine = siStack.Pop();
                    errors += $"Advertencia: El bloque 'si' en la l�nea {unclosedIfLine} no tiene un 'sino' correspondiente.\n";
                }
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
