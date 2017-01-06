using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataMining.Transport
{
    public class Transport
    {
        public int time;
        public double x;
        public double y;
        public string name;
    }
    public class Points
    {
        public double x;
        public double y;
    }
    class Program
    {
        public static object Сonsole { get; private set; }

        static void Main(string[] args)
        {
             List<Transport> list;
            list = new List<Transport>();
            string path = @"D:\\DataMiningProject\\transport\\train.txt";
            string line;
            System.IO.StreamReader file =new System.IO.StreamReader(path);
            int k = 0;
            while ((((line = file.ReadLine()) != null))&(k<=60000))
            {
                k++;
                Char delimiter = '\t';
                String[] substrings = line.Split(delimiter);
                Transport transport = new Transport();

                //  int timestamp = Convert.ToInt32(substrings[0]);
                // transport.time = new DateTime(1970, 1, 1).AddSeconds(timestamp);
                //Console.WriteLine(transport.time);

                transport.time = Convert.ToInt32(substrings[0]);
            
                transport.x = Convert.ToDouble(substrings[1].Replace(".", ","));

                transport.y = Convert.ToDouble( substrings[2].Replace(".", ","));
                    transport.name = substrings[3].ToString();
                    //Console.WriteLine(transport.name);
                list.Add(transport);

            }
           
            file.Close();

            //Разделили на отрезки, которые будем анализировать
            double start = 11038.08464497;
            double end = 283.08479678;
            int n = 27;
            double[] X = new double[n];
            X[0] = 11038.08464497;
            X[n-1] = end;
            for (int i=1;i<n-1; i++)
            {
                X[i] = X[i - 1] + (end - start) / (n-1) ;
                Console.WriteLine(X[i]);

            }
          

            //Весь перечень транспортных средств
            List<string> TransportId;
            TransportId = new List<string>();//k сколько точек
            int check = -1;
            for (int i = 0; i<k;i++)
            {
                check = -1;
                foreach (string id in TransportId)
                {
                    if (list[i].name == id)
                         check = 0;
                 }
                if (check!=0)
                        TransportId.Add(list[i].name);
                
            }
            foreach (string id in TransportId)
                Console.WriteLine(id );

            List<Points> BestPoints;
            BestPoints = new List<Points>();

            //nкол-во промежутков или n+1

            for (int i = 1; i < n; i++)
            {
                Console.WriteLine(i);
                double x0 = X[i-1];
                double x1 = X[i ];
                foreach (string id in TransportId)
                {
                    List<Transport> transportToIntarval;
                    transportToIntarval = new List<Transport>();

                   
                    
                    for (int j = 1; j <=k; j++)//все координаты
                    {
                        if (list[j-1].name == id)//транспорт с нужным именем
                        {
                            if ((list[j-1].x<= x0) && (list[j-1 ].x >= x1)) //проверка на принадлежность промежутка
                            {
                                transportToIntarval.Add(list[j-1]); //все точки конкретного транспортного средства в данном интервале (x[i]x[i+1])
                            }
                        }

                    }
                    int count = transportToIntarval.Count; //кол-во точек
                    if (count != 0)
                    {
                        double[] speed = new double[count - 1];//=кол-ву интервалов
                        for (int j = 1; j < count - 1; j++)
                        {
                            double s = Math.Sqrt(Math.Pow((transportToIntarval[j].x - transportToIntarval[j - 1].x), 2) + Math.Pow((transportToIntarval[j].y - transportToIntarval[j - 1].y), 2));
                            int t = transportToIntarval[j].time - transportToIntarval[j - 1].time;
                            if ((t < 3600) & (s < 1000)) //                        
                            {
                                speed[j - 1] = s / t;

                            }
                            else speed[j - 1] = 10000;
                        }
                        int R = count/5; //cколько интервалов выбираем с наименьшей скоростью, берем серединуу интервала
                        for (int j = 0; j < R; j++)
                        {
                            double min_speed = speed.Min();// добавить ограничения на индексы типа последний нельзя
                            int indexMin = Array.IndexOf(speed, min_speed);
                            // Console.WriteLine(transportToIntarval[indexMin].x);
                            // Console.WriteLine(transportToIntarval[indexMin+1].x);
                            double x_mean = (transportToIntarval[indexMin].x - transportToIntarval[indexMin + 1].x) / 2;
                            double y_mean = (transportToIntarval[indexMin].y - transportToIntarval[indexMin + 1].y) / 2;
                            Points points = new Points();
                            points.x = transportToIntarval[indexMin + 1].x + x_mean;
                            points.y = transportToIntarval[indexMin + 1].y+y_mean;
                            speed[indexMin] = 100000;
                            BestPoints.Add(points);
                        }
                        Array.Clear(speed, 0, speed.Length);
                    }
                    for (int j = 0; j < transportToIntarval.Count; j++)
                        transportToIntarval.RemoveAt(j);
                    
                }

            }
            for (int i =1; i< 2;i++)
             Console.WriteLine(BestPoints.Count);

         
            using (System.IO.StreamWriter filey =
                       new System.IO.StreamWriter(@"D:\\DataMiningProject\\transport\\pointsy.txt"))
            {
                for (int i = 0; i < BestPoints.Count; i++)
                {

                    filey.Write(BestPoints[i].x.ToString().Replace(",","."));
                    filey.Write('\t');
                    filey.WriteLine(BestPoints[i].y.ToString().Replace(",", "."));

                }
            }
            Console.ReadKey(true);
        }
    }
}
