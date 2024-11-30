using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Enrutamiento_VLSM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] elevacionOcteto = { 1, 2, 4, 8, 16, 32, 64, 128 };
            int[] cuartoOcteto = { 0, 0, 0, 0, 0, 0, 0, 0 };

            //Console.WriteLine("---Calculo enrutamiento VLSM---");
            //Console.Write("Ingrese la ip, debe estar separada con . : ");
            //string ipIngresada = Console.ReadLine();

            //string[] arrayIp = ipIngresada.Split('.');

            //int[] ipConvertida = ConvertirIP(arrayIp);

            Console.Write("Ingrese cantidad de sub-redes a manejar: ");
            int contadorSubRed = Int32.Parse(Console.ReadLine());
            int contadorSubRed2 = contadorSubRed;

            //Manejo de sub-redes
            while (contadorSubRed > 0)
            {   
                Console.Write("\nIngrese #Host: ");
                int numeroHost= Int32.Parse(Console.ReadLine());

                for(int i = 0; i < elevacionOcteto.Length; i++)
                {
                    //Calculo para la nueva mascara de red
                    int acumuladoIpv4 =+ elevacionOcteto[i]-1;

                    if (elevacionOcteto[i]-2 > numeroHost)
                    {
                        Console.WriteLine($"\nHost solicitados: {numeroHost} ");
                        Console.WriteLine($"Host ecnontrados: {elevacionOcteto[i] - 2} ");
                        Console.WriteLine($"Octeto 2^{i} = {elevacionOcteto[i]}");

                        //Enciende en la posicion de las tablas de la direccion IPV4
                        int contadorEncendido = elevacionOcteto.Length - i;
                        for (int j=0; j<contadorEncendido;j++)
                        {
                            cuartoOcteto[j] = 1;
                        }

                        Console.Write("\nNueva octeto de mascara de red: ");
                        foreach(int j in cuartoOcteto) { Console.Write(j); }
                        Console.WriteLine($"\nNueva mascara de red: 255.255.255.{255-acumuladoIpv4}");
                        Console.WriteLine($"El salto de red es de: {256-(255-acumuladoIpv4)}");
                        Console.WriteLine();

                        i = 7;

                    }
                    
                }

                contadorSubRed--;
            }
            
            Console.ReadKey();
            
        }
        static public int[] ConvertirIP(String[] arrayIp)
        {
            int[] partesIP = new int[arrayIp.Length];


            for (int i = 0; i < arrayIp.Length; i++)
            {
                partesIP[i] = Int32.Parse(arrayIp[i]);
            }
            return partesIP;
        }


    }
}
