/////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved
// Written by Forge Partner Development
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compor90Revit
{
    public partial class AplicarSinapi : Form
    {
        public AplicarSinapi()
        {
            InitializeComponent();
        }

        private List<TreeNode> listaEncontrados = new List<TreeNode>();
        public delegate void AplicarSinapiEvent(int elementId, string sinapi);

        public event AplicarSinapiEvent ElementoSelecionado;

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSinapi.Text) && tvCompor.SelectedNode == null)
                MessageBox.Show("Informe o código de integração.");

            string textoParaAplicar = string.Empty;
            if (tvCompor.SelectedNode != null)
                textoParaAplicar = tvCompor.SelectedNode.Text.Substring(0, tvCompor.SelectedNode.Text.IndexOf("-") - 1);

            if (!string.IsNullOrEmpty(txtSinapi.Text))
                textoParaAplicar = txtSinapi.Text;

            if (treeElementos.SelectedNode.Nodes.Count == 0)
            {
                ElementoSelecionado.Invoke(Int32.Parse((string)treeElementos.SelectedNode.Tag), textoParaAplicar);
            }
            else
            {
                foreach (TreeNode node in treeElementos.SelectedNode.Nodes)
                {
                    ElementoSelecionado.Invoke(Int32.Parse((string)node.Tag), textoParaAplicar);
                }
            }

            MessageBox.Show("Código associado com sucesso.");

        }

        public TreeNodeCollection Tree { get { return treeElementos.Nodes; } }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TreeNode treePrincipal = new System.Windows.Forms.TreeNode("SINAPI");

            // carregar o txt referente ao item selecionado
            // item 0 - SINAP
            #region SINAPI

            var arquivoSinapi = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\Autodesk\\REVIT\\Addins\\2018\\comp_sinapi.txt";

            string[] lines = System.IO.File.ReadAllLines(arquivoSinapi, Encoding.GetEncoding("iso-8859-1"));

            // Display the file contents to the console. Variable text is a string.
            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                string texto = line.Substring(0, line.IndexOf("-") - 1).TrimEnd() + " - " + line.Substring(line.IndexOf("-") + 1).Trim();//line.Substring(147, line.Length - 147).TrimEnd() + " - " + line.Substring(23, 61).Trim();
                System.Windows.Forms.TreeNode tnItem = new System.Windows.Forms.TreeNode(texto);
                tnItem.Tag = line.Substring(0, line.IndexOf("-") - 1).TrimEnd();
                treePrincipal.Nodes.Add(tnItem);
                //Console.WriteLine("\t" + line);
            }

            tvCompor.Nodes.Add(treePrincipal);
            tvCompor.Sort();
            tvCompor.ExpandAll();
            #endregion
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private TreeNode FindNodeWithContent(TreeNode node, string searchedContent)
        {
            if (!listaEncontrados.Contains(node) && node.Text.ToUpper().Contains(searchedContent.ToUpper()))
                return node;

            foreach (TreeNode childNode in node.Nodes)
            {
                TreeNode foundNode = FindNodeWithContent(childNode, searchedContent);
                if (foundNode != null)
                    return foundNode;
            }

            return null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tvCompor.Nodes.Count > 0)
            {
                tvCompor.SelectedNode = FindNodeWithContent(tvCompor.Nodes[0], txtProcurarItemCompor.Text);
                listaEncontrados.Add(tvCompor.SelectedNode);
            }
            else
                MessageBox.Show("Informe a base de dados.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeElementos.Nodes.Count > 0)
                treeElementos.SelectedNode = FindNodeWithContent(treeElementos.Nodes[0], txtProcurarElementoRevit.Text);
            else
                MessageBox.Show("Não há elementos para a pesquisa.");
        }

        private void txtProcurarItemCompor_TextChanged(object sender, EventArgs e)
        {
            listaEncontrados.Clear();
        }
    }
}
