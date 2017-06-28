namespace Catan.Client.PresentationLayer
{
    public class StrasseEventArgs
    {
        public StrasseTexture ClickedStrasse { private set; get; }

        public StrasseEventArgs(StrasseTexture strasse)
        {
            this.ClickedStrasse = strasse;
        }
    }
}