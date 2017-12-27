using System;
using System.Windows;
using Autofac;
using UnitConverter.Model.Accessor;
using UnitConverter.View;

/// <summary>
/// Small and simple unit converter application.
/// New units and evaluation expressions can be added in units.db3 (sqlite database file).
/// Can be managed by SQLite/SQL Server Compact Toolbox (Visual Studio Extension).
/// Used:
/// - Sqlite database for unit dictionary and conversion expressions - I realy want to use it.
/// - EntityFramework6 as a db accessor - very havy and slow solution for that simple project. 
/// - Autofac as DI container - its realy not needed ;)
/// - Some repository pattern.
/// 
/// Project for training purposes and it's a 000 of programming chalange ;)
/// Adam Kostecki.
/// 2017 POLAND
/// </summary>

namespace UnitConverter
{
    public static class Program
    {
        static IContainer container;

        [STAThread]
        public static void Main()
        {
            InitializeDIContainer();

            var app = new Application
            {
                MainWindow = new MainWindow
                {
                    DataContext = container.Resolve<MainWindowViewModel>()
                }
            };
            app.Run(app.MainWindow);
        }

        private static void InitializeDIContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<UnitDbContext>().AsSelf().SingleInstance();

            builder.RegisterType<UnitConverter.Model.Repositories.UnitRepository>()
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<UnitConverter.Conversion.ConversionEngine>()
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();

            container = builder.Build();
        }
    }
}
