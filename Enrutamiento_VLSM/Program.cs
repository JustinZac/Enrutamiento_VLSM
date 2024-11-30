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

            Console.WriteLine("---Calculo enrutamiento VLSM---");
            Console.Write("Ingrese la ip, debe estar separada con . : ");
            string ipIngresada = Console.ReadLine();

            string[] arrayIp = ipIngresada.Split('.');

            int[] ipConvertida = ConvertirIP(arrayIp);

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
            imprimirTabla(ipConvertida,contadorSubRed2, elevacionOcteto);
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
        public static void imprimirTabla(int[] ipConvertida, int contadorSubRed2, int[] elevacionOcteto )
        {
            int[] numeroHost = {20,15,10};
            Console.WriteLine();
            Console.WriteLine($"SubRed\tHost Solicitados\tHost encontrados\tDireccion de red\tPrefijo de red\tNueva Mascara\tPrimeraIp\tUltimaIp\tBroadcast");
            for (int i = 0; i < contadorSubRed2; i++)
            {
                Console.Write($"Linea: {i}\t");

                Console.Write($"{ImpresionNumeroHost(i,numeroHost)}\t\t\t");
                Console.Write($"{ImpresionHostEncontrados(numeroHost,elevacionOcteto,i)}\t\t");
                if (i == 0)
                {
                    Console.Write($"{ipConvertida[0]}.{ipConvertida[1]}.{ipConvertida[2]}.{ipConvertida[3]}\t\t");

                }
                else
                {
                    Console.Write($"{ipConvertida[0]}.{ipConvertida[1]}.{ipConvertida[2]}.{(ipConvertida[3] + SaltoDeRed(numeroHost, elevacionOcteto, i))}\t\t");
                }
                Console.Write($"/{PrefijoDeRed(elevacionOcteto,numeroHost,i)}\t\t");
                Console.Write($"255.255.255.{NuevMascaraDeRed(numeroHost,elevacionOcteto,i)}\t");

                if (i == 0)
                {
                    Console.Write($"{ipConvertida[0]}.{ipConvertida[1]}.{ipConvertida[2]}.{ipConvertida[3]+1}\t\t");
                }
                else
                {
                    Console.Write($"{ipConvertida[0]}.{ipConvertida[1]}.{ipConvertida[2]}.{(ipConvertida[3] + SaltoDeRed(numeroHost, elevacionOcteto, i)+1)}\t\t");
                }

                if (i == 0)
                {
                    Console.WriteLine($"{ipConvertida[0]}.{ipConvertida[1]}.{ipConvertida[2]}.{(ipConvertida[3] + CalculoSaltos(numeroHost, elevacionOcteto, i)-2 )}\t\t");

                }
                else
                {
                    Console.WriteLine($"{ipConvertida[0]}.{ipConvertida[1]}.{ipConvertida[2]}.{(ipConvertida[3] + SaltosAcumulados(numeroHost, elevacionOcteto, i-1) )}\t\t");
                }
               

                //Console.WriteLine($"\t{SaltoDeRed(numeroHost,elevacionOcteto,i)}");
            }
            //for (int j = 0; j < numeroHost.Length; j++) Console.Write(numeroHost[j]);

        }

        public static  int ImpresionNumeroHost(int i, int[] numeroHost)
        {
            for (int j = i; j < numeroHost.Length; j++)
            {
                
                return numeroHost[j];
            }
            return 0;
        }
        public static int ImpresionHostEncontrados(int[] numeroHost, int[] elevacionOcteto, int k)
        {
            //for(int j=k; j < numeroHost.Length; j++)
            //{
                for (int i = 0; i < elevacionOcteto.Length; i++)
                {
                    if ((elevacionOcteto[i] - 2) > numeroHost[k])
                    {
                        return elevacionOcteto[i] - 2;
                    }
                }
            //}

            return 0;
        }
        public static int SaltoDeRed(int[] numeroHost, int[] elevacionOcteto, int k)
        {
            int acumulado;
            for (int i = 0; i < elevacionOcteto.Length; i++)
            {
                acumulado =+ elevacionOcteto[i]-1;
                if ((elevacionOcteto[i] - 2) > numeroHost[k-1])
                {
                    return 256-(255 - acumulado); ;
                }
            }        
            return 0;
        }
        public static int PrefijoDeRed(int[] elevacionOcteto, int[] numeroHost, int k)
        {
            for(int i = 0; i < elevacionOcteto.Length; i++)
            {
                if(elevacionOcteto[i] - 2 > numeroHost[k])
                {
                    return 24 + (elevacionOcteto.Length-i);
                }
            }
            return 0;
        }

        public static int NuevMascaraDeRed(int[] numeroHost, int[] elevacionOcteto, int k)
        {
            int acumulado;
            for (int i = 0; i < elevacionOcteto.Length; i++)
            {
                acumulado = +elevacionOcteto[i] - 1;
                if ((elevacionOcteto[i] - 2) > numeroHost[k])
                {
                    return (255 - acumulado); ;
                }
            }
            return 0;
        }

        public static int CalculoSaltos(int[] numeroHost, int[] elevacionOcteto, int k)
        {
            int acumulado;
            for (int i = 0; i < elevacionOcteto.Length; i++)
            {
                acumulado = +elevacionOcteto[i] - 1;
                if ((elevacionOcteto[i] - 2) > numeroHost[k])
                {
                    return 256 - (255 - acumulado); ;
                }
            }
            return 0;
        }
        public static int SaltosAcumulados(int[] numeroHost, int[] elevacionOcteto, int k)
        {
            int acumulado=0;
            for (int i = 0; i < elevacionOcteto.Length; i++)
            {
                acumulado = acumulado +elevacionOcteto[i] - 1;
                if ((elevacionOcteto[i] - 2) > numeroHost[k])
                {
                    return 256 - (255 - acumulado); ;
                }
            }
            return 0;
        }


    }
}
