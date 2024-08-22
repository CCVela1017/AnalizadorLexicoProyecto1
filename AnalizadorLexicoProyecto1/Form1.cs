namespace AnalizadorLexicoProyecto1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string root = openFileDialog1.FileName;
                    string contenido = File.ReadAllText(root);
                    richTextBox1.Text = contenido;
                }
            } catch { MessageBox.Show("El archivo tiene un formato incorrecto o es inaccesible.", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }
    }
}
