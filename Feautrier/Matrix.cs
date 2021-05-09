using System;
namespace Feautrier
{
    public class Matrix
    {	
    	private double[,] mtx;
    	private int n;
    	private int m;
    	
        public Matrix(double[,] Mtx)
        {
        	this.mtx = Mtx;
        	this.n = Mtx.GetLength(0);
        	this.m = Mtx.GetLength(1);
        }
        
        public Matrix(int nsize)
        {
            this.mtx = new double[nsize, nsize];
        	this.n = nsize;
        	int i;
        	int j;
        	
        	for(i=0; i<this.n; i++)
        	{
        		for(j=0; j<this.n; j++)
        		{
        			if (i==j)
        			{
	        			this.mtx[i,j] = 1;
	        		}
	        		else
	        		{
	        			this.mtx[i,j] = 0;
	        		}
        		}
        	}
        }
        
        public double[,] GetValues()
        {
        	return this.mtx;
        }
        
        public int GetHeight()
        {
        	return this.n;
        }
        
        public int GetWidth()
        {
        	return this.m;
        }
        
        public static Matrix operator -(Matrix mtx)
        {
        	double[,] new_mtx = mtx.GetValues();
        	int new_n = mtx.GetHeight();
        	int new_m = mtx.GetWidth();
        	int i;
        	int j;
        	for(i=0; i<new_n; i++)
        	{
        		for(j=0; j<new_m; j++)
        		{
        			new_mtx[i,j] = -new_mtx[i,j];
        		}
        	}
        	return new Matrix(new_mtx);
        }
        
        public static Matrix operator +(Matrix mtx1, Matrix mtx2)
        { 
        	double[,] mas1 = mtx1.GetValues();
        	double[,] mas2 = mtx2.GetValues();
        	int new_n = mtx1.GetHeight();
        	int new_m = mtx1.GetWidth();
            double[,] new_mtx = new double[new_n, new_m];
            int i;
        	int j;
        	for(i=0; i<new_n; i++)
        	{
        		for(j=0; j<new_m; j++)
        		{
        			new_mtx[i,j] = mas1[i,j] + mas2[i,j];
        		}
        	}
        	return new Matrix(new_mtx);
        }
        
        public void MtxPrint()
        {
        	int i;
        	int j;
        	for(i=0; i<this.n; i++)
			{
	            for (j=0; j<this.m; j++)
	            {
	                Console.Write(string.Format("{0:f8} ", this.mtx[i, j]));
	            }
	            Console.Write(Environment.NewLine + Environment.NewLine);
	        }
        }
        
        public static bool operator == (Matrix m1, Matrix m2)
        {
        	bool boo = true;
        	int h = m1.GetHeight();
        	int w = m1.GetWidth();
        	double[,] mas1 = m1.GetValues();
        	double[,] mas2 = m2.GetValues();
        	int i;
        	int j;
        	for(i=0; i<h; i++)
        	{
        		for(j=0; j<w; j++)
        		boo &= (mas1[i,j] == mas2[i,j]);
        	}
        	return boo;
        }

        public override bool Equals(Object mtx)
        {
            if ((mtx == null) || !this.GetType().Equals(mtx.GetType()))
            {
                return false;
            }
            else
            {
                Matrix p = (Matrix)mtx;
                Matrix current_mtx = new Matrix(this.mtx); 
                return current_mtx == p;
            }
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            bool boo = false;
            int h = m1.GetHeight();
            int w = m1.GetWidth();
            double[,] mas1 = m1.GetValues();
            double[,] mas2 = m2.GetValues();
            int i;
            int j;
            for (i = 0; i < h; i++)
            {
                for (j = 0; j < w; j++)
                    boo = boo || (mas1[i, j] != mas2[i, j]);
            }
            return boo;
        }

        public static Matrix operator -(Matrix mtx1, Matrix mtx2)
        {
            double[,] mas1 = mtx1.GetValues();
            double[,] mas2 = mtx2.GetValues();
            int new_n = mtx1.GetHeight();
            int new_m = mtx1.GetWidth();
            double[,] new_mtx = new double[new_n, new_m];
            int i;
            int j;
            for (i = 0; i < new_n; i++)
            {
                for (j = 0; j < new_m; j++)
                {
                    new_mtx[i, j] = mas1[i, j] - mas2[i, j];
                }
            }
            return new Matrix(new_mtx);
        }

        public static Matrix operator *(Matrix mtx1, Matrix mtx2)
        {
        	double[,] mas1 = mtx1.GetValues();
        	double[,] mas2 = mtx2.GetValues();
        	int new_n = mtx1.GetHeight();
        	int new_m = mtx2.GetWidth();
            double[,] new_mtx = new double[new_n, new_m];
            int l = mtx1.GetWidth();
        	int i;
        	int j;
        	int k;
        	for(i=0; i<new_n; i++)
        	{
        		for(j=0; j<new_m; j++)
        		{
        			new_mtx[i,j] = 0;
        			for(k=0; k<l; k++)
        			{
        				new_mtx[i,j] += mas1[i,k]*mas2[k,j];
        			}
        		}
        	}
        	return new Matrix(new_mtx);
        }
        
