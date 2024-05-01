using Adw;
using GdkPixbuf;
using QRCoder;

public class MainWindow : Gtk.Window
{
    Gtk.Box box_main;
    Gtk.Box box_1;

    Gtk.Entry text;
    Gtk.Button btn_create;
    Gtk.Button btn_about;
    Gtk.Picture pic_qr;
    protected override void Initialize()
    {
#if DEBUG
        AddCssClass("devel");
#endif

        box_main = Gtk.Box.New(Gtk.Orientation.Vertical, 0);
        box_1 = Gtk.Box.New(Gtk.Orientation.Vertical, 1);
        text = Gtk.Entry.New();
        btn_create = Gtk.Button.New();
        btn_about = Gtk.Button.New();
        pic_qr = new Gtk.Picture();


        this.SetChild(box_main);
        text.SetParent(box_1);
        text.MarginStart = 50;
        text.MarginEnd = 50;
        box_1.MarginTop = 50;
        btn_create.SetParent(box_1);
        btn_about.SetParent(box_1);
        box_1.SetParent(box_main);
        btn_about.MarginStart = 100;
        btn_about.MarginEnd = 100;
        btn_about.MarginTop = 50;
        btn_about.AddCssClass("suggested-action");
        btn_about.Label = "About This App";
        btn_create.MarginStart = 100;
        btn_create.MarginEnd = 100;
        btn_create.MarginTop = 50;
        btn_create.AddCssClass("suggested-action");
        btn_create.Label = "Create";
        btn_create.OnClicked += (sender, args) =>
        {
            if (!string.IsNullOrWhiteSpace(text.GetText()))
            {
                btn_create.SetSensitive(false);
                btn_create.SetLabel("Creating");
                var a = text.GetText();
                var url = new PayloadGenerator.Url(text.GetText());
                var genrator = new QRCodeGenerator();
                var qr = genrator.CreateQrCode(url);
                pic_qr.SetPixbuf(PixbufLoader.FromBytes(new PngByteQRCode(qr).GetGraphic(512)));
                btn_create.SetLabel("Create");
                btn_create.SetSensitive(true);
            }
            else
            {
                var dialog = Adw.MessageDialog.New(this, "", "");
                var box_dialog = Gtk.Box.New(Gtk.Orientation.Vertical, 0);
                var btn_dialog = Gtk.Button.New();
                btn_dialog.Label = "Input is Empty. Press on me to try again.";
                dialog.SetChild(btn_dialog);
                btn_dialog.OnClicked += (sender, args) => { dialog.Close(); };

                dialog.Show();

            }
        };
        btn_about.OnClicked += (sender, args) => {
            var about_window = Adw.AboutWindow.New();
            about_window.ApplicationName = "MiniQR";
            about_window.DeveloperName = "Mohammad Mahdi Karimi";
            about_window.IssueUrl = "https://github.com/Karimiprogramer/MiniQR/issues";
            about_window.SupportUrl = "https://github.com/Karimiprogramer/MiniQR";
            about_window.SetParent(this);
            about_window.Show();

        };
        pic_qr.MarginTop = 50;
        pic_qr.SetParent(box_main);
        base.Initialize();

    }
}