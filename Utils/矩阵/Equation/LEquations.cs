using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager
{
    //C# 中线性方程组类的设计     
    /// <summary> 
    /// 求解线性方程组的类LEquations     
    /// </summary> 
    public class LEquations
    {

        #region 线性方程组类的构造函数及一般方法
        private Matrix mtxLECoef;  // 系数矩阵        
        private Matrix mtxLEConst;  // 常数矩阵         
        /// <summary>         /// 基本构造函数         /// </summary> 
        public LEquations()
        { }
        /// <summary> 
        /// 指定系数和常数的构造函数       
        /// </summary> 
        /// <param name="mtxCoef">指定的系数矩阵</param>         
        /// <param name="mtxConst">指定的常数矩阵</param>         
        public LEquations(Matrix mtxCoef, Matrix mtxConst)
        {
            Init(mtxCoef, mtxConst);
        }
        /// <summary>         /// 初始化函数        /// </summary> 
        /// <param name="mtxCoef">指定的系数矩阵</param>         
        /// <param name="mtxConst">指定的常数矩阵</param> 
        public bool Init(Matrix mtxCoef, Matrix mtxConst)
        {
            if (mtxCoef.Row != mtxConst.Row)
                return false;
            mtxLECoef = Matrix.Clone(mtxCoef);
            mtxLEConst = Matrix.Clone(mtxConst);
            return true;
        }
        /// <summary>         /// 获取系数矩阵         /// </summary> 
        /// <returns> Matrix 型，返回系数矩阵</returns>        
        public Matrix GetCoefMatrix()
        {
            return mtxLECoef;
        }
        /// <summary>         /// 获取常数矩阵         /// </summary> 
        public Matrix GetConstMatrix()
        {
            return mtxLEConst;
        }
        /// <summary>         /// 获取方程个数         /// </summary> 
        public int GetNumEquations()
        {
            return GetCoefMatrix().Row;
        }
        /// <summary> 
        /// 获取未知数个数         /// </summary> 
        public int GetNumUnknowns()
        {
            return GetCoefMatrix().Col;
        }
        #endregion    全选主元高斯消去法
        public bool GetRootsetGauss(Matrix mtxResult)
        {
            int l, k, i, j, nIs = 0, p, q;
            double d, t;
            // 方程组的属性，将常数矩阵赋给解矩阵           
            //mtxResult.SetValue(mtxLEConst);
            mtxResult=mtxLEConst.Clone();
            double[] pDataCoef = GetData(mtxLECoef);
            double[] pDataConst = GetData(mtxResult);
            int n = GetNumUnknowns();             // 临时缓冲区，存放列数          
            int[] pnJs = new int[n];             // 消元         
            l = 1;
            for (k = 0; k <= n - 2; k++)
            {
                d = 0.0;
                for (i = k; i <= n - 1; i++)
                {
                    for (j = k; j <= n - 1; j++)
                    {
                        t = Math.Abs(pDataCoef[i * n + j]);
                        if (t > d)
                        {
                            d = t;

                            pnJs[k] = j;
                            nIs = i;
                        }
                    }
                }
                if (d == 0.0) l = 0;
                else
                {
                    if (pnJs[k] != k)
                    {
                        for (i = 0; i <= n - 1; i++)
                        {
                            p = i * n + k;
                            q = i * n + pnJs[k]; t = pDataCoef[p];
                            pDataCoef[p] = pDataCoef[q]; pDataCoef[q] = t;
                        }
                    }
                    if (nIs != k)
                    {
                        for (j = k; j <= n - 1; j++)
                        {
                            p = k * n + j; q = nIs * n + j; t = pDataCoef[p];
                            pDataCoef[p] = pDataCoef[q]; pDataCoef[q] = t;
                        }
                        t = pDataConst[k];
                        pDataConst[k] = pDataConst[nIs]; pDataConst[nIs] = t;
                    }
                }
                // 求解失败                
                if (l == 0)
                {
                    return false;
                }
                d = pDataCoef[k * n + k];
                for (j = k + 1; j <= n - 1; j++)
                {
                    p = k * n + j;
                    pDataCoef[p] = pDataCoef[p] / d;
                }
                pDataConst[k] = pDataConst[k] / d; 
                for (i = k + 1; i <= n - 1; i++)
                {
                    for (j = k + 1; j <= n - 1; j++)
                    {
                        p = i * n + j;
                        pDataCoef[p] = pDataCoef[p] - pDataCoef[i * n + k] * pDataCoef[k * n + j];
                    }
                    pDataConst[i] = pDataConst[i] - pDataCoef[i * n + k] * pDataConst[k];
                }
            }
            // 求解失败 
            d = pDataCoef[(n - 1) * n + n - 1]; 
            if (d == 0.0)
            {
                return false;
            }
            // 求解 

            pDataConst[n - 1] = pDataConst[n - 1] / d;
            for (i = n - 2; i >= 0; i--)
            {
                t = 0.0;
                for (j = i + 1; j <= n - 1; j++)
                    t = t + pDataCoef[i * n + j] * pDataConst[j];
                pDataConst[i] = pDataConst[i] - t;
            }
            // 调整解的位置 

            pnJs[n - 1] = n - 1;
            for (k = n - 1; k >= 0; k--)
            {
                if (pnJs[k] != k)
                {
                    t = pDataConst[k];
                    pDataConst[k] = pDataConst[pnJs[k]];
                    pDataConst[pnJs[k]] = t;
                }
            }
            return true;
        }


        public double[] GetData(Matrix matrix)
        {
            List<double> dbs =new List<double>();
            for (int i = 0;i< matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Col; j++)
                {
                    dbs.Add(matrix[i, j]);
                }
            }

            return dbs.ToArray();
        }


        public double[] Gause(double[,] a, int n)
        {
            int i, j, k;
            int rank, columm;
            double temp, l, s;
            double[] x = new double[n];
            for (i = 0; i <= n - 2; i++)
            {
                rank = i;
                columm = i;
                for (j = i + 1; j <= n - 1; j++)                     //选主元
                    if (a[j, i] > a[i, i])
                    {
                        rank = j;
                        columm = i;
                    }
                for (k = 0; k <= n; k++)                //主元行变换
                {
                    temp = a[i, k];
                    a[i, k] = a[rank, k];
                    a[rank, k] = temp;
                }
                for (j = i + 1; j <= n - 1; j++)         //解线性方程
                {
                    l = a[j, i] / a[i, i];
                    for (k = i; k <= n; k++)
                        a[j, k] = a[j, k] - l * a[i, k];
                }
            }
            x[n - 1] = a[n - 1, n] / a[n - 1, n - 1];       //回代方程求解x
            for (i = n - 2; i >= 0; i--)
            {
                s = 0;
                for (j = i + 1; j <= n - 1; j++)
                    s = s + a[i, j] * x[j];
                x[i] = (a[i, n] - s) / a[i, i];
            }
            return x;
        }

        public Matrix GetRootSetGauss(Matrix matrix)
        {
            return Gause(matrix.Mat, matrix.Col-1);
        }

    }
}
