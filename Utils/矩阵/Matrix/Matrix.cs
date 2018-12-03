using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary>
    /// 这是一个实矩阵类，矩阵大小可更改
    /// </summary>
    /// <remarks>功能如下：
    /// （1）矩阵的加、减、数乘和乘等运算；
    /// （2）矩阵的转置运算；
    /// （3）矩阵列向量的最大值与最小值；
    /// （4）矩阵列向量的内积运算，范数；
    /// （5）方阵的特征值与特征向量。
    /// 在这个类中用到了LSMat类，它是一个矩阵数据文件存取类（自编自定义的数据文
    /// 件格式，非常简单，ACSII码形式）。
    /// 注：数学常数都用大写来表达
    /// </remarks>
    public class Matrix
    {
        private static int matrixnumber = 0;

        private int hashcode;

        /// <summary>  存储矩阵数据的二维数组  </summary>
        public double[,] Mat;

        /// <summary>  矩阵实际的行数  </summary>
        private int row;

        /// <summary>  矩阵实际的列数  </summary>
        private int col;

        /// <summary>  获取矩阵行数的属性，用于获得矩阵的行数  </summary>
        public int Row
        {
            get
            {
                return this.row;
            }
        }

        /// <summary>  获取矩阵列数的属性，用于获得矩阵的列数  </summary>
        public int Col
        {
            get
            {
                return this.col;
            }
        }

        /// <summary>  重置矩阵的大小  </summary>
        /// <remarks>注：结果是新的行和列成为矩阵的真实大小；新矩阵与原矩阵的重合部分
        /// 的数据自动保留，其余数据丢失或遭到破坏</remarks>
        public void ReSetRowAndCol(int newrow, int newcol)
        {
            if (newrow <= 0 || newcol <= 0)
                throw new MatrixException(".ReSetRowAndCol(int,int)>重置的行数或列数不是正整数");
            Matrix.ChangeMat(ref this.Mat, ref this.row, ref this.col, newrow, newcol);
        }

        /// <summary>  恢复矩阵到目前最大的存储空间的大小  </summary>
        public void Revert()
        {
            this.row = this.Mat.GetLength(0);
            this.col = this.Mat.GetLength(1);
        }

        /// <summary>  随机数类的静态实例  </summary>
        private static RandNumber rr = new RandNumber();

        /// <summary>  数值计算的精度，默认值是1e-8  </summary>
        private static double Eps = 1.0e-8;

        /// <summary>  欧拉常数，是一个双精度常量  </summary>
        private const double Euler = 0.57721566490153286060651;

        /// <summary>  用于获得欧拉常数的公共静态属性，只读  </summary>
        public static double EULER
        {
            get
            {
                return Matrix.Euler;
            }
        }

        /// <summary>  用于获得数值计算的精度的公共静态属性，可读写  </summary>
        public static double EPS
        {
            set
            {
                Eps = value;
            }
            get
            {
                return Eps;
            }
        }

        /// <summary>  重新分配矩阵的存储空间  </summary>
        /// <remarks>
        /// 注：对矩阵的存储空间进行检查，如果原二维数组能容纳新的变化，则
        /// 保持原数组不变；否则，则按新的变化重新分配存储空间，并保留原数
        /// 组中的值不变，返回。对新分配的部分初始化为零或者对新扩大的部分
        /// 重新置零。
        /// </remarks>
        private static void ChangeMat(ref double[,] Mat, ref int row, ref int col, int newrow, int newcol)
        {
            int i = 0, j = 0; //循环变量
            if (newrow <= Mat.GetLength(0) && newcol <= Mat.GetLength(1))//原二维数组能满足新的要求
            {
                //对如下三种情况产生的对原二维数组的新扩大部分重新置零
                if (newrow > row && newcol > col)
                {
                    for (i = row; i < newrow; i++)
                        for (j = 0; j < newcol; j++)
                            Mat[i, j] = 0;
                    for (j = col; j < newcol; j++)
                        for (i = 0; i < row; i++)
                            Mat[i, j] = 0;
                }
                else if (newrow > row && newcol <= col)
                {
                    for (i = row; i < newrow; i++)
                        for (j = 0; j < newcol; j++)
                            Mat[i, j] = 0;
                }
                else if (newrow <= row && newcol > col)
                {
                    for (j = col; j < newcol; j++)
                        for (i = 0; i < newrow; i++)
                            Mat[i, j] = 0;
                }
            }
            else if (newrow > Mat.GetLength(0) && newcol > Mat.GetLength(1))
            //新行数和列数都大于当前二维数组的行数和列数
            {
                double[,] newMat = new double[newrow, newcol];

                for (i = 0; i < Mat.GetLength(0); i++)
                    for (j = 0; j < Mat.GetLength(1); j++)
                        newMat[i, j] = Mat[i, j];
                Mat = newMat;
            }
            else if (newrow > Mat.GetLength(0) && newcol <= Mat.GetLength(1))
            //新行数大于当前二维数组的行数但新列数不大于当前二维数组的列数
            {
                double[,] newMat = new double[newrow, newcol];
                for (i = 0; i < Mat.GetLength(0); i++)
                    for (j = 0; j < newcol; j++)
                        newMat[i, j] = Mat[i, j];
                Mat = newMat;
            }
            else//新列数大于当前二维数组的列数但新行数不大于当前二维数组的行数
            {
                double[,] newMat = new double[newrow, newcol];
                for (i = 0; i < newrow; i++)
                    for (j = 0; j < Mat.GetLength(1); j++)
                        newMat[i, j] = Mat[i, j];
                Mat = newMat;
            }
            row = newrow;//原矩阵行数重置为新行数
            col = newcol;//原矩阵列数重置为新列数
        }


        public Matrix()
        {
            Mat = new double[0, 0];
            this.col = this.row = 0;
            this.hashcode = Matrix.matrixnumber;
            Matrix.matrixnumber++;
        }

        /// <summary>
        /// 构造函数2：用于产生一个指定行数和列数的矩阵，如果
        /// 指定的行数和列数至少有一个非正数，则引发异常 
        /// </summary>
        /// <param name="row">指定的行数</param>
        /// <param name="col">指定的列数</param>
        public Matrix(int row, int col)
        {
            if (row <= 0 || col <= 0) throw new MatrixException("");
            Mat = new double[row, col];
            this.row = row;
            this.col = col;
            this.hashcode = Matrix.matrixnumber;
            Matrix.matrixnumber++;
        }

        /// <summary>  构造函数3：用于产生n阶方阵  </summary>
        /// <param name="n">方阵的阶数</param>
        public Matrix(int n)
        {
            Mat = new double[n, n];
            this.row = this.col = n;
            this.hashcode = Matrix.matrixnumber;
            Matrix.matrixnumber++;
        }

        /// <summary> double数组构造矩阵 </summary>
        public Matrix(double[,] DataFileName)
        {
            this.row = DataFileName.GetLength(0);
            this.col = DataFileName.GetLength(1);
            if (row <= 0 || col <= 0) throw new MatrixException("");
            Mat = DataFileName;
            this.hashcode = Matrix.matrixnumber;
            Matrix.matrixnumber++;
        }

        /// <summary  判断两矩阵的每个元素是否相等  </summary>
        public static Matrix operator ==(Matrix mat1, Matrix mat2)
        {
            if (mat1.Row != mat2.Row || mat1.Col != mat2.Col)
                throw new MatrixException("在判断矩阵相等中两矩阵不同型");
            Matrix mat = Matrix.Ones(mat1.Row, mat1.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    if (mat1.Mat[i, j] != mat2.Mat[i, j])
                        mat.Mat[i, j] = 0;
            return mat;
        }

        /// <summary>  判断两矩阵的每个元素是否不相等  </summary>
        public static Matrix operator !=(Matrix mat1, Matrix mat2)
        {
            if (mat1.Row != mat2.Row || mat1.Col != mat2.Col)
                throw new MatrixException("在判断矩阵相等中两矩阵不同型");
            Matrix mat = Matrix.Zeros(mat1.Row, mat1.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    if (mat1.Mat[i, j] != mat2.Mat[i, j])
                        mat.Mat[i, j] = 1;
            return mat;
        }

        /// <summary>  判断一个矩阵的每个元素是否与给定的数相等  </summary>
        public static Matrix operator ==(Matrix mat, double v)
        {
            Matrix mat1 = new Matrix(mat.Row, mat.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                {
                    if (mat.Mat[i, j] != v)
                        mat1.Mat[i, j] = 0;
                    else
                        mat1.Mat[i, j] = 1;
                }
            return mat1;
        }

        /// <summary>  判断一个矩阵的每个元素是否与给定的数不相等  </summary>
        public static Matrix operator !=(Matrix mat, double v)
        {
            Matrix mat1 = new Matrix(mat.Row, mat.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                {
                    if (mat.Mat[i, j] != v)
                        mat1.Mat[i, j] = 1;
                    else
                        mat1.Mat[i, j] = 0;
                }
            return mat1;
        }

        /// <summary>  判断一个矩阵的每个元素是否与给定的数相等  </summary>
        public static Matrix operator ==(double v, Matrix mat)
        {
            Matrix mat1 = new Matrix(mat.Row, mat.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                {
                    if (mat.Mat[i, j] != v)
                        mat1.Mat[i, j] = 0;
                    else
                        mat1.Mat[i, j] = 1;
                }
            return mat1;
        }

        /// <summary>  判断一个矩阵的每个元素是否与给定的数不相等  </summary>
        public static Matrix operator !=(double v, Matrix mat)
        {
            Matrix mat1 = new Matrix(mat.Row, mat.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                {
                    if (mat.Mat[i, j] != v)
                        mat1.Mat[i, j] = 1;
                    else
                        mat1.Mat[i, j] = 0;
                }
            return mat1;
        }

        /// <summary>  判断两个矩阵是否相等  </summary>
        public override bool Equals(object obj)
        {
            Matrix mat = (Matrix)obj;
            if (this.Row != mat.Row || this.Col != mat.Col)
                return false;
            for (int i = 0; i < this.Row; i++)
                for (int j = 0; j < this.Col; j++)
                    if (this.Mat[i, j] != mat.Mat[i, j])
                        return false;
            return true;
        }

        /// <summary>  判断两个矩阵是否相等  </summary>
        public static new bool Equals(object obj1, object obj2)
        {
            Matrix mat = (Matrix)obj1;
            return mat.Equals(obj2);
        }

        /// <summary>  获得矩阵的哈希码  </summary>
        public override int GetHashCode()
        {
            return this.hashcode;
        }

        /// <summary>  矩阵取反  </summary>
        public static Matrix operator !(Matrix mat)
        {
            Matrix mat1 = new Matrix(mat.Row, mat.Col);
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if (mat.Mat[i, j] != 0) mat1.Mat[i, j] = 0;
                    else mat1.Mat[i, j] = 1;
                }
            return mat1;
        }

        /// <summary>  比较同型两矩阵的大小  </summary>
        public static Matrix operator >(Matrix mat1, Matrix mat2)
        {
            if (mat1.Row != mat2.Row || mat1.Col != mat2.Col)
                throw new MatrixException("在大于比较中两矩阵不同型");
            Matrix mat = new Matrix(mat1.Row, mat1.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                {
                    if (mat1.Mat[i, j] > mat2.Mat[i, j])
                        mat.Mat[i, j] = 1;
                    else
                        mat1.Mat[i, j] = 0;
                }
            return mat;
        }

        /// <summary>  比较同型两矩阵的大小  </summary>
        public static Matrix operator <(Matrix mat1, Matrix mat2)
        {
            if (mat1.Row != mat2.Row || mat1.Col != mat2.Col)
                throw new MatrixException("在小于比较中两矩阵不同型");
            Matrix mat = new Matrix(mat1.Row, mat1.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                {
                    if (mat1.Mat[i, j] < mat2.Mat[i, j])
                        mat.Mat[i, j] = 1;
                    else
                        mat1.Mat[i, j] = 0;
                }
            return mat;
        }

        /// <summary>  比较同型两矩阵的大小  </summary>
        public static Matrix operator <=(Matrix mat1, Matrix mat2)
        {
            if (mat1.Row != mat2.Row || mat1.Col != mat2.Col)
                throw new MatrixException("在小于等于比较中两矩阵不同型");
            Matrix mat = new Matrix(mat1.Row, mat1.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                {
                    if (mat1.Mat[i, j] <= mat2.Mat[i, j])
                        mat.Mat[i, j] = 1;
                    else
                        mat1.Mat[i, j] = 0;
                }
            return mat;
        }

        /// <summary>  比较同型两矩阵的大小  </summary>
        public static Matrix operator >=(Matrix mat1, Matrix mat2)
        {
            if (mat1.Row != mat2.Row || mat1.Col != mat2.Col)
                throw new MatrixException("在大于等于比较中两矩阵不同型");
            Matrix mat = new Matrix(mat1.Row, mat1.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                {
                    if (mat1.Mat[i, j] >= mat2.Mat[i, j])
                        mat.Mat[i, j] = 1;
                    else
                        mat1.Mat[i, j] = 0;
                }
            return mat;
        }

        //--------------------------
        //从矩阵中获得元素
        //--------------------------
        /// <summary>  获得矩阵的指定的一行构成一个行矩阵  </summary>
        /// <param name="Rownum">指定的行，从0开始</param>
        public Matrix GetRow(int Rownum)
        {
            Matrix rowmat = new Matrix(1, this.Col);
            for (int j = 0; j < this.Col; j++)
                rowmat.Mat[0, j] = this.Mat[Rownum, j];
            return rowmat;
        }

        /// <summary>  获得矩阵的指定的行构成相应的一个新矩阵  </summary>
        /// <param name="vec">指定行数构成的一个行矩阵</param>
        public Matrix GetRow(Matrix vecrow)
        {
            if (vecrow.row > 1) throw new MatrixException(".GetRow(Matrix)>输入数据不是行向量");
            Matrix newmat = new Matrix(vecrow.Col, this.Col);
            for (int i = 0; i < newmat.Row; i++)
            {
                int vv = (int)vecrow[i];
                for (int j = 0; j < newmat.Col; j++)
                    newmat[i, j] = this.Mat[vv, j];
            }
            return newmat;
        }

        /// <summary>  获得矩阵的指定一列  </summary>
        /// <param name="Colnum">指定的列，从0开始</param>
        public Matrix GetCol(int Colnum)
        {
            Matrix colmat = new Matrix(this.Row, 1);
            for (int i = 0; i < this.Row; i++)
                colmat.Mat[i, 0] = this.Mat[i, Colnum];
            return colmat;
        }

        /// <summary>  获得矩阵的指定的列构成相应的一个新矩阵  </summary>
        /// <param name="vec">指定列数构成的一个行矩阵</param>
        public Matrix GetCol(Matrix veccol)
        {
            if (veccol.row > 1) throw new MatrixException(".GetCol(Matrix)>输入数据不是行向量");
            Matrix newmat = new Matrix(this.Row, veccol.Col);
            for (int j = 0; j < newmat.Col; j++)
            {
                int vv = (int)veccol[j];
                for (int i = 0; i < newmat.Row; i++)
                    newmat[i, j] = this.Mat[i, vv];
            }
            return newmat;
        }

        /// <summary>  存取指定行指定列的矩阵元素或按行计数存取指定位置的元素  </summary>
        /// <remarks>
        /// 对于存取指定行指定列的矩阵元素这种情况，就是一般的存和取。对于
        /// 按行计数存取指定位置的元素的情况，
        /// </remarks>
        public double this[int rownum, int colnum]
        {
            get
            {
                if (colnum >= 0) return this.Mat[rownum, colnum];
                else
                {
                    int j = rownum % this.Col;
                    int i = (rownum - j) / this.Col;
                    return this.Mat[i, j];
                }
            }
            set
            {
                if (colnum >= 0) this.Mat[rownum, colnum] = value;
                else
                {
                    int j = rownum % this.Col;
                    int i = (rownum - j) / this.Col;
                    this.Mat[i, j] = value;
                }
            }
        }

        /// <summary>  按列对矩阵的指定元素存取  </summary>
        public double this[int num]
        {
            get
            {
                int rown, coln;
                if (num % this.Row == 0)
                {
                    coln = num / this.Row;
                    rown = 0;
                }
                else
                {
                    coln = num / this.Row;
                    rown = num - this.Row * coln;
                }
                return this.Mat[rown, coln];
            }
            set
            {
                int rown, coln;
                if (num % this.Row == 0)
                {
                    coln = num / this.Row;
                    rown = 0;
                }
                else
                {
                    coln = num / this.Row;
                    rown = num - this.Row * coln;
                }
                this.Mat[rown, coln] = value;
            }
        }

        /// <summary>  给矩阵的指定行赋一定值  </summary>
        /// <param name="Value">待赋数值</param>
        /// <param name="Rownum">被赋值行的行数，从0开始</param>
        public void SetRow(double Value, int Rownum)
        {
            for (int j = 0; j < this.Col; j++)
                this.Mat[Rownum, j] = Value;
            return;
        }

        /// <summary>  给矩阵的指定行赋一定值  </summary>
        /// <param name="Value">待赋数值</param>
        /// <param name="Rownum">被赋值行的行数构成的行矩阵</param>
        public void SetRow(double Value, Matrix Rownum)
        {
            if (Rownum.Row > 1) throw new MatrixException(".SetRow(double,Matrix)>中的行数矩阵不是行矩阵");
            for (int i = 0; i < Rownum.Col; i++)
                for (int j = 0; j < this.Col; j++)
                    this.Mat[(int)Rownum[i], j] = Value;
        }

        /// <summary>  给矩阵的指定行赋一个行矩阵  </summary>
        /// <param name="Rowmat">待赋的矩阵</param>
        /// <param name="Rownum">被赋值行的行数，从0开始</param>
        /// <remarks>
        /// 注：Rowmat不是行矩阵或者它的元素个数没有被赋值的矩阵的列数大，这都没有关系。这
        /// 里采用了最灵活的方式，根据矩阵Rowmat中元素的个数和被赋值的矩阵的列数的情况，谁
        /// 少按谁的来赋值，并且Rowmat按列来读数据。因此，本实现保证不会出现异常。显然，若
        /// Rowmat按最标准的方式（它是一个行矩阵并且列数等于被赋值的矩阵的列数）输入，结果
        /// 就是预期的。本实现方式既实现了对标准输入的支持，又体现了很大的灵活性。
        /// </remarks>
        public void SetRow(Matrix Rowmat, int Rownum)
        {
            if (Rowmat.Col * Rowmat.Row >= this.Col)
            {
                for (int j = 0; j < this.Col; j++)
                    this.Mat[Rownum, j] = Rowmat[j];
            }
            else
            {
                for (int j = 0; j < Rowmat.Col * Rowmat.Row; j++)
                    this.Mat[Rownum, j] = Rowmat[j];
            }
        }

        /// <summary>  给矩阵的指定行赋一个行矩阵  </summary>
        /// <param name="Rowmat">待赋的矩阵</param>
        /// <param name="Rownum">被赋值行的行数构成的行矩阵</param>
        /// <remarks>注释与SetRow(Matrix,int)类似</remarks>
        public void SetRow(Matrix Rowmat, Matrix Rownum)
        {
            if (Rownum.Row > 1) throw new MatrixException(".SetRow(Matrix,Matrix)>中的行数矩阵不是行矩阵");
            if (Rowmat.Col * Rowmat.Row >= this.Col)
            {
                for (int i = 0; i < Rownum.Col; i++)
                {
                    int vv = (int)Rownum[i];
                    for (int j = 0; j < this.Col; j++)
                        this.Mat[vv, j] = Rowmat[j];
                }
            }
            else
            {
                for (int i = 0; i < Rownum.Col; i++)
                {

                    int vv = (int)Rownum[i];
                    for (int j = 0; j < Rowmat.Col * Rowmat.Row; j++)
                        this.Mat[vv, j] = Rowmat[j];
                }
            }
        }

        /// <summary>  给矩阵的指定列赋一定值  </summary>
        /// <param name="Value">待赋数值</param>
        /// <param name="Rownum">被赋值列的行数，从0开始</param>
        public void SetCol(double Value, int Colnum)
        {
            for (int i = 0; i < this.Row; i++)
                this.Mat[i, Colnum] = Value;
        }

        /// <summary>  给矩阵的指定列赋一定值  </summary>
        /// <param name="Value">待赋数值</param>
        /// <param name="Rownum">被赋值列的行数构成的行矩阵</param>
        public void SetCol(double Value, Matrix Colnum)
        {
            if (Colnum.Row > 1) throw new MatrixException(".SetCol(double,Matrix)>中的列数构成的矩阵不是行矩阵");
            for (int j = 0; j < Colnum.Col; j++)
            {
                int vv = (int)Colnum[j];
                for (int i = 0; i < this.Row; i++)
                    this.Mat[i, vv] = Value;
            }
        }

        /// <summary>  给矩阵的指定列赋一个列矩阵  </summary>
        /// <param name="Rowmat">待赋的矩阵</param>
        /// <param name="Rownum">被赋值列的列数，从0开始</param>
        /// <remarks>注：与SetRow(Matrix,int)中注释类似。</remarks>
        public void SetCol(Matrix Colmat, int Colnum)
        {
            if (Colmat.Row * Colmat.Col >= this.Row)
            {
                for (int i = 0; i < this.Row; i++)
                    this.Mat[i, Colnum] = Colmat[i];
            }
            else
            {
                for (int i = 0; i < Colmat.Row * Colmat.Col; i++)
                    this.Mat[i, Colnum] = Colmat[i];
            }
        }

        /// <summary>  给矩阵的指定列赋一个列矩阵  </summary>
        /// <param name="Rowmat">待赋的矩阵</param>
        /// <param name="Rownum">被赋值列的列数构成的行矩阵</param>
        /// <remarks>注：与SetRow(Matrix,int)中注释类似。</remarks>
        public void SetCol(Matrix Colmat, Matrix Colnum)
        {
            if (Colnum.Row > 1) throw new MatrixException(".SetCol(Matrix,Matrix)>中的列数构成的矩阵不是行矩阵");
            if (Colmat.Row * Colmat.Col >= this.Row)
            {
                for (int j = 0; j < Colnum.Col; j++)
                {
                    int vv = (int)Colnum[j];
                    for (int i = 0; i < this.Row; i++)
                        this.Mat[i, vv] = Colmat[i];
                }
            }
            else
            {
                for (int j = 0; j < Colnum.Col; j++)
                {
                    int vv = (int)Colnum[j];
                    for (int i = 0; i < Colmat.Row * Colmat.Col; i++)
                        this.Mat[i, vv] = Colmat[i];
                }
            }
        }

        /// <summary>  在矩阵的（0，0）位置赋一个矩阵  </summary>
        /// <param name="mat">待赋的矩阵</param>
        /// <remarks>如果待赋的矩阵的左上角放置在比原矩阵大，则舍去大的那部分</remarks>
        public void Set(Matrix mat)
        {
            this.Set(mat, 0, 0);
        }

        /// <summary>  在矩阵的指定位置赋一个矩阵  </summary>
        /// <param name="mat">待赋的矩阵</param>
        /// <param name="rownum">行位置</param>
        /// <param name="colnum">列位置</param>
        /// <remarks>
        /// 如果待赋的矩阵的左上角放置在指定位置后比原矩阵大，则舍去大的那部分
        /// </remarks>
        public void Set(Matrix mat, int rownum, int colnum)
        {
            int m, n;
            if (rownum + mat.Row > this.Row) m = this.Row;
            else m = rownum + mat.Row;
            if (colnum + mat.Col > this.Col) n = this.Col;
            else n = colnum + mat.Col;
            for (int i = rownum; i < m; i++)
                for (int j = colnum; j < n; j++)
                    this.Mat[i, j] = mat.Mat[i - rownum, j - colnum];
        }

        public Matrix Clone()
        {
            Matrix mat1 = new Matrix(this.Row, this.Col);
            for (int i = 0; i < this.Row; i++)
                for (int j = 0; j < this.Col; j++)
                    mat1.Mat[i, j] = this.Mat[i, j];
            return mat1;
        }

        //----------------------------------------------------------
        //矩阵数据类型的转换
        //----------------------------------------------------------
        /// <summary>  把double数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(double Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把float数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(float Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把ulong数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(ulong Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把long数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(long Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把uint数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(uint Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把int数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(int Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把ushort数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(ushort Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把short数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(short Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把byte数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(byte Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把sbyte数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(sbyte Value)
        {
            Matrix mat = new Matrix(1, 1);
            mat.Mat[0, 0] = Value;
            return mat;
        }

        /// <summary>  把bool数隐式转换为矩阵  </summary>
        public static implicit operator Matrix(bool Value)
        {
            Matrix mat = new Matrix(1, 1);
            if (Value) mat.Mat[0, 0] = 1;
            else mat.Mat[0, 0] = 0;
            return mat;
        }

        /// <summary>  把string数隐式转换为矩阵  </summary>
        //public static implicit operator Matrix(string Value)
        //{
        //    Matrix mat = new Matrix(1, 1);
        //    Maths.ValidateNum.Validate(Value, out Value);
        //    if (Value.Length != 0) mat.Mat[0, 0] = double.Parse(Value);
        //    else mat.Mat[0, 0] = 0;
        //    return mat;
        //}
        /// <summary>  把矩阵显式转换为double数  </summary>
        public static explicit operator double(Matrix mat)
        {
            if (mat.Col * mat.Row == 1)
                return mat.Mat[0, 0];
            else
                throw new MatrixException("矩阵不是1行1列");
        }

        /// <summary>  二维double型数组隐式转换为矩阵  </summary>
        /// <param name="mat2">二维数组</param>
        /// <returns>与数组同型的一个矩阵</returns>
        public static implicit operator Matrix(double[,] mat2)
        {
            Matrix mat1 = new Matrix(mat2.GetLength(0), mat2.GetLength(1));
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat1.Mat[i, j] = mat2[i, j];
            return mat1;
        }

        /// <summary>  二维float型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(float[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维ulong型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(ulong[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维long型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(long[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维uint型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(uint[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维int型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(int[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维ushort型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(ushort[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维short型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(short[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维byte型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(byte[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维sbyte型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(sbyte[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维bool型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(bool[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                {
                    if (mat[i, j]) mat2.Mat[i, j] = 1;
                    else mat2.Mat[i, j] = 0;
                }
            return mat2;
        }

        /// <summary>  二维char型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(char[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat2.Mat[i, j] = mat[i, j];
            return mat2;
        }

        /// <summary>  二维string型数组隐式转换为矩阵  </summary>
        public static implicit operator Matrix(string[,] mat)
        {
            Matrix mat2 = new Matrix(mat.GetLength(0), mat.GetLength(1));
            string str;
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                {
                    ValidateNum.Validate(mat[i, j], out str);
                    if (str.Length != 0) mat2.Mat[i, j] = double.Parse(str);
                    else mat2.Mat[i, j] = 0;
                }
            return mat2;
        }

        /// <summary>  一维double型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(double[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < vec.Length; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }

        /// <summary>  一维float型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(float[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }

        /// <summary>  一维ulong型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(ulong[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }

        /// <summary>  一维long型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(long[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }
        /// <summary>  一维uint型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(uint[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }

        /// <summary>  一维int型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(int[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }

        /// <summary>  一维ushort型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(ushort[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }

        /// <summary>  一维short型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(short[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }

        /// <summary>  一维byte型数组隐式转换为同长列矩阵  </summary>
        public static implicit operator Matrix(byte[] mat)
        {
            Matrix mat2 = new Matrix(mat.Length, 1);
            for (int i = 0; i < mat.Length; i++)
                mat2.Mat[i, 0] = mat[i];
            return mat2;
        }

        /// <summary>  一维sbyte型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(sbyte[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }

        /// <summary>  一维bool型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(bool[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
            {
                if (vec[i]) mat.Mat[i, 0] = 1;
                else mat.Mat[i, 0] = 0;
            }
            return mat;
        }

        /// <summary>  一维char型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(char[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = vec[i];
            return mat;
        }

        /// <summary>  一维string型数组隐式转换为列矩阵  </summary>
        public static implicit operator Matrix(string[] vec)
        {
            Matrix mat = new Matrix(vec.Length, 1);
            string str;
            for (int i = 0; i < mat.Row; i++)
            {
                ValidateNum.Validate(vec[i], out str);
                if (str.Length != 0) mat.Mat[i, 0] = double.Parse(str);
                else mat.Mat[i, 0] = 0;
            }
            return mat;
        }

        /// <summary>  矩阵显式转换为二维double型数组  </summary>
        /// <param name="mat">矩阵</param>
        /// <returns></returns>
        public static explicit operator double[,](Matrix mat)
        {
            double[,] mat2 = new double[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                    mat2[i, j] = mat.Mat[i, j];
            return mat2;
        }

        /// <summary>  矩阵显式转换为二维float型数组  </summary>
        public static explicit operator float[,](Matrix mat)
        {
            float[,] mat2 = new float[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] > float.MaxValue) || (mat.Mat[i, j] < float.MinValue))
                        throw new MatrixException("double向float转化时，double数越出float型数的范围");
                    mat2[i, j] = (float)mat.Mat[i, j];
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为二维ulong型数组  </summary>
        public static explicit operator ulong[,](Matrix mat)
        {
            ulong[,] mat2 = new ulong[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] > ulong.MaxValue) || (mat.Mat[i, j] < ulong.MinValue))
                        throw new MatrixException("double向ulong转化时，double数越出ulong型数的范围");
                    mat2[i, j] = (ulong)mat.Mat[i, j];
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为二维long型数组  </summary>
        public static explicit operator long[,](Matrix mat)
        {
            long[,] mat2 = new long[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] > long.MaxValue) || (mat.Mat[i, j] < long.MinValue))
                        throw new MatrixException("double向long转化时，double数越出long型数的范围");
                    mat2[i, j] = (long)mat.Mat[i, j];
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为二维uint型数组  </summary>
        public static explicit operator uint[,](Matrix mat)
        {
            uint[,] mat2 = new uint[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] > uint.MaxValue) || (mat.Mat[i, j] < uint.MinValue))
                        throw new MatrixException("double向uint转化时，double数越出uint型数的范围");
                    mat2[i, j] = (uint)mat.Mat[i, j];
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为二维int型数组  </summary>
        public static explicit operator int[,](Matrix mat)
        {
            int[,] mat2 = new int[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] > int.MaxValue) || (mat.Mat[i, j] < int.MinValue))
                        throw new MatrixException("double向int转化时，double数越出int型数的范围");
                    mat2[i, j] = (int)mat.Mat[i, j];
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为二维ushort型数组  </summary>
        public static explicit operator ushort[,](Matrix mat)
        {
            ushort[,] mat2 = new ushort[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] > ushort.MaxValue) || (mat.Mat[i, j] < ushort.MinValue))
                        throw new MatrixException("double向ushort转化时，double数越出ushort型数的范围");
                    mat2[i, j] = (ushort)mat.Mat[i, j];
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为二维short型数组  </summary>
        public static explicit operator short[,](Matrix mat)
        {
            short[,] mat2 = new short[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] > short.MaxValue) || (mat.Mat[i, j] < short.MinValue))
                        throw new MatrixException("double向short转化时，double数越出short型数的范围");
                    mat2[i, j] = (short)mat.Mat[i, j];
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为同型二维byte型数组  </summary>
        public static explicit operator byte[,](Matrix mat)
        {
            byte[,] mat2 = new byte[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] >= byte.MaxValue) || (mat.Mat[i, j] < byte.MinValue))
                        throw new MatrixException("double向byte转化时，double数越出byte型数的范围");
                    mat2[i, j] = (byte)mat.Mat[i, j];
                }
            return mat2;
        }
        /// <summary>  矩阵显式转换为同型二维sbyte型数组  </summary>
        public static explicit operator sbyte[,](Matrix mat)
        {
            sbyte[,] mat2 = new sbyte[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] >= sbyte.MaxValue) || (mat.Mat[i, j] < sbyte.MinValue))
                        throw new MatrixException("double向sbyte转化时，double数越出sbyte型数的范围");
                    mat2[i, j] = (sbyte)mat.Mat[i, j];
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为同型二维bool型数组  </summary>
        public static explicit operator bool[,](Matrix mat)
        {
            bool[,] mat2 = new bool[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if (mat.Mat[i, j] != 0) mat2[i, j] = true;
                    else mat2[i, j] = false;
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为同型二维char型数组  </summary>
        public static explicit operator char[,](Matrix mat)
        {
            char[,] mat2 = new char[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    if ((mat.Mat[i, j] > char.MaxValue) || (mat.Mat[i, j] < char.MinValue))
                        throw new MatrixException("double向char转化时，double数越出char型数的范围");
                    mat2[i, j] = (char)mat.Mat[i, j];
                }
            return mat2;
        }

        /// <summary>  矩阵显式转换为同型二维string型数组  </summary>
        public static explicit operator string[,](Matrix mat)
        {
            string[,] mat2 = new string[mat.Row, mat.Col];
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                    mat2[i, j] = mat.Mat[i, j].ToString();
            return mat2;
        }

        /// <summary>  矩阵按列显式转换为一维double型数组  </summary>
        public static explicit operator double[](Matrix mat)
        {
            double[] vec = new double[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                    vec[i + j * mat.Row] = mat.Mat[i, j];
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维float型数组  </summary>
        public static explicit operator float[](Matrix mat)
        {
            float[] vec = new float[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] > float.MaxValue) || (mat.Mat[i, j] < float.MinValue))
                        throw new MatrixException("double向float转化时，double数越出float型数的范围");
                    vec[i + j * mat.Row] = (float)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维ulong型数组  </summary>
        public static explicit operator ulong[](Matrix mat)
        {
            ulong[] vec = new ulong[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] > ulong.MaxValue) || (mat.Mat[i, j] < ulong.MinValue))
                        throw new MatrixException("double向ulong转化时，double数越出ulong型数的范围");
                    vec[i + j * mat.Row] = (ulong)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维long型数组  </summary>
        public static explicit operator long[](Matrix mat)
        {
            long[] vec = new long[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] > long.MaxValue) || (mat.Mat[i, j] < long.MinValue))
                        throw new MatrixException("double向long转化时，double数越出long型数的范围");
                    vec[i + j * mat.Row] = (long)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维uint型数组  </summary>
        public static explicit operator uint[](Matrix mat)
        {
            uint[] vec = new uint[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] > uint.MaxValue) || (mat.Mat[i, j] < uint.MinValue))
                        throw new MatrixException("double向uint转化时，double数越出uint型数的范围");
                    vec[i + j * mat.Row] = (uint)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维int型数组  </summary>
        public static explicit operator int[](Matrix mat)
        {
            int[] vec = new int[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] > int.MaxValue) || (mat.Mat[i, j] < int.MinValue))
                        throw new MatrixException("double向int转化时，double数越出int型数的范围");
                    vec[i + j * mat.Row] = (int)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维float型数组  </summary>
        public static explicit operator ushort[](Matrix mat)
        {
            ushort[] vec = new ushort[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] > ushort.MaxValue) || (mat.Mat[i, j] < ushort.MinValue))
                        throw new MatrixException("double向ushort转化时，double数越出ushort型数的范围");
                    vec[i + j * mat.Row] = (ushort)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维short型数组  </summary>
        public static explicit operator short[](Matrix mat)
        {
            short[] vec = new short[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] > short.MaxValue) || (mat.Mat[i, j] < short.MinValue))
                        throw new MatrixException("double向short转化时，double数越出short型数的范围");
                    vec[i + j * mat.Row] = (short)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维byte型数组  </summary>
        public static explicit operator byte[](Matrix mat)
        {
            byte[] vec = new byte[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] >= 256) || (mat.Mat[i, j] < 0))
                        throw new MatrixException("double向byte转化时，double数不能在[0，255]范围外");
                    vec[i + j * mat.Row] = (byte)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维sbyte型数组  </summary>
        public static explicit operator sbyte[](Matrix mat)
        {
            sbyte[] vec = new sbyte[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] > sbyte.MaxValue) || (mat.Mat[i, j] < sbyte.MinValue))
                        throw new MatrixException("double向sbyte转化时，double数越出sbyte型数的范围");
                    vec[i + j * mat.Row] = (sbyte)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维bool型数组  </summary>
        public static explicit operator bool[](Matrix mat)
        {
            bool[] vec = new bool[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if (mat.Mat[i, j] != 0) vec[i + j * mat.Row] = true;
                    else vec[i + j * mat.Row] = false;
                }
            return vec;
        }
        /// <summary>  矩阵按列显式转换为一维char型数组  </summary>
        public static explicit operator char[](Matrix mat)
        {
            char[] vec = new char[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    if ((mat.Mat[i, j] > char.MaxValue) || (mat.Mat[i, j] < char.MinValue))
                        throw new MatrixException("double向char转化时，double数越出char型数的范围");
                    vec[i + j * mat.Row] = (char)mat.Mat[i, j];
                }
            return vec;
        }

        /// <summary>  矩阵按列显式转换为一维string型数组  </summary>
        public static explicit operator string[](Matrix mat)
        {
            string[] vec = new string[mat.Row * mat.Col];
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                    vec[i + j * mat.Row] = mat.Mat[i, j].ToString();
            return vec;
        }

        /// <summary>  克隆矩阵  </summary>
        /// <param name="mat2">被克隆矩阵</param>
        /// <returns>一个与mat2一样的矩阵</returns>
        /// <remarks>所谓克隆就是一份完整的拷贝</remarks>
        public static Matrix Clone(Matrix mat2)
        {
            Matrix mat1 = new Matrix(mat2.Row, mat2.Col);
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat1.Mat[i, j] = mat2.Mat[i, j];
            return mat1;
        }


        //-----------------------
        //矩阵的基本运算
        //-----------------------
        /// <summary>  矩阵的转置  </summary>
        /// <param name="mat2">被转置矩阵</param>
        /// <returns>一个转置后的矩阵</returns>
        public static Matrix Trans(Matrix A)
        {
            Matrix mat = new Matrix(A.Col, A.Row);
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                    mat.Mat[i, j] = A.Mat[j, i];
            return mat;
        }

        /// <summary>  矩阵沿逆时针旋转90度  </summary>
        /// <param name="mat">输入矩阵</param>
        /// <returns>旋转后的矩阵</returns>
        public static Matrix Rot90(Matrix mat)
        {
            Matrix rotmat = Matrix.Zeros(mat.Col, mat.Row);
            int col = mat.Col;
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                    rotmat.Mat[col - 1 - j, i] = mat.Mat[i, j];
            return rotmat;
        }

        /// <summary>  上下交换矩阵元素  </summary>
        /// <param name="mat">输入矩阵</param>
        /// <returns>交换后的矩阵</returns>
        /// <remarks>以矩阵“水平中线”为对称轴，交换上下对称位置上的元素</remarks>
        public static Matrix Flipud(Matrix mat)
        {
            Matrix matud = Matrix.Clone(mat);
            int row = mat.Row;
            for (int i = 0; i < mat.Row / 2; i++)
                for (int j = 0; j < mat.Col; j++)
                {
                    matud.Mat[row - i - 1, j] = mat.Mat[i, j];
                    matud.Mat[i, j] = mat.Mat[row - i - 1, j];
                }
            return matud;
        }

        /// <summary>  上下交换矩阵元素  </summary>
        public void Flipud()
        {
            double vv = 0;  //中间存储矩阵
            for (int i = 0; i < this.Row / 2; i++)
                for (int j = 0; j < this.Col; j++)
                {
                    vv = this.Mat[i, j];
                    this.Mat[i, j] = this.Mat[this.Row - i - 1, j];
                    this.Mat[this.Row - i - 1, j] = vv;
                }
        }

        /// <summary>  左右交换矩阵元素，返回一个新矩阵  </summary>
        /// <param name="mat">输入矩阵</param>
        /// <returns>交换后的矩阵</returns>
        /// <remarks>以矩阵“垂直中线”为对称轴，交换左右对称位置上的元素</remarks>
        public static Matrix Fliplr(Matrix mat)
        {
            Matrix matlr = Matrix.Clone(mat);
            int col = mat.Col;
            for (int j = 0; j < mat.Col / 2; j++)
                for (int i = 0; i < mat.Row; i++)
                {
                    matlr.Mat[i, col - j - 1] = mat.Mat[i, j];
                    matlr.Mat[i, j] = mat.Mat[i, col - j - 1];
                }
            return matlr;
        }

        /// <summary>  左右交换矩阵元素，不产生新矩阵  </summary>
        public void Fliplr()
        {
            double vv = 0;  //中间存储变量
            for (int j = 0; j < this.Col / 2; j++)
                for (int i = 0; i < this.Row; i++)
                {
                    vv = this.Mat[i, j];
                    this.Mat[i, j] = this.Mat[i, this.Col - j - 1];
                    this.Mat[i, this.Col - j - 1] = vv;
                }
        }

        /// <summary>  提取矩阵的下三角阵生成下三角阵  </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static Matrix Tril(Matrix mat)
        {
            Matrix mat2 = Matrix.Zeros(mat.Row, mat.Col);
            for (int j = 0; j < mat.Col; j++)
                for (int i = j; i < mat.Row; i++)
                    mat2.Mat[i, j] = mat.Mat[i, j];
            return mat2;
        }

        /// <summary>  提取矩阵的下三角阵生成下三角阵  </summary>
        /// <param name="mat"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Matrix Tril(Matrix mat, int k)
        {
            if (k >= (mat.Col - 1))
                return Matrix.Clone(mat);
            if (k <= (-mat.Row))
                return Matrix.Zeros(mat.Row, mat.Col);
            Matrix mat2 = Matrix.Zeros(mat.Row, mat.Col);
            if (k >= 0)
            {
                for (int j = 0; j < k; j++)
                    for (int i = 0; i < mat.Row; i++)
                        mat2.Mat[i, j] = mat.Mat[i, j];
                for (int j = k; j < mat.Col; j++)
                    for (int i = j - k; i < mat.Row; i++)
                        mat2.Mat[i, j] = mat.Mat[i, j];
            }
            else
            {
                for (int j = 0; j < mat.Col; j++)
                    for (int i = j - k; i < mat.Row; i++)
                        mat2.Mat[i, j] = mat.Mat[i, j];
            }
            return mat2;
        }

        /// <summary>  提取矩阵的上三角阵生成上三角阵  </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static Matrix Triu(Matrix mat)
        {
            Matrix mat2 = Matrix.Zeros(mat.Row, mat.Col);
            for (int i = 0; i < mat.Row; i++)
                for (int j = i; j < mat.Col; j++)
                    mat2.Mat[i, j] = mat.Mat[i, j];
            return mat2;
        }

        /// <summary>  提取矩阵的上三角阵生成上三角阵  </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static Matrix Triu(Matrix mat, int k)
        {
            if (k >= mat.Col)
                return Matrix.Zeros(mat.Row, mat.Col);
            if (k <= (1 - mat.Row))
                return Matrix.Clone(mat);
            Matrix mat2 = Matrix.Zeros(mat.Row, mat.Col);
            if (k >= 0)
            {
                for (int i = 0; i < mat.Row; i++)
                    for (int j = i + k; j < mat.Col; j++)
                        mat2.Mat[i, j] = mat.Mat[i, j];
            }
            else
            {
                for (int i = 0; i < (-k); i++)
                    for (int j = 0; j < mat.Col; j++)
                        mat2.Mat[i, j] = mat.Mat[i, j];
                for (int i = (-k); i < mat.Row; i++)
                    for (int j = i + k; j < mat.Col; j++)
                        mat2.Mat[i, j] = mat.Mat[i, j];
            }
            return mat2;
        }

        /// <summary>  清掉指定行  </summary>
        public static Matrix ClearRow(Matrix A, params int[] rowvec)
        {
            bool[] sign = new bool[A.Row];
            for (int i = 0; i < rowvec.Length; i++)
            {
                if (rowvec[i] >= A.Row || rowvec[i] < 0)
                    throw new MatrixException("要清掉的第" + rowvec[i] + "行没有");
                sign[rowvec[i]] = true;
            }
            int row = 0;
            for (int i = 0; i < A.Row; i++)
                if (!sign[i]) row++;
            Matrix mat = new Matrix(row, A.Col);
            row = 0;
            for (int i = 0; i < A.Row; i++)
            {
                if (!sign[i])
                {
                    for (int j = 0; j < A.Col; j++)
                        mat.Mat[row, j] = A.Mat[i, j];
                    row++;
                }
            }
            return mat;
        }

        /// <summary>  清掉指定列  </summary>
        public static Matrix ClearCol(Matrix A, params int[] colvec)
        {
            bool[] sign = new bool[A.Col];  //用于标记矩阵的每一列是否要被清掉
            for (int i = 0; i < colvec.Length; i++)
            {
                if (colvec[i] >= A.Row || colvec[i] < 0)
                    throw new MatrixException("要清掉的第" + colvec[i] + "列没有");
                sign[colvec[i]] = true;
            }
            int col = 0;  //清掉指定列后剩余的总列数，初始化为0
            for (int i = 0; i < A.Col; i++)  //找到清掉指定列后剩余的总列数
                if (!sign[i]) col++;
            Matrix mat = new Matrix(A.Row, col);  //创建用于存储清掉指定列后矩阵
            //下面为mat赋值
            col = 0;   //初始化为0，代表新矩阵的当前行
            for (int j = 0; j < A.Col; j++)
            {
                if (!sign[j])
                {
                    for (int i = 0; i < A.Row; i++)
                        mat.Mat[i, col] = A.Mat[i, j];  //把原矩阵的行赋到新矩阵中
                    col++;
                }
            }
            return mat;
        }

        /// <summary>  按列改变矩阵的行数和列数得到新矩阵  </summary>
        /// <param name="mat">原始矩阵</param>
        /// <param name="m">新矩阵的行数</param>
        /// <param name="n">新矩阵的列数</param>
        /// <returns></returns>
        /// <remarks>
        /// 在总元素数不变的前提下，改变矩阵的行数和列数。如果给的行
        /// 数和列数的乘积大于原矩阵总元素数，那么返回原矩阵的一个拷贝。
        /// </remarks>
        public static Matrix ReshapeCol(Matrix mat, int m, int n)
        {
            if ((m * n) <= (mat.Row * mat.Col))
            {
                Matrix mat2 = Matrix.Zeros(m, n);
                for (int i = 0; i < m * n; i++)
                    mat2[i] = mat[i];
                return mat2;
            }
            else
                return Matrix.Clone(mat);
        }

        /// <summary>  按列改变矩阵的行数和列数得到新型矩阵  </summary>
        /// <remarks>
        /// 在总元素数不变的前提下，改变矩阵的行数和列数，并且按列重置元素。
        /// </remarks>
        public void ReshapeCol(int newRow, int newCol)
        {
            if (newRow * newCol <= this.Row * this.Col)
            {
                double[,] newMat = new double[newRow, newCol];
                for (int j = 0; j < newCol; j++)
                    for (int i = 0; i < newRow; i++)
                        newMat[i, j] = this[j * newRow + i];
                this.Mat = newMat;
                this.row = newRow;
                this.col = newCol;
            }
        }

        /// <summary>  按行改变矩阵的行数和列数得到新矩阵  </summary>
        /// <param name="mat">原始矩阵</param>
        /// <param name="m">新矩阵的行数</param>
        /// <param name="n">新矩阵的列数</param>
        /// <returns></returns>
        /// <remarks>
        /// 在总元素数不变的前提下，改变矩阵的行数和列数。如果给的行
        /// 数和列数的乘积大于原矩阵总元素数，那么返回原矩阵的一个拷贝。
        public static Matrix ReshapeRow(Matrix mat, int newRow, int newCol)
        {
            if (newRow * newCol <= mat.Row * mat.Col)
            {
                Matrix newmat = Matrix.Zeros(newRow, newCol);
                for (int i = 0; i < newRow * newCol; i++)
                    newmat[i, -1] = mat[i, -1];
                return newmat;
            }
            else
                return Matrix.Clone(mat);
        }

        /// <summary>  按行改变矩阵的行数和列数得到新型矩阵  </summary>
        /// <remarks>
        /// 在总元素数不变的前提下，改变矩阵的行数和列数，并且按行重置元素。
        /// </remarks>
        public void ReshapeRow(int newRow, int newCol)
        {

            if (newRow * newCol <= this.Row * this.Col)
            {
                double[,] newMat = new double[newRow, newCol];
                for (int i = 0; i < newRow; i++)
                    for (int j = 0; j < newCol; j++)
                        newMat[i, j] = this[i * newCol + j, -1];
                this.Mat = newMat;
                this.row = newRow;
                this.col = newCol;
            }
        }

        /// <summary>  按行把矩阵拉直成行矩阵  </summary>
        public static Matrix StraightRow(Matrix mat)
        {
            Matrix newmat = new Matrix(1, mat.Row * mat.Col);
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                    newmat[i * mat.Col + j] = mat.Mat[i, j];
            return newmat;
        }

        /// <summary>  按列把矩阵拉直成列矩阵  </summary>
        public static Matrix StraightCol(Matrix mat)
        {
            Matrix newmat = new Matrix(mat.Row * mat.Col, 1);
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                    newmat[j * mat.Row + i] = mat.Mat[i, j];
            return newmat;
        }

        /// <summary>  生成间隔为1的行矩阵  </summary>
        public static Matrix Colon(double a, double b)
        {
            if (a > b) throw new MatrixException(".Colon(int,int)>中的下界大于上界");
            Matrix mat = new Matrix(1, (int)Math.Floor(b - a + 1));
            for (int i = 0; i < mat.Col; i++)
                mat.Mat[0, i] = i + a;
            return mat;
        }

        /// <summary>  生成指定间隔的行矩阵  </summary>
        public static Matrix Colon(double a, double b, double inc)
        {
            if (inc != 0)
            {
                if (inc > 0)
                {
                    if (a > b) throw new MatrixException(".Colon(double,double,double)>中的下界大于上届");
                }
                else
                {
                    if (a < b) throw new MatrixException(".Colon(double,double,double)>中的下界小于上届");
                }
                Matrix mat = new Matrix(1, (int)Math.Floor((b - a) / inc + 1));
                for (int i = 0; i < mat.Col; i++)
                    mat.Mat[0, i] = i * inc + a;
                return mat;
            }
            else throw new MatrixException(".Colon(double,double,double)>间隔为0");
        }

        /// <summary>  均匀生成指定的两个数之间指定数目的数字的行矩阵  </summary>
        /// <param name="n">采样点个数</param>
        public static Matrix Linspace(double a, double b, int n)
        {
            if (n <= 0) throw new MatrixException(".Linspace(double,double,int)>采样点个数非正");
            return Matrix.Colon(a, b, (b - a) / (n - 1));
        }

        /// <summary>  在设定总数的情况下，经“常用对数”采样生成行矩阵  </summary>
        public static Matrix Logspace(double a, double b, int n)
        {
            return 10 ^ Matrix.Linspace(a, b, n);
        }

        /// <summary>  按行组合两个矩阵  </summary>
        /// <param name="mat1">前矩阵</param>
        /// <param name="mat2">后矩阵</param>
        /// <param name="mat3">接着的任意个（没有也可）矩阵</param>
        /// <remarks>如果矩阵不同行，那么新矩阵的行取较大的行，空缺处用0填补</remarks>
        public static Matrix CombineRow(Matrix mat1, Matrix mat2, params Matrix[] mat3)
        {
            int maxrow;  //所有矩阵的行的最大值
            int sumcol;  //所有矩阵的列的和
            //找出所有矩阵的行的最大值与所有矩阵的列的和
            if (mat1.Row > mat2.Row) maxrow = mat1.Row;
            else maxrow = mat2.Row;
            sumcol = mat1.Col + mat2.Col;
            for (int i = 0; i < mat3.Length; i++)
            {
                if (maxrow < mat3[i].Row) maxrow = mat3[i].Row;
                sumcol += mat3[i].Col;
            }
            //创建新矩阵
            Matrix mat = Matrix.Zeros(maxrow, sumcol);
            //构造新矩阵
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat.Mat[i, j] = mat1.Mat[i, j];
            int n = mat1.Col;
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat.Mat[i, j + n] = mat2.Mat[i, j];
            n += mat2.Col;
            for (int k = 0; k < mat3.Length; k++)
            {
                for (int i = 0; i < mat3[k].Row; i++)
                    for (int j = 0; j < mat3[k].Col; j++)
                        mat.Mat[i, j + n] = mat3[k].Mat[i, j];
                n += mat3[k].Col;
            }
            return mat;
        }

        /// <summary>  按列组合两个矩阵  </summary>
        /// <param name="mat1">上矩阵</param>
        /// <param name="mat2">下矩阵</param>
        /// <param name="mat3">接着的任意个（没有也可）矩阵</param>
        /// <remarks>如果矩阵不同列，那么新矩阵的列取较大的列，空缺处用0填补
        /// </remarks>
        public static Matrix CombineCol(Matrix mat1, Matrix mat2, params Matrix[] mat3)
        {
            int maxcol;  //所有矩阵的列的最大值
            int sumrow;  //所有矩阵的行的和
            //找出所有矩阵的列的最大值与所有矩阵的行的和
            if (mat1.Col > mat2.Col) maxcol = mat1.Col;
            else maxcol = mat2.Col;
            sumrow = mat1.Row + mat2.Row;
            for (int k = 0; k < mat3.Length; k++)
            {
                if (maxcol < mat3[k].Col) maxcol = mat3[k].Col;
                sumrow += mat3[k].Row;
            }
            //创建新矩阵
            Matrix mat = Matrix.Zeros(sumrow, maxcol);
            //构造新矩阵
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat.Mat[i, j] = mat1.Mat[i, j];
            int m = mat1.Row;
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat.Mat[i + m, j] = mat2.Mat[i, j];
            m += mat2.Row;
            for (int k = 0; k < mat3.Length; k++)
            {
                for (int i = 0; i < mat3[k].Row; i++)
                    for (int j = 0; j < mat3[k].Col; j++)
                        mat.Mat[i + m, j] = mat3[k].Mat[i, j];
                m += mat3[k].Row;
            }
            return mat;
        }

        /// <summary>  两矩阵对应列作内积，要求两矩阵同型。  </summary>
        /// <param name="mat1">矩阵1</param>
        /// <param name="mat2">矩阵2</param>
        /// <returns>按时地方 </returns>
        /// <remarks>得到一行向量，其维数是和两矩阵的列数</remarks>
        public static Matrix Dot(Matrix mat1, Matrix mat2)
        {
            Matrix mat = new Matrix(1, mat1.Col);   //分配新的行矩阵（只含1行）
            double sum;
            for (int i = 0; i < mat.Col; i++)
            {
                sum = 0.0;      //求和变量，置0
                for (int j = 0; j < mat1.Row; j++)
                    sum += (mat1.Mat[j, i] * mat2.Mat[j, i]);
                mat.Mat[0, i] = sum;
            }
            return mat;
        }

        /// <summary>  史密特正交化算法  </summary>
        /// <param name="mat">待正交化的线性无关向量组按列构成的矩阵</param>
        /// <returns></returns>
        public static Matrix Schmidt(Matrix mat)
        {
            if (Matrix.Rank(mat) < mat.Col)
                throw new MatrixException("不是线性无关的向量组");
            Matrix mat2 = new Matrix(mat.Row, mat.Col);
            mat2.SetCol(mat.GetCol(0), 0);
            for (int i = 1; i < mat.Col; i++)
            {
                Matrix vec1, vec2;
                vec2 = vec1 = mat.GetCol(i);
                for (int j = 0; j < i; j++)
                {
                    vec2 -= Matrix.Prj(vec1, mat2.GetCol(j));
                }
                mat2.SetCol(vec2, i);
            }
            return mat2;
        }

        /// <summary>  向量到向量的投影向量  </summary>
        /// <param name="mat1">投影向量</param>
        /// <param name="mat2">被投影向量</param>
        /// <returns></returns>
        public static Matrix Prj(Matrix mat1, Matrix mat2)
        {
            if (mat1.Row != mat2.Row)
                throw new MatrixException("向量不同维");
            Matrix mat = new Matrix(mat1.Row, 1);
            double v1 = 0, v2 = 0;
            for (int i = 0; i < mat.Row; i++)
            {
                double v;
                v = mat2.Mat[i, 0];
                v1 += (mat1.Mat[i, 0] * v);
                v2 += (v * v);
            }
            if (v2 == 0.0) throw new MatrixException("被投影向量是零向量");
            v1 /= v2;
            for (int i = 0; i < mat.Row; i++)
                mat.Mat[i, 0] = v1 * mat2.Mat[i, 0];
            return mat;
        }

        /// <summary>  生成全1矩阵  </summary>
        /// <param name="row">待生成矩阵的行数</param>
        /// <param name="col">待生成矩阵的列数</param>
        /// <returns>全1矩阵</returns>
        public static Matrix Ones(int row, int col)
        {
            Matrix mat = new Matrix(row, col);   //分配新的矩阵
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    mat.Mat[i, j] = 1;
            return mat;
        }

        /// <summary>  生成全1矩阵  </summary>
        /// <param name="n">待生成方阵的阶数</param>
        /// <returns>n阶全1方阵</returns>
        public static Matrix Ones(int n)
        {
            Matrix mat = new Matrix(n, n);    //分配新的矩阵
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    mat.Mat[i, j] = 1;
            return mat;
        }

        /// <summary>  返回准单位阵  </summary>
        /// <param name="row">矩阵行数</param>
        /// <param name="col">矩阵列数</param>
        /// <returns></returns>
        /// <remarks>若不是方阵，只对行标和列标相等的元素赋1</remarks>
        public static Matrix Eye(int row, int col)
        {
            Matrix mat = new Matrix(row, col);
            if (row >= col)           //1的个数为col
            {
                for (int i = 0; i < col; i++)
                    mat.Mat[i, i] = 1;
            }
            else                 //1的个数为row
            {
                for (int i = 0; i < row; i++)
                    mat.Mat[i, i] = 1;
            }
            return mat;
        }

        /// <summary>  返回n阶单位方阵  </summary>
        /// <param name="n">方阵的阶数</param>
        /// <returns></returns>
        public static Matrix Eye(int n)
        {
            Matrix mat = new Matrix(n, n);
            for (int i = 0; i < n; i++)
                mat.Mat[i, i] = 1;
            return mat;
        }

        /// <summary>  生成全0矩阵  </summary>
        /// <param name="row">矩阵的行数</param>
        /// <param name="col">矩阵的列数</param>
        /// <returns></returns>
        public static Matrix Zeros(int row, int col)
        {
            Matrix mat = new Matrix(row, col);
            return mat;
        }

        /// <summary>  生成n阶全0方阵  </summary>
        /// <param name="n">矩阵的阶数</param>
        /// <returns></returns>
        public static Matrix Zeros(int n)
        {
            Matrix mat = new Matrix(n, n);
            return mat;
        }

        /// <summary>  生成0，1之间的随机数矩阵  </summary>
        /// <param name="row">矩阵的行数</param>
        /// <param name="col">矩阵的列数</param>
        /// <returns></returns>
        public static Matrix Rand(int row, int col)
        {
            Matrix mat = new Matrix(row, col);
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    mat.Mat[i, j] = rr.Rand01();
            return mat;
        }

        /// <summary>  生成0，1之间的随机数方阵  </summary>
        /// <param name="n">矩阵的阶数</param>
        /// <returns></returns>
        public static Matrix Rand(int n)
        {
            Matrix mat = new Matrix(n, n);  //分配一新矩阵
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    mat.Mat[i, j] = rr.Rand01();
            return mat;
        }

        /// <summary>  返回low与high之间的随机整数矩阵  </summary>
        /// <param name="row">矩阵的行数</param>
        /// <param name="col">矩阵的列数</param>
        /// <param name="low">随机整数的下界</param>
        /// <param name="high">随机整数的上界</param>
        /// <returns></returns>
        /// <remarks>随机整数包括上界和下界</remarks>
        public static Matrix RandInt(int row, int col, int low, int high)
        {
            Matrix mat = new Matrix(row, col);
            for (int j = 0; j < col; j++)
                for (int i = 0; i < row; i++)
                    mat.Mat[i, j] = rr.RandInt(low, high);
            return mat;
        }

        /// <summary>  返回low与high之间的不同随机整数的行矩阵  </summary>
        /// <param name="number">行矩阵的长度或列数</param>
        /// <param name="low">随机整数的下界</param>
        /// <param name="high">随机整数的上界</param>
        /// <returns></returns>
        /// <remarks>number应小于等于high-low+1，否则number被赋值为high-low+1</remarks>
        public static Matrix RandDifferInt(int number, int low, int high)
        {
            if (number > (high - low + 1)) number = high - low + 1;
            Matrix mat = new Matrix(1, number);
            mat.Mat[0, 0] = rr.RandInt(low, high);
            int randi = 0;  //存储中间过程产生的随机整数
            bool IsDiffer;//用于判断新产生的随机整数是否与以前产生的相同，若不同为真，否则为假
            for (int i = 1; i < number; i++)
            {
                while (true)
                {
                    randi = rr.RandInt(low, high);
                    IsDiffer = true;   //设定为真
                    for (int j = 0; j < i; j++)
                    {
                        if (randi == mat.Mat[0, j])
                        {
                            IsDiffer = false; //相同为假
                            break;
                        }
                    }
                    if (IsDiffer)
                    {
                        mat.Mat[0, i] = randi;
                        break;
                    }
                }
            }
            return mat;
        }

        /// <summary>  返回low与high之间的所有不同随机整数构成的行矩阵  </summary>
        /// <param name="low">随机整数的下界</param>
        /// <param name="high">随机整数的上界</param>
        /// <returns></returns>
        /// <remarks>行矩阵包括上下界间的所有随机整数</remarks>
        public static Matrix RandDifferInt(int low, int high)
        {
            return Matrix.RandDifferInt(high - low + 1, low, high);
        }

        // 返回0与high之间的high+1个不同的随机整数（包括0和high）
        /// <summary>  返回0与high之间的所有不同随机整数构成的行矩阵  </summary>
        /// <param name="high">随机整数的上界</param>
        /// <returns></returns>
        public static Matrix RandDifferInt(int high)
        {
            return Matrix.RandDifferInt(high + 1, 0, high);
        }

        /// <summary>  取出主对角线元素或者生成主对角矩阵  </summary>
        /// <param name="vec">向量或者矩阵</param>
        /// <returns></returns>
        /// <remarks>
        /// 矩阵的构造，vec是一个行矩阵或列矩阵，作为构造后矩阵的主对角线元素，
        /// 否则则取出输入矩阵的主对角线元素构成一个行矩阵返回
        /// </remarks>
        public static Matrix Diag(Matrix vec)
        {
            if (vec.Row == 1)   //vec是行矩阵
            {
                Matrix mat = new Matrix(vec.Col);
                for (int i = 0; i < vec.Col; i++)
                    mat.Mat[i, i] = vec.Mat[0, i];
                return mat;
            }
            else if (vec.Col == 1)         //vec是列矩阵
            {
                Matrix mat = new Matrix(vec.Row);
                for (int i = 0; i < vec.Row; i++)
                    mat.Mat[i, i] = vec.Mat[i, 0];
                return mat;
            }
            else
            {
                if (vec.Row < vec.Col)
                {
                    Matrix mat = new Matrix(1, vec.Row);
                    for (int i = 0; i < mat.Col; i++)
                        mat.Mat[0, i] = vec.Mat[i, i];
                    return mat;
                }
                else
                {
                    Matrix mat = new Matrix(1, vec.Col);
                    for (int i = 0; i < mat.Col; i++)
                        mat.Mat[0, i] = vec.Mat[i, i];
                    return mat;
                }
            }
        }

        /// <summary>  按主对角线把矩阵组合成分块对角矩阵  </summary>
        /// <param name="mat1">第一个矩阵</param>
        /// <param name="mat2">第二个矩阵</param>
        /// <param name="mat3">接着的任意个（没有也可）矩阵</param>
        public static Matrix Diag(Matrix mat1, Matrix mat2, params Matrix[] mat3)
        {
            int m, n;
            m = 0;    //除前两个矩阵外的所有矩阵行的和
            n = 0;    //除前两个矩阵外的所有矩阵列的和
            //求出m和n
            for (int i = 0; i < mat3.Length; i++)
            {
                m += mat3[i].Row;
                n += mat3[i].Col;
            }
            //创建新矩阵
            Matrix mat = Matrix.Zeros(mat1.Row + mat2.Row + m, mat1.Col + mat2.Col + n);
            //构造新矩阵
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat.Mat[i, j] = mat1.Mat[i, j];
            int row, col;
            row = mat1.Row;
            col = mat1.Col;
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat.Mat[row + i, col + j] = mat2.Mat[i, j];
            row += mat2.Row;
            col += mat2.Col;
            for (int k = 0; k < mat3.Length; k++)
            {
                for (int i = 0; i < mat3[k].Row; i++)
                    for (int j = 0; j < mat3[k].Col; j++)
                        mat.Mat[row + i, col + j] = mat3[k].Mat[i, j];
                row += mat3[k].Row;
                col += mat3[k].Col;
            }
            return mat;
        }

        /// <summary>  矩阵指定列向量的1范数  </summary>
        /// <param name="Colnum">指定列</param>
        /// <returns>该列的范数</returns>
        public double Norm1(int Colnum)
        {
            double sum = 0.0;  //求和变量
            for (int i = 0; i < this.Row; i++)
                sum += Math.Abs(Mat[i, Colnum]);
            return sum;
        }

        /// <summary> 矩阵的列和范数  </summary>
        public static double Norm1(Matrix mat)
        {
            Matrix vec = Matrix.SumCol(Matrix.Abs(mat));
            return (double)Matrix.MaxRow(vec);
        }

        /// <summary>  矩阵指定列向量的2范数  </summary>
        /// <param name="Colnum">指定列</param>
        /// <returns></returns>
        public double Norm2(int Colnum)
        {
            double sum = 0.0;
            for (int i = 0; i < this.Row; i++)
                sum += (Mat[i, Colnum] * Mat[i, Colnum]);
            return Math.Sqrt(sum);
        }

        /// <summary>  矩阵的谱范数  </summary>
        public static double Norm2(Matrix mat)
        {
            Matrix EigVec, EigVal;
            Matrix.EigJcb(Matrix.MulWino(mat), out EigVec, out EigVal);
            EigVal = Matrix.Diag(EigVal);
            return Math.Sqrt((double)Matrix.MaxRow(EigVal));
        }

        /// <summary>  矩阵的Frobenius范数（简称F-范数）  </summary>
        public static double NormF(Matrix mat)
        {
            double sum = 0;
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                    sum += (mat.Mat[i, j] * mat.Mat[i, j]);
            return Math.Sqrt(sum);
        }

        /// <summary>  矩阵指定列向量的Inf范数  </summary>
        /// <param name="Colnum">指定列</param>
        /// <returns></returns>
        public double NormInf(int Colnum)
        {
            double vv = double.NegativeInfinity;   //把vv赋值为负无穷大
            for (int i = 0; i < this.Row; i++)
            {
                double vv2 = Math.Abs(Mat[i, Colnum]);
                if (vv2 > vv) vv = vv2;
            }
            return vv;
        }

        /// <summary>  矩阵的行和范数  </summary>
        public static double NormInf(Matrix mat)
        {
            Matrix vec = Matrix.SumRow(Matrix.Abs(mat));
            return (double)Matrix.MaxCol(vec);
        }

        /// <summary>  求矩阵指定行向量的1范数  </summary>
        /// <param name="Rownum">指定行</param>
        /// <returns></returns>
        public double Norm1Row(int Rownum)
        {
            double sum = 0.0;  //求和变量
            for (int j = 0; j < this.Col; j++)
                sum += Math.Abs(Mat[Rownum, j]);
            return sum;
        }

        /// <summary>  求矩阵指定行向量的2范数  </summary>
        /// <param name="Rownum">指定行</param>
        /// <returns></returns>
        public double Norm2Row(int Rownum)
        {
            double sum = 0.0;
            for (int j = 0; j < this.Col; j++)
                sum += (Mat[Rownum, j] * Mat[Rownum, j]);
            return Math.Sqrt(sum);
        }

        /// <summary>  求矩阵指定行向量的Inf范数  </summary>
        /// <param name="Rownum">指定行</param>
        /// <returns></returns>
        public double NormInfRow(int Rownum)
        {
            double vv = double.NegativeInfinity;   //把vv赋值为负无穷大
            for (int j = 0; j < this.Col; j++)
            {
                double vv2 = Math.Abs(Mat[Rownum, j]);
                if (vv2 > vv) vv = vv2;
            }
            return vv;
        }

        /// <summary>  矩阵所有列的最大值  </summary>
        /// <remarks>返回一个行向量</remarks>
        public static Matrix MaxCol(Matrix mat)
        {
            Matrix vec = new Matrix(1, mat.Col);
            double maxv;
            for (int j = 0; j < vec.Col; j++)
            {
                maxv = double.NegativeInfinity;  //把maxvv赋值为负无穷大
                for (int i = 0; i < mat.Row; i++)
                    if (mat.Mat[i, j] > maxv)
                        maxv = mat.Mat[i, j];
                vec.Mat[0, j] = maxv;
            }
            return vec;
        }

        /// <summary>  矩阵所有列的最大值  </summary>
        /// <param name="Rownum">输出每一列最大值所在的行数</param>
        public static Matrix MaxCol(Matrix mat, out int[] Rownum)
        {
            Matrix vec = new Matrix(1, mat.Col);
            Rownum = new int[mat.Col];
            double maxv;
            for (int j = 0; j < vec.Col; j++)
            {
                maxv = double.NegativeInfinity;  //把maxvv赋值为负无穷大
                for (int i = 0; i < mat.Row; i++)
                    if (mat.Mat[i, j] > maxv)
                    {
                        maxv = mat.Mat[i, j];
                        Rownum[j] = i;
                    }
                vec.Mat[0, j] = maxv;
            }
            return vec;
        }

        /// <summary>  矩阵指定列向量的最大值  </summary>
        /// <param name="Colnum">指定列</param>
        /// <returns></returns>
        public static double MaxCol(Matrix mat, int Colnum)
        {
            double maxv = double.NegativeInfinity;  //把maxvv赋值为负无穷大
            for (int i = 0; i < mat.Row; i++)
                if (mat.Mat[i, Colnum] > maxv)
                    maxv = mat.Mat[i, Colnum];
            return maxv;
        }

        /// <summary>  矩阵指定列向量的最大值  </summary>
        /// <param name="Rownum">输出该指定列的最大值</param>
        public static double MaxCol(Matrix mat, int Colnum, out int Rownum)
        {
            double maxv = double.NegativeInfinity;  //把maxvv赋值为负无穷大
            Rownum = 0;
            for (int i = 0; i < mat.Row; i++)
            {
                if (mat.Mat[i, Colnum] > maxv)
                {
                    maxv = mat.Mat[i, Colnum];
                    Rownum = i;
                }
            }
            return maxv;
        }

        /// <summary>  矩阵所有列的最小值  </summary>
        /// <returns></returns>
        /// <remarks>一个行矩阵</remarks>
        public static Matrix MinCol(Matrix mat)
        {
            Matrix vec = new Matrix(1, mat.Col);
            double minv;
            for (int j = 0; j < vec.Col; j++)
            {
                minv = double.PositiveInfinity;  //把minv赋值为正无穷大
                for (int i = 0; i < mat.Row; i++)
                    if (mat.Mat[i, j] < minv)
                        minv = mat.Mat[i, j];
                vec.Mat[0, j] = minv;
            }
            return vec;
        }

        /// <summary>  矩阵所有列的最小值  </summary>
        /// <param name="position">每一列最小值所在的行号</param>
        /// <returns></returns>
        public static Matrix MinCol(Matrix mat, out int[] Rownum)
        {
            Matrix vec = new Matrix(1, mat.Col);
            Rownum = new int[mat.Col];
            double minv;
            for (int j = 0; j < vec.Col; j++)
            {
                minv = double.PositiveInfinity;  //把minv赋值为正无穷大
                for (int i = 0; i < mat.Row; i++)
                    if (mat.Mat[i, j] < minv)
                    {
                        minv = mat.Mat[i, j];
                        Rownum[j] = i;
                    }
                vec.Mat[0, j] = minv;
            }
            return vec;
        }

        /// <summary>  矩阵指定列向量的最小值  </summary>
        /// <param name="Colnum">指定列</param>
        /// <returns></returns>
        public static double MinCol(Matrix mat, int Colnum)
        {
            double minv = double.PositiveInfinity;  //把minv赋值为正无穷大
            for (int i = 0; i < mat.Row; i++)
                if (mat.Mat[i, Colnum] < minv)
                    minv = mat.Mat[i, Colnum];
            return minv;
        }

        /// <summary>  矩阵指定列向量的最小值  </summary>
        /// <param name="Colnum">指定列</param>
        /// <param name="position">该指定列最小值所在的行号</param>
        /// <returns></returns>
        public static double MinCol(Matrix mat, int Colnum, out int Rownum)
        {
            double minv = double.PositiveInfinity;  //把minv赋值为正无穷大
            Rownum = 0;
            for (int i = 0; i < mat.Row; i++)
                if (mat.Mat[i, Colnum] < minv)
                {
                    minv = mat.Mat[i, Colnum];
                    Rownum = i;
                }
            return minv;
        }

        /// <summary>  矩阵所有行的最大值  </summary>
        /// <remarks>一个列矩阵</remarks>
        public static Matrix MaxRow(Matrix mat)
        {
            Matrix vec = new Matrix(mat.Row, 1);
            double maxv;
            for (int i = 0; i < vec.Row; i++)
            {
                maxv = double.NegativeInfinity;  //把maxvv赋值为负无穷大
                for (int j = 0; j < mat.Col; j++)
                    if (mat.Mat[i, j] > maxv)
                        maxv = mat.Mat[i, j];
                vec.Mat[i, 0] = maxv;
            }
            return vec;
        }

        /// <summary>  矩阵所有行的最大值  </summary>
        /// <param name="Colnum">输出矩阵每一行的最大值所在的列数</param>
        public static Matrix MaxRow(Matrix mat, out int[] Colnum)
        {
            Matrix vec = new Matrix(mat.Row, 1);
            Colnum = new int[mat.Row];
            double maxv;
            for (int i = 0; i < vec.Row; i++)
            {
                maxv = double.NegativeInfinity;  //把maxvv赋值为负无穷大
                for (int j = 0; j < mat.Col; j++)
                    if (mat.Mat[i, j] > maxv)
                    {
                        maxv = mat.Mat[i, j];
                        Colnum[i] = j;
                    }
                vec.Mat[i, 0] = maxv;
            }
            return vec;
        }

        /// <summary>  求矩阵指定行向量的最大值  </summary>
        /// <param name="Rownum">指定的行</param>
        /// <returns></returns>
        public static double MaxRow(Matrix mat, int Rownum)
        {
            double maxv = double.NegativeInfinity;  //把maxvv赋值为负无穷大
            for (int i = 0; i < mat.Col; i++)
                if (mat.Mat[Rownum, i] > maxv)
                    maxv = mat.Mat[Rownum, i];
            return maxv;
        }

        /// <summary>  求矩阵指定行向量的最大值  </summary>
        public static double MaxRow(Matrix mat, int Rownum, out int Colnum)
        {
            double maxv = double.NegativeInfinity;  //把maxvv赋值为负无穷大
            Colnum = 0;
            for (int i = 0; i < mat.Col; i++)
                if (mat.Mat[Rownum, i] > maxv)
                {
                    maxv = mat.Mat[Rownum, i];
                    Colnum = i;
                }
            return maxv;
        }

        /// <summary>  矩阵所有行的最小值  </summary>
        /// <remarks>一个列矩阵</remarks>
        public static Matrix MinRow(Matrix mat)
        {
            Matrix vec = new Matrix(mat.Row, 1);
            double minv;
            for (int i = 0; i < vec.Row; i++)
            {
                minv = double.PositiveInfinity;
                for (int j = 0; j < mat.Col; j++)
                    if (mat.Mat[i, j] < minv)
                        minv = mat.Mat[i, j];
                vec.Mat[i, 0] = minv;
            }
            return vec;
        }

        /// <summary>  矩阵所有行的最小值  </summary>
        public static Matrix MinRow(Matrix mat, out int[] Colnum)
        {
            Matrix vec = new Matrix(mat.Row, 1);
            Colnum = new int[mat.Row];
            double minv;
            for (int i = 0; i < vec.Row; i++)
            {
                minv = double.PositiveInfinity;
                for (int j = 0; j < mat.Col; j++)
                    if (mat.Mat[i, j] < minv)
                    {
                        minv = mat.Mat[i, j];
                        Colnum[i] = j;
                    }
                vec.Mat[i, 0] = minv;
            }
            return vec;
        }

        /// <summary>  求矩阵指定行向量的最小值  </summary>
        /// <param name="Rownum">指定的行</param>
        /// <returns></returns>
        public static double MinRow(Matrix mat, int Rownum)
        {
            double minv = double.PositiveInfinity;  //把minv赋值为正无穷大
            for (int i = 0; i < mat.Col; i++)
                if (mat.Mat[Rownum, i] < minv)
                    minv = mat.Mat[Rownum, i];
            return minv;
        }

        /// <summary>  求矩阵指定行向量的最小值  </summary>
        /// <param name="Rownum">指定的行</param>
        /// <param name="position">该指定行最小值所在的列号</param>
        public static double MinRow(Matrix mat, int Rownum, out int Colnum)
        {
            double minv = double.PositiveInfinity;  //把minv赋值为正无穷大
            Colnum = 0;
            for (int i = 0; i < mat.Col; i++)
                if (mat.Mat[Rownum, i] < minv)
                {
                    minv = mat.Mat[Rownum, i];
                    Colnum = i;
                }
            return minv;
        }

        /// <summary>  矩阵列求和  </summary>
        /// <param name="Colnum">指定的列</param>
        /// <returns></returns>
        public static double SumCol(Matrix mat, int Colnum)
        {
            double sum = 0.0;
            for (int i = 0; i < mat.Row; i++)
                sum += mat.Mat[i, Colnum];
            return sum;
        }

        /// <summary>  矩阵所有列求和  </summary>
        /// <returns></returns>
        /// <remarks>返回一个行矩阵</remarks>
        public static Matrix SumCol(Matrix mat)
        {
            Matrix summat = new Matrix(1, mat.Col);
            double sum;
            for (int j = 0; j < mat.Col; j++)
            {
                sum = 0.0;
                for (int i = 0; i < mat.Row; i++)
                    sum += mat.Mat[i, j];
                summat.Mat[0, j] = sum;
            }
            return summat;
        }

        /// <summary>  矩阵行向量求和  </summary>
        /// <param name="Rownum">指定的行</param>
        /// <returns></returns>
        public static double SumRow(Matrix mat, int Rownum)
        {
            double sum = 0.0;
            for (int j = 0; j < mat.Col; j++)
                sum += mat.Mat[Rownum, j];
            return sum;
        }

        /// <summary>  矩阵所有行求和  </summary>
        /// <returns></returns>
        /// <remarks>返回一个列矩阵</remarks>
        public static Matrix SumRow(Matrix mat)
        {
            Matrix Rowmat = new Matrix(mat.Row, 1);
            double sum;
            for (int i = 0; i < mat.Row; i++)
            {
                sum = 0;
                for (int j = 0; j < mat.Col; j++)
                    sum += mat.Mat[i, j];
                Rowmat.Mat[i, 0] = sum;
            }
            return Rowmat;
        }

        /// <summary>  求矩阵的列向量的平均值  </summary>
        /// <param name="Colnum">指定的列</param>
        /// <returns>平均值</returns>
        public static double MeanCol(Matrix mat, int Colnum)
        {
            double sum = 0.0;
            for (int i = 0; i < mat.Row; i++)
                sum += mat.Mat[i, Colnum];
            sum /= (double)mat.Row;
            return sum;
        }

        /// <summary>  矩阵所有列向量的平均值  </summary>
        /// <returns></returns>
        /// <remarks>返回行矩阵</remarks>
        public static Matrix MeanCol(Matrix mat)
        {
            Matrix mean = new Matrix(1, mat.Col);
            double sum;
            for (int i = 0; i < mat.Col; i++)
            {
                sum = 0;
                for (int j = 0; j < mat.Row; j++)
                    sum += mat.Mat[j, i];
                sum /= (double)mat.Row;
                mean.Mat[0, i] = sum;
            }
            return mean;
        }

        //---------------------------------
        //求矩阵的行向量的平均值
        //---------------------------------
        /// <summary>  矩阵指定行向量的平均值  </summary>
        public static double MeanRow(Matrix mat, int Rownum)
        {
            double sum = 0.0;
            for (int i = 0; i < mat.Col; i++)
                sum += mat.Mat[Rownum, i];
            sum /= (double)mat.Col;
            return sum;
        }

        /// <summary>  矩阵所有行向量的平均值，返回列矩阵  </summary>
        public static Matrix MeanRow(Matrix mat)
        {
            Matrix mean = new Matrix(mat.Row, 1);
            double sum;
            for (int i = 0; i < mat.Row; i++)
            {
                sum = 0;
                for (int j = 0; j < mat.Col; j++)
                    sum += mat.Mat[i, j];
                sum /= (double)mat.Col;
                mean.Mat[i, 0] = sum;
            }
            return mean;
        }

        /// <summary>  求矩阵的迹  </summary>
        /// <remarks>一般是对方阵而言，若不是方阵，就直接对主对角线元素求和即可</remarks>
        public static double Trace(Matrix mat)
        {
            double sum = 0;
            if (mat.Row <= mat.Col)
            {
                for (int i = 0; i < mat.Row; i++)
                    sum += mat.Mat[i, i];
                return sum;
            }
            else
            {
                for (int i = 0; i < mat.Col; i++)
                    sum += mat.Mat[i, i];
                return sum;
            }
        }

        //----------------------------------------
        //两矩阵的加、减、数乘和乘法运算
        //----------------------------------------
        /// <summary>  求矩阵的负矩阵  </summary>
        public static Matrix operator -(Matrix mat2)
        {
            Matrix mat1 = new Matrix(mat2.Row, mat2.Col);
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat1.Mat[i, j] = -mat2.Mat[i, j];
            return mat1;
        }

        /// <summary>  两矩阵相加  </summary>
        public static Matrix operator +(Matrix mat1, Matrix mat2)
        {
            if (mat1.Row != mat2.Row || mat1.Col != mat2.Col)
                throw new MatrixException("相加的两矩阵不同型");
            Matrix mat = new Matrix(mat1.Row, mat1.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat.Mat[i, j] = mat1.Mat[i, j] + mat2.Mat[i, j];
            return mat;
        }

        /// <summary>  两矩阵相减  </summary>
        public static Matrix operator -(Matrix mat1, Matrix mat2)
        {
            Matrix mat = new Matrix(mat1.Row, mat1.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat.Mat[i, j] = mat1.Mat[i, j] - mat2.Mat[i, j];
            return mat;
        }

        /// <summary>  矩阵右加一个数  </summary>
        public static Matrix operator +(Matrix mat1, double v)
        {
            Matrix mat = Matrix.Clone(mat1);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat.Mat[i, j] += v;
            return mat;
        }

        /// <summary>  矩阵左加一个数  </summary>
        public static Matrix operator +(double v, Matrix mat1)
        {
            Matrix mat = Matrix.Clone(mat1);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat.Mat[i, j] += v;
            return mat;
        }

        /// <summary>  矩阵的某一指定列自身加上一个数  </summary>
        public void PlusCol(double Value, int colnum)
        {
            for (int i = 0; i < this.Row; i++)
                this.Mat[i, colnum] += Value;
            return;
        }

        /// <summary>  矩阵的每一列自身加上一个行矩阵对应每一列的数  </summary>
        public void PlusCol(Matrix vmat)
        {
            double vv;
            for (int j = 0; j < this.Col; j++)
            {
                vv = vmat.Mat[0, j];
                for (int i = 0; i < this.Row; i++)
                    this.Mat[i, j] += vv;
            }
            return;
        }

        /// <summary>  矩阵的某一指定行自身加上一个数  </summary>
        public void PlusRow(double Value, int rownum)
        {
            for (int j = 0; j < this.Col; j++)
                this.Mat[rownum, j] += Value;
            return;
        }

        /// <summary>  矩阵的每一行自身加上一个列矩阵对应每一行的数  </summary>
        public void PlusRow(Matrix vmat)
        {
            double vv;
            for (int i = 0; i < this.Row; i++)
            {
                vv = vmat.Mat[i, 0];
                for (int j = 0; j < this.Col; j++)
                    this.Mat[i, j] += vv;
            }
            return;
        }

        /// <summary>  矩阵右减一个数  </summary>
        public static Matrix operator -(Matrix mat1, double v)
        {
            Matrix mat = Matrix.Clone(mat1);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat.Mat[i, j] -= v;
            return mat;
        }

        /// <summary>  矩阵左减一个数  </summary>
        public static Matrix operator -(double v, Matrix mat1)
        {
            Matrix mat = Matrix.Clone(mat1);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat.Mat[i, j] = v - mat.Mat[i, j];
            return mat;
        }

        /// <summary>  矩阵的某一指定列自身减去一个数  </summary>
        public void MinusCol(double Value, int colnum)
        {
            for (int i = 0; i < this.Row; i++)
                this.Mat[i, colnum] -= Value;
            return;
        }

        /// <summary>  矩阵的每一列自身减去一个行矩阵对应每一列的数  </summary>
        public void MinusCol(Matrix vmat)
        {
            double vv;
            for (int j = 0; j < this.Col; j++)
            {
                vv = vmat.Mat[0, j];
                for (int i = 0; i < this.Row; i++)
                    this.Mat[i, j] -= vv;
            }
            return;
        }

        /// <summary>  矩阵的某一指定行的每个数自身减去一个数  </summary>
        public void MinusRow(double Value, int rownum)
        {
            for (int j = 0; j < this.Col; j++)
                this.Mat[rownum, j] -= Value;
            return;
        }

        /// <summary>  矩阵的每一行的每个数自身都减去一个列矩阵对应该行的数  </summary>
        public void MinusRow(Matrix vmat)
        {
            double vv;
            for (int i = 0; i < this.Row; i++)
            {
                vv = vmat.Mat[i, 0];
                for (int j = 0; j < this.Col; j++)
                    this.Mat[i, j] -= vv;
            }
            return;
        }

        /// <summary>  矩阵右乘一个数  </summary>
        public static Matrix operator *(double lamda, Matrix mat2)
        {
            Matrix mat1 = new Matrix(mat2.Row, mat2.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat1.Mat[i, j] = mat2.Mat[i, j] * lamda;
            return mat1;
        }

        /// <summary>  矩阵左乘一个数  </summary>
        public static Matrix operator *(Matrix mat2, double lamda)
        {
            Matrix mat1 = new Matrix(mat2.Row, mat2.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat1.Col; j++)
                    mat1.Mat[i, j] = mat2.Mat[i, j] * lamda;
            return mat1;
        }

        /// <summary>  矩阵的某一指定列自身乘以一个数  </summary>
        public void MultiplyCol(double Value, int colnum)
        {
            for (int i = 0; i < this.Row; i++)
                this.Mat[i, colnum] *= Value;
            return;
        }


        /// <summary>  矩阵的每一列自身乘以一个行矩阵对应每一列的数  </summary>
        public void MultiplyCol(Matrix vmat)
        {
            double vv;
            for (int j = 0; j < this.Col; j++)
            {
                vv = vmat.Mat[0, j];
                for (int i = 0; i < this.Row; i++)
                    this.Mat[i, j] *= vv;
            }
            return;
        }

        /// <summary>  矩阵的某一指定行自身乘以一个数  </summary>
        public void MultiplyRow(double Value, int rownum)
        {
            for (int j = 0; j < this.Col; j++)
                this.Mat[rownum, j] *= Value;
            return;
        }

        /// <summary>  矩阵的每一行自身乘以一个列矩阵对应每一行的数  </summary>
        public void MultiplyRow(Matrix vmat)
        {
            double vv;
            for (int i = 0; i < this.Row; i++)
            {
                vv = vmat.Mat[i, 0];
                for (int j = 0; j < this.Col; j++)
                    this.Mat[i, j] *= vv;
            }
            return;
        }

        /// <summary>  矩阵左除一个数  </summary>
        public static Matrix operator /(Matrix mat2, double lamda)
        {
            return 1.0 / lamda * mat2;
        }

        /// <summary>  矩阵右除一个数  </summary>
        public static Matrix operator /(double lamda, Matrix mat2)
        {
            Matrix mat = new Matrix(mat2.Row, mat2.Col);
            for (int i = 0; i < mat2.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                    mat.Mat[i, j] = lamda / mat2.Mat[i, j];
            return mat;
        }

        /// <summary>  矩阵的某一指定列自身除以一个数  </summary>
        public void DivideCol(double Value, int colnum)
        {
            for (int i = 0; i < this.Row; i++)
                this.Mat[i, colnum] /= Value;
            return;
        }

        /// <summary>  矩阵的每一列自身除以一个行矩阵对应每一列的数  </summary>
        public void DivideCol(Matrix vmat)
        {
            double vv;
            for (int j = 0; j < this.Col; j++)
            {
                vv = vmat.Mat[0, j];
                if (vv == 0) throw new MatrixException("除数不能为0");
                for (int i = 0; i < this.Row; i++)
                    this.Mat[i, j] /= vv;
            }
            return;
        }

        /// <summary>  矩阵的某一指定行自身除以一个数  </summary>
        public void DivideRow(double Value, int rownum)
        {
            for (int j = 0; j < this.Col; j++)
                this.Mat[rownum, j] /= Value;
            return;
        }

        /// <summary  矩阵的每一行自身除以一个列矩阵对应每一行的数  </summary>
        public void DivideRow(Matrix vmat)
        {
            double vv;
            for (int i = 0; i < this.Row; i++)
            {
                vv = vmat.Mat[i, 0];
                for (int j = 0; j < this.Col; j++)
                    this.Mat[i, j] /= vv;
            }
            return;
        }

        /// <summary>  返回矩阵指定列的所有元素的积  </summary>
        public static double ProductCol(Matrix mat, uint colnum)
        {
            if (colnum >= mat.Col)
                throw new MatrixException(".ProductCol(Matrix,uint)>第二个参数越界");
            double p = mat.Mat[0, colnum];
            for (int i = 1; i < mat.Row; i++)
                p *= mat[i, (int)colnum];
            return p;
        }

        /// <summary>  返回矩阵每列所有元素的积，得到一个行矩阵  </summary>
        public static Matrix ProductCol(Matrix mat)
        {
            Matrix vec = Matrix.Ones(1, mat.Col);
            for (int j = 0; j < mat.Col; j++)
                for (int i = 0; i < mat.Row; i++)
                    vec[j] *= mat.Mat[i, j];
            return vec;
        }

        /// <summary>  返回矩阵指定行的所有元素的积  </summary>
        public static Matrix ProductRow(Matrix mat, uint rownum)
        {
            if (rownum >= mat.Row)
                throw new MatrixException(".ProductCol(Matrix,uint)>第二个参数越界");
            double p = mat.Mat[rownum, 0];
            for (int j = 1; j < mat.Col; j++)
                p *= mat.Mat[rownum, j];
            return p;
        }

        /// <summary>  返回矩阵每行所有元素的积，得到一个列矩阵  </summary>
        public static Matrix ProductRow(Matrix mat)
        {
            Matrix vec = Matrix.Ones(mat.Row, 1);
            for (int i = 0; i < mat.Row; i++)
                for (int j = 0; j < mat.Col; j++)
                    vec.Mat[i, 0] *= mat.Mat[i, j];
            return vec;
        }

        /// <summary>  返回矩阵主对角线元素的积  </summary>
        public static double ProductDiag(Matrix mat)
        {
            double p = mat.Mat[0, 0];
            for (int i = 1; i < Math.Min(mat.Col, mat.Row); i++)
                p *= mat.Mat[i, i];
            return p;
        }

        /// <summary>  两矩阵相乘  </summary>
        public static Matrix operator *(Matrix mat1, Matrix mat2)
        {
            Matrix mat = new Matrix(mat1.Row, mat2.Col);
            for (int i = 0; i < mat1.Row; i++)
                for (int j = 0; j < mat2.Col; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < mat1.Col; k++)
                    {
                        //sum+=(mat1.GetValue(i,k)*mat2.GetValue(k,j));
                        sum += mat1.Mat[i, k] * mat2.Mat[k, j];
                    }
                    mat.Mat[i, j] = sum;
                }
            return mat;
        }

        /// <summary>  两矩阵相乘的  </summary>
        public static Matrix MulWino(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.Row, B.Col);
            Matrix t = new Matrix(1, A.Row);
            Matrix s = new Matrix(1, B.Col);
            int p = A.Col / 2;
            double sum;
            for (int i = 0; i < A.Row; i++)
            {
                sum = 0;
                for (int j = 0; j < 2 * p; j += 2)
                    sum += A.Mat[i, j] * A.Mat[i, j + 1];
                t.Mat[0, i] = sum;
            }
            for (int i = 0; i < B.Col; i++)
            {
                sum = 0;
                for (int j = 0; j < 2 * p; j += 2)
                    sum += B.Mat[j, i] * B.Mat[j + 1, i];
                s.Mat[0, i] = sum;
            }
            int k;
            double tv;
            for (int i = 0; i < A.Row; i++)
            {
                tv = t.Mat[0, i];
                for (int j = 0; j < B.Col; j++)
                {
                    sum = 0;
                    for (k = 0; k < 2 * p; k += 2)
                        sum += (A.Mat[i, k] + B.Mat[k + 1, j]) * (A.Mat[i, k + 1] + B.Mat[k, j]);
                    C.Mat[i, j] = sum - tv - s.Mat[0, j];
                }
            }
            if ((A.Col % 2) != 0)
            {
                for (int i = 0; i < A.Row; i++)
                    for (int j = 0; j < B.Col; j++)
                        C.Mat[i, j] += A.Mat[i, A.Col - 1] * B.Mat[A.Col - 1, j];
            }
            return C;
        }

        /// <summary>  矩阵与其转置矩阵的乘积矩阵  </summary>
        public static Matrix MulWino(Matrix A)
        {
            Matrix C = new Matrix(A.Col);
            Matrix t = new Matrix(1, A.Col);
            int p = A.Row / 2;
            double sum;
            for (int i = 0; i < A.Col; i++)
            {
                sum = 0;
                for (int j = 0; j < 2 * p; j += 2)
                    sum += A.Mat[j, i] * A.Mat[j + 1, i];
                t.Mat[0, i] = sum;
            }
            int k;
            double tv;
            for (int i = 0; i < A.Col; i++)
            {
                tv = t.Mat[0, i];
                for (int j = i; j < A.Col; j++)
                {
                    sum = 0;
                    for (k = 0; k < 2 * p; k += 2)
                        sum += (A.Mat[k, i] + A.Mat[k + 1, j]) * (A.Mat[k + 1, i] + A.Mat[k, j]);
                    C.Mat[i, j] = sum - tv - t.Mat[0, j];
                }
            }
            if ((A.Row % 2) != 0)
            {
                for (int i = 0; i < A.Col; i++)
                    for (int j = i; j < A.Col; j++)
                        C.Mat[i, j] += A.Mat[A.Col - 1, i] * A.Mat[A.Col - 1, j];
            }
            for (int i = 0; i < A.Col; i++)
                for (int j = i + 1; j < A.Col; j++)
                    C.Mat[j, i] = C.Mat[i, j];
            return C;
        }

        /// <summary>  求前矩阵的主对角元素构成的方阵与后矩阵的乘积  </summary>
        public static Matrix MulFrontDiag(Matrix A, Matrix B)
        {
            if (A.Row > A.Col)
            {
                if (A.Col != B.Row) throw new MatrixException("前矩阵的主对角元素构成的方阵的阶数与后矩阵的行数不一致");
            }
            else
            {
                if (A.Row != B.Row) throw new MatrixException("前矩阵的主对角元素构成的方阵的阶数与后矩阵的行数不一致");
            }
            Matrix C = new Matrix(B.Row, B.Col);
            double a = 0;
            for (int i = 0; i < B.Row; i++)
            {
                a = A.Mat[i, i];
                for (int j = 0; j < B.Col; j++)
                    C.Mat[i, j] = a * B.Mat[i, j];
            }
            return C;
        }

        /// <summary>  求前矩阵与后矩阵的主对角元素构成的方阵的乘积  </summary>
        public static Matrix MulBackDiag(Matrix A, Matrix B)
        {
            if (B.Row > B.Col)
            {
                if (A.Col != B.Col) throw new MatrixException("前矩阵的列数与后矩阵的主对角元素构成的方阵的阶数不一致");
            }
            else
            {
                if (A.Col != B.Row) throw new MatrixException("前矩阵的列数与后矩阵的主对角元素构成的方阵的阶数不一致");
            }
            Matrix C = new Matrix(A.Row, A.Col);
            double a = 0;
            for (int j = 0; j < A.Col; j++)
            {
                a = B.Mat[j, j];
                for (int i = 0; i < A.Row; i++)
                    C.Mat[i, j] = a * A.Mat[i, j];
            }
            return C;
        }

        /// <summary>  用乘幂法求实方阵的最大特征值及其特征向量  </summary>
        public static Matrix EigPower(Matrix A, Matrix vv0, double epsl, out double eigvalue)
        {
            Matrix v0 = Matrix.Clone(vv0);//为了不造成vv0被改变，克隆至v0
            Matrix u1 = A * v0;
            Matrix v1 = u1 / u1.NormInf(0);
            Matrix u0 = A * v1;
            v0 = u0 / u0.NormInf(0);
            bool IsOdd = true;
            while (true)
            {
                if (IsOdd)
                {
                    u1 = A * v0;
                    v1 = u1 / u1.NormInf(0);
                    IsOdd = false;
                }
                else
                {
                    u0 = A * v1;
                    v0 = u0 / u0.NormInf(0);
                    IsOdd = true;
                }
                Matrix u = (u0 - u1);
                if (u.NormInf(0) <= epsl)
                {
                    if (IsOdd)
                    {
                        if (Matrix.Dot(v0, v1).Mat[0, 0] > 0)
                            eigvalue = u0.NormInf(0);
                        else
                            eigvalue = -(u0.NormInf(0));
                        return u0;
                    }
                    else
                    {
                        if (Matrix.Dot(v0, v1).Mat[0, 0] > 0)
                            eigvalue = u0.NormInf(0);
                        else
                            eigvalue = -(u0.NormInf(0));
                        return u1;
                    }
                }
            }
        }

        /// <summary>  用雅可比过关法求实对称矩阵的所有特征值与特征向量  </summary>
        public static void EigJcb(Matrix A, double epsl, out Matrix EigVec, out Matrix EigVal)
        {
            EigVec = Matrix.Eye(A.Row);  //产生单位阵
            EigVal = Matrix.Clone(A);    //克隆A
            double E = 0;
            for (int i = 0; i < A.Row - 1; i++)
                for (int j = i + 1; j < A.Row; j++)
                    E += A.Mat[i, j] * A.Mat[i, j];
            E *= 2;
            E = Math.Sqrt(E);  //矩阵A的非对角线元素平方之和的平方根，在设置关口要用到
            double r = E / (double)A.Row;  //关口
            //表明A是主对角矩阵
            if (r <= epsl)
            {
                EigVec = Matrix.Eye(A.Row);
                EigVal = Matrix.Clone(A);
                for (int i = 0; i < EigVal.Row - 1; i++)
                    for (int j = i + 1; j < EigVal.Col; j++)
                        EigVal[i, j] = EigVal[j, i] = 0;
                return;
            }
            do
            {
                r /= (double)A.Row;
                bool l; //用来判断是否所有非对角线元素都过关，若过关，l赋值为false,否则，赋值为true
                do
                {
                    l = false; //首先认为所有非对角线元素都过关
                    for (int p = 0; p < A.Row - 1; p++)     //逐行扫描
                        for (int q = p + 1; q < A.Row; q++)
                        {
                            if (Math.Abs(EigVal.Mat[p, q]) >= r)  //判断元素是否过关
                            {
                                l = true;       //该元素没有过关
                                //由于以下的三个元素下面要经常用到并且会被覆盖掉，所以先把它们寄存起来
                                double v1 = EigVal.Mat[p, p];
                                double v2 = EigVal.Mat[p, q];
                                double v3 = EigVal.Mat[q, q];

                                double u = 0.5 * (v1 - v3);
                                double g, st, ct;      //下面用到的几个变量，在参考书中有对应的意义
                                if (Math.Abs(u) < 1.0e-10)
                                    g = 1.0;
                                else
                                    g = -(u / Math.Abs(u) * 1) * v2 / Math.Sqrt(v2 * v2 + u * u);
                                st = g / Math.Sqrt(2 * (1 + Math.Sqrt(1 - g * g)));
                                ct = Math.Sqrt(1 - st * st);
                                //以下是算法的主要部分
                                //下面这个for循环是整个程序的精彩部分，也是算法的主要部分，不理解
                                //这个算法，就编不出下面的程序
                                for (int i = 0; i < A.Row; i++)
                                {
                                    g = EigVal.Mat[i, p] * ct - EigVal.Mat[i, q] * st;
                                    EigVal.Mat[i, q] = EigVal.Mat[i, p] * st + EigVal.Mat[i, q] * ct;
                                    EigVal.Mat[i, p] = g;
                                    g = EigVec.Mat[i, p] * ct - EigVec.Mat[i, q] * st;
                                    EigVec.Mat[i, q] = EigVec.Mat[i, p] * st + EigVec.Mat[i, q] * ct;
                                    EigVec.Mat[i, p] = g;
                                }
                                for (int i = 0; i < A.Row; i++)
                                {
                                    EigVal.Mat[p, i] = EigVal.Mat[i, p];
                                    EigVal.Mat[q, i] = EigVal.Mat[i, q];
                                }
                                g = 2 * v2 * st * ct;
                                EigVal.Mat[p, p] = v1 * ct * ct + v3 * st * st - g;//为p行p列赋值
                                EigVal.Mat[q, q] = v1 * st * st + v3 * ct * ct + g;//为q行q列赋值
                                EigVal.Mat[p, q] = (v1 - v3) * st * ct + v2 * (ct * ct - st * st); //为p行q列赋值
                                EigVal.Mat[q, p] = EigVal.Mat[p, q]; //据对称性为q行p列赋值
                            }
                        }
                } while (l); //在这一关口下，若元素全部过关，则结束循环，否则继续
            } while (r > epsl);//若关口满足算法的终止条件，则算法终止，否则继续
            for (int i = 0; i < EigVal.Row - 1; i++)
                for (int j = i + 1; j < EigVal.Col; j++)
                    EigVal[i, j] = EigVal[j, i] = 0;
        }

        /// <summary>  用雅可比过关法求实对称矩阵的所有特征值与特征向量  </summary>
        public static void EigJcb(Matrix A, out Matrix EigVec, out Matrix EigVal)
        {
            Matrix.EigJcb(A, Matrix.EPS, out EigVec, out EigVal);
        }

        /// <summary>  线性代数方程组求解  </summary>
        public static Matrix[] DJordan(Matrix A, Matrix B)
        {
            Matrix[] X;
            Matrix A1 = Matrix.Clone(A); //克隆矩阵A到矩阵A1中，以免在求矩阵的秩是影响到A
            Matrix B1 = Matrix.Clone(B);
            double vv = 0;  //寄存变量
            double d = 0.0;
            int n = A1.Row;
            int k;
            int[] JS = new int[n];
            for (k = 0; k < n; k++)
            {
                int L = k;
                d = 0;
                for (int i = k; i < A1.Row; i++)
                    for (int j = k; j < A1.Col; j++)
                    {
                        vv = Math.Abs(A1.Mat[i, j]);
                        if (vv > d)
                        {
                            d = vv;
                            L = i;
                            JS[k] = j;
                        }
                    }
                if (d < Matrix.EPS)
                {
                    X = new Matrix[B1.Col];
                    for (int j = 0; j < B1.Col; j++)
                    {
                        d = 0.0;
                        for (int i = k; i < B1.Row; i++)
                        {
                            vv = Math.Abs(B1.Mat[i, j]);
                            if (vv > d) d = vv;
                        }
                        if (d == 0.0)
                        {
                            X[j] = Matrix.Zeros(A1.Col, A1.Col - k + 1);
                            for (int j1 = k; j1 < A1.Col; j1++)
                                for (int i1 = 0; i1 < k; i1++)
                                    X[j].Mat[i1, j1 - k] = -A1.Mat[i1, j1];
                            for (int i1 = 0; i1 < k; i1++)
                                X[j].Mat[i1, A1.Col - k] = B1.Mat[i1, j];
                            for (int j1 = 0; j1 < A1.Col - k; j1++)
                                X[j].Mat[j1 + k, j1] = 1;
                        }
                        else
                        {
                            X[j] = new Matrix(0);
                        }
                    }
                    for (int l = k - 1; l >= 0; l--)
                    {
                        if (JS[l] != l)
                        {
                            for (int p = 0; p < X.Length; p++)
                                for (int j = 0; j < X[p].Col; j++)
                                {
                                    vv = X[p].Mat[l, j];
                                    X[p].Mat[l, j] = X[p].Mat[JS[l], j];
                                    X[p].Mat[JS[l], j] = vv;
                                }
                        }
                    }
                    return X;
                }
                if (JS[k] != k)      //列变换
                {
                    for (int i = 0; i < A.Row; i++)
                    {
                        vv = A1.Mat[i, k];
                        A1.Mat[i, k] = A1.Mat[i, JS[k]];
                        A1.Mat[i, JS[k]] = vv;
                    }
                }
                if (L != k)        //行变换
                {
                    for (int j = 0; j < A.Col; j++)
                    {
                        vv = A1.Mat[k, j];
                        A1.Mat[k, j] = A1.Mat[L, j];
                        A1.Mat[L, j] = vv;
                    }
                    for (int j = 0; j < B1.Col; j++)
                    {
                        vv = B1.Mat[k, j];
                        B1.Mat[k, j] = B1.Mat[L, j];
                        B1.Mat[L, j] = vv;
                    }
                }
                //归一化
                for (int j = k + 1; j < A1.Col; j++)
                    A1.Mat[k, j] /= A1.Mat[k, k];
                for (int j = 0; j < B1.Col; j++)
                    B1.Mat[k, j] /= A1.Mat[k, k];
                //消去
                for (int i = 0; i < A1.Row; i++)
                {
                    if (i != k)
                    {
                        for (int j = k + 1; j < A1.Col; j++)
                            A1.Mat[i, j] -= (A1.Mat[i, k] * A1.Mat[k, j]);
                        for (int j = 0; j < B1.Col; j++)
                            B1.Mat[i, j] -= (A1.Mat[i, k] * B1.Mat[k, j]);
                    }
                }
            }
            X = new Matrix[B.Col];
            for (int j = 0; j < B.Col; j++)
            {
                d = 0.0;
                for (int i = k; i < B.Row; i++)
                {
                    vv = Math.Abs(B.Mat[i, j]);
                    if (vv > d) d = vv;
                }
                if (d == 0.0)
                {
                    X[j] = Matrix.Zeros(A1.Col, A1.Col - k + 1);
                    for (int j1 = k; j1 < A1.Col; j1++)
                        for (int i1 = 0; i1 < k; i1++)
                            X[j].Mat[i1, j1 - k] = -A1.Mat[i1, j1];
                    for (int i1 = 0; i1 < k; i1++)
                        X[j].Mat[i1, A1.Col - k] = B1.Mat[i1, j];
                    for (int j1 = 0; j1 < A1.Col - k; j1++)
                        X[j].Mat[j1 + k, j1] = 1;
                }
                else
                {
                    X[j] = new Matrix(0);
                }
            }
            for (int l = k - 1; l >= 0; l--)
            {
                if (JS[l] != l)
                {
                    for (int p = 0; p < X.Length; p++)
                        for (int j = 0; j < X[p].Col; j++)
                        {
                            vv = X[p].Mat[l, j];
                            X[p].Mat[l, j] = X[p].Mat[JS[l], j];
                            X[p].Mat[JS[l], j] = vv;
                        }
                }
            }
            return X;
        }

        /// <summary>  方阵的LU分解 / </summary>
        public static Matrix[] Lu(Matrix mat)
        {
            Matrix Q = Matrix.Clone(mat);
            //求出Q
            for (int k = 0; k < mat.Row - 1; k++)
            {
                if (Math.Abs(Q.Mat[k, k]) < Matrix.EPS)
                    throw new MatrixException("方阵的第" + (k + 1) + "个顺序主子式为0，不能进行LU分解");
                for (int i = k + 1; i < mat.Row; i++)
                    Q.Mat[i, k] /= Q.Mat[k, k];
                for (int i = k + 1; i < mat.Row; i++)
                    for (int j = k + 1; j < mat.Row; j++)
                        Q.Mat[i, j] -= Q.Mat[i, k] * Q.Mat[k, j];
            }
            if (Math.Abs(Q.Mat[Q.Row - 1, Q.Col - 1]) < Matrix.EPS)
                throw new MatrixException("方阵的第" + mat.Row + "个顺序主子式为0，不能进行LU分解");
            //从Q中分离出L和U
            Matrix[] LU = new Matrix[2];
            LU[0] = Matrix.Zeros(mat.Row);
            LU[1] = Matrix.Zeros(mat.Row);
            for (int i = 0; i < mat.Row; i++)
            {
                for (int j = 0; j <= i - 1; j++)
                    LU[0].Mat[i, j] = Q.Mat[i, j];
                LU[0].Mat[i, i] = 1;
                LU[1].Mat[i, i] = Q.Mat[i, i];
                for (int j = i + 1; j < mat.Row; j++)
                    LU[1].Mat[i, j] = Q.Mat[i, j];
            }
            return LU;
        }

        /// <summary>  方阵的QR分解  </summary>
        public static Matrix[] QrSchmidt(Matrix mat)
        {
            Matrix Q = Matrix.Zeros(mat.Row);
            Matrix R = Matrix.Eye(mat.Row);
            Matrix a, b, c;
            double v;
            for (int k = 0; k < mat.Row; k++)
            {
                b = a = mat.GetCol(k);
                for (int i = 0; i < k; i++)
                {
                    c = Q.GetCol(i);
                    v = (double)Matrix.Dot(a, c) / (double)Matrix.Dot(c, c);
                    b -= v * c;
                    R.Mat[i, k] = v;
                }
                Q.SetCol(b, k);
            }
            for (int k = 0; k < mat.Col; k++)
            {
                v = Q.Norm2(k);
                for (int i = 0; i < mat.Row; i++)
                    Q.Mat[i, k] /= v;
                for (int i = k; i < mat.Col; i++)
                    R.Mat[k, i] *= v;
            }
            Matrix[] QR = new Matrix[2];
            QR[0] = Q;
            QR[1] = R;
            return QR;
        }

        /// <summary>  方阵的QR分解  </summary>
        public static Matrix[] QrHouse(Matrix mat)
        {
            //初始化
            Matrix R = Matrix.Clone(mat);
            double s = R.Norm2(0);
            double miu = 1 / Math.Sqrt(2 * s * (s - R[0, 0]));
            Matrix w = R.GetCol(0);
            w[0] -= s;
            w *= miu;
            Matrix P = Matrix.Householder(w);
            Matrix Q = P;
            R = P * R;
            for (int i = 1; i < mat.Row - 1; i++)
            {
                w = R.GetCol(i);
                for (int j = 0; j < i; j++)
                    w[j] = 0;
                s = w.Norm2(0);
                miu = 1 / Math.Sqrt(2 * s * (s - R[i, i]));
                w[i] -= s;
                w *= miu;
                P = Matrix.Householder(w);
                Q = P * Q;
                R = P * R;
            }
            Matrix[] QR = new Matrix[2];
            QR[0] = Matrix.Trans(Q);
            QR[1] = R;
            return QR;
        }

        /// <summary>  用全主元高斯消去法求矩阵的秩  </summary>
        public static int Rank(Matrix A)
        {
            double vv = 0;  //寄存变量
            //
            //由矩阵秩的理论知：矩阵的秩既不大于矩阵的行数也不大于列数，n 即为行数和列数的较小者
            int n = A.Row;
            if (n > A.Col) n = A.Col;
            //从第0行开始进行全主元的行变换和列变换
            for (int k = 0; k < n; k++)
            {
                int H = k, L = k; //存储待变换的行数和列数
                double d = 0;  //存储目前矩阵左下角（沿主对角线方向）元素的绝对值的最大值
                //找最大值
                for (int i = k; i < A.Row; i++)
                    for (int j = k; j < A.Col; j++)
                    {
                        vv = Math.Abs(A.Mat[i, j]);
                        if (vv > d)
                        {
                            d = vv;
                            L = i;
                            H = j;
                        }
                    }
                if (d < Matrix.EPS) return k;  //d等于0表明矩阵左下角（沿主对角线方向）元素全为0，矩阵的秩就为k
                if (H != k)         //列变换
                    for (int i = 0; i < A.Row; i++)
                    {
                        vv = A.Mat[i, k];
                        A.Mat[i, k] = A.Mat[i, H];
                        A.Mat[i, H] = vv;
                    }
                if (L != k)        //行变换
                    for (int i = 0; i < A.Col; i++)
                    {
                        vv = A.Mat[k, i];
                        A.Mat[k, i] = A.Mat[L, i];
                        A.Mat[L, i] = vv;
                    }
                for (int j = k + 1; j < A.Col; j++) A.Mat[k, j] /= A.Mat[k, k]; //归一化
                for (int i = k + 1; i < A.Row; i++)      //消去
                    for (int j = k + 1; j < A.Col; j++)
                        A.Mat[i, j] -= (A.Mat[i, k] * A.Mat[k, j]);
            }
            return n;  //循环完毕表明d一直不为0，所以矩阵的秩为n
        }

        /// <summary>  求方阵A的逆  </summary>
        public static Matrix Inverse(Matrix A)
        {
            int n = A.Row;  //方阵的阶数
            double vv;  //中间变量
            int IJ;//中间变量
            double d;   //每次挑出的最大元素
            Matrix B = Matrix.Clone(A);
            int[] IS = new int[n];
            int[] JS = new int[n];
            int i, j, k;  //循环变量
            for (k = 0; k < n; k++)
            {
                d = 0;
                for (i = k; i < n; i++)
                    for (j = k; j < n; j++)
                    {
                        vv = Math.Abs(B[i, j]);
                        if (vv > d)
                        {
                            d = vv;
                            IS[k] = i;
                            JS[k] = j;
                        }
                    }
                if (d < Matrix.EPS)  //方阵为奇异阵
                {
                    return new Matrix();  //返回空矩阵
                }
                IJ = IS[k];
                if (IJ != k)  //行交换
                {
                    for (j = 0; j < n; j++)
                    {
                        vv = B[k, j];
                        B[k, j] = B[IJ, j];
                        B[IJ, j] = vv;
                    }
                }
                IJ = JS[k];
                if (IJ != k)  //列交换
                {
                    for (i = 0; i < n; i++)
                    {
                        vv = B[i, k];
                        B[i, k] = B[i, IJ];
                        B[i, IJ] = vv;
                    }
                }
                vv = B[k, k] = 1.0 / B[k, k];
                for (j = 0; j < n; j++)
                {
                    if (j != k)
                        B[k, j] *= vv;
                }
                for (i = 0; i < n; i++)
                {
                    if (i != k)
                        for (j = 0; j < n; j++)
                            if (j != k) B[i, j] -= B[i, k] * B[k, j];
                }
                for (i = 0; i < n; i++)
                {
                    if (i != k) B[i, k] *= (-vv);
                }
            }
            for (k = n - 1; k >= 0; k--)
            {
                IJ = JS[k];
                for (j = 0; j < n; j++)
                {
                    if (IJ != k)         //行交换
                    {
                        vv = B[k, j];
                        B[k, j] = B[IJ, j];
                        B[IJ, j] = vv;
                    }
                }
                IJ = IS[k];
                for (i = 0; i < n; i++) //列交换
                {
                    if (IJ != k)
                    {
                        vv = B[i, k];
                        B[i, k] = B[i, IJ];
                        B[i, IJ] = vv;
                    }
                }
            }
            return B;
        }

        /// <summary>  求方阵的逆矩阵  </summary>
        public static Matrix Inverse(Matrix A, bool IsDiag)
        {
            if (IsDiag)
            {
                Matrix B = Matrix.Zeros(A.Row);
                for (int i = 0; i < A.Row; i++)
                {
                    if (A.Mat[i, i] == 0) throw new MatrixException("主对角的第" + (i + 1) + "个元素是0");
                    B.Mat[i, i] = 1 / A.Mat[i, i];
                }
                return B;
            }
            else
                return Matrix.Inverse(A);
        }

        /// <summary>  全选主元消去法求方阵的行列式值  </summary>
        public static double Det(Matrix A1)
        {
            Matrix A = Matrix.Clone(A1);
            double DetV = 1;
            double vv = 0;  //寄存变量
            int n = A.Row;
            for (int k = 0; k < n; k++)
            {
                int H = k, L = k; //存储待变换的行数和列数
                double d = 0;  //存储目前矩阵左下角（沿主对角线方向）元素的绝对值的最大值
                //找最大值
                for (int i = k; i < A.Row; i++)
                    for (int j = k; j < A.Col; j++)
                    {
                        vv = Math.Abs(A.Mat[i, j]);
                        if (vv > d)
                        {
                            d = vv;
                            L = i;
                            H = j;
                        }
                    }
                DetV *= d;
                if (d < Matrix.EPS) return 0;  //d等于0表明矩阵左下角（沿主对角线方向）元素全为0，矩阵的秩就为k
                if (H != k)         //列变换
                    for (int i = 0; i < A.Row; i++)
                    {
                        vv = A.Mat[i, k];
                        A.Mat[i, k] = A.Mat[i, H];
                        A.Mat[i, H] = vv;
                    }
                if (L != k)        //行变换
                    for (int i = 0; i < A.Col; i++)
                    {
                        vv = A.Mat[k, i];
                        A.Mat[k, i] = A.Mat[L, i];
                        A.Mat[L, i] = vv;
                    }
                for (int j = k + 1; j < A.Col; j++) A.Mat[k, j] /= A.Mat[k, k]; //归一化
                for (int i = k + 1; i < A.Row; i++)      //消去
                    for (int j = k + 1; j < A.Col; j++)
                        A.Mat[i, j] -= (A.Mat[i, k] * A.Mat[k, j]);
            }
            return DetV;  //循环完毕表明d一直不为0，所以矩阵的秩为n
        }

        /// <summary>  正弦矩阵函数  </summary>
        public static Matrix Sin(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Sin(X.Mat[i, j]);
            return Y;
        }

        /// <summary> / 反正弦矩阵函数  </summary>
        public static Matrix Asin(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Asin(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  余弦矩阵函数  </summary>
        public static Matrix Cos(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Cos(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  反余弦矩阵函数  </summary>
        public static Matrix Acos(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Acos(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  正切矩阵函数  </summary>
        public static Matrix Tan(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Tan(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  反余切矩阵函数  </summary>
        public static Matrix Atan(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Atan(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  余切矩阵函数  </summary>
        public static Matrix Cot(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = 1.0 / Math.Tan(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  反余切矩阵函数  </summary>
        public static Matrix Acot(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            double vv = Math.PI / 2;
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = vv - Math.Atan(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  双精度数的正割函数  </summary>
        public static double Sec(double x)
        {
            return 1.0 / Math.Cos(x);
        }

        /// <summary>  双精度数的反正割函数  </summary>
        public static double Asec(double y)
        {
            return Math.Acos(1.0 / y);
        }

        /// <summary>  正割矩阵函数  </summary>
        public static Matrix Sec(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = 1.0 / Math.Cos(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  反正割矩阵函数  </summary>
        public static Matrix Asec(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Matrix.Asec(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  双精度数的余割函数  </summary>
        public static double Csc(double x)
        {
            return 1.0 / Math.Sin(x);
        }

        /// <summary>  双精度数的反余割函数  </summary>
        public static double Acsc(double y)
        {
            return Math.Asin(1.0 / y);
        }

        /// <summary>  余割矩阵函数  </summary>
        public static Matrix Csc(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = 1.0 / Math.Sin(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  反余割矩阵函数  </summary>
        public static Matrix Acsc(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Matrix.Acsc(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  乘方矩阵函数（X.^a）  </summary>
        public static Matrix Pow(Matrix X, double a)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Pow(X.Mat[i, j], a);
            return Y;
        }

        /// <summary>  矩阵的连乘  </summary>
        public static Matrix operator ^(Matrix X, int n)
        {
            if (X.Row != X.Col) throw new MatrixException("矩阵的连乘函数的输入矩阵不是方阵");
            if (X.Row * X.Col == 0) throw new MatrixException("矩阵的连乘函数的输入矩阵是空矩阵");
            if (n == 0) return Matrix.Ones(X.Row);
            if (n == 1) return Matrix.Clone(X);
            if (n > 1)
            {
                Matrix Y = Matrix.Eye(X.Row);
                Matrix Ypow = Matrix.Clone(X);
                if (n % 2 == 1) Y = Matrix.Clone(X);
                n = n / 2;
                while (n > 0)
                {
                    Ypow = Matrix.MulWino(Ypow, Ypow);
                    if (n % 2 == 1)
                        Y = Matrix.MulWino(Y, Ypow);
                    n = n / 2;
                }
                return Y;
            }
            if (n == -1) return Matrix.Inverse(X);
            else
            {
                n = -n;
                Matrix Y = Matrix.Eye(X.Row);
                Matrix Ypow = Matrix.Clone(X);
                if (n % 2 == 1) Y = Matrix.Clone(X);
                n = n / 2;
                while (n > 0)
                {
                    Ypow = Matrix.MulWino(Ypow, Ypow);
                    if (n % 2 == 1)
                        Y = Matrix.MulWino(Y, Ypow);
                    n = n / 2;
                }
                return Matrix.Inverse(Y);
            }
        }

        /// <summary>  开平方矩阵函数  </summary>
        public static Matrix Sqrt(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Sqrt(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  指数矩阵函数  </summary>
        public static Matrix Exp(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Exp(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  以为a底数的指数矩阵函数  </summary>
        public static Matrix operator ^(double a, Matrix X)
        {
            if (a <= Matrix.EPS) throw new MatrixException("指数函数的底" + a + "<=0");
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < Y.Row; i++)
                for (int j = 0; j < Y.Col; j++)
                    Y.Mat[i, j] = Math.Pow(a, X.Mat[i, j]);
            return Y;
        }

        /// <summary>  自然对数的矩阵函数  </summary> 
        public static Matrix Log(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Log(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  以a为底的对数的矩阵函数  </summary>
        public static Matrix Log(double a, Matrix X)
        {
            if (a <= Matrix.EPS) throw new MatrixException("对数矩阵函数的底" + a + "<=0");
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Log(a, X.Mat[i, j]);
            return Y;
        }

        /// <summary>  以10为底的对数的矩阵函数  </summary>
        public static Matrix Log10(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Log10(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  以2为底的对数矩阵函数  </summary>
        public static Matrix Log2(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);  //创建一个与X同型的一个矩阵
            //逐个求X中的每一个元素的以2为底的对数，并对应赋给Y
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Log(2, X.Mat[i, j]);
            return Y;  //返回Y
        }

        /// <summary>  绝对值矩阵函数  </summary>
        public static Matrix Abs(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);  //创建一个与X同型的一个矩阵
            //逐个求X中的每一个元素的绝对值，并对应赋给Y
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Abs(X.Mat[i, j]);
            return Y;  //返回Y
        }

        /// <summary>  四舍五入矩阵函数  </summary>
        public static Matrix Round(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);  //创建一个与X同型的一个矩阵
            //逐个对X中的每一个元素进行四舍五入，并对应赋给Y
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Round(X.Mat[i, j]);
            return Y;  //返回Y
        }

        public static Matrix Round(Matrix X, int n)
        {
            Matrix Y = new Matrix(X.Row, X.Col);  //初始化矩阵Y
            double vv = Math.Pow(10, n);  //求出10的n次方
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Round(X.Mat[i, j] / vv) * vv;
            return Y;
        }

        /// <summary>  向下取整矩阵函数  </summary> 
        public static Matrix Floor(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Floor(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  小数点左右移一定位数的向下取整矩阵函数  </summary>
        public static Matrix Floor(Matrix X, int n)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            double vv = Math.Pow(10, n);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Floor(X.Mat[i, j] / vv) * vv;
            return Y;
        }

        /// <summary>  向上取整矩阵函数  </summary>
        public static Matrix Ceiling(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Ceiling(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  小数点左右移一定位数的向上取整矩阵函数  </summary> 
        public static Matrix Ceiling(Matrix X, int n)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            double vv = Math.Pow(10, n);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Ceiling(X.Mat[i, j] / vv) * vv;
            return Y;
        }

        /// <summary>  向0取整函数  </summary>
        public static double Fix(double x)
        {
            if (x > 0) return Math.Floor(x);
            else
                return Math.Ceiling(x);
        }

        /// <summary>  向0取整矩阵函数  </summary>
        public static Matrix Fix(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Matrix.Fix(X.Mat[i, j]);
            return Y;
        }
        /// <summary>  符号矩阵函数  </summary>
        public static Matrix Sign(Matrix X)
        {
            Matrix Y = new Matrix(X.Row, X.Col);
            for (int i = 0; i < X.Row; i++)
                for (int j = 0; j < X.Col; j++)
                    Y.Mat[i, j] = Math.Sign(X.Mat[i, j]);
            return Y;
        }

        /// <summary>  每个指标的样本均方差，返回一个行矩阵（除以N-1）  </summary>
        public static Matrix Std(Matrix Data)
        {
            Matrix stdmat = new Matrix(1, Data.Col);//初始化均方差行矩阵
            Matrix mean = Matrix.MeanCol(Data); //获得数据矩阵的每一列的均值
            double vv;  //中间变量
            Matrix vmat;//中间矩阵
            for (int j = 0; j < Data.Col; j++)
            {
                vmat = Data.GetCol(j);   //获得数据矩阵的第j列
                vmat = vmat - mean.Mat[0, j];
                vv = (double)(Matrix.Trans(vmat) * vmat); //求出vmat'*vmat
                vv /= (Data.Row - 1);
                stdmat[0, j] = Math.Sqrt(vv);
            }
            return stdmat;
        }

        /// <summary>  数据的中心化变换  </summary> 
        public static Matrix CenterTrans(Matrix Data)
        {
            Matrix Data1 = Matrix.Clone(Data);
            Data1.MinusCol(Matrix.MeanCol(Data1));
            return Data1;
        }

        /// <summary>  数据的标准化变换  </summary> 
        public static Matrix StandardTrans(Matrix Data)
        {
            Matrix Data1 = Matrix.Clone(Data);
            Data1.MinusCol(Matrix.MeanCol(Data1));
            Data1.DivideCol(Matrix.Std(Data));
            return Data1;
        }

        /// <summary>  求样品矩阵的协方差矩阵  </summary>
        public static Matrix Cov(Matrix Data)
        {
            double vv;  //中间变量
            Matrix meanmat = Matrix.MeanCol(Data);  //数据矩阵的均值矩阵
            double N = (double)Data.Row;   //样本容量，即样本中所含的样品总数
            Matrix covmat = new Matrix(Data.Col); //初始化协方差矩阵
            //求协方差矩阵，注意到它是对称阵
            for (int i = 0; i < covmat.Row; i++)
                for (int j = i; j < covmat.Col; j++)
                {
                    vv = 0;
                    if (i == j)
                    {
                        for (int k = 0; k < Data.Row; k++) vv += (Data.Mat[k, i] - meanmat[i]) * (Data.Mat[k, i] - meanmat[i]);
                    }
                    else
                    {
                        for (int k = 0; k < Data.Row; k++) vv += (Data.Mat[k, i] - meanmat[i]) * (Data.Mat[k, j] - meanmat[j]);
                    }
                    //vv=(double)Matrix.Dot(Data.GetCol(i)-meanmat[0,i],Data.GetCol(j)-meanmat[0,j]);
                    //数据矩阵的第i行和第j列作内积
                    vv /= (N - 1);     //除以N-1
                    covmat[i, j] = vv;//vv即为协方差矩阵的第i行和第j列的元素
                    covmat[j, i] = vv;//由对称性得到协方差矩阵的第j行和第i列的元素
                }
            return covmat;   //返回协方差矩阵
        }

        /// <summary>  求两样本矩阵决定的协方差矩阵  </summary>
        public static Matrix Covxy(Matrix X, Matrix Y)
        {
            if (X.Row != Y.Row)
                throw new MatrixException("两样本的样本容量不相等");
            double vv;  //中间变量
            Matrix meanX = Matrix.MeanCol(X);  //X矩阵的均值矩阵
            Matrix meanY = Matrix.MeanCol(Y);  //Y矩阵的均值矩阵
            double N = (double)X.Row;   //样本容量，即样本中所含的样品总数
            Matrix covxy = Matrix.Zeros(X.Col, Y.Col);  //初始化协方差矩阵
            for (int i = 0; i < covxy.Row; i++)
                for (int j = 0; j < covxy.Col; j++)
                {
                    vv = (double)Matrix.Dot(X.GetCol(i), Y.GetCol(j));
                    vv -= (double)Matrix.SumCol(meanY.Mat[0, j] * X.GetCol(i));
                    vv -= (double)Matrix.SumCol(meanX.Mat[0, i] * Y.GetCol(j));
                    covxy.Mat[i, j] = vv;
                }
            covxy += N * Matrix.Trans(meanX) * meanY;
            covxy /= (N - 1);
            return covxy;
        }


        /// <summary>  求相关系数矩阵  </summary>
        public static Matrix Correl(Matrix Data)
        {
            Matrix cormat = Matrix.Cov(Data);  //求出数据矩阵的协方差矩阵
            double vv;    //中间变量
            for (int i = 0; i < cormat.Row; i++)
            {
                vv = cormat[i, i]; //获得协方差矩阵的主对角线上的第i个元素
                for (int j = i + 1; j < cormat.Row; j++)
                    cormat[j, i] = cormat[i, j] = cormat[i, j] / Math.Sqrt(vv * cormat[j, j]);
            }
            for (int i = 0; i < cormat.Row; i++)
                cormat[i, i] = 1;         //指标与其自身的相关系数为1
            return cormat;     //返回相关系数矩阵
        }

        /// <summary>  构造Givens矩阵（初等旋转矩阵）  </summary>
        public static Matrix Givens(int n, double c, double s, int i, int j)
        {
            if (i >= j)
                throw new MatrixException("参数4应小于参数5");
            if (j >= n)
                throw new MatrixException("参数5应小于参数1");
            Matrix T = Matrix.Eye(n);
            double sum = Math.Sqrt(c * c + s * s);
            c /= sum;
            s /= sum;
            T.Mat[i, i] = c;
            T.Mat[i, j] = s;
            T.Mat[j, j] = c;
            T.Mat[j, i] = -s;
            return T;
        }

        /// <summary>  生成Householder矩阵（初等反射矩阵）  </summary>
        public static Matrix Householder(Matrix u)
        {
            if (u.Col > 1)
                throw new MatrixException("输入的应是列向量");
            u /= u.Norm2(0);
            return Matrix.Eye(u.Row) - 2 * u * Matrix.Trans(u);
        }

        /// <summary>  生成n阶Hilbert矩阵  </summary>
        public static Matrix Hilbert(int n)
        {
            Matrix H = new Matrix(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    H.Mat[i, j] = 1.0 / (i + j + 1.0);
            return H;
        }

        private static int dotRightDigit = 4;  //小数点右侧的位数
        /// <summary>  dotRightDigit的属性  </summary>
        public static int DotRightDigit
        {
            get
            {
                return Matrix.dotRightDigit;
            }
            set
            {
                if (value < 0) throw new MatrixException("小数点右侧位数不能是负数");
                Matrix.dotRightDigit = value;
            }
        }

        /// <summary>  非奇异方阵的条件数（列和范数） </summary>
        public static double Cond1(Matrix mat)
        {
            if (mat.Row != mat.Col)
                throw new MatrixException("矩阵不是方阵");
            if (Matrix.Det(mat) == 0) throw new MatrixException("矩阵是奇异阵");
            return Matrix.Norm1(mat) * Matrix.Norm1(Matrix.Inverse(mat));
        }

        /// <summary>  非奇异方阵的条件数（谱范数）  </summary>
        public static double Cond2(Matrix mat)
        {
            if (mat.Row != mat.Col)
                throw new MatrixException("矩阵不是方阵");
            if (Matrix.Det(mat) == 0) throw new MatrixException("矩阵是奇异阵");
            return Matrix.Norm2(mat) * Matrix.Norm2(Matrix.Inverse(mat));
        }

        /// <summary>  非奇异方阵的条件数（行和范数）  </summary>
        public static double CondInf(Matrix mat)
        {
            if (mat.Row != mat.Col)
                throw new MatrixException("矩阵不是方阵");
            if (Matrix.Det(mat) == 0) throw new MatrixException("矩阵是奇异阵");
            return Matrix.NormInf(mat) * Matrix.NormInf(Matrix.Inverse(mat));
        }

        /// <summary>  非奇异方阵的条件数（F范数）  </summary>
        public static double CondF(Matrix mat)
        {
            if (mat.Row != mat.Col)
                throw new MatrixException("矩阵不是方阵");
            if (Matrix.Det(mat) == 0) throw new MatrixException("矩阵是奇异阵");
            return Matrix.NormF(mat) * Matrix.NormF(Matrix.Inverse(mat));
        }

        /// <summary>  重载ToString()  </summary>
        public override string ToString()
        {
            int maxlen;   //数字的最大长度
            Matrix mat;   //变换后的矩阵
            //string str="";
            StringBuilder str = new StringBuilder();
            double maxv = (double)Matrix.MaxRow(Matrix.MaxCol(Matrix.Abs(this)));
            if (maxv >= 1000)
            {
                double pv = Math.Floor(Math.Log10(maxv));
                str = str.Append("1.0e+" + (int)pv + " */n");
                mat = this / Math.Pow(10, pv);
                mat = Matrix.Round(mat, -Matrix.dotRightDigit);
                maxlen = 2 + Matrix.dotRightDigit;
            }
            else if (maxv >= 1)
            {
                maxlen = (int)Math.Log10(maxv) + 3 + Matrix.dotRightDigit;
                mat = Matrix.Round(this, -Matrix.dotRightDigit);
            }
            else if (maxv > 0)
            {
                double pv = Math.Floor(Math.Log10(maxv));
                str = str.Append("1.0e" + (int)pv + " */n");
                mat = this / Math.Pow(10, pv);
                mat = Matrix.Round(mat, -Matrix.dotRightDigit);
                maxlen = 2 + Matrix.dotRightDigit;
            }
            else
            {
                mat = Matrix.Clone(this);
                maxlen = 1;
            }
            str = str.Insert(0, "Matrix: " + this.Row + "," + this.Col + "/n");
            int len;   //下面用到的寄存器
            maxlen += 3; //数之间的有最少3个空格
            for (int i = 0; i < this.Row; i++)
            {
                if ((i + 1) < 10) str = str.Append(" " + (i + 1) + ":   ");
                else if ((i + 1) < 100) str = str.Append(" " + (i + 1) + ":  ");
                else str = str.Append(" " + (i + 1) + ": ");
                for (int j = 0; j < this.Col; j++)
                {
                    len = mat.Mat[i, j].ToString().Length;
                    str = str.Append(mat.Mat[i, j].ToString());
                    for (int l = 0; l < maxlen - len; l++)
                        str = str.Append(" ");
                }
                if (i < this.Row - 1) str = str.Append("/n");
            }
            return str.ToString();
        }

        /// <summary>  判断矩阵是否行满秩  </summary>
        public bool IsRowRank
        {
            get
            {
                if (Matrix.Rank(this) == this.Row) return true;
                return false;
            }
        }

        /// <summary>  判断矩阵是否列满秩  </summary>
        public bool IsColRank
        {
            get
            {
                if (Matrix.Rank(this) == this.Col) return true;
                return false;
            }
        }

        /// <summary>  判断矩阵是否是对称阵  </summary>
        public bool IsSymmetry
        {
            get
            {
                if (this.Row != this.Col) return false;  //如果矩阵不是方阵，那么就不是对称阵
                for (int i = 0; i < this.Row; i++)
                    for (int j = i + 1; j < this.Col; j++)
                        if (this.Mat[i, j] != this.Mat[j, i]) return false;
                return true;
            }

        }

        /// <summary> / 判断矩阵是否主对角占优  </summary>
        public bool IsDiagSup
        {
            get
            {
                if (this.Row != this.Col)
                    throw new MatrixException("矩阵不是方阵");
                double sum;
                for (int i = 0; i < this.Row; i++)
                {
                    sum = 0;
                    for (int j = 0; j <= (i - 1); j++)
                        sum += Math.Abs(this.Mat[i, j]);
                    for (int j = i + 1; j < this.Col; j++)
                        sum += Math.Abs(this.Mat[i, j]);
                    if (sum >= Math.Abs(this.Mat[i, i])) return false;
                }
                return true;
            }
        }

        /// <summary>  Gauss-Seidel迭代法求系数阵主对角占优的方程组的解  </summary>
        public static Matrix GaussSeidelDiagSup(Matrix A, Matrix b, ref int loop)
        {
            if (A.Row != A.Col)
                throw new MatrixException("矩阵不是方阵");
            if (b.Col != 1) throw new MatrixException("方程组的右端常数项系数不是列矩阵");
            if (A.Row != b.Row) throw new MatrixException("系数阵的阶数与常数项向量的维数不一致");
            Matrix X = new Matrix(b.Row, 1);  //解向量矩阵
            double p = Matrix.EPS + 1;  //精度估计
            double sum, t;  //中间存储变量
            int n = X.Row;  //解向量的维数
            loop = 0;
            while (p >= Matrix.EPS)
            {
                loop++;
                p = 0;
                for (int i = 0; i < n; i++)
                {
                    t = X.Mat[i, 0];
                    sum = 0;
                    for (int j = 0; j <= (i - 1); j++) sum += A.Mat[i, j] * X.Mat[j, 0];
                    for (int j = i + 1; j < n; j++) sum += A.Mat[i, j] * X.Mat[j, 0];
                    X.Mat[i, 0] = (b.Mat[i, 0] - sum) / A.Mat[i, i];
                    sum = Math.Abs(X.Mat[i, 0] - t);
                    if (sum > p) p = sum;
                }
                if (p > 1.0e+20) throw new MatrixException("本方程组的GaussSeidel格式发散");
            }
            return X;
        }

        /// <summary>  Gauss-Seidel迭代法求系数阵主对角占优的方程组的解  </summary>
        public static Matrix GaussSeidelDiagSup(Matrix A, Matrix b)
        {
            int loop = 0;
            return Matrix.GaussSeidelDiagSup(A, b, ref loop);
        }

        /// <summary>  SOR方法求系数阵主对角占优的方程组的解 </summary>
        public static Matrix SorDiagSup(Matrix A, Matrix b, double omga, ref int loop)
        {
            if (A.Row != A.Col)
                throw new MatrixException("矩阵不是方阵");
            if (b.Col != 1) throw new MatrixException("方程组的右端常数项系数不是列矩阵");
            if (A.Row != b.Row) throw new MatrixException("系数阵的阶数与常数项向量的维数不一致");
            if (omga <= 0 || omga >= 2) throw new MatrixException("松弛因子不在0和2之间");
            Matrix X = new Matrix(b.Row, 1);  //解向量矩阵
            double p = Matrix.EPS + 1;  //精度估计
            double sum, t;  //中间存储变量
            int n = X.Row;  //解向量的维数
            loop = 0;
            while (p >= Matrix.EPS)
            {
                loop++;
                p = 0;
                for (int i = 0; i < n; i++)
                {
                    t = X.Mat[i, 0];
                    sum = 0;
                    //
                    for (int j = 0; j < n; j++) sum += A.Mat[i, j] * X.Mat[j, 0];
                    X.Mat[i, 0] += omga * (b.Mat[i, 0] - sum) / A.Mat[i, i];
                    //
                    sum = Math.Abs(X.Mat[i, 0] - t);
                    if (sum > p) p = sum;
                }
                if (p > 1.0e+20) throw new MatrixException("本方程组的SOR格式发散");
            }
            return X;
        }

        /// <summary>  SOR方法求系数阵主对角占优的方程组的解  </summary>
        public static Matrix SorDiagSup(Matrix A, Matrix b, double omga)
        {
            int loop = 0;
            return Matrix.SorDiagSup(A, b, omga, ref loop);
        }

        /// <summary> / 给出指定数在哪个区间中  </summary>
        public static int SelectSpan(double[] pv, double v)
        {
            int k = Array.BinarySearch(pv, v);
            if (k < 0) return ~k - 1;
            return k - 1;
        }

    }
    /// <summary>  类Matrix相应的异常类  </summary>
    public class MatrixException : System.ApplicationException
    {
        /// <summary>  错误信息字符串  </summary>
        private string errorstring;

        /// <summary>  构造函数  </summary>
        public MatrixException(string errorstr)
        {
            this.errorstring = errorstr;
        }

        public override string ToString()
        {
            return this.errorstring;
        }

    }
}
