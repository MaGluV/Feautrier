using System;

namespace Feautrier
{
    class MainClass
    {
        public static void Main()
        {
            Vector TestVector1 = new Vector({1.2,-5.4,0.1,9.8});
            Vector TestVector2 = new Vector({3.5,4.4,3.6,-1.02});
            Vector ResultVectorP;
            Vector ResultVectorM;
            float ResultMul;
            Vector ResultVectorMtx; 
            Vector AnswerVectorP = new Vector({4.7,-1.,3.7,8.78});
            Vector TestVectorM = new Vector({-2.3,-9.8,-3.5,10.82});
            float AnswerMul = -29.196;
            Vector TestVectorMtx = new Vector({-11.582,21.6286,115.0366,45.1536});
            
            Matrix TestDiagMatrix = new Matrix({
            									{3.81,0,0,0},
            									{0,1.74,0,0},
            									{0,0,-10.22,0},
            									{0,0,0,0.98}
            									});
            Matrix TestMatrix1 = new Matrix({
            								{1.62,7.34,-9.91,13.6},
            								{8.19,-5.23,4.67,0.82},
            								{20.66,-3.21,15.84,0.17},
            								{6.54,3.21,1.02,-4.38}
            								});
            Matrix TestMatrix2 = new Matrix({
            								{-6.08,17.24,3.46,-5.24},
            								{-5.55,4.01,0.29,11.87},
            								{1.22,7.32,-0.66,-4.0},
            								{7.77,2.91,2.74,5.33}
            								});
            Matrix ResultMatrixP;
            Matrix ResultMatrixM;
            Matrix ResultMatrixMul;
            Matrix ResultDiagMatrixInv;
            Matrix ResultDiagMatrixInvGS;
            Matrix ResultDiagMatrixInvG;
            Matrix ResultMatrixInvGS;
            Matrix ResultMatrixInvG;
            Matrix AnswerMatrixP = new Matrix({
            									{-4.46,24.58,-6.45,8.36},
            									{2.64,-1.22,4.96,12.69},
            									{21.88,4.11,15.18,-3.83},
            									{14.31,6.12,3.76,0.95}
            									});
            Matrix AnswerMatrixM = new Matrix({
            									{7.7,-9.9,-13.37,18.84},
            									{13.74,-9.24,4.38,-11.05},
            									{19.44,-10.53,16.5,4.17},
            									{-1.23,0.3,-1.72,-9.71}
            									});
			Matrix AnswerMatrixMul = new Matrix({
            									{42.9952,24.397,51.5384,190.765},
            									{-8.6999,156.7939,25.9853,-119.3051},
            									{-87.1516,459.7498,60.5641,-208.815},
            									{-90.3669,120.3423,10.8849,-23.5923}
            									});
            Matrix AnswerDiagMatrixInv = new Matrix({
            										{0.26246719,0.,0.,0.},
            										{0.,0.57471264,0.,0.},
            										{0.,0.,-0.09784736,0.},
            										{0.,0.,0.,1.02040816},
            										});
            Matrix AnswerMatrixInv = new Matrix({
            									{0.02278631,0.09892974,-0.0206081,0.08847323},
            									{0.01862935,-0.19052145,0.06623171,0.02474679},
            									{-0.02639047,-0.16731147,0.10298418,-0.10926907},
            									{0.04153068,-0.03087466,0.0417513,-0.10351651}
            									});
            
            Console.WriteLine("Vector test");								
            ResultVectorP = TestVector1 + TestVector2;
            ResultVectorM = TestVector1 - TestVector2;
            ResultMul = TestVector1*TestVector2;
            ResultVectorMtx = TestMatrix1*TestVector2;
            if (ResultVectorP == AnswerVectorP)
            {
            	Console.WriteLine("Addition test was passed");
            }
            
            if (ResultVectorM == AnswerVectorM)
            {
            	Console.WriteLine("Subtraction test was passed");
            }
            
            if (ResultMul == AnswerMul)
            {
            	Console.WriteLine("Multiplication test was passed");
            }
            
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
            ResultMatrixMul = TestMatrix1*TestMatrix2;
            ResultDiagMatrixInv = TestDiagMatrix.DiagReverse();
            ResultDiagMatrixInvGS = TestDiagMatrix.GSReverse();
            ResultDiagMatrixInvG = TestDiagMatrix.GReverse();
            ResultMatrixInvGS = TestMatrix1.GSReverse();
            ResultMatrixInvG = TestMatrix1.GReverse();
            if (ResultMatrixP == AnswerMatrixP)
            {
            	Console.WriteLine("Addition test was passed");
            }
            
            if (ResultMatrixM == AnswerMatrixM)
            {
            	Console.WriteLine("Addition test was passed");
            }
            
            if (ResultMatrixMul == AnswerMatrixMul)
            {
            	Console.WriteLine("Multiplication test was passed");
            }
            Console.WriteLine("Diagonal Matrix Inversion Test:");
            Console.WriteLine("Diagonal Matrix Inversion Answer:");
            AnswerDiagMatrixInv.MtxPrint();
            Console.WriteLine("Diagonal Matrix Inversion Result");
            ResultDiagMatrixInv.MtxPrint();
            Console.WriteLine("Diagonal Matrix Inversion Result (Gauss-Seidel):");
            ResultDiagMatrixInvGS.MtxPrint();
            Console.WriteLine("Diagonal Matrix Inversion Result (Gauss):");
            ResultDiagMatrixInvG.MtxPrint();
            Console.WriteLine("Matrix Inversion Test:");
            Console.WriteLine("Matrix Inversion Answer:");
            AnswerMatrixInv.MtxPrint();
            Console.WriteLine("Matrix Inversion Result (Gauss-Seidel):");
            ResultMatrixInvGS.MtxPrint();
            Console.WriteLine("Matrix Inversion Result (Gauss):");
            ResultMatrixInvG.MtxPrint();
        }
    }
}
