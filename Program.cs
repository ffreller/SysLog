using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SysLog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sistema Empresa");
            Console.WriteLine("");
            Console.WriteLine("Digite um das opções abaixo para seguir: ");
            int opcao;
             Cliente cliente = new Cliente();
            do
            {   
                Console.WriteLine("1-Cadastrar\n2-Logar\n3-Logout\n9-Sair");
                opcao = int.Parse(Console.ReadLine());
               
                switch(opcao)
                    {
                        case 1:
                        cliente.Cadastrar();
                        break;

                        case 2:
                        cliente.Loginsup += new Cliente.Deleg1(LoginReg); 
                        cliente.Logar();
                        break;

                        case 3:
                        cliente.Logoutsup += new Cliente.Deleg2(LogoutReg);
                        cliente.Sair();
                        break;

                        case 9:
                        return;
                        
                    }
            }
            while (opcao != 9);  
        }
        static void LoginReg(string email)
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
        static void LogoutReg(string email)
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
                    log.WriteLine(dados[0] + ";" +dados[1] + ";Logout;" + DateTime.Now);
                    log.Close();
                    ler.Close();
                    break;
                }
            
            }
        }   
    }
}

