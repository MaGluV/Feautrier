using System;

namespace Feautrier
{
    class Feautrier
    {
        private Vector[] SourceFunc;
        private Matrix[,] TauMu;
        private Vector[] j;
        int t;
        int m;
        
        public Feautrier(double[] taus, double[] mu, double[] source)
        {
        	this.t = taus.Length;
        	this.m = mu.Length;
        	int i;
        	double[,] mtx = new double[this.m, this.m];
        	double[] v = new double[this.m];
        	double dtau;
        	this.SourceFunc = new Vector[this.t]
        	this.TauMu = new Matrix[this.t, this.t]
        	this.j = new Vector[this.t]
        	
        	for(i=0; i<this.m; i++)
        	{
        		v[i] = 1;
        		for(j=0; j<this.m; j++)
        		{
        			mtx[i,j] = 0;
        		}
        	}
        	
        	for(i=0; i<this.t; i++)
        	{
        		this.SourceFunc[i] = new Matrix(v);
        		for(j=0; j<this.t; j++)
        		{
        			this.TauMu[i,j] = new Matrix(mtx);
        		}
        	}
        	
        	dtau = taus[1] - taus[0];
        	this.TauMu[0,0] = this.NearBoundary(mu, dtau);
        	this.TauMu[0,1] = - this.LUMtxCreate(mu, dtau);
        	this.SourceFunc[0] = 0.5*source[0]*this.SourceFunc[0];
        	dtau = taus[this.t-1] - taus[this.t-2];
        	this.TauMu[this.t-1,this.t-1] = this.FarBoundary(mu, dtau);
        	this.TauMu[this.t-1,this.t-2] = - this.LUMtxCreate(mu, dtau);
        	this.SourceFunc[this.t-1] = 0.5*source[this.t-1]*this.SourceFunc[this.t-1];
        	
        	for(i=1; i<this.t-1; i++)
        	{
        		dtau = taus[i] - taus[i-1];
        		this.TauMu[i,i-1] = - this.LUMtxCreate(mu, dtau);
        		this.TauMu[i,i] = new Matrix(this.mu);
        		this.TauMu[i,i] = this.TauMu[i,i] + 2.0*this.DiagMtxCreate(mu, dtau);
        		this.TauMu[i,i+1] = - this.LUMtxCreate(mu, dtau);
        		this.SourceFunc[i] = source[i]*this.SourceFunc[i];
        	}
        }
        
        private Matrix LUMtxCreate(double[] mu, double dtau)
        {
        	double[,] mtx = new double[this.m, this.m];
        	int i;
        	
        	for(i=0; i<this.m; i++)
        	{
        		for(i=0; i<this.m; i++)
        		{
	        		mtx[i,j] = 0;
	        	}
	        	mtx[i,i] = Math.Pow(mu[i]/dtau, 2.0);
	        }
	        return new Matrix(mtx);
        }
        
        private Matrix DiagMtxCreate(double[] mu, double dtau)
        {
        	double[,] mtx = new double[this.m, this.m];
        	int i;
        	
        	for(i=0; i<this.m; i++)
        	{
        		for(i=0; i<this.m; i++)
        		{
	        		mtx[i,j] = 0;
	        	}
	        	mtx[i,i] = Math.Pow(mu[i]/dtau, 2.0);
	        }
	        return new Matrix(mtx);
        }
        
        private Matrix NearBoundary(double[] mu, double dtau0)
        {
        	double[,] mtx = new double[this.m, this.m];
        	int i;
        	
        	for(i=0; i<this.m; i++)
        	{
        		for(i=0; i<this.m; i++)
        		{
	        		mtx[i,j] = 0;
	        	}
	        	mtx[i,i] = 0.5 + mu[i]/dtau0 + Math.Pow(mu[i]/dtau0, 2.0);
	        }
	        return new Matrix(mtx);
        }
        
        private Matrix FarBoundary(double[] mu, double dtauN)
        {
        	double[,] mtx = new double[this.m, this.m];
        	int i;
        	
        	for(i=0; i<this.m; i++)
        	{
        		for(i=0; i<this.m; i++)
        		{
	        		mtx[i,j] = 0;
	        	}
	        	mtx[i,i] = 0.5 + mu[i]/dtauN + Math.Pow(mu[i]/dtauN, 2.0);
	        }
	        return new Matrix(mtx);
        }
        
        public Vector[] FeautrierCalc()
        {
        	Matrix[] alpha = new Matrix[this.t-1];
        	Vector[] beta = new Vector[this.t-1];
        	Vector[] u = new Vector[this.t];
        	int i;
        	int n = this.t;
        	Matrix denominator;
        	Vector numerator;
        	
        	alpha[0] = - (this.TauMu[0,0].DiagReverse() * this.TauMu[0,1]);
        	beta[0] = this.TauMu[0,0].DiagReverse() * this.SourceFunc[0];
        	for(i=1; i<n-1; i++)
        	{
        		denominator = this.TauMu[i,i-1]*alpha[i-1] + this.TauMu[i,i];
        		numerator = this.SourceFunc[i] - (this.TauMu[i, i-1]*beta[i-1]);
        		alpha[i] = - (denominator.DiagReverse() * this.TauMu[i,i+1]);
        		beta[i] = denominator.DiagReverse() * numerator;
        	}
        	
        	numerator = this.SourceFunc[n-1] - (this.SourceFunc[n-1,n-2]*beta[n-2]);
			denominator = this.SourceFunc[n-1,n-1] + (this.SourceFunc[n-1,n-2]*alpha[n-2]);
			u[n-1] = denominator.DiagReverse() * numerator;
        	for(i=n-2; i>-1; i--)
        	{
        		u[i] = (alpha[i]*u[i+1]) + beta[i];
        	}
        	
        	return u;
        }
        
        public double[] Flux(Vector[] u, double[] mu, double[] w, double[] tau)
        {
        	Vector[] v = new Vector[this.t];
        	double[] Flux = new double[this.t];
        	double[] vmas = new double[this.m];
        	int n = this.t;
        	double sum;
        	int i;
        	int j;
        	
        	v[0] = (1.0/(2.0*(tau[1] - tau[0])))*(4*u[1] - 3*u[0] - u[2]);
        	for(i=1, i<n-1; i++)
        	{
        		v[i] = (1.0/(2.0*(tau[i+1] - tau[i])))*(u[i+1] - u[i-1]);
        	}
        	v[n-1] = (1.0/(2.0*(tau[n-1] - tau[n-2])))*(3*u[n-1] + u[n-3] - 4*u[n-2]);
        	
        	for(i=0; i<n; i++)
        	{
        		sum = 0;
        		vmas = v[i].GetValues();
        		for(j=0; j<this.m; j++)
        		{
        			sum += w[j]*vmas[j]*Math.Pow(mu,2.0);
        		}
        		Flux[i] = 4*sum;
        	}
        	
        	return Flux;
        }
    }
}
