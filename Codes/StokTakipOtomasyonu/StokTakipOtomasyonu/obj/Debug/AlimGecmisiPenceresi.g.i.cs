﻿#pragma checksum "..\..\AlimGecmisiPenceresi.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "06551A3B8B19C04450AFEAD97E599C196097B31DDF5636609A4EA44577584948"
//------------------------------------------------------------------------------
// <auto-generated>
//     Bu kod araç tarafından oluşturuldu.
//     Çalışma Zamanı Sürümü:4.0.30319.42000
//
//     Bu dosyada yapılacak değişiklikler yanlış davranışa neden olabilir ve
//     kod yeniden oluşturulursa kaybolur.
// </auto-generated>
//------------------------------------------------------------------------------

using StokTakipOtomasyonu;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace StokTakipOtomasyonu {
    
    
    /// <summary>
    /// AlimGecmisiPenceresi
    /// </summary>
    public partial class AlimGecmisiPenceresi : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 100 "..\..\AlimGecmisiPenceresi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView alimListesi;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\AlimGecmisiPenceresi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDonemIci;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\AlimGecmisiPenceresi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOncekiDonem;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\AlimGecmisiPenceresi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTumu;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StokTakipOtomasyonu;component/alimgecmisipenceresi.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AlimGecmisiPenceresi.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.alimListesi = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.btnDonemIci = ((System.Windows.Controls.Button)(target));
            
            #line 168 "..\..\AlimGecmisiPenceresi.xaml"
            this.btnDonemIci.Click += new System.Windows.RoutedEventHandler(this.btnDonemIci_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnOncekiDonem = ((System.Windows.Controls.Button)(target));
            
            #line 169 "..\..\AlimGecmisiPenceresi.xaml"
            this.btnOncekiDonem.Click += new System.Windows.RoutedEventHandler(this.btnOncekiDonem_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnTumu = ((System.Windows.Controls.Button)(target));
            
            #line 170 "..\..\AlimGecmisiPenceresi.xaml"
            this.btnTumu.Click += new System.Windows.RoutedEventHandler(this.btnTumu_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