        public static Matrix operator *(double a, Matrix mtx)
        {
        	double[,] new_mtx = mtx.GetValues();
        	int new_n = mtx.GetHeight();
        	int new_m = mtx.GetWidth();
        	int i;
        	int j;
        	for(i=0; i<new_n; i++)
        	{
        		for(j=0; j<new_m; j++)
        		{
        			new_mtx[i,j] *= a;
        		}
        	}
        	return new Matrix(new_mtx);
        }
        
        public Matrix DiagReverse()
        {
            double[,] new_mtx = new double[this.n,this.n];
        	int i;
        	for(i=0; i < this.n; i++)
        	{
        		new_mtx[i,i] = 1/this.mtx[i,i];
        	}
        	return new Matrix(new_mtx);
        }
        
        public Matrix GSReverse()
        {
        	Matrix E = new Matrix(this.n);
            double[,] mas = new double[this.n, this.n];
            double[,] e = E.GetValues();
        	double[,] x0 = new double[this.n, this.n];
        	double[,] x = new double[this.n, this.n];
        	double coef0;
        	double coef;
        	double s = 1;
        	int i;
        	int j;
        	int k;
            for (i = 0; i < this.n; i++)
            {
                for (j = 0; j < this.n; j++)
                {
                    mas[i, j] = this.mtx[i, j];
                    x0[i, j] = 0.1;
                }
            }

            while (s > 0.000001)
        	{
        		s = 0;
                Matrix testm = new Matrix(x0);
                testm.MtxPrint();
                Console.WriteLine("--------------");
	        	for(j=0; j<this.n; j++)
    	    	{
    	    		for(i=0; i<this.n; i++)
    	    		{
    	    			coef = 0;
    	    			coef0 = 0;
    	    			if (i > 0)
    	    			{
    	    				for(k=0; k<i; k++)
    	    				{
    	    					coef += mas[i,k]*x[k,j]/mas[i,i];
    	    				}
    	    			}
    	    			if (i < this.n-1)
    	    			{
    	    				for(k=i+1; k<this.n; k++)
    	    				{
    	    					coef0 += mas[i,k]*x0[k,j]/ mas[i,i];
    	    				}
    	    			}
    	    			x[i,j] = e[i,j]/ mas[i,i] - coef - coef0;
    	    			s += Math.Pow(x[i,j] - x0[i,j], 2.0);
    	    		}	
    	    	}
    	    	x0 = x;
                s = Math.Pow(s, 0.5);
    	    }
        	return new Matrix(x);
        }
        
        public Matrix GReverse()
        {
            double[,] mas = new double[this.n, this.n];
            double[,] new_mtx = new double[this.n, this.n];
        	Matrix E = new Matrix(this.n);
        	double[,] e = E.GetValues();
        	double coef;
        	double max;
        	int max_index;
        	int i;
        	int j;
        	int k;
            for (i=0; i < this.n; i++)
            {
                for (j = 0; j < this.n; j++)
                {
                    mas[i, j] = this.mtx[i, j];
                }
            }

        	for(k=0; k<(this.n-1); k++)
        	{
                max = -1e10;
                max_index = 0;
                for (i=k; i< this.n; i++)
        		{
        			if (max < mas[i,k])
        			{
        				max = mas[i,k];
        				max_index = i;
                    }
        		}
                

        		for(j=0; j< this.n; j++)
        		{
                    if (max_index != k)
                    {
                        mas[max_index, j] -= mas[k, j];
                        mas[k, j] += mas[max_index, j];
                        mas[max_index, j] = mas[k, j] - mas[max_index, j];
                        e[max_index, j] -= e[k, j];
                        e[k, j] += e[max_index, j];
                        e[max_index, j] = e[k, j] - e[max_index, j];
                    }
        		}

                for (i=0; i<this.n; i++)
                {
                    if (mas[i, i] == 0)
                    {
                        throw new ArgumentException("Diagonal elemets equal to 0!");
                    }
                }

        		for(i=k+1; i< this.n; i++)
        		{
        			coef = mas[i,k]/mas[k,k];

                    for (j = k; j < this.n; j++)
                    {
                        mas[i, j] -= coef * mas[k, j];
                    }
                    for (j = 0; j < this.n; j++)
                    {
                        e[i,j] -= coef*e[k,j];
        			}
                }
            }

        	for(j=0; j<this.n; j++)
        	{
        		for(i=this.n-1; i>-1; i--)
        		{
        			coef = 0;
        			for(k=i+1; k<this.n; k++)
        			{
        				coef += mas[i,k]*new_mtx[k,j]/mas[i,i];
        			}
        			new_mtx[i,j] = e[i,j]/mas[i,i] - coef;
                }
            }
        	return new Matrix(new_mtx);
        }
    }
}
