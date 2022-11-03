using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.IO;

namespace JackboxModManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Config CurrentConfig { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            CurrentConfig = new()
            {
                ModFolder = @"C:\Users\dninemfive\Documents\workspaces\mods\Jackbox\",
                SteamFolder = @"C:\Program Files (x86)\Steam\steamapps\common\"
            };
            foreach(PackInfo pi in CurrentConfig.InstalledPacks)
            {
                TreeViewItem tvi = new()
                {
                    Header = pi
                };
                foreach(string subpath in Directory.GetDirectories(pi.GamesPath))
                {
                    tvi.Items.Add(subpath.Replace(pi.GamesPath, ""));
                }
                FolderStructure.Items.Add(tvi);
            }
        }
    }
}
