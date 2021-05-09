using System;

namespace Feautrier
{
    class TestClass
    {
        public static void Main()
        {
            Vector TestVector1 = new Vector(new double [] { 1.2, -5.4, 0.1, 9.8 });
            Vector TestVector2 = new Vector(new double [] { 3.5, 4.4, 3.6, -1.02 });
            Vector ResultVectorP;
            Vector ResultVectorM;
            double ResultMul;
            Vector ResultVectorMtx;
            Vector AnswerVectorP = new Vector(new double[] { 4.7, -1.0, 3.7, 8.78 });
            Vector AnswerVectorM = new Vector(new double[] { -2.3, -9.8, -3.5, 10.82 });
            double AnswerMul = -29.196;
            Vector AnswerVectorMtx = new Vector(new double[] { -11.582, 21.6286, 115.0366, 45.1536 });

            Matrix TestDiagMatrix = new Matrix(new double [,]{
                                                {3.81,0,0,0},
                                                {0,1.74,0,0},
                                                {0,0,-10.22,0},
                                                {0,0,0,0.98}
                                                });
            Matrix TestMatrix1 = new Matrix(new double[,]{
                                            {1.62,7.34,-9.91,13.6},
                                            {8.19,-5.23,4.67,0.82},
                                            {20.66,-3.21,15.84,0.17},
                                            {6.54,3.21,1.02,-4.38}
                                            });
            Matrix TestMatrix2 = new Matrix(new double[,]{
                                            {-6.08,17.24,3.46,-5.24},
                                            {-5.55,4.01,0.29,11.87},
                                            {1.22,7.32,-0.66,-4.0},
                                            {7.77,2.91,2.74,5.33}
                                            });
            /*Matrix TestMatrix3 = new Matrix(new double[,]{
                                            {1.62,0.34,-0.91,0.6},
                                            {0.19,-5.23,0.67,0.82},
                                            {0.66,-0.21,15.84,0.17},
                                            {0.54,0.21,0.02,-4.38}
                                            });*/
            Matrix ResultMatrixP;
            Matrix ResultMatrixM;
            Matrix ResultMatrixMul;
            Matrix ResultDiagMatrixInv;
            Matrix ResultDiagMatrixInvGS;
            Matrix ResultDiagMatrixInvG = null;
            Matrix ResultMatrixInvGS;
            Matrix ResultMatrixInvG;
            Matrix AnswerMatrixP = new Matrix(new double[,]{
                                                {-4.46,24.58,-6.45,8.36},
                                                {2.64,-1.22,4.96,12.69},
                                                {21.88,4.11,15.18,-3.83},
                                                {14.31,6.12,3.76,0.95}
                                                });
            Matrix AnswerMatrixM = new Matrix(new double[,]{
                                                {7.7,-9.9,-13.37,18.84},
                                                {13.74,-9.24,4.38,-11.05},
                                                {19.44,-10.53,16.5,4.17},
                                                {-1.23,0.3,-1.72,-9.71}
                                                });
            Matrix AnswerMatrixMul = new Matrix(new double[,]{
                                                {42.9952,24.397,51.5384,190.765},
                                                {-8.6999,156.7939,25.9853,-119.3051},
                                                {-87.1516,459.7498,60.5641,-208.815},
                                                {-90.3669,120.3423,10.8849,-23.5923}
                                                });
            Matrix AnswerDiagMatrixInv = new Matrix(new double[,]{
                                                    {0.26246719,0.0,0.0,0.0},
                                                    {0.0,0.57471264,0.0,0.0},
                                                    {0.0,0.0,-0.09784736,0.0},
                                                    {0.0,0.0,0.0,1.02040816},
                                                    });
            Matrix AnswerMatrixInv = new Matrix(new double[,]{
                                                {0.02278631,0.09892974,-0.0206081,0.08847323},
                                                {0.01862935,-0.19052145,0.06623171,0.02474679},
                                                {-0.02639047,-0.16731147,0.10298418,-0.10926907},
                                                {0.04153068,-0.03087466,0.0417513,-0.10351651}
                                                });

            Console.WriteLine("Vector test");
            ResultVectorP = TestVector1 + TestVector2;
            ResultVectorM = TestVector1 - TestVector2;
            ResultMul = TestVector1 * TestVector2;
            ResultVectorMtx = TestMatrix1 * TestVector2;

            Console.WriteLine("Addition test");
            Console.Write(Environment.NewLine);
            ResultVectorP.VecPrint();
            Console.Write(Environment.NewLine);
            AnswerVectorP.VecPrint();
            Console.Write(Environment.NewLine + Environment.NewLine);
            if (ResultVectorP == AnswerVectorP)
            {
                Console.WriteLine("Addition test was passed");
                Console.Write(Environment.NewLine);
            }

            Console.WriteLine("Subtraction test");
            Console.Write(Environment.NewLine);
            ResultVectorM.VecPrint();
            Console.Write(Environment.NewLine);
            AnswerVectorM.VecPrint();
            Console.Write(Environment.NewLine + Environment.NewLine);
            if (ResultVectorM == AnswerVectorM)
            {
                Console.WriteLine("Subtraction test was passed");
                Console.Write(Environment.NewLine);
            }

            Console.WriteLine("Multiplication test");
            Console.Write(Environment.NewLine);
            Console.WriteLine(string.Format("{0}  {1}", ResultMul, AnswerMul));
            Console.Write(Environment.NewLine + Environment.NewLine);
            if (ResultMul == AnswerMul)
            {
                Console.WriteLine("Multiplication test was passed");
                Console.Write(Environment.NewLine);
            }

            Console.WriteLine("Matrix Multiplication test");
            Console.Write(Environment.NewLine);
            ResultVectorMtx.VecPrint();
            Console.Write(Environment.NewLine);
            AnswerVectorMtx.VecPrint();
            Console.Write(Environment.NewLine + Environment.NewLine);
            if (ResultVectorMtx == AnswerVectorMtx)
            {
                Console.WriteLine("Matrix Multiplication test was passed");
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Matrix test");
            ResultMatrixP = TestMatrix1 + TestMatrix2;
            ResultMatrixM = TestMatrix1 - TestMatrix2;
            ResultMatrixMul = TestMatrix1 * TestMatrix2;
            ResultDiagMatrixInv = TestDiagMatrix.DiagReverse();
            ResultDiagMatrixInvGS = TestDiagMatrix.GSReverse();
            try
            {
                ResultDiagMatrixInvG = TestDiagMatrix.GReverse();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
            ResultMatrixInvGS = TestMatrix1.GSReverse();
            ResultMatrixInvG = TestMatrix1.GReverse();

            Console.WriteLine("Addition test");
            Console.Write(Environment.NewLine);
            ResultMatrixP.MtxPrint();
            Console.Write(Environment.NewLine);
            AnswerMatrixP.MtxPrint();
            Console.Write(Environment.NewLine + Environment.NewLine);
            if (ResultMatrixP == AnswerMatrixP)
            {
                Console.WriteLine("Addition test was passed");
                Console.Write(Environment.NewLine);
            }

            Console.WriteLine("Subtraction test");
            Console.Write(Environment.NewLine);
            ResultMatrixM.MtxPrint();
            Console.Write(Environment.NewLine);
            AnswerMatrixM.MtxPrint();
            Console.Write(Environment.NewLine + Environment.NewLine);
            if (ResultMatrixM == AnswerMatrixM)
            {
                Console.WriteLine("Subtraction test was passed");
                Console.Write(Environment.NewLine);
            }

            Console.WriteLine("Multiplication test");
            Console.Write(Environment.NewLine);
            ResultMatrixMul.MtxPrint();
            Console.Write(Environment.NewLine);
            AnswerMatrixMul.MtxPrint();
            Console.Write(Environment.NewLine + Environment.NewLine);
            if (ResultMatrixMul == AnswerMatrixMul)
            {
                Console.WriteLine("Multiplication test was passed");
            }
            Console.WriteLine("Diagonal Matrix Inversion Test:");
            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);
            Console.WriteLine("Diagonal Matrix Inversion Answer:");
            Console.Write(Environment.NewLine);
            AnswerDiagMatrixInv.MtxPrint();
            Console.Write(Environment.NewLine);
            Console.WriteLine("Diagonal Matrix Inversion Result");
            Console.Write(Environment.NewLine);
            ResultDiagMatrixInv.MtxPrint();
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Diagonal Matrix Inversion Result (Gauss-Seidel):");
            Console.Write(Environment.NewLine);
            ResultDiagMatrixInvGS.MtxPrint();
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Diagonal Matrix Inversion Result (Gauss):");
            Console.Write(Environment.NewLine);
            try
            {
                ResultDiagMatrixInvG.MtxPrint();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Matrix Inversion Test:");
            Console.Write(Environment.NewLine);
            Console.WriteLine("Matrix Inversion Answer:");
            Console.Write(Environment.NewLine);
            AnswerMatrixInv.MtxPrint();
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Matrix Inversion Result (Gauss):");
            Console.Write(Environment.NewLine);
            ResultMatrixInvG.MtxPrint();
            Console.WriteLine("Matrix Inversion Result (Gauss-Seidel):");
            ResultMatrixInvGS.MtxPrint();
        }
    }
}

/*double[] testvec = ResultVectorMtx.GetValues();
            for (int i = 0; i < ResultVectorMtx.GetSize(); i++)
            {
                Console.Write(string.Format("{0} ", testvec[i]));
            }
            
*/