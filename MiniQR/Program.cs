using Adw;

var application = Application.New("ir.karimi.miniqr", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    MainWindow window = new MainWindow();
    window.Application = application;
    window.Title = "MiniQR";
    window.SetDefaultSize(360, 600);
    window.SetResizable(false);
    window.Show();
};
return application.RunWithSynchronizationContext();