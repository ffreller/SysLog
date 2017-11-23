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
            StreamWriter cadastro = new StreamWriter("CadUsuario.csv", true);
            cadastro.WriteLine(nome + ";" + email + ";" + encripSenha(senha));
            Console.WriteLine("Cadastro efetuado com sucesso!");   
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