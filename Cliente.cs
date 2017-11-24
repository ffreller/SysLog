using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace SysLog
{
    public class Cliente
    {
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }

        public void Cadastrar()
        {
            Console.WriteLine("Qual é seu nome?");
            this.nome = Console.ReadLine();
            Console.WriteLine("Qual é seu email?");
            this.email = Console.ReadLine();
            Console.WriteLine("Qual é sua senha?");
            this.senha = Console.ReadLine();
            if(!File.Exists("CadUsuario.csv"))
            {
                StreamWriter criarquivo = new StreamWriter("CadUsuario.csv", true);
                criarquivo.WriteLine("Nome;E-mail;Senha Criptografada");
                criarquivo.Close();
            }
                     
            StreamWriter cadastro = new StreamWriter("CadUsuario.csv", true);
            cadastro.WriteLine(nome + ";" + email + ";" + encripSenha(senha));
            Console.WriteLine("Cadastro efetuado com sucesso!"); 
            cadastro.Close();  
            
            
        }

        public void Logar()
        {
             Console.WriteLine("Qual é seu email?");
             this.email = Console.ReadLine();
             StreamReader ler = new StreamReader("CadUsuario.csv");
             string linha;
             while((linha=ler.ReadLine())!=null)
             {
                string[] dados = linha.Split(';');
                if (dados[1]==this.email)
                {
                    Console.WriteLine("Qual é sua senha?");
                    this.senha = Console.ReadLine();
                    if(encripSenha(senha) == dados[2])
                    {
                        Console.WriteLine("Login efetuado!");
                        string email = dados[1];
                        Del del1 = new Del();
                        del1.Loginsup += new Del.Deleg1(LoginReg);
                    }
                    
                    else
                    {
                        Console.WriteLine("Senha incorreta");
                        return;
                    }
                }
            }
            return;
        ler.Close();    
        }
        public void Sair()
        {
            Console.WriteLine("Qual é seu email?");
            this.email = Console.ReadLine();
            StreamReader ler = new StreamReader("CadUsuario.csv");
            string linha;
            while((linha=ler.ReadLine())!=null)
            {
                string[] dados = linha.Split(';');
                if (dados[1]==this.email)
                {
                    
                }
            }
        }    

        static string encripSenha (string senha)   
        {
            byte[] senhaOriginal;
            byte[] senhaModificada;
            SHA512 md5;
            senhaOriginal = Encoding.Default.GetBytes(senha);
            md5 = SHA512.Create();
            senhaModificada = md5.ComputeHash(senhaOriginal);

            return Convert.ToBase64String(senhaModificada);

        }

        public void LoginReg(string email)
        {
            StreamReader ler = new StreamReader("CadUsuario.csv");
            string linha;
            while((linha=ler.ReadLine())!=null)
            {
                string[] dados = linha.Split(';');
                if (dados[1]==email)
                {
                    
                    if(!File.Exists("Superior.csv"))
                    {   
                        StreamWriter criarquivo = new StreamWriter("Superior.csv", true);
                        criarquivo.WriteLine("Nome do Usuário;Email;Login ou Logout;Data do Login/Logout");
                        criarquivo.Close();
                    }
                    StreamWriter superior = new StreamWriter("Superior.csv", true);
                    superior.WriteLine(dados[0] + ";" +dados[1] + ";Login;" + DateTime.Now);
                    superior.Close();
                    
                    if (!File.Exists("LogSistema.csv"))
                    {
                        StreamWriter criarquivo = new StreamWriter("LogSistema.csv", true);
                        criarquivo.WriteLine("Nome do Usuário;Email;Login ou Logout;Data do Login/Logout");
                        criarquivo.Close();
                    }
                    StreamWriter log = new StreamWriter("LogSistema.csv", true);
                    log.WriteLine(dados[0] + ";" +dados[1] + ";Login;" + DateTime.Now);
                    ler.Close();
                    log.Close();
                    break;
                }
            
            }
        }
        static void LogoutSuperior(string email)
        {
            StreamReader ler = new StreamReader("CadUsuario.csv");
            string linha;
            while((linha=ler.ReadLine())!=null)
            {
                string[] dados = linha.Split(';');
                if (dados[1]==email)
                {
                    if (!File.Exists("Superior.csv"))
                    {
                        StreamWriter criarquivo = new StreamWriter("Superior.csv", true);
                        criarquivo.WriteLine("Nome do Usuário;Email; Login ou Logout; Data do Login/Logout");
                        criarquivo.Close();
                    }
                    StreamWriter superior = new StreamWriter("Superior.csv", true);
                    superior.WriteLine(dados[0] + ";" +dados[1] + ";Logout;" + DateTime.Now);
                    superior.Close();
                    if (!File.Exists("LogSistema.csv"))
                    {
                        StreamWriter criarquivo = new StreamWriter("LogSistema.csv", true);
                        criarquivo.WriteLine("Nome do Usuário;Email;Login ou Logout;Data do Login/Logout");
                        criarquivo.Close();
                    }
                    StreamWriter log = new StreamWriter("LogSistema.csv", true);
                    log.WriteLine(dados[0] + ";" +dados[1] + ";Login;" + DateTime.Now);
                    log.Close();
                    ler.Close();
                    break;
                }
            
            }
        }
    }
}