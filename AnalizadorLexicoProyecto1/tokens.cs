using System.Text.RegularExpressions;
namespace Tokens;

public class Token
{
    public string[] reserved_words = ["entero", "decimal", "booleano", "cadena", "si"
        , "sino", "mientras", "hacer", "verdadero", "falso", "imprimir"];
    public string[] operators = ["+", "-", "*", "/", "%", "=", "==", "<", ">", "<=", ">=", "!=", "&&", "||"];
    public string[] signs = ["(", ")", "{", "}", "\"", "", ";"];
    
    public bool CheckReservedWords(string word) 
    {
        foreach (string researvedWord in reserved_words)
        {
            if (word == researvedWord)
            {
                return true;
            }
        }

        return false;
    }
    public bool CheckOperators(string word)
    {
        foreach (string operator_ in operators)
        {
            if (word == operator_)
            {
                return true;
            }
        }

        return false;
    }
    public bool CheckSigns(string word)
    {
        foreach (string sign in signs)
        {
            if (word == sign)
            {
                return true;
            }
        }

        return false;
    }
    public bool CheckNumbers(string word)
    {
        if (Regex.IsMatch(word, @"^\d+$"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIdentifiers(string word)
    {
        if (Regex.IsMatch(word, @"^[a-zA-Z0-9]*$"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Refresh(DataGridView myDataGridView, string word, string type)
    {
        foreach (DataGridViewRow fila in myDataGridView.Rows)
        {
            string? valor = fila.Cells["Token"].Value?.ToString();
            if (valor == word)
            {
                string? current_amount = fila.Cells["Cantidad"].Value?.ToString();
                int new_amount = 1 + Convert.ToInt32(current_amount);

                fila.Cells["Cantidad"].Value = new_amount.ToString();
                    
                return;
            }
        }

        myDataGridView.Rows.Add(word, type, 1);

    }
}

