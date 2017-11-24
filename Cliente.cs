using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SysLog
{
    public class Cliente
    {
        public delegate void Deleg1(string email);
        public event Deleg1 Loginsup;
        
        public delegate void Deleg2(string email);
        public event Deleg2 Logoutsup;

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
                        this.Loginsup(email);
                    }
                    
                    else
                    {
                        Console.WriteLine("Senha incorreta");
                        return;
                    }
                }
            }
            ler.Close();    
            return;
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
                    Console.WriteLine("Logout concluído com sucesso!");
                    this.Logoutsup(email);
                }
            }
            ler.Close();
            return;
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
    }
}