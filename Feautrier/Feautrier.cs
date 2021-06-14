using System;

namespace ColMod
{
    class Feautrier
    {
        private Vector[] SourceFunc;
        private Matrix[,] TauMu;
        private int t;
        private int m;

        public Feautrier(double[][] dtauL, double[][] dtauU, double[][] dtaus, double[] mu, double[] source)
        {
            this.t = dtaus[0].Length;
            this.m = dtaus.Length;
            int i;
            int k;
            double[,] mtx = new double[this.m, this.m];
            double[] v = new double[this.m];
            double dtau;
            this.SourceFunc = new Vector[this.t];
            this.TauMu = new Matrix[this.t, 3];

            /*for (i = 0; i < this.t; i++) Console.Write(string.Format("{0:f12} ", taus[i]));
            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);

            for (i = 0; i < this.m; i++) Console.Write(string.Format("{0:f8} ", mu[i]));
            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);*/

            for (i = 0; i < this.m; i++)
            {
                v[i] = 1;
                for (k = 0; k < this.m; k++)
                {
                    mtx[i, k] = 0;
                }
            }

            for (i = 0; i < this.t; i++)
            {
                this.SourceFunc[i] = new Vector(v);
                for (k = 0; k < 3; k++)
                {
                    this.TauMu[i, k] = new Matrix(mtx);
                }
            }
            double[] dt = new double[this.m];
            double[] dtu = new double[this.m];
            double[] dtl = new double[this.m];
            for (i = 0; i < this.m; i++) dt[i] = dtauU[i][0]; //dtaus[i][0];
            this.TauMu[0, 1] = this.NearBoundary(mu, dt);
            for (i = 0; i < this.m; i++) dt[i] = 1.0; //dtauU[i][0];
            for (i = 0; i < this.m; i++) dtu[i] = dtauU[i][0];
            this.TauMu[0, 2] = -this.LUMtxCreate(mu, dt); //dt
            this.SourceFunc[0] = 0.5 * SourceInit(source[0], dtu, dtu, dt);
            //this.SourceFunc[0] = 0.5 * source[0] * this.SourceFunc[0];

            for (i = 0; i < this.m; i++) dt[i] = dtauL[i][this.t-2]; //dtaus[i][this.t-1];
            this.TauMu[this.t - 1, 1] = this.FarBoundary(mu, dt);
            for (i = 0; i < this.m; i++) dt[i] = 1.0; //dtauL[i][this.t - 2];
            for (i = 0; i < this.m; i++) dtl[i] = dtauL[i][this.t-2];
            this.TauMu[this.t - 1, 0] = -this.LUMtxCreate(mu, dt); // dt
            this.SourceFunc[this.t - 1] = 0.5 * SourceInit(source[this.t - 1], dtl, dtl, dt);
            //this.SourceFunc[this.t - 1] = 0.5 * source[this.t - 1] * this.SourceFunc[this.t - 1];

            for (i = 1; i < this.t - 1; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    dt[j] = dtaus[j][i];    // <- особое внимание за этим выражением!
                    dtu[j] = dtauU[j][i];   // <- особое внимание за этим выражением!
                    dtl[j] = dtauL[j][i-1]; // <- особое внимание за этим выражением!
                }
                this.TauMu[i, 0] = -this.LUMtxCreate(mu, dtu); // dt, dtl
                //this.TauMu[i, 1] = new Matrix(this.m);
                this.TauMu[i, 1] = this.DiagMtxCreate(mu, dt, dtl, dtu);
                this.TauMu[i, 2] = -this.LUMtxCreate(mu, dtl); //dt, dtu
                this.SourceFunc[i] = SourceInit(source[i], dt, dtl, dtu);
                //this.SourceFunc[i] = source[i] * this.SourceFunc[i];
            }
        }
        
        private Vector SourceInit(double s, double[] dtau, double[] dtau1, double[] dtau2)
        {
        	double[] S = new double[this.m];
        	for (int i = 0; i < this.m; i++) S[i] = dtau[i]*dtau1[i]*dtau2[i]*s;
        	
        	return new Vector(S);
        }
        	

