using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeDepth
{

    class Node 
    {
        private int nodeNumber;
        private Node leftNode, rightNode;

        public Node(int nodeNumber) 
        {
            this.nodeNumber = nodeNumber;
            this.leftNode = this.rightNode = null;
        }

        public int getNodeNumber() 
        {
            return nodeNumber;
        }

        public void setNodeNumber(int nodeNumber)
        {
            this.nodeNumber = nodeNumber;
        }

        public Node getNode(Boolean isLeft)
        {
            return isLeft ? this.leftNode : this.rightNode;
        }

        public void setNode(Node node, Boolean isLeft)
        {
            if (isLeft)
                this.leftNode = node;
            else 
                this.rightNode = node;
        }
    }

    class BinaryTree 
    {
        private Node root;
        private int height = 0;

        public BinaryTree(List<Node> nodes)
        {
            if (nodes == null && nodes.Any()) 
                return;

            this.root = nodes.First();
            // Inicializimi i pemes
            for (int i = 1; i < nodes.Count; i++)
            {
                Node node = nodes[i];
                setTreeNode(root, node);
            }
            //Inicializimi i lartesise
            this.height = getTreeHeight(root);
        }

        public int getHeight() 
        {
            return height;
        }

        public void setHeight(int height) 
        {
            this.height = height;
        }

        private void setTreeNode(Node root, Node node)
        {
            Node leftNode = root.getNode(true);
            Node rightNode = root.getNode(false);
            Boolean isLeft = node.getNodeNumber() < root.getNodeNumber();
            if (isLeft)
            {
                if (leftNode != null)
                    setTreeNode(leftNode, node);
                else
                    root.setNode(node, isLeft);
            }
            else
            {
                if (rightNode != null)
                    setTreeNode(rightNode, node);
                else
                    root.setNode(node, isLeft);
            }
        }

        private static int getTreeHeight(Node node)
        {
            if (node == null)
                return 0;

            // Lartesia eshte e barabarte me 1 + maksimumi i pjeses se majte dhe te djathte te pemes
            return (1 + Math.Max(getTreeHeight(node.getNode(true)), getTreeHeight(node.getNode(false))));
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------Gjetja e lartësisë së pemës---------\n");
            Console.Write("Jepni listën e numrave të ndara me presje: ");
            String listAsString = Console.ReadLine();
            String [] array = listAsString.Split(";").Where(item => !string.IsNullOrWhiteSpace(item)).ToArray();
            List<Node> nodes = new List<Node>();
            foreach (string item in array)
            {
                int number ;
                try
                {
                    number = int.Parse(item);
                }
                catch (Exception ex) when (ex is FormatException || ex is OverflowException)
                {
                    Console.WriteLine("Të dhënat hyrëse janë invalide!");
                    return;
                }
                Node node = new Node(number);
                nodes.Add(node);
            }
            BinaryTree tree = new BinaryTree(nodes);
            Console.WriteLine("\nLartësia e kësaj peme është: " + tree.getHeight());
        }
    }
}
