using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.ViewModel
{
    /// <summary>
    /// 二叉树类
    /// </summary>
    /// <typeparam name="ElementType"></typeparam>
    public class BinaryTree<ElementType> where ElementType : IComparable<ElementType>
    {
        public ElementType element;
        public BinaryTree<ElementType> left;
        public BinaryTree<ElementType> right;

        /// <summary>
        /// 清空树
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public BinaryTree<ElementType> MakeEmpty(BinaryTree<ElementType> T)
        {
            T.left = null;
            T.right = null;
            return T;
        }

        /// <summary>
        /// 查找特定元素，返回BinaryTree类
        /// </summary>
        /// <param name="X"></param>
        /// <param name="T"></param>
        /// <returns></returns>
        public BinaryTree<ElementType> Find(ElementType X, BinaryTree<ElementType> T)
        {
            if (T == null)
                return null;
            if (X.CompareTo(T.element) < 0)
                return Find(X, T.left);
            else if (X.CompareTo(T.element) > 0)
                return Find(X, T.right);
            else return T;
        }

        /// <summary>
        /// 查找最大元素
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public BinaryTree<ElementType> FindMax(BinaryTree<ElementType> T)
        {
            if (T == null)
                return null;
            else if (T.right == null)
                return T;
            else
                return FindMax(T.right);
        }
        /// <summary>
        /// 查找最小元素
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public BinaryTree<ElementType> FindMin(BinaryTree<ElementType> T)
        {
            if (T == null)
                return null;
            else if (T.left == null)
                return T;
            else
                return FindMin(T.left);
        }
        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="X"></param>
        /// <param name="T"></param>
        /// <returns></returns>
        public BinaryTree<ElementType> Insert(ElementType X, BinaryTree<ElementType> T)
        {
            if (T == null)
            {
                T = new BinaryTree<ElementType>();
                if (T == null)
                    return null;
                T.element = X;
            }
            if (X.CompareTo(T.element) < 0)
                T.left = Insert(X, T.left);
            else if (X.CompareTo(T.element) > 0)
                T.right = Insert(X, T.right);
            return T;

        }
        /// <summary>
        /// 删除特定元素
        /// </summary>
        /// <param name="X"></param>
        /// <param name="T"></param>
        /// <returns></returns>
        public BinaryTree<ElementType> Delete(ElementType X, BinaryTree<ElementType> T)
        {
            BinaryTree<ElementType> Tmp;
            if (T == null)
            {
                return null;
            }
            else if (X.CompareTo(T.element) < 0)
                T.left = Delete(X, T.left);
            else if (X.CompareTo(T.element) > 0)
                T.right = Delete(X, T.right);
            else if (T.left != null && T.right != null)
            {
                Tmp = FindMin(T.right);
                T.element = Tmp.element;
                T.right = Delete(Tmp.element, T.right);
            }
            else
            {
                Tmp = T;
                if (T.left == null)
                {
                    T = T.right;
                }
                else
                    T = T.left;
            }
            return T;

        }

        public int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public int Height(BinaryTree<ElementType> T)
        {
            if (T == null)
                return -1;
            return Max(Height(T.left), Height(T.right)) + 1;
        }

        /// <summary>
        /// 打印单个结点的元素，根据树高
        /// </summary>
        /// <param name="T"></param>
        public void PrintElement(BinaryTree<ElementType> T)
        {
            int height = Height(T);
            for (int i = 0; i < height; i++)
            {
                Console.Write("\t");
            }
            Console.Write(T.element + "\n");
        }

        public void PrintTree(BinaryTree<ElementType> T)
        {
            if (T != null)
            {
                PrintTree(T.left);
                PrintElement(T);
                PrintTree(T.right);
            }
        }
    }
    /*
    public class BinaryTree<T>
    {
        public T Data { get; set; }
        public BinaryTree<T> LeftBinaryTree { get; set; }
        public BinaryTree<T> RightBinaryTree { get; set; }

        public BinaryTree(T val, BinaryTree<T> lp, BinaryTree<T> rp)
        {
            Data = val;
            LeftBinaryTree = lp;
            RightBinaryTree = rp;
        }

        //构造器
        public BinaryTree(BinaryTree<T> lp, BinaryTree<T> rp)
        {

            Data = default(T);
            LeftBinaryTree = lp;
            RightBinaryTree = rp;
        }

        //构造器
        public BinaryTree(T val)
        {
            Data = val;
            LeftBinaryTree = null;
            RightBinaryTree = null;
        }

        public BinaryTree()
        {
            Data = default(T);
            LeftBinaryTree = null;
            RightBinaryTree = null;
        }

        public static void PreOrder(BinaryTree<T> root)
        {
            //根结点为空
            if (root == null)
            {
                return;
            }
            //处理根结点
            Console.WriteLine("{0}", root.Data);
            //先序遍历左子树
            PreOrder(root.LeftBinaryTree);
            //先序遍历右子树
            PreOrder(root.RightBinaryTree);

        }





        public static void InOrder(BinaryTree<T> root)
        {
            //根结点为空
            if (root == null)
            {
                return;
            }
            //中序遍历左子树
            InOrder(root.LeftBinaryTree);
            Console.WriteLine("{0}", root.Data);
            //中序遍历右子树
            InOrder(root.RightBinaryTree);

        }

        public static void PostnOrder(BinaryTree<T> root)
        {
            //根结点为空
            if (root == null)
            {
                return;
            }
            //后序遍历左子树
            PostnOrder(root.LeftBinaryTree);
            //后序遍历右子树
            PostnOrder(root.RightBinaryTree);
            Console.WriteLine("{0}", root.Data);

        }



        public static void LevelOrder(BinaryTree<T> root)
        {
            //根结点为空
            if (root == null)
            {
                return;
            }
            //设置一个队列保存层序遍历的结点
            CSeqQueue<BinaryTree<T>> sq = new CSeqQueue<BinaryTree<T>>(50);
            //根结点入队
            sq.In(root);
            //队列非空，结点没有处理完
            while (!sq.IsEmpty())
            {
                //结点出队
                BinaryTree<T> tmp = sq.Out();
                //处理当前结点
                Console.WriteLine("{0}", tmp.Data);
                //将当前结点的左孩子结点入队
                if (tmp.LeftBinaryTree != null)
                {
                    sq.In(tmp.LeftBinaryTree);
                }
                //将当前结点的右孩子结点入队
                if (tmp.RightBinaryTree != null)
                {
                    sq.In(tmp.RightBinaryTree);
                }
            }
        }
    }
    
    //Queue接口
    public interface IQueue<T>
    {
        int GetLength();          //求队列的长度
        bool IsEmpty();           //判断对列是否为空
        void Clear();             //清空队列
        void In(T item);          //入队
        T Out();                  //出队
        T GetFront();             //取对头元素
    }
    

    public class CSeqQueue<T> : IQueue<T>
    {

        private int maxsize;       //循环顺序队列的容量
        private T[] data;          //数组， 用于存储循环顺序队列中的数据元素
        private int front;          //指示循环顺序队列的队头
        private int rear;           //指示循环顺序队列的队尾

        //索引器
        public T this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;
            }
        }

        //容量属性
        public int Maxsize
        {
            get
            {
                return maxsize;
            }
            set
            {
                maxsize = value;
            }
        }

        //队头属性
        public int Front
        {
            get
            {
                return front;
            }
            set
            {
                front = value;
            }
        }



        //队尾属性
        public int Rear
        {
            get
            {
                return rear;
            }
            set
            {
                rear = value;
            }
        }

        //构造器
        public CSeqQueue(int size)
        {
            data = new T[size];
            maxsize = size;
            front = rear = -1;
        }

        //求循环顺序队列的长度
        public int GetLength()
        {
            return (rear - front + maxsize) % maxsize;
        }

        //清空循环顺序队列
        public void Clear()
        {
            front = rear = -1;
        }

        //判断循环顺序队列是否为空
        public bool IsEmpty()
        {
            if (front == rear)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断循环顺序队列是否为满

        public bool IsFull()
        {
            if ((rear + 1) % maxsize == front)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //入队
        public void In(T item)
        {
            if (IsFull())
            {
                Console.WriteLine("Queue is full");
                return;
            }
            data[++rear] = item;

        }

        //出队
        public T Out()
        {
            T tmp = default(T);
            if (IsEmpty())
            {
                Console.WriteLine("Queue is empty");
                return tmp;
            }
            tmp = data[++front];
            return tmp;

        }

        //获取队头数据元素
        public T GetFront()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Queue is empty!");
                return default(T);
            }
            return data[front + 1];
        }
    }*/
}