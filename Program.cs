using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkiingSingapore
{
    class Program
    {
        static int[][] mapMatrix;
        static int m=1000, n=1000;
        private static int x = 0;
        private static int y = 0;
        private static int currentLength = 0;
        private static int currentDrop = 0;
        static readonly int[][] emptyArray = new int[0][];
        private static int maxLength = 0;
        private static int maxDrop = 0;
        private static int maxX = 0;
        private static int maxY = 0;
        static List<Tuple<int, int>> currentList = new List<Tuple<int, int>>();
        static void Main(string[] args)
        {
            
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Data\map.txt");
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            mapMatrix = MatrixFromFile(file);
            for(int i=0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    currentLength = FindLengthRecursive(i, j);
                    if (currentLength > maxLength)
                    {
                        maxLength = currentLength;
                        maxX = i;
                        maxY = j;
                    }else if(currentLength == maxLength)
                    {
                        currentList = findPath(i, j);
                        List<Tuple<int, int>> maxList = findPath(maxX, maxY);
                        currentDrop = mapMatrix[currentList.ElementAt(0).Item1][currentList.ElementAt(0).Item2] - mapMatrix[currentList.ElementAt(currentList.Count() - 1).Item1][currentList.ElementAt(currentList.Count() - 1).Item2];
                        maxDrop = mapMatrix[maxList.ElementAt(0).Item1][maxList.ElementAt(0).Item2] - mapMatrix[maxList.ElementAt(maxList.Count() - 1).Item1][maxList.ElementAt(maxList.Count() - 1).Item2];
                        if (currentDrop > maxDrop)
                        {
                            maxDrop = currentDrop;
                            maxX = i;
                            maxY = j;
                        }
                    }
                }
            }
            Console.WriteLine("/n");

            Console.WriteLine("Max Length: " + maxLength);
            Console.WriteLine("Max Drop: " + maxDrop);
            Console.WriteLine("Path:");
        }

        private static int[][] MatrixFromFile(StreamReader file)
        {
            int[][] crMatrix = new int[1000][];
		    try {
			        String line = null;
                    int i = 0;
                    while ((line = file.ReadLine()) != null)
                    {
				      if(line.Split(' ').Length==1000)
                        {
					      int[] rowElements = new int[1000];
                            int j = 0;
					        foreach (String points in line.Split(' ')) {
						        rowElements[j]=Convert.ToInt32(points);
						        j++;
					        }
                        crMatrix[i]=rowElements;
					            i++;
				            }else {
					            continue;
				            }
			            }
			            file.Close();
		            } catch (Exception e) {
			            throw e;
		            }
		            return crMatrix;
	}

        private static int FindLengthRecursive(int a, int b)
        {
            int currentNum = mapMatrix[a][b];
            // don't search for values less than current max length
            if (currentNum < maxLength)
            {
                return 0;
            }
            int temp = 0;
            if (b > 0 && currentNum > mapMatrix[a][b - 1])
            { // up
                int newTemp = FindLengthRecursive(a, b - 1);
                if (newTemp > temp)
                {
                    temp = newTemp;
                }
            }
            if (b < (y - 1) && currentNum > mapMatrix[a][b + 1])
            { // down
                int newTemp = FindLengthRecursive(a, b + 1);
                if (newTemp > temp)
                {
                    temp = newTemp;
                }
            }
            if (a > 0 && currentNum > mapMatrix[a - 1][b])
            { // left
                int newTemp = FindLengthRecursive(a - 1, b);
                if (newTemp > temp)
                {
                    temp = newTemp;
                }
            }
            if (a < (x - 1) && currentNum > mapMatrix[a + 1][b])
            { // right
                int newTemp = FindLengthRecursive(a + 1, b);
                if (newTemp > temp)
                {
                    temp = newTemp;
                }
            }
            return temp + 1;
        }

        private static List<Tuple<int, int>> findPath(int a, int b)
        {
            int currentNum = mapMatrix[a][b];
            List<Tuple<int, int>> tempList = new List<Tuple<int, int>>();
            if (b > 0 && currentNum > mapMatrix[a][b - 1])
            { // up
                List<Tuple<int, int>> newList = findPath(a, b - 1);
                if (newList.Count() > tempList.Count())
                {
                    tempList = newList;
                }
            }
            if (b < (y - 1) && currentNum > mapMatrix[a][b + 1])
            { // down
                List<Tuple<int, int>> newList = findPath(a, b + 1);
                if (newList.Count() > tempList.Count())
                {
                    tempList = newList;
                }
            }
            if (a > 0 && currentNum > mapMatrix[a - 1][b])
            { // left
                List<Tuple<int, int>> newList = findPath(a - 1, b);
                if (newList.Count() > tempList.Count())
                {
                    tempList = newList;
                }
            }
            if (a < (x - 1) && currentNum > mapMatrix[a + 1][b])
            { // right
                List<Tuple<int, int>> newList = findPath(a + 1, b);
                if (newList.Count() > tempList.Count())
                {
                    tempList = newList;
                }
            }
            List<Tuple<int, int>> temp = new List<Tuple<int, int>>();
            temp.Add(new Tuple<int, int>(a, b));
            temp.AddRange(tempList);
            return temp;
        }

    }
}
