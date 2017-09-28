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
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Compor90Revit
{
    class Aplicativo : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            a.CreateRibbonTab("Compor 90");
            RibbonPanel painel = a.CreateRibbonPanel("Compor 90", "Integra��o Compor 90");

            PushButtonData aplicarParametro = new PushButtonData("APLICAR_PARAMETRO", "Aplicar",
                System.Reflection.Assembly.GetExecutingAssembly().Location, typeof(CmdAplicarParametros).FullName);
            // Set the large image shown on button            
            //aplicarParametro.LargeImage = new BitmapImage(new Uri(@"D:\Sistemas\Compor\work\Sinapi-Revit\SinapiRevit\Resources\aplicar.png"));
            aplicarParametro.LargeImage = new BitmapImage(new Uri(@"pack://application:,,,/Compor90Revit;component/Resources/aplicar.png"));
            painel.AddItem(aplicarParametro);

            PushButtonData atribuirParametro = new PushButtonData("ATRIBUIR_PARAMETRO", "Integra��o",
                System.Reflection.Assembly.GetExecutingAssembly().Location, typeof(CmdAtribuirParametros).FullName);
            atribuirParametro.LargeImage = new BitmapImage(new Uri(@"pack://application:,,,/Compor90Revit;component/Resources/lista3.png"));
            painel.AddItem(atribuirParametro);

            PushButtonData exportarDados = new PushButtonData("EXPORTAR_DADOS", "Expotar",
                System.Reflection.Assembly.GetExecutingAssembly().Location, typeof(CmdExtrairDados).FullName);
            exportarDados.LargeImage = new BitmapImage(new Uri(@"pack://application:,,,/Compor90Revit;component/Resources/download.png"));
            painel.AddItem(exportarDados);

            //PushButtonData identificarElemento = new PushButtonData("IDENTIFICA_ELEMENTO", "Identificar \nComponente",
            //System.Reflection.Assembly.GetExecutingAssembly().Location, typeof(IdentificarComponente).FullName);
            //painel.AddItem(identificarElemento);

            //PushButtonData selecionarTodosElementos = new PushButtonData("SELECIONAR_ELEMENTOS", "Listar \nElementos",
            //    System.Reflection.Assembly.GetExecutingAssembly().Location, typeof(ListarTodosElementos).FullName);
            //painel.AddItem(selecionarTodosElementos);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
