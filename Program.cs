using System;


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
            do
            {   
                Console.WriteLine("1-Cadastrar\n2-Logar\n3-Logout\n9-Sair");
                opcao = int.Parse(Console.ReadLine());
                switch(opcao)
                    {
                        case 1:
                        Cliente cliente = new Cliente();
                        cliente.Cadastrar();
                        break;

                        case 2:
                        
                        break;


                        case 3:
                        
                        break;

                        case 9:
                        return;
                        
                    }
            }
            while (opcao != 9);  
        }   
    }
}

