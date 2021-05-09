using System;
namespace Feautrier
{
    public class Vector
    {
        private double[] vec;
        private int n;

        public Vector(double[] Vec)
        {
            vec = Vec;
            n = Vec.Length;
        }

        public Vector(int n)
        {
            int i;
            for (i = 0; i < n; i++) { vec[i] = 0; }
        }

        public double[] GetValues()
        {
        	return this.vec;
        }
        
        public int GetSize()
        {
        	return this.n;
        }
        public void VecPrint()
        {
            int i;
            for (i = 0; i < this.n; i++)
            {
                Console.Write(string.Format("{0:f8} ", this.vec[i]));
            }
        }
        public static Vector operator -(Vector v)
        {
        	double[] new_vec = v.GetValues();
        	int m = v.GetSize();
        	int i;
        	for(i=0; i<m; i++){new_vec[i] = -new_vec[i];}
        	return new Vector(new_vec);
        }
        
        public static bool operator == (Vector v1, Vector v2)
        {
        	bool boo = true;
        	int m = v1.GetSize();
        	double[] mas1 = v1.GetValues();
        	double[] mas2 = v2.GetValues();
        	int i;
        	for(i=0; i<m; i++)
        	{
        		boo &= (mas1[i] == mas2[i]);
        	}
        	return boo;
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            bool boo = false;
            int m = v1.GetSize();
            double[] mas1 = v1.GetValues();
            double[] mas2 = v2.GetValues();
            int i;
            for (i = 0; i < m; i++)
            {
                boo = boo || (mas1[i] != mas2[i]);
            }
            return boo;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
        	double[] mas1 = v1.GetValues();
        	double[] mas2 = v2.GetValues();
        	int m = v1.GetSize();
            double[] new_vec = new double[m];
            int i;
        	for(i=0; i<m; i++){new_vec[i] = mas1[i] + mas2[i];}
        	return new Vector(new_vec);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            double[] mas1 = v1.GetValues();
            double[] mas2 = v2.GetValues();
            int m = v1.GetSize();
            double[] new_vec = new double[m];
            int i;
            for (i = 0; i < m; i++) { new_vec[i] = mas1[i] - mas2[i]; }
            return new Vector(new_vec);
        }

        public static double operator *(Vector v1, Vector v2)
        {
        	double mul=0;
        	double[] mas1 = v1.GetValues();
        	double[] mas2 = v2.GetValues();
        	int m = v1.GetSize();
        	int i;
        	for(i=0; i<m; i++){mul += mas1[i]*mas2[i];}
        	return mul;
        }
        
        public static Vector operator *(Matrix mtx, Vector v)
        {
        	
        	double[,] mas1 = mtx.GetValues();
        	double[] mas2 = v.GetValues();
        	int l = mtx.GetHeight();
        	int m = v.GetSize();
            double[] new_vec = new double[l];
            int i;
        	int j;
        	for(i=0; i<l; i++)
        	{
        		new_vec[i] = 0;
        		for(j=0; j<m; j++)
        		{
        			new_vec[i] += mas1[i,j]*mas2[j];
        		}
        	}
        	return new Vector(new_vec);
        }
        
        public static Vector operator *(double a, Vector v)
        {
        	double[] new_vec = v.GetValues();
        	int m = v.GetSize();
        	int i;
        	for(i=0; i<m; i++)
        	{
        		new_vec[i] *= a;
        	}
        	return new Vector(new_vec);
        }
    }
}
