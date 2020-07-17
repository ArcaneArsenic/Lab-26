/* UserInterface.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KansasStateUniversity.TreeViewer2;
using Ksu.Cis300.PriorityQueueLibrary;
using Ksu.Cis300.ImmutableBinaryTrees;
using System.IO;


namespace Ksu.Cis300.HuffmanTrees
{
    /// <summary>
    /// A GUI for a program that builds a Huffman tree for a given input file.
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// Constructs the GUI.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
        }
        /// <summary>
        /// returns an array of longs (freq values)
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private long[] freqTableBuilder(string fn)
        {
            long[] chars = new long[256];
            int i = 0;
            using (FileStream fs = File.OpenRead(fn))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                    chars[i] = b[i];
                    i++;
                }
            }
            return chars;
        }

        /// <summary>
        /// do not iterate with a byte
        /// set priority to freq table equivalent 
        /// check zero, create node
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private MinPriorityQueue<long, BinaryTreeNode<byte>> HuffBuild(long[] x)
        {
            x = freqTableBuilder(Console.ReadLine());
            MinPriorityQueue<long, BinaryTreeNode<byte>> mpq = new 
                MinPriorityQueue<long, BinaryTreeNode<byte>>();
            while (x.Length > mpq.Count)
            {
                BinaryTreeNode<byte> node = new BinaryTreeNode<byte>(Convert.ToByte(x), null, null);
                mpq.Add(x[0], node);
            }
            return mpq;

        }
        /// <summary>
        /// Handles a Click event on the "Select a File" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSelectFile_Click(object sender, EventArgs e)
        {
            if (uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BinaryTreeNode<byte> t = null;

                    // Add code to build the Huffman tree and assign it to t.
                    HuffBuild(freqTableBuilder(Convert.ToString(sender)));

                    new TreeForm(t, 100).Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
