using System;
namespace Feautrier
{
    public class Vector
    {
    	private float[] vec;
    	private int n;
    	
        public Vector(float[] Vec)
        {
        	vec = Vec;
        	n = Vec.Length;
        }
        
        public Vector(int n)
        {
        	int i;
        	for(i=0; i<n; i++){vec[i] = 0;}
        }
        
        public GetValues()
        {
        	return this.vec;
        }
        
        public GetSize()
        {
        	return this.n;
        }
        
        public static Vector operator -(Vector v)
        {
        	float[] new_vec = v.GetValues();
        	int m = v.GetSize();
        	int i;
        	for(i=0; i<m; i++){new_vec[i] = -new_vec[i];}
        	return new Vector(new_vec);
        }
        
        public static Vector operator +(Vector v1, Vector v2)
        {
        	float[] new_vec;
        	float[] mas1 = v1.GetValues();
        	float[] mas2 = v2.GetValues();
        	int m = v1.GetSize();
        	int i;
        	for(i=0; i<m; i++){new_vec[i] = mas1[i] + mas2[i];}
        	return new Vector(new_vec);
        }
        
        public static Vector operator -(Vector v1, Vector v2) => v1 + (-v2);
        
        public static float operator *(Vector v1, Vector v2)
        {
        	float mul=0;
        	float[] mas1 = v1.GetValues();
        	float[] mas2 = v2.GetValues();
        	int m = v1.GetSize();
        	int i;
        	for(i=0; i<m; i++){mul += mas1[i]*mas2[i];}
        	return mul;
        }
        
        public static Vector operator *(Matrix mtx, Vector v)
        {
        	float[] new_vec;
        	float[,] mas1 = mtx.GetValues();
        	float[] mas2 = v.GetValues();
        	int l = mtx.getHeight();
        	int m = v.GetSize();
        	int i;
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
    }
}