        private Matrix LUMtxCreate(double[] mu, double[] dtau) //, double[] dtau1)
        {
            double[,] mtx = new double[this.m, this.m];
            int i;
            int k;

            for (i = 0; i < this.m; i++)
            {
                for (k = 0; k < this.m; k++)
                {
                	mtx[i, k] = (i == k) ? Math.Pow(mu[i], 2.0)*dtau[i] : 0;
                    //mtx[i, k] = (i == k) ? Math.Pow(mu[i], 2.0) / (dtau[i]*dtau1[i]) : 0; 
                    // Math.Pow(mu[i]/dtau, 2.0) : 0;
                }
            }
            return new Matrix(mtx);
        }

        private Matrix DiagMtxCreate(double[] mu, double[] dtau, double[] dtau1, double[] dtau2)
        {
            double[,] mtx = new double[this.m, this.m];
            int i;
            int k;

            for (i = 0; i < this.m; i++)
            {
                for (k = 0; k < this.m; k++)
                {
                    mtx[i, k] = (i == k) ? dtau[i]*dtau1[i]*dtau2[i] + Math.Pow(mu[i], 2.0)*(dtau1[i] + dtau2[i]) : 0; 
                    // Math.Pow(mu[i]/dtau, 2.0) : 0;
                }
            }
            return new Matrix(mtx);
        }

        private Matrix NearBoundary(double[] mu, double[] dtau0)
        {
            double[,] mtx = new double[this.m, this.m];
            int i;
            int k;

            for (i = 0; i < this.m; i++)
            {
                for (k = 0; k < this.m; k++)
                {
                    mtx[i, k] = (i == k) ? 0.5*Math.Pow(dtau0[i], 2.0) + mu[i]*dtau0[i] + Math.Pow(mu[i], 2.0) : 0; //0.5*Math.Pow(dtau0, 2.0) + mu[i] * dtau0 + Math.Pow(mu[i], 2.0) : 0; //
                }
            }
            return new Matrix(mtx);
        }

        private Matrix FarBoundary(double[] mu, double[] dtauN)
        {
            double[,] mtx = new double[this.m, this.m];
            int i;
            int k;

            for (i = 0; i < this.m; i++)
            {
                for (k = 0; k < this.m; k++)
                {
                    mtx[i, k] = (i == k) ? 0.5*Math.Pow(dtauN[i], 2.0) + mu[i]*dtauN[i] + Math.Pow(mu[i], 2.0) : 0; //0.5 * Math.Pow(dtauN, 2.0) + mu[i] * dtauN + Math.Pow(mu[i], 2.0) : 0; //
                }
            }
            return new Matrix(mtx);
        }

        public Vector[] FeautrierCalc()
        {
            Matrix[] alpha = new Matrix[this.t - 1];
            Vector[] beta = new Vector[this.t - 1];
            Vector[] u = new Vector[this.t];
            int i;
            int n = this.t;
            Matrix denominator;
            Vector numerator;

            alpha[0] = -(this.TauMu[0, 1].DiagReverse() * this.TauMu[0, 2]);
            beta[0] = this.TauMu[0, 1].DiagReverse() * this.SourceFunc[0];
            for (i = 1; i < n - 1; i++)
            {
                //Console.Write(string.Format("MTX[{0:d}, {1:d}] = ", this.TauMu[i,0].GetHeight(), this.TauMu[i,0].GetWidth()));
                //Console.Write(Environment.NewLine + Environment.NewLine);
                //this.TauMu[i, 0].MtxPrint();
                denominator = this.TauMu[i, 0] * alpha[i - 1] + this.TauMu[i, 1];
                numerator = this.SourceFunc[i] - (this.TauMu[i, 0] * beta[i - 1]);
                alpha[i] = -(denominator.DiagReverse() * this.TauMu[i, 2]);
                beta[i] = denominator.DiagReverse() * numerator;
            }

            numerator = this.SourceFunc[n - 1] - (this.TauMu[n - 1, 0] * beta[n - 2]);
            denominator = this.TauMu[n - 1, 1] + (this.TauMu[n - 1, 0] * alpha[n - 2]);
            u[n - 1] = denominator.DiagReverse() * numerator;
            for (i = n - 2; i > -1; i--)
            {
                u[i] = (alpha[i] * u[i + 1]) + beta[i];
            }

            return u;
        }

