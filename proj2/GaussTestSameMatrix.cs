using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Gauss
{

    class GaussTestSameMatrix
    {
        public static string FILENAME = "deafult_file_name";

        static private MyMatrix<MyType> mtA, mtX;
        static private MyMatrix<double> dA, dX;
        static private MyMatrix<float> fA, fX;

        public static void Run(int size, MultiThreading multiThreadingSetting) 
        {
            Run(size, multiThreadingSetting, size.ToString() + "_" + multiThreadingSetting.ToString());
        }

        public static void Run(int size, MultiThreading multiThreadingSetting, string filename)
        {
            FILENAME = filename + ".txt";

            // Zapis daty i godziny rozpoczęcia badania
            WriteOperationLog(multiThreadingSetting);

            // Tworzenie macierzy 3 typów
            mtA = new MyMatrix<MyType>(Randomizer<MyType>.GenerateMatrix(size));
            mtX = new MyMatrix<MyType>(Randomizer<MyType>.GenerateVector(size));

            Serializer.PackMatrix(mtA, filename);
            Serializer.PackVector(mtX, filename);

            dA = new MyMatrix<double>(size, size);
            dX = new MyMatrix<double>(size, 1);

            fA = new MyMatrix<float>(size, size);
            fX = new MyMatrix<float>(size, 1);

            // Wypełnianie macierzy tymi samymi danymi 
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    dA.Data[i][j] = (double)mtA.Data[i][j];
                    fA.Data[i][j] = (float)dA.Data[i][j];
                }

                dX.Data[i][0] = (double)mtX.Data[i][0];
                fX.Data[i][0] = (float)dX.Data[i][0];
            }



            // Definiowanie testów w threadach

            // Uruchamianie Gaussa w 6 wariantach na raz
            Thread[] tOnWithoutMyType = new Thread[6] 
            {
                
                 new Thread(() =>GaussTestSpecific<float>.Run(fA, fX, Choice.NONE)),
                 new Thread(() =>GaussTestSpecific<float>.Run(fA, fX, Choice.PARTIAL)),
                 new Thread(() =>GaussTestSpecific<float>.Run(fA, fX, Choice.FULL)),

                 new Thread(() =>GaussTestSpecific<double>.Run(dA, dX, Choice.NONE)),
                 new Thread(() =>GaussTestSpecific<double>.Run(dA, dX, Choice.PARTIAL)),
                 new Thread(() =>GaussTestSpecific<double>.Run(dA, dX, Choice.FULL)),
            };

            // Uruchamianie Gaussa w 3 wariantach MyType na raz
            Thread tMyTypeNONE = new Thread(() => GaussTestSpecific<MyType>.Run(mtA, mtX, Choice.NONE));
            Thread tMyTypePARTIAL = new Thread(() => GaussTestSpecific<MyType>.Run(mtA, mtX, Choice.PARTIAL));
            Thread tMyTypeFULL = new Thread(() => GaussTestSpecific<MyType>.Run(mtA, mtX, Choice.FULL));

            // Uruchamianie Gaussa w 9 wariantach po kolei
            Thread tOffWithMyType = new Thread(() =>
            {
                GaussTestSpecific<float>.Run(fA, fX, Choice.NONE);
                GaussTestSpecific<float>.Run(fA, fX, Choice.PARTIAL);
                GaussTestSpecific<float>.Run(fA, fX, Choice.FULL);

                GaussTestSpecific<double>.Run(dA, dX, Choice.NONE);
                GaussTestSpecific<double>.Run(dA, dX, Choice.PARTIAL);
                GaussTestSpecific<double>.Run(dA, dX, Choice.FULL);

                GaussTestSpecific<MyType>.Run(mtA, mtX, Choice.NONE);
                GaussTestSpecific<MyType>.Run(mtA, mtX, Choice.PARTIAL);
                GaussTestSpecific<MyType>.Run(mtA, mtX, Choice.FULL);
            });

            // Uruchamianie Gaussa w 6 wariantach po kolei
            Thread tOffWithoutMyType = new Thread(() =>
            {
                GaussTestSpecific<float>.Run(fA, fX, Choice.NONE);
                GaussTestSpecific<float>.Run(fA, fX, Choice.PARTIAL);
                GaussTestSpecific<float>.Run(fA, fX, Choice.FULL);

                GaussTestSpecific<double>.Run(dA, dX, Choice.NONE);
                GaussTestSpecific<double>.Run(dA, dX, Choice.PARTIAL);
                GaussTestSpecific<double>.Run(dA, dX, Choice.FULL);
            });

            // Nastawienie timera liczącego całkowity czas wszystkich funkcji
            Stopwatch stopWatch = new Stopwatch();
            Thread timerStopper = new Thread(() =>
            {
                while(true)
                {
                    if (!(anyThreadIsAlive(tOnWithoutMyType) || tMyTypeNONE.IsAlive || tMyTypePARTIAL.IsAlive || tMyTypeFULL.IsAlive || tMyTypeFULL.IsAlive || tOffWithMyType.IsAlive || tOffWithoutMyType.IsAlive))
                    {
                        stopWatch.Stop();
                        WriteWholeOperationTimer(stopWatch);
                        Console.Beep();
                        Console.Beep();
                        Console.Beep();
                        break;
                    }
                    Thread.Sleep(100);
                }
            });



            //Uruchamienie testów w threadach
            stopWatch.Start();

            switch (multiThreadingSetting) 
            {
                case MultiThreading.ON_WITH_MY_TYPE:
                    tOffWithoutMyType.Start();
                    tMyTypeFULL.Start();
                    tMyTypeNONE.Start();
                    tMyTypePARTIAL.Start();
                    break;

                case MultiThreading.ON_WITHOUT_MY_TYPE:
                    foreach (Thread thread in tOnWithoutMyType)
                    {
                        thread.Start();
                    }
                    break;

                case MultiThreading.OFF_WITH_MY_TYPE:
                    tOffWithMyType.Start();
                    break;

                case MultiThreading.OFF_WITHOUT_MY_TYPE:
                    tOffWithoutMyType.Start();
                    break;
            }

            timerStopper.Start();

        }


        


        private static void WriteOperationLog(MultiThreading mt)
        {
            StreamWriter sw = File.AppendText(FILENAME);
            sw.WriteLine(DateTime.Now.ToString("F"));
            sw.WriteLine("Multithreading:\t" + mt);
            sw.Close();
        }
        private static void WriteSeparator()
        {
            StreamWriter sw = File.AppendText(FILENAME);
            sw.WriteLine("----------------------------------------------");
            sw.Close();
        }
        private static void WriteWholeOperationTimer(Stopwatch stopWatch)
        {
            Console.WriteLine("-- ZAPISUJĘ CZAS WSZYSTKICH OPERACJI ---");
            StreamWriter sw = File.AppendText(FILENAME);
            
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            sw.Write("--- ŁĄCZNY CZAS WSZYSTKICH OPERACJI: " + elapsedTime + " ---\n\n\n");
            sw.Write(sw.NewLine);
            sw.Close();
        }
        private static bool anyThreadIsAlive(Thread[] th)
        {
            foreach (Thread thread in th)
            {
                if (thread.IsAlive)
                    return true;
            }
            return false;
            
        }


    }
}
