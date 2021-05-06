using System;
namespace Feautrier
{
    public class Matrix
    {	
    	private float[,] mtx;
    	private int n;
    	private int m;
    	
        public Matrix(float[,] Mtx)
        {
        	this.mtx = Mtx;
        	this.n = Mtx.GetLength(0);
        	this.m = Mtx.GetLength(1);
        }
        
        public Matrix(int nsize)
        {
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
        
        public GetVaues()
        {
        	return this.mtx;
        }
        
        public GetHeight()
        {
        	return this.n;
        }
        
        public GetWidth()
        {
        	return this.m;
        }
        
        public static Matrix operator -(Matrix mtx)
        {
        	float[,] new_mtx = mtx.GetValues();
        	int new_n = mtx.GetHeight();
        	int new_m = mtx.GetWigth();
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
        	float[,] new_mtx 
        	float[,] mas1 = mtx1.GetValues();
        	float[,] mas2 = mtx2.GetValues();
        	int new_n = mtx1.GetHeight();
        	int new_m = mtx1.GetWigth();
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
        
        public static Matrix operator -(matrix m1, Matrix m2) => m1 + (-m2);
        
        public static Matrix operator *(Matrix mtx1, Matrix mtx2)
        {
        	float[,] new_mtx 
        	float[,] mas1 = mtx1.GetValues();
        	float[,] mas2 = mtx2.GetValues();
        	int new_n = mtx1.GetHeight();
        	int new_m = mtx2.GetWigth();
        	int l = mtx1.GetWigth();
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
        
        public static Matrix operator *(float a, Matrix mtx)
        {
        	float[,] new_mtx = mtx.GetValues();
        	int new_n = mtx.GetHeight();
        	int new_m = mtx.GetWigth();
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
        
        public static Matrix DiagReverse(Matrix mtx)
        {
        	float[,] new_mtx;
        	float[,] mas = mtx.GetValue();
        	int new_n = mtx.GetHeight();
        	int i;
        	for(i=0; i<new_n; i++)
        	{
        		new_mtx[i,i] = 1/mtx[i,i];
        	}
        	return new Matrix(new_mtx);
        }
        
        public static Matrix GSReverse(Matrix mtx)
        {
        	float[,] new_mtx;
        	float[,] mas = mtx.GetValue();
        	int new_n = mtx.GetHeight();
        	Matrix E = new Matrix(new_n);
        	float[,] e = E.GetValue();
        	float[,] x0 = E.GetValue();
        	float[,] x;
        	float coef0;
        	float coef;
        	float s = 1;
        	int i;
        	int j;
        	int k;
        	while (s > 0.000001)
        	{
        		s = 0;
	        	for(j=0; j<new_n; j++)
    	    	{
    	    		for(i=0; i<new_n; i++)
    	    		{
    	    			coef = 0;
    	    			coef0 = 0;
    	    			if (i > 0)
    	    			{
    	    				for(k=0; k<i; k++)
    	    				{
    	    					coef += mas[i,k]*x[k,j]/a[i,i];
    	    				}
    	    			}
    	    			if (i < new_n-1)
    	    			{
    	    				for(k=i+1; k<new_n; k++)
    	    				{
    	    					coef0 += mas[i,k]*x0[k,j]/a[i,i];
    	    				}
    	    			}
    	    			x[i,j] = e[i,j]/mas[i,i] - coef - coef0;
    	    			s += Math.Pow(x[i,j] - x0[i,j], 2.0);
    	    		}	
    	    	}
    	    	x0 = x;
    	    	s = Math.Pow(s, 0.5)
    	    }
    	    new_mtx = x;
        	return new Matrix(new_mtx);
        }
        
        public static Matrix GReverse(Matrix mtx)
        {
        	float[,] new_mtx;
        	float[,] mas = mtx.GetValue();
        	int new_n = mtx.GetHeight();
        	Matrix E = new Matrix(new_n);
        	float[,] e = E.GetValue();
        	float coef;
        	float max;
        	int index;
        	int i;
        	int j;
        	int k;
        	for(k=0; k<new_n-1; j++)
        	{
        		max = -1e10
        		for(i=k; i<new_n; i++)
        		{
        			if (max < mas[i,k])
        			{
        				max = mas[i,k];
        				index = i;
        			}
        		}
        		for(j=0; j<new_n; j++)
        		{
        			mas[index,j] -= mas[k,j];
        			mas[k,j] += mas[index,j];
        			mas[index,j] = mas[k,j] - mas[index,j];
        			e[index,j] -= e[k,j];
        			e[k,j] += e[index,j];
        			e[index,j] = e[k,j] - e[index,j];
        		}
        		for(i=k+1; i<new_n; i++)
        		{
        			coef = mas[i,k]/mas[k,k];
        			for(j=k+1; j<new_n; j++)
        			{
        				mas[i,j] -= coef*mas[k,j];
        				e[i,j] -= coef*e[k,j];
        			}
        		}
        	}
        	for(j=0; j<new_n; j++)
        	{
        		for(i=new_n-1; i>-1; i--)
        		{
        			coef = 0;
        			for(k=i+1; k<new_n; k++)
        			{
        				coef += mas[i,k]*new_mtx[k,j]/mas[i,i];
        			}
        			new_mtx[i,j] = b[i,j]/mas[i,i] - coef;
        		}
        	}
        	return new Matrix(new_mtx);
        }
    }
}