        public double[] Flux(Vector[] u, double[] mu, double[] w, double[][] tau)
        {
            Vector[] v = new Vector[this.t];
            double[] Flux = new double[this.t];
            double[] vmas = new double[this.m];
            double[] umas1;
            double[] umas2;
            double[] umas3;
            int n = this.t;
            double sum;
            int i;
            int k;

            umas1 = u[0].GetValues();
            umas2 = u[1].GetValues();
            umas3 = u[2].GetValues();
            for (int j = 1; j < this.m; j++)
            {
                vmas[j] = (1.0 / (2.0 * (tau[j][1] - tau[j][0]))) * (4 * umas2[j] - 3 * umas1[j] - umas3[j]);
            }
            v[0] = new Vector(vmas);
            for (i = 1; i < n - 1; i++)
            {
                umas1 = u[i+1].GetValues();
                umas2 = u[i-1].GetValues();
                for (int j = 1; j < this.m; j++)
                {
                    vmas[j] = (1.0 / (2.0 * (tau[j][i+1] - tau[j][i]))) * (umas1[j] - umas2[j]);
                }
                v[i] = new Vector(vmas);
            }
            umas1 = u[n-3].GetValues();
            umas2 = u[n-2].GetValues();
            umas3 = u[n-1].GetValues();
            for (int j = 1; j < this.m; j++)
            {
                vmas[j] = (1.0 / (2.0 * (tau[j][n - 1] - tau[j][n - 2]))) * (3 * umas3[j] + umas1[j] - 4 * umas2[j]);
            }
            v[n-1] = new Vector(vmas);

            for (i = 0; i < n; i++)
            {
                sum = 0;
                vmas = v[i].GetValues();
                for (k = this.m / 2; k < this.m; k++)
                {
                    sum += w[k] * vmas[k] * Math.Pow(mu[k], 2.0);
                }
                Flux[i] = 4 * sum;
            }

            return Flux;
        }

        public double[] Flux_NK(Vector[] u, double[] mu, double[][] tau)
        {
            Vector[] v = new Vector[this.t];
            double[] Flux = new double[this.t];
            double[] vmas = new double[this.m];
            double[] umas1;
            double[] umas2;
            double[] umas3;
            int n = this.t;
            double sum;
            int i;
            int k;

            umas1 = u[0].GetValues();
            umas2 = u[1].GetValues();
            umas3 = u[2].GetValues();
            for (int j = 1; j < this.m; j++)
            {
                vmas[j] = (1.0 / (2.0 * (tau[j][1] - tau[j][0]))) * (4 * umas2[j] - 3 * umas1[j] - umas3[j]);
            }
            v[0] = new Vector(vmas);
            for (i = 1; i < n - 1; i++)
            {
                umas1 = u[i + 1].GetValues();
                umas2 = u[i - 1].GetValues();
                for (int j = 1; j < this.m; j++)
                {
                    vmas[j] = (1.0 / (2.0 * (tau[j][i + 1] - tau[j][i]))) * (umas1[j] - umas2[j]);
                }
                v[i] = new Vector(vmas);
            }
            umas1 = u[n - 3].GetValues();
            umas2 = u[n - 2].GetValues();
            umas3 = u[n - 1].GetValues();
            for (int j = 1; j < this.m; j++)
            {
                vmas[j] = (1.0 / (2.0 * (tau[j][n - 1] - tau[j][n - 2]))) * (3 * umas3[j] + umas1[j] - 4 * umas2[j]);
            }
            v[n-1] = new Vector(vmas);

            for (i = 0; i < n; i++)
            {
                sum = 0;
                vmas = v[i].GetValues();
                for (k = this.m / 2 + 1; k < this.m; k++)
                {
                    sum += (mu[k] - mu[k - 1]) * (vmas[k] + vmas[k - 1]) / 2.0;
                }
                Flux[i] = sum;
            }

            return Flux;
        }
    }
}
